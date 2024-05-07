
namespace API.Services
{
    public class ServiceBusBackground : IHostedService
    {
        private readonly ServiceBusService _service;

        public ServiceBusBackground(IServiceProvider provider)
        {
            _service = new ServiceBusService("", "email", provider);
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _service.StartSubscribingAsync();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _service.StopSubscribingAsync();
        }
    }
}
