using Hangfire;
using Hangfire.LiteDB;
using Hangfire.Storage;
using Owin;

namespace WinServiceTopshelfHangfire
{
    public class Startup
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public void Configuration(IAppBuilder appBuilder)
        {

            GlobalConfiguration.Configuration.UseLiteDbStorage();

            var options = new BackgroundJobServerOptions
            {
                WorkerCount = 1,
            };

            appBuilder.UseHangfireServer(options);
            appBuilder.UseHangfireDashboard();

            StopJobs();

        }

        public void StopJobs()
        {
            Log.Info("Verificando si hay Jobs pendientes.");
            using (var conn = JobStorage.Current.GetConnection())
            {
                var manager = new RecurringJobManager();
                foreach (var job in conn.GetRecurringJobs())
                {
                    manager.RemoveIfExists(job.Id);
                    Log.InfoFormat("Job has been stopped: {0}", job.Id);
                }
            }
        }
    }
}
