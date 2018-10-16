using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeekHunters.DAL.EF;
using GeekHunters.DAL.Models;
using GeekHunters.Models.DTO;
using GeekHunters.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GeekHunters.DAL.Repositories
{
    public class CandidateRepository : ICandidateRepository
    {
        private readonly GeekHunterContext _db;

        public CandidateRepository(GeekHunterContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Getting candidate's list 
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<CandidateDto>> GetCandidates()
        {
            return await _db.Candidates.Include(c => c.CandidateSkills)
                .Select(c => new CandidateDto
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Skills = c.CandidateSkills.Select(s => new SkillDto
                {
                    Id = s.SkillId,
                    Name = s.Skill.Name
                }).ToList()
            }).ToListAsync();
        }

        /// <summary>
        /// Adding new candidate
        /// </summary>
        /// <param name="candidate"></param>
        /// <returns></returns>
        public async Task<bool> AddCandidate(AddCandidateDto candidate)
        {
            List<Skill> skills = _db.Skills.Where(s => candidate.Skills.Contains(s.Name)).ToList();
            Candidate newCandidate = new Candidate
            {
                FirstName = candidate.FirstName,
                LastName = candidate.LastName,
                Skills = skills
            };

            List<CandidateSkill> candidateSkills = skills.Select(s => new CandidateSkill
            {
                Candidate = newCandidate,
                CandidateId = newCandidate.Id,
                Skill = s,
                SkillId = s.Id
            }).ToList();
            newCandidate.CandidateSkills = candidateSkills;

            _db.Candidates.Add(newCandidate);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<CandidateDto>> GetCandidateBySkill(string skillName)
        {
            return await _db.Candidates.Include(c => c.Skills)
                .Where(c => c.Skills.Any(s => s.Name == skillName))
                .Select(c => new CandidateDto
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Skills = c.Skills.Select(s => new SkillDto
                    {
                        Id = s.Id,
                        Name = s.Name
                    }).ToList()
                }).ToListAsync();
        }

        /// <summary>
        /// Getting all available skills
        /// </summary>
        /// <returns></returns>
        public async Task<ICollection<SkillDto>> GetAvailableSkills()
        {
            return await _db.Skills.Select(s => new SkillDto
            {
                Id = s.Id,
                Name = s.Name
            }).ToListAsync();
        }
    }
}
