using System.ComponentModel.DataAnnotations;

namespace GeekHunters.DAL.Models
{
    public class CandidateSkill
    {
        [Key]
        public long CandidateId { get; set; }
        public Candidate Candidate { get; set; }

        [Key]
        public long SkillId { get; set; }
        public Skill Skill { get; set; }

    }
}
