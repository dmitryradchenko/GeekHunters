using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GeekHunters.DAL.EF;
using GeekHunters.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GeekHunters.Tests.Controllers
{
    public class ControllerTestsBase
    {
        private readonly GeekHunterContext _context;

        public ControllerTestsBase()
        {
            var options = new DbContextOptionsBuilder<GeekHunterContext>()
                .UseInMemoryDatabase("ShouldReturnAllCustomers")
                .Options;


            _context = new GeekHunterContext(options);
            _context.Database.EnsureCreated();

        }

        protected void Seed(GeekHunterContext context)
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

            var candidates = new List<Candidate>
            {
                new Candidate
                {
                    FirstName = "Donald",
                    LastName = "Trump",
                    Skills = new List<Skill>
                    {
                        new Skill
                        {
                            Name = "SQL"
                        },
                        new Skill
                        {
                            Name = "ReactJS"
                        }
                    }
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
            context.Candidates.AddRange(candidates);
            context.SaveChanges();
        }

        protected static DbContextOptions<T> CreateNewContextOptions<T>() where T : DbContext
        {
            // Create a fresh service provider, and therefore a fresh 
            // InMemory database instance.
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            // Create a new options instance telling the context to use an
            // InMemory database and the new service provider.
            var builder = new DbContextOptionsBuilder<T>();
            builder.UseInMemoryDatabase()
                .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }
    }
}
