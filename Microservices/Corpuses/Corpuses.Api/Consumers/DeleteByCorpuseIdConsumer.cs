using MassTransit;

namespace Corpuses.Api.Consumers
{
    public class DeleteByCorpuseIdConsumer : IConsumer<DeleteByCorpuseIdDto>
    {
        public async Task Consume( ConsumeContext<DeleteByCorpuseIdDto> context )
        {
            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage( HttpMethod.Delete, $"http://localhost:5000/api/audiences/corpuse/{context.Message.Id}" );

            HttpResponseMessage response = await client.SendAsync( request );
        }
    }
}
