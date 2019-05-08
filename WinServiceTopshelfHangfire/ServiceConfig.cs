using System;
using Hangfire;
using Microsoft.Owin.Hosting;

namespace WinServiceTopshelfHangfire
{
    public class ServiceConfig
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private IDisposable _host;

        public void Start()
        {
            Log.Info("Iniciando Servicio de Conciliación");
            Console.WriteLine(DateTime.Now);

            var options = new StartOptions { Port = 8999 };

            _host = WebApp.Start<Startup>(options);
            Console.WriteLine();
            Console.WriteLine("HangFire has started");
            Console.WriteLine("Dashboard is available at http://localhost:8999/hangfire");
            Console.WriteLine();


            RecurringJob.AddOrUpdate("business-rules", () => BusinessRules.Execute(DateTime.Now), "*/1 * * * *");

        }

        public void Stop()
        {
            Log.Info("Deteniendo Servicio de Conciliación");
            _host.Dispose();
        }
    }
}
