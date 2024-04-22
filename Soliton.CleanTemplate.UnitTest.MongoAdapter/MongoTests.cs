using Soliton.Shared.TestExtensions;
using MongoDB.Driver;
using Soliton.CleanTemplate.Adapters.Mongo;
using Soliton.CleanTemplate.Core;

namespace Soliton.CleanTemplate.UnitTest.MongoAdapter
{
    public class MongoTests
    {
        [Fact]
        public async void NoUsersInDB_GetAllUsers_ReturnsEmptyList()
        {
            RepositoryMocker<SolitonUser> mocker = new("users", []);
            mocker.Collection.Setup(collection => collection.FindAsync<SolitonUser>(FilterDefinition<SolitonUser>.Empty, null, default))
                .Returns(Task.FromResult(mocker.Cursor.Object));
            SolitonRepository userRepository = new(mocker.Database.Object);

            List<SolitonUser>? getResults = await userRepository.GetAllUsers();

            Assert.NotNull(getResults);
            Assert.Empty(getResults);
        }

        [Fact]
        public async void TwoUsersInDB_GetAllUsers_ReturnsTwoElements()
        {
            List<SolitonUser> users =
            [
                new SolitonUser(),
                new SolitonUser()
            ];
            RepositoryMocker<SolitonUser> mocker = new("users", users);
            mocker.Collection.Setup(collection => collection.FindAsync<SolitonUser>(FilterDefinition<SolitonUser>.Empty, null, default))
                .Returns(Task.FromResult(mocker.Cursor.Object));
            SolitonRepository userRepository = new(mocker.Database.Object);

            List<SolitonUser>? getResults = await userRepository.GetAllUsers();

            Assert.NotNull(getResults);
            Assert.Equal(users.Count, getResults.Count);
        }
    }
}