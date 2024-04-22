using Moq;
using Soliton.CleanTemplate.Core;
using Soliton.Shared;

namespace Soliton.CleanTemplate.UnitTest
{
    public class ServiceTests
    {
        [Fact]
        public async void NoUsersInDB_GetUsers_ReturnsEmptyList()
        {
            Mock<ISolitonRepository> repository = new();
            SolitonService service = new(repository.Object);
            repository.Setup(repo => repo.GetAllUsers())
            .Returns(Task.FromResult(new MessageHolder<List<SolitonUser>>(new List<SolitonUser>())));

            var response = await service.GetAllUsers();

            Assert.NotNull(response.Data);
            Assert.Empty(response.Data);
        }

        [Fact]
        public async void TwoUsersInDB_GetUsers_ReturnsTwoElements()
        {
            Mock<ISolitonRepository> repository = new();
            SolitonService service = new(repository.Object);
            var users = new List<SolitonUser>()
            {
                new ()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Gowtham",
                    EmailId = "gowtham@solitontech.com",
                    EmployeeId = 1689
                },
                new ()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Krishnan",
                    EmailId = "Krishnan@solitontech.com",
                    EmployeeId = 1690
                }
            };
            repository.Setup(repo => repo.GetAllUsers()).Returns(Task.FromResult(new MessageHolder<List<SolitonUser>>(users)));

            var response = await service.GetAllUsers();
            string? outputId = response.Data?[0].Id;

            Assert.NotNull(response.Data);
            Assert.Equal(response.Data.Count, users.Count);
            Assert.NotNull(outputId);
        }
    }
}