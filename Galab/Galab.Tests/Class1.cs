using EventStore.Client;
using Microsoft.Azure.Cosmos;
using Xunit;

namespace Galab.Tests
{
    public class Class1
    {
        [Fact]
        [Trait("Category", "EventStore")]
        public async Task test_event_store()
        {
            var c = new EventStoreClient(EventStoreClientSettings.Create("esdb://localhost:2113?tls=false"));
            await c.AppendToStreamAsync("str", StreamState.Any, new List<EventData> { new(Uuid.NewUuid(), "et", new ReadOnlyMemory<byte>()), });
        }

        [Fact]
        [Trait("Category", "CosmosDb")]
        public async Task test_cosmos_db()
        {
            var clientOptions = new CosmosClientOptions
            {
                HttpClientFactory = () => new HttpClient(new HttpClientHandler { ServerCertificateCustomValidationCallback = (_, _, _, _) => true, }),
                ConnectionMode = ConnectionMode.Gateway,
            };

            var c = new CosmosClient("AccountEndpoint=https://cosmosdb:2223/;AccountKey=C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==", clientOptions);
            await c.CreateDatabaseIfNotExistsAsync("testdbgalab");
        }

        [Theory]
        [InlineData(2)]
        [InlineData(4)]
        [InlineData(6)]
        [InlineData(15)]
        [Trait("Category", "CosmosDb")]
        public async Task test_cosmos_db2(int t)
        {
            await Task.Delay(TimeSpan.FromSeconds(t));

            var clientOptions = new CosmosClientOptions
            {
                HttpClientFactory = () => new HttpClient(new HttpClientHandler { ServerCertificateCustomValidationCallback = (_, _, _, _) => true, }),
                ConnectionMode = ConnectionMode.Gateway,
            };

            var c = new CosmosClient("AccountEndpoint=https://cosmosdb:2223/;AccountKey=C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==", clientOptions);
            await c.CreateDatabaseIfNotExistsAsync("testdbgalab");
        }
    }
}
