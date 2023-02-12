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
            await new X().DoX();
        }
    }
}

public class X
{
    public async Task DoX()
    {
        await new Y().DoT().ConfigureAwait(false);
    }
}

public class Y
{
    public async Task DoT(CancellationToken ct = default)
    {
        var clientOptions = new CosmosClientOptions
        {
            HttpClientFactory = () => new HttpClient(new HttpClientHandler { ServerCertificateCustomValidationCallback = (_, _, _, _) => true, }),
            ConnectionMode = ConnectionMode.Gateway,
        };

        var c = new CosmosClient("AccountEndpoint=https://localhost:8081/;AccountKey=C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==", clientOptions);
        await c.CreateDatabaseIfNotExistsAsync("testdbgalab", cancellationToken: ct);
    }
}
