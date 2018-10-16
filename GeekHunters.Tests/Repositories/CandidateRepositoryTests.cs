using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeekHunters.DAL.EF;
using GeekHunters.DAL.Models;
using GeekHunters.DAL.Repositories;
using GeekHunters.Models.DTO;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace GeekHunters.Tests.Repositories
{
    public class CandidateRepositoryTests
    {

        [Fact]
        public async Task GetCandidates_ShouldReturnAllCandidates()
        {
            var options = new DbContextOptionsBuilder<GeekHunterContext>()
                .UseInMemoryDatabase("GetCandidates_ShouldReturnAllCandidates")
                .Options;

            var context = new GeekHunterContext(options);

            await Seed(context);

            CandidateRepository candidateRepository = new CandidateRepository(context);

            var customers = (await candidateRepository.GetCandidates()).ToList();

            Assert.Equal(3, customers.Count);
            Assert.Equal(2, customers[0].Skills.Count);
        }

        [Fact]
        public async Task AddCandidate_ShouldAddOneCandidate()
        {
            var options = new DbContextOptionsBuilder<GeekHunterContext>()
                .UseInMemoryDatabase("AddCandidate_ShouldAddOneCandidate")
                .Options;

            var context = new GeekHunterContext(options);

            await Seed(context);

            CandidateRepository candidateRepository = new CandidateRepository(context);

            await candidateRepository.AddCandidate(new AddCandidateDto
            {
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                Skills = new List<string> { "SQL", "Entity Framework" }
            });

            var candidates = (await candidateRepository.GetCandidates()).ToList();

            Assert.Equal(4, candidates.Count);
            var addedCustomer = candidates[3];
            Assert.Equal("TestFirstName", addedCustomer.FirstName);
            Assert.Equal("TestLastName", addedCustomer.LastName);
            Assert.Equal(2, addedCustomer.Skills.Count);
        }

        private async Task<bool> Seed(GeekHunterContext context)
        {
            DateTime now = DateTime.Now;
            var skills = new List<Skill>
            {
                new Skill
                {
                    Name = "SQL"
                },
                new Skill
                {
                    Name = "Entity Framework"
                },
                new Skill
                {
                    Name = "ReactJS"
                }
            };
            context.Skills.AddRange(skills);

            var skillsForFirstCandidate = skills.Where(s => s.Name == "SQL" || s.Name == "ReactJS").ToList();
            var candidates = new List<Candidate>
            {
                new Candidate
                {
                    FirstName = "Donald",
                    LastName = "Trump"
                    //,CandidateSkills = skillsForFirstCandidate
                    ,Skills = skillsForFirstCandidate
                },
                new Candidate
                {
                    FirstName = "Barak",
                    LastName = "Obama",
                    Skills = new List<Skill>()
                },
                new Candidate
                {
                    FirstName = "Bill",
                    LastName = "Clinton",
                    Skills = new List<Skill>()
                }
            };

            List<CandidateSkill> candidateSkills = skillsForFirstCandidate.Select(s => new CandidateSkill
            {
                Candidate = candidates[0],
                CandidateId = candidates[0].Id,
                Skill = s,
                SkillId = s.Id
            }).ToList();
            candidates[0].CandidateSkills = candidateSkills;

            context.Candidates.AddRange(candidates);
            
            await context.SaveChangesAsync();
            return true;
        }
    }
}
