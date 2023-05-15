using Hangfire.Dashboard;

namespace GUVENYOLDAS.Hangfire.Web.Helper
{

    public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            //TODO : where authorization definitions, checks and other things are done.
            return true;
        }
    }
}