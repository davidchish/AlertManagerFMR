using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text.Json;
using System.Threading.Channels;
using InfoProperty;

namespace RabbitMQ
{
    public class RabbitMqFactory : IMessageBroker
    {
        static string currentDirectory = Directory.GetCurrentDirectory();
        private Dictionary<string, RabbitMQSetting> _jsonRabbitSettingDictionery = new Dictionary<string, RabbitMQSetting>();
        private RabbitMQSetting rabbitMQSettingListner;
        private RabbitMQSetting rabbitMQSettingSender;
        private string _RabbitConnfiguratiomPath = @"C:\david\source\repos\AlertManagerFMR\RabbitMQ\RabbitMqConfiguration.json";
        private Task<IChannel> channelSender;
        private Task<IChannel> channelReciver;

        public RabbitMqFactory()
        {
            string jsonRabbitSettingstring = File.ReadAllText(_RabbitConnfiguratiomPath);
            _jsonRabbitSettingDictionery = JsonSerializer.Deserialize<Dictionary<string, RabbitMQSetting>>(jsonRabbitSettingstring) ?? new Dictionary<string, RabbitMQSetting>();
            rabbitMQSettingSender = _jsonRabbitSettingDictionery["RabbitMQTarget"];
            rabbitMQSettingListner = _jsonRabbitSettingDictionery["RabbitMQListener"];

        }
        public async void Start()
        {
            var factory = new ConnectionFactory { HostName = rabbitMQSettingSender.HostName,/*UserName*/};
            var connection = await factory.CreateConnectionAsync();
            channelSender = connection.CreateChannelAsync();
            channelReciver = connection.CreateChannelAsync();

            await channelReciver.Result.QueueDeclareAsync(queue: "hello", durable: false, exclusive: false, autoDelete: false,
    arguments: null);

            await channelSender.Result.ExchangeDeclareAsync(rabbitMQSettingSender.Exchange, ExchangeType.Direct);
        }

        public async Task SendMessageAsync<T>(T message)
        {
            byte[] body = JsonSerializer.SerializeToUtf8Bytes(message);
            await channelSender.Result.BasicPublishAsync(exchange: string.Empty, routingKey: "ClientRule", body: body);
        }

        public void ReciveMessage(byte[] message)
        {
            var consumer = new AsyncEventingBasicConsumer(channelReciver.Result);
            consumer.ReceivedAsync += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                FlightInfo message = JsonSerializer.Deserialize<FlightInfo>(body);
                SendMessageAsync(message);
                return Task.CompletedTask;
            };
        }
    }
}
