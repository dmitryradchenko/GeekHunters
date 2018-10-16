using System.Collections.Generic;
using System.Threading.Tasks;
using GeekHunters.Models.DTO;

namespace GeekHunters.Models.Interfaces
{
    public interface ICandidateRepository
    {
        /// <summary>
        /// Getting candidate's list 
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<CandidateDto>> GetCandidates();

        /// <summary>
        /// Adding new candidate
        /// </summary>
        /// <param name="candidate"></param>
        /// <returns></returns>
        Task<bool> AddCandidate(AddCandidateDto candidate);

        Task<IEnumerable<CandidateDto>> GetCandidateBySkill(string skillName);

        Task<ICollection<SkillDto>> GetAvailableSkills();
    }
}
