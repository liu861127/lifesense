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
            new SyncDataManager().start();
        }
    }
}
