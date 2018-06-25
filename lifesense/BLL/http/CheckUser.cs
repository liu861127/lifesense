using ConsoleLifesense;
using lifesense.BLL.http.consts;
using Maticsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lifesense.BLL.http
{
 public   class CheckUser
    {
      public CheckUser()
       {

       }

      public String getTempAuthorizeCode()
      {
          WebClient webClient = WebClient.instance;
          try
          {
              String param = Consts.CHECK_USER + getParams();
              String userInfo = webClient.GetHtml(param);
              return userInfo;
          }
          catch (Exception ex)
          {
              return "";

          }
      }

      private String getParams()
      {
          StringBuilder sb = new StringBuilder();
          sb.Append("?username=lingxizhixia");
          sb.Append("&password=jkeodmdk228H");
          sb.Append("&tempAuthorizeCode=" + SyncDataManager.token);
          return sb.ToString();
      }

    }
}
