using Hangfire;
using GUVENYOLDAS.Hangfire.Web.Helper;

namespace GUVENYOLDAS.Hangfire.Web.Models.Base
{
    public abstract class HangfireBase
    {
        //TODO : here are some classes you might consider manipulating.
        protected IBackgroundJobClient JobClient;
        protected IRecurringJobManager RecurringJobManager;
        protected IConfiguration Configuration;

        public abstract string CronPattern { get; }
        public abstract Task ExecuteMethod();
        public HangfireBase()
        {

        }
    }
}
