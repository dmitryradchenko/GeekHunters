using System.Collections.Generic;

namespace GeekHunters.Models.DTO
{
    public class CandidateDto
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual List<SkillDto> Skills { get; set; } = new List<SkillDto>();
    }
}
