using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using System.Windows.Forms;
using System.Collections;
using lifesense.BLL.http.config;

namespace ConsoleLifesense
{
    class Program
    {
        static void Main(string[] args)
        {
            log4net.ILog log = log4net.LogManager.GetLogger("控制台模式");
            SyncDataManager temp = new SyncDataManager();
            temp.log = log;
            temp.start();
        }
    }
}
