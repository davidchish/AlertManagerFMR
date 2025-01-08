using RabbitMQ.Client;
using System.Text.Json;

namespace RabbitMQ
{
    public class RabbitMqFactory
    {
        private Dictionary<string, RabbitMQSetting> _jsonRabbitSettingDictionery = new Dictionary<string, RabbitMQSetting>();
        private RabbitMQSetting rabbitMQSettingListner;
        private  RabbitMQSetting rabbitMQSettingSender;
        private string _RabbitConnfiguratiomPath ="s//";

        public RabbitMqFactory()
        {
            string jsonRabbitSettingstring = File.ReadAllText(_RabbitConnfiguratiomPath);
            _jsonRabbitSettingDictionery = JsonSerializer.Deserialize<Dictionary<string,RabbitMQSetting>>(jsonRabbitSettingstring)?? new Dictionary<string, RabbitMQSetting>();
            rabbitMQSettingSender = _jsonRabbitSettingDictionery["RabbitMQTarget"];
            rabbitMQSettingListner = _jsonRabbitSettingDictionery["RabbitMQListner"];

        }
        public async void Start()
        {
            var factory = new ConnectionFactory { HostName = rabbitMQSettingSender.HostName,/*UserName*/};
            var connection = await factory.CreateConnectionAsync();
        }

        public void SendMessage<T>(T message)
        {
           
        }
    }
}
