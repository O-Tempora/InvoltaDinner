using BLL.Interfaces;
using BLL.Models;

namespace Dinner.BackgroundTasks 
{
    public class MenuTask : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private PeriodicTimer _timer = new (GetTimeUntilNextMonth());
        public MenuTask(IServiceScopeFactory scopeFactory)
        {      
            _scopeFactory = scopeFactory;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken) {
            while (await _timer.WaitForNextTickAsync(stoppingToken)
                && !stoppingToken.IsCancellationRequested)
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var scopedService = scope.ServiceProvider.GetRequiredService<IDbCrud>();
                    scopedService.UpdateMonthMenu();
                }
                _timer = new PeriodicTimer(GetTimeUntilNextMonth());
            }
        }

        public static TimeSpan GetTimeUntilNextMonth()
        {
            DateTime currentTime = DateTime.Now;
            DateTime desiredTime = new DateTime(DateTime.Now.Year,
                DateTime.Now.Month, 1, 0, 0, 0);
            desiredTime = desiredTime.AddMonths(1);
            TimeSpan timeDifference = desiredTime - currentTime;
            return timeDifference;
        }
    }
}
