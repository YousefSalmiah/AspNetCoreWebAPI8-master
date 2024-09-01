using AspNetCoreWebAPI8.Models;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using System.Text;

namespace AspNetCoreWebAPI8
{
    public class ExportData
    {

        private const int NumberOfRetries = 3;
        private const int DelayOnRetry = 1000;

        public static void ExportCsv(Candidate genericList)
        {

                for (int i=1; i <= NumberOfRetries; ++i) {
                try {
                    TextWriter sw;
                    var basePath = AppDomain.CurrentDomain.BaseDirectory;
                    var finalPath = Path.Combine(basePath, "candidates.csv");
                    var sb = new StringBuilder();
                    var header = "";
                    var info = typeof(Candidate).GetProperties();
                    if (!File.Exists(finalPath))
                    {
                        var file = File.Create(finalPath);
                        file.Close();
                        foreach (var prop in typeof(Candidate).GetProperties())
                        {
                            header += prop.Name + "; ";
                        }
                        header = header.Substring(0, header.Length - 2);
                        sb.AppendLine(header);
                        sw = new StreamWriter(finalPath, true);
                        sw.Write(sb.ToString());
                        sw.Close();
                    }

                    String candidateExist = GetAddress(genericList.Email.ToString());

                    if (!string.IsNullOrEmpty(candidateExist))
                    {
                        string[] lines = System.IO.File.ReadAllLines(finalPath);
                        string[] newlines = new string[lines.Length-1];
                        int Count = 0;

                        foreach (string _line in lines)
                        {
                            string[] columns = _line.Split(',');
                            foreach (string column in columns)
                            {
                                if (!column.Split(';')[3].Trim().Equals(genericList.Email))
                                {
                                    newlines[Count] = column;
                                    Count++;
                                }
                            }
                        }


                        using (StreamWriter writer = new StreamWriter(finalPath, false))
                        {
                            foreach (var item in newlines)
                                writer.WriteLine(item);
                        }
                    }


                        sb = new StringBuilder();
                        var line = "";
                        foreach (var prop in info)
                        {
                            line += prop.GetValue(genericList, null) + "; ";
                        }
                        line = line.Substring(0, line.Length - 2);

                        sb.AppendLine(line);
                        sw = new StreamWriter(finalPath, true);
                        sw.Write(sb.ToString());
                        sw.Close();
                        break; // When done we can break loop
                    }
                catch (IOException e) when(i <= NumberOfRetries)
                    {
                        // You may check error code to filter some exceptions, not every error
                        // can be recovered.
                        Thread.Sleep(DelayOnRetry);
                    }
                }
        }



         
        
        static String GetAddress(string searchName)
        {
            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            var finalPath = Path.Combine(basePath, "candidates.csv");
            for (int i = 1; i <= NumberOfRetries; ++i)
            {
                try
                {
                    if (System.IO.File.Exists(finalPath))
                    {
                        var strLines = System.IO.File.ReadLines(finalPath);
                        foreach (var line in strLines)
                        {
                            if (!string.IsNullOrEmpty(line))
                            {
                                if (line.Split(';')[3].Trim().Equals(searchName.Trim()))
                                    return line.Split(';')[3];
                            }
                        }
                    }
                    break; // When done we can break loop
                }
                catch (IOException e) when (i <= NumberOfRetries)
                {
                    // You may check error code to filter some exceptions, not every error
                    // can be recovered.
                    Thread.Sleep(DelayOnRetry);
                }
            }

            return "";
        }
    }

}
