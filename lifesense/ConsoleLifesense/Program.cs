using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using System.Windows.Forms;

namespace ConsoleLifesense
{
    class Program
    {
        string APPid = "1b80cf89b99d608d0ee2845e1fae137d3aa3e04c";
        string APPsecret = "ef1a5ab65c0c26747d85fc0832a9d5548e1c9cb7";
        string Url = "";
        static void Main(string[] args)
        {
            //FormsAuthentication.HashPasswordForStoringInConfigFile（""，"SHA1");
            //Application.Run();
            new SyncDataManager().start();
        }

    }
}
