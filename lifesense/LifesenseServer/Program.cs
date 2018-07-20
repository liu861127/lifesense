using ConsoleLifesense;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace LifesenseServer
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        static void Main()
        {
            Service1 cs = new Service1();
            //new SyncDataManager().start();
            if (Environment.UserInteractive)
            {
                cs.DebugStart();
            }
            else
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[] 
            { 
                cs
            };
                ServiceBase.Run(ServicesToRun);
            }
        }
    }
}
