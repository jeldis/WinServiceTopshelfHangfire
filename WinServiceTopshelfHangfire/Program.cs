using System;
using Topshelf;

namespace WinServiceTopshelfHangfire
{
    class Program
    {
        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();

            if (args.Length > 0 && (args[0] == "-all" || args[0] == "-manual"))
            {
                Manual(args);
                return;
            }

            HostFactory.Run(x =>
            {
                x.Service<ServiceConfig>(s =>
                {
                    s.ConstructUsing(name => new ServiceConfig());
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });

                x.RunAsLocalSystem();
                x.SetDescription("Servicio - WinServiceTopshelf");
                x.SetDisplayName("WinServiceTopshelf");
                x.SetServiceName("WinServiceTopshelf");

            });
        }

        static void Manual(string[] args)
        {
            string msg = string.Empty;

            switch (args[0])
            {
                case "-all":
                    Console.WriteLine("-all");


                    break;
                case "-manual":
                    Console.WriteLine("-manual");

                    break;
                default:
                    msg = string.Format("Debe pasar al menos un argumento");
                    Console.WriteLine(msg);

                    break;
            }
        }
    }
}
