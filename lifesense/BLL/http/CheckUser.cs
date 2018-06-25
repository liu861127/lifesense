using ConsoleLifesense;
using lifesense.BLL.http.consts;
using Maticsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

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
              String userInfo = webClient.Post(param,"");
              return getAuthorizeCode(userInfo);
          }
          catch (Exception ex)
          {
              return "";

          }
      }

      private String getAuthorizeCode(String userInfo)
      {
          JavaScriptObject jsonObj = JavaScriptConvert.DeserializeObject<JavaScriptObject>(userInfo);
          if (jsonObj.ContainsKey("redirect") && jsonObj["redirect"] != null)
          {
              String value = jsonObj["redirect"].ToString();
              int start = value.IndexOf("authorize_code=") + "authorize_code".Length;
              String authorizeCode = value.Substring(start,41);
              return authorizeCode;
          }
          return "";
      }

      private String getParams()
      {
          StringBuilder sb = new StringBuilder();
          sb.Append("?username=13560721536");
          sb.Append("&password=861127");
          sb.Append("&tempAuthorizeCode=" + SyncDataManager.token);
          return sb.ToString();
      }

    }
}
