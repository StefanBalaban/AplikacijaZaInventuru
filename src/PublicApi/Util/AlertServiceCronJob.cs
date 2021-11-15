using ApplicationCore.Interfaces;
using EasyCronJob.Abstractions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PublicApi.Util
{
    public class AlertServiceCronJob : CronJobService
    {
        private readonly ILogger<AlertServiceCronJob> logger;
        private readonly IAlertService _alertService;

        public AlertServiceCronJob(ICronConfiguration<AlertServiceCronJob> cronConfiguration, IAlertService alertService)
            : base(cronConfiguration.CronExpression, cronConfiguration.TimeZoneInfo)
        {
            _alertService = alertService;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            return base.StartAsync(cancellationToken);
        }


        protected override Task ScheduleJob(CancellationToken cancellationToken)
        {
            return base.ScheduleJob(cancellationToken);
        }

        public override async Task DoWork(CancellationToken cancellationToken)
        {
            await _alertService.LowerAmountForDay(DateTime.Today);
            await _alertService.SendEmailAlerts();
            _ = base.DoWork(cancellationToken);
        }
    }
}
