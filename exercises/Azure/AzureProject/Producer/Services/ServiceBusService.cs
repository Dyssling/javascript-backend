using Azure.Messaging.ServiceBus;
using System.Diagnostics;

namespace Producer.Services
{
    public class ServiceBusService
    {
        private readonly ServiceBusClient _client;
        private readonly ServiceBusSender _sender;
        private readonly ServiceBusProcessor _processor;

        public ServiceBusService(string connectionString, string publishQueueName, string receiveQueueName)
        {
            _client = new ServiceBusClient(connectionString);
            _sender = _client.CreateSender(publishQueueName);
            _processor = _client.CreateProcessor(receiveQueueName);

            _processor.ProcessMessageAsync += MessageHandler;
            _processor.ProcessErrorAsync += ErrorHandler;

        }

        public async Task MessageHandler(ProcessMessageEventArgs args)
        {
            try
            {
                string response = args.Message.Body.ToString();

                Console.WriteLine(response);

                await args.CompleteMessageAsync(args.Message);
            }

            catch (Exception ex)
            { 
                Debug.WriteLine(ex.Message);
            }
        }

        public Task ErrorHandler(ProcessErrorEventArgs args)
        {
            string message = args.Exception.ToString();
            Console.WriteLine(message);

            return Task.CompletedTask;
        }

        public async Task PublishAsync(string message)
        {
            try
            {
                var payload = new ServiceBusMessage(message);

                await _sender.SendMessageAsync(payload);
            }

            catch { }
        }

        public async Task StartSubscribingAsync() => await _processor.StartProcessingAsync();
        public async Task StopSubscribingAsync() => await _processor.StopProcessingAsync();
    }
}
