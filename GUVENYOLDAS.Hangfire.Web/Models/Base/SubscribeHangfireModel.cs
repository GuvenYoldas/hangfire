using Hangfire;

namespace GUVENYOLDAS.Hangfire.Web.Models.Base
{
    public class SubscribeHangfireModel
    {
        public static void SubscribeCustomHangfires(IConfiguration configuration)
        {
            var schedulerType = typeof(HangfireBase);
            var hangfireModelList = schedulerType.Assembly.GetTypes().Where(w => w.IsSubclassOf(schedulerType) && !w.IsAbstract)
                                                                     .Select(s => (HangfireBase)Activator.CreateInstance(s))
                                                                     .ToList();
            foreach (var hangfireModel in hangfireModelList)
            {
                var className = hangfireModel.GetType().Name;
                var hangfireIsEnableValue = configuration[$"AppConfig:HangfireJobs:{className}:Enable"];
                if (!string.IsNullOrEmpty(hangfireIsEnableValue) && !string.IsNullOrWhiteSpace(hangfireIsEnableValue))
                {
                    bool hangfireIsEnable = bool.Parse(hangfireIsEnableValue);
                    if (hangfireIsEnable)
                    {
                        RecurringJob.AddOrUpdate(() => hangfireModel.ExecuteMethod(), hangfireModel.CronPattern);
                    }
                }
            }
        }
    }
}
