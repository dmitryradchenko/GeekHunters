using System.Threading.Tasks;
using GeekHunters.Models.DTO;
using GeekHunters.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GeekHunters.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private readonly ICandidateService _candidateService;

        public CandidateController(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        /// <summary>
        /// Getting candidate's list 
        /// </summary>
        /// <returns></returns>
        [HttpGet("list")]
        public async Task<IActionResult> GetCandidates()
        {
            var candidates = await _candidateService.GetCandidates();
            return Ok(candidates);
        }

        /// <summary>
        /// Adding new candidate
        /// </summary>
        /// <param name="candidate"></param>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<IActionResult> AddCandidate(AddCandidateDto candidate)
        {
            bool result = await _candidateService.AddCandidate(candidate);
            return Ok(result);
        }

        /// <summary>
        /// Getting all available skills
        /// </summary>
        /// <returns></returns>
        [HttpGet("availableSkills")]
        public async Task<IActionResult> GetAvailableSkills()
        {
            var result = await _candidateService.GetAvailableSkills();
            return Ok(result);
        }
    }
}