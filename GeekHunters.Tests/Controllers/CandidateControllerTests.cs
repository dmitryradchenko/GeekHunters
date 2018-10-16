using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeekHunters.BLL.Services;
using GeekHunters.Controllers;
using GeekHunters.DAL.EF;
using GeekHunters.DAL.Repositories;
using GeekHunters.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace GeekHunters.Tests.Controllers
{
    public class CandidateControllerTests : ControllerTestsBase
    {
        [Fact]
        public async Task GetCandidates_ReturnsAllCandidates()
        {
            using (var context = new GeekHunterContext(CreateNewContextOptions<GeekHunterContext>()))
            {
                var controller = CandidateController(context);

                var result = await controller.GetCandidates();

                var objectResult = Assert.IsType<OkObjectResult>(result);
                Assert.IsAssignableFrom<IEnumerable<CandidateDto>>(objectResult.Value);
                Assert.Equal(3, ((IEnumerable<CandidateDto>)objectResult.Value).Count());
            }
        }

        [Fact]
        public async Task AddCustomer_ShouldReturnOneMoreCustomer()
        {
            using (var context = new GeekHunterContext(CreateNewContextOptions<GeekHunterContext>()))
            {
                var controller = CandidateController(context);

                var result = await controller.AddCandidate(new AddCandidateDto
                {
                    FirstName = "TestAddedCandidateFirstName",
                    LastName = "TestAddedCandidateLastName",
                    Skills = new List<string> { "Entity Framework", "ReactJS" }
                });

                Assert.IsType<OkObjectResult>(result);
                Assert.Equal(200, ((OkObjectResult) result).StatusCode);

                var customers = await controller.GetCandidates();

                var objectResult = Assert.IsType<OkObjectResult>(customers);
                Assert.IsAssignableFrom<IEnumerable<CandidateDto>>(objectResult.Value);
                Assert.Equal(4, ((IEnumerable<CandidateDto>)objectResult.Value).Count());
                Assert.Equal(2, ((IEnumerable<CandidateDto>)objectResult.Value).ToList()[3].Skills.Count);

                var result2 = await controller.AddCandidate(new AddCandidateDto
                {
                    FirstName = "TestAddedCandidateFirstName2",
                    LastName = "TestAddedCandidateLastName2",
                    Skills = new List<string> { }
                });

                Assert.IsType<OkObjectResult>(result2);
                Assert.Equal(200, ((OkObjectResult)result2).StatusCode);

                customers = await controller.GetCandidates();

                objectResult = Assert.IsType<OkObjectResult>(customers);
                Assert.IsAssignableFrom<IEnumerable<CandidateDto>>(objectResult.Value);
                Assert.Equal(5, ((IEnumerable<CandidateDto>)objectResult.Value).Count());
                Assert.Equal(0, ((IEnumerable<CandidateDto>)objectResult.Value).ToList()[4].Skills.Count);
            }
        }

        private CandidateController CandidateController(GeekHunterContext context)
        {
            Seed(context);
            var candidateRepository = new CandidateRepository(context);
            var candidateService = new CandidateService(candidateRepository);
            var controller = new CandidateController(candidateService);
            return controller;
        }
    }
}
