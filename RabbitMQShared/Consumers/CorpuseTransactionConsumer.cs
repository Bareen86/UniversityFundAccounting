using MassTransit;
using RabbitMQShared.Models;

namespace RabbitMQShared.Consumers
{
    public class CorpuseTransactionConsumer : IConsumer<CorpuseTransaction>
    {
        public async Task Consume( ConsumeContext<CorpuseTransaction> context )
        {

            await Task.CompletedTask;
        }
    }
}
