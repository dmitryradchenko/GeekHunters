using System.Collections.Generic;
using System.Threading.Tasks;
using GeekHunters.Models.DTO;

namespace GeekHunters.Models.Interfaces
{
    public interface ICandidateService
    {
        /// <summary>
        /// Getting candidate's list 
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<CandidateDto>> GetCandidates();

        Task<bool> AddCandidate(AddCandidateDto candidate);

        Task<IEnumerable<CandidateDto>> GetCandidateBySkill(string skillName);

        /// <summary>
        /// Getting all available skills
        /// </summary>
        /// <returns></returns>
        Task<ICollection<SkillDto>> GetAvailableSkills();
    }
}
