using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AspNetCoreWebAPI8.Models
{

    [Index(nameof(Email), IsUnique = true)]
    public class Candidate
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string?  PhoneNumber { get; set; }
        public required string Email { get; set; }
        public string? TimeInterval { get; set; }
        public string? LinkedInURL { get; set; }
        public string? GitHubURL { get; set; }
        public required string Comment { get; set; }

    }

    //class Time
    //{
    //    public int? Hours { get; set; }
    //    public int? Minutes { get; set; }
    //    public int Seconds { get; set; }

    //    public override string ToString()
    //    {
    //        return String.Format("{0:00}:{1:00}:{2:00}",this.Hours, this.Minutes, this.Seconds);
    //    }
    //}


}
