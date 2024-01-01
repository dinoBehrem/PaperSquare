using PaperSquare.Infrastructure.Data.BackgroundJobs;
using Quartz;

namespace PaperSquare.API.Infrastructure.Quartz
{
    public static class QuartzConfiguration
    {
        public static IServiceCollection AddQuartzConfiguration(this IServiceCollection services)
        {
            services.AddQuartz(cfg =>
            {
                var jobKey = new JobKey(nameof(OutboxMessagesProcessingJob));

                cfg.AddJob<OutboxMessagesProcessingJob>(jobKey)
                   .AddTrigger(t => t.ForJob(jobKey)
                                     .WithSimpleSchedule(s => s.WithIntervalInSeconds(10).WithRepeatCount(1)));

                cfg.UseMicrosoftDependencyInjectionJobFactory();
            });

            services.AddQuartzHostedService();

            return services;
        }
    }
}
