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
        string APPid = "1b80cf89b99d608d0ee2845e1fae137d3aa3e04c";
        string APPsecret = "ef1a5ab65c0c26747d85fc0832a9d5548e1c9cb7";
        string Url = "";
        static void Main(string[] args)
        {
            new SyncDataManager().start();
            //String sha = getSHA();
            //sha = "";
        }


        private static String getSHA()
        {
            List<System.String> array = new List<System.String>();
            array.Add("bd5cdb1a49ad26878a9ca800fdd53956c6b61882");
            array.Add("818ea8c3572ec6277fe2bf0e8e58ffa274ca11a0");
            array.Add("UKjPas12");
            array.Add("code");
            array.Add("1528937315037");
            String[] arr = array.ToArray();
            return SHAUtils.getSHACode(arr);
        }
    }
}
