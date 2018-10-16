using System.Collections.Generic;
using System.Threading.Tasks;
using GeekHunters.Models.DTO;
using GeekHunters.Models.Interfaces;

namespace GeekHunters.BLL.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly ICandidateRepository _candidateRepository;

        public CandidateService(ICandidateRepository candidateRepository)
        {
            _candidateRepository = candidateRepository;
        }

        /// <summary>
        /// Getting candidate's list 
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<CandidateDto>> GetCandidates()
        {
            return await _candidateRepository.GetCandidates();
        }

        /// <summary>
        /// Adding new candidate
        /// </summary>
        /// <param name="candidate"></param>
        /// <returns></returns>
        public async Task<bool> AddCandidate(AddCandidateDto candidate)
        {
            return await _candidateRepository.AddCandidate(candidate);
        }

        public async Task<IEnumerable<CandidateDto>> GetCandidateBySkill(string skillName)
        {
            return await _candidateRepository.GetCandidateBySkill(skillName);
        }

        /// <summary>
        /// Getting all available skills
        /// </summary>
        /// <returns></returns>
        public async Task<ICollection<SkillDto>> GetAvailableSkills()
        {
            return await _candidateRepository.GetAvailableSkills();
        }
    }
}
