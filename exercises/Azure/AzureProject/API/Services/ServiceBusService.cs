﻿using API.Contexts;
using API.Repositories;
using Azure.Messaging.ServiceBus;
using System.Diagnostics;

namespace API.Services
{
    public class ServiceBusService
    {
        private readonly ServiceBusClient _client;
        //private readonly ServiceBusSender _sender;
        private readonly ServiceBusProcessor _processor;

        private readonly IServiceProvider _provider;

        public ServiceBusService(string connectionString, string queueName, IServiceProvider provider)
        {
            _client = new ServiceBusClient(connectionString);
            _processor = _client.CreateProcessor(queueName);

            _processor.ProcessMessageAsync += MessageHandler;
            _processor.ProcessErrorAsync += ErrorHandler;

            _provider = provider;
        }

        public async Task MessageHandler(ProcessMessageEventArgs args)
        {
            try
            {
                string message = args.Message.Body.ToString();

                using (var scope = _provider.CreateScope())
                {
                    var service = scope.ServiceProvider.GetRequiredService<EmailService>();

                    var result = await service.AddNewEmailAsync(message);

                    if (result)
                    {
                        await args.CompleteMessageAsync(args.Message);
                    }
                }
            }

            catch (Exception ex){ Debug.WriteLine(ex.Message); }
        }

        public Task ErrorHandler(ProcessErrorEventArgs args)
        {
            string message = args.Exception.ToString();
            Debug.WriteLine(message);

            return Task.CompletedTask;
        }

        public async Task StartSubscribingAsync() => await _processor.StartProcessingAsync();
        public async Task StopSubscribingAsync() => await _processor.StopProcessingAsync();
    }
}
