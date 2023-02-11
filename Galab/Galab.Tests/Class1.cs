using EventStore.Client;
using Xunit;

namespace Galab.Tests
{
    public class Class1
    {
        [Fact]
        public async Task test()
        {
            var c = new EventStoreClient(EventStoreClientSettings.Create("esdb://localhost:2113?tls=false"));
            await c.AppendToStreamAsync("str", StreamState.Any, new List<EventData> { new(Uuid.NewUuid(), "et", new ReadOnlyMemory<byte>()), });
        }
    }
}
