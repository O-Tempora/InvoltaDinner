using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Interfaces;
using BLL.Models;
using BLL.Services;

namespace Dinner.BackgroundTasks
{
    public class ClaimRecordsTask : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private PeriodicTimer _timer = new (GetTimeUntilNoon());

        public ClaimRecordsTask(IServiceScopeFactory scopeFactory)
        {      
            _scopeFactory = scopeFactory;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken) {
            while (await _timer.WaitForNextTickAsync(stoppingToken)
                && !stoppingToken.IsCancellationRequested)
            {
                await ClaimRecords();
                _timer = new PeriodicTimer(GetTimeUntilNoon());
            }
        }
        private async Task ClaimRecords() {
            using (var scope = _scopeFactory.CreateScope())
            {
                var scopedService = scope.ServiceProvider.GetRequiredService<IDbCrud>();
                DateTime tomorrowDate = DateTime.Today.AddDays(1);
                List<RecordModel> records = scopedService.GetAllRecords();
                foreach (RecordModel record in records)
                {
                    if (record.Date.Date == tomorrowDate.Date) 
                    {
                        scopedService.CreateTransaction(record.UserId, record.Price * -1, record.Date);
                    }
                }
            }
        }
        public static TimeSpan GetTimeUntilNoon()
        {
            DateTime currentTime = DateTime.Now;
            DateTime desiredTime = new DateTime(DateTime.Now.Year,
                DateTime.Now.Month, DateTime.Now.Day, 12, 0, 0);
            TimeSpan timeDifference = (currentTime - desiredTime);
            var timePeriod = currentTime.Hour >= 12 ?
                (desiredTime.AddDays(1) - currentTime) :
                -timeDifference;
            return timePeriod;
        }
    }
}
