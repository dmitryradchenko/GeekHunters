using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekHunters.DAL.Models
{
    public class Skill
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CandidateSkill> CandidateSkills { get; set; } = new List<CandidateSkill>();
        [NotMapped]
        public ICollection<Candidate> Candidates { get; set; } = new List<Candidate>();
    }
}
