using System.Collections.Generic;

namespace GeekHunters.Models.DTO
{
    public class AddCandidateDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public List<string> Skills { get; set; } = new List<string>();
    }
}
