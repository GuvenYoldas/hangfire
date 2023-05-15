using GUVENYOLDAS.Hangfire.Web.Models.Base;
using System.Diagnostics;

namespace GUVENYOLDAS.Hangfire.Web.Models
{
    public class ExampleModel : HangfireBase
    {
        //TODO : where you specify how often the job will run.
        public override string CronPattern => "* * * * *";
        public override async Task ExecuteMethod()
        {
            //TODO : write what you will do here.
            Debug.WriteLine("Hello, this task runs once every minute!");
        }
    }
}

