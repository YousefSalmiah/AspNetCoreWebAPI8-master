using AspNetCoreWebAPI8.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Reflection.Metadata;
using System.Text.Json;


namespace AspNetCoreWebAPI8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidatesController : ControllerBase
    {
        //private readonly CandidateContext _candidateContext;
        //public CandidatesController(CandidateContext candidateContext)
        //{
        //    _candidateContext = candidateContext;
        //}




        // Post : api/Candidates
        [HttpPost]
        public async Task<ActionResult<Candidate>> PostCandidate(Candidate _Candidate)
        {

                if (string.IsNullOrEmpty(_Candidate.Email))
                {
                    return BadRequest();
                }
                //_candidateContext.Entry(_Candidate).State = EntityState.Modified;
                try
                {
                        //await _candidateContext.SaveChangesAsync();
                        ExportData.ExportCsv(_Candidate);

                }
                catch (DbUpdateConcurrencyException)
                {
                    //if (!CandidateExists(Email)) { return NotFound(); }
                    //else { throw; }
                }
                return NoContent();
        }








    }
}
