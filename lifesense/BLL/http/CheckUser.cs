using ConsoleLifesense;
using lifesense.BLL.http.consts;
using Maticsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace lifesense.BLL.http
{
 public   class CheckUser
    {
     private String mToken;
     public CheckUser(String token)
       {
           this.mToken = token;
       }

      public String getTempAuthorizeCode()
      {
          WebClient webClient = WebClient.instance;
          try
          {
              String param = Consts.CHECK_USER + getParams();
              String userInfo = webClient.Post(param,"","");
              return getAuthorizeCode(userInfo);
          }
          catch (Exception ex)
          {
              return "";

          }
      }

      private String getAuthorizeCode(String userInfo)
      {
          JObject jo = (JObject)JsonConvert.DeserializeObject(userInfo);
          //JavaScriptObject jsonObj = JavaScriptConvert.DeserializeObject<JavaScriptObject>(userInfo);
          if (jo["redirect"] != null)
          {
              String value = jo["redirect"].ToString();
              int start = value.IndexOf("authorize_code=") + "authorize_code".Length;
              int end = value.IndexOf("&state=");
              int len = end - start-1;
              String authorizeCode = value.Substring(start+1,len);
              return authorizeCode;
          }
          return "";
      }

      private String getParams()
      {
          StringBuilder sb = new StringBuilder();
          sb.Append("?username=13560721536");//13560721536
          sb.Append("&password=861127");//861127
          sb.Append("&tempAuthorizeCode=" + mToken);
          return sb.ToString();
      }

    }
}
