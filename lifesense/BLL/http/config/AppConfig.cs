using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace lifesense.BLL.http.config
{
  public  class AppConfig
    {
      public static String getAPPid()
      {
          return ConfigurationManager.AppSettings["APPid"];  
      }

      public static String getAPPsecret()
      {
          return ConfigurationManager.AppSettings["APPsecret"];  
      }
    }
}
