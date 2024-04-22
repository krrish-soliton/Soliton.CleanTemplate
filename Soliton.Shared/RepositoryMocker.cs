using Moq;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Soliton.Shared.TestExtensions
{
    public class RepositoryMocker<T>
    {
        public Mock<IMongoDatabase> Database { get; } = new Mock<IMongoDatabase>();

        public Mock<IMongoCollection<T>> Collection { get; } = new Mock<IMongoCollection<T>>();

        public Mock<IAsyncCursor<T>> Cursor { get; } = new Mock<IAsyncCursor<T>>();

        public RepositoryMocker(string collectionName, List<T> data)
        {
            Database.Setup(database => database.GetCollection<T>(collectionName, default)).Returns(Collection.Object);
            Mock<IMongoIndexManager<T>> mockIndexer = new Mock<IMongoIndexManager<T>>();
            mockIndexer.Setup(m => m.CreateOne(It.IsAny<CreateIndexModel<T>>(), null, default)).Returns("");
            Collection.Setup(collection => collection.Indexes).Returns(mockIndexer.Object);

            Cursor.Setup(enumerable => enumerable.Current).Returns(data);
            Cursor.SetupSequence(enumerable => enumerable.MoveNext(It.IsAny<CancellationToken>())).Returns(true).Returns(false);
            Cursor.SetupSequence(enumerable => enumerable.MoveNextAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult(true)).Returns(Task.FromResult(false));
        }
    }
}