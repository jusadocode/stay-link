namespace stay_link.Server.Services
{
    public class RoomUsageBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public RoomUsageBackgroundService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var roomService = scope.ServiceProvider.GetRequiredService<RoomService>();

                    // Check and update room usage periodically
                    await roomService.PeriodicRoomUsageUpdate();
                }

                // Wait for 24 hours before the next update
                await Task.Delay(TimeSpan.FromSeconds(24), stoppingToken);
            }
        }
    }
}
