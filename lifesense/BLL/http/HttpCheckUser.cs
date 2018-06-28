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
 public   class HttpCheckUser
    {
     private String mToken;
     private lifesense.Model.t_userinfo mModel;
     public HttpCheckUser(String token, lifesense.Model.t_userinfo model)
       {
           this.mToken = token;
           this.mModel = model;
       }

      public String getTempAuthorizeCode()
      {
          string returnMsg = string.Empty;
          WebClient webClient = WebClient.instance;
          try
          {
              if (mModel != null)
              {
                  String param = Consts.CHECK_USER + getParams(mModel);
                  String userInfo = webClient.Post(param, "", "");
                  returnMsg= getAuthorizeCode(userInfo);
              }
          }
          catch (Exception ex)
          {
              returnMsg = string.Empty;
          }
          return returnMsg;
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

      private String getParams(lifesense.Model.t_userinfo model)
      {
          StringBuilder sb = new StringBuilder();
          sb.AppendFormat("?username={0}",model.UserName);//13560721536
          sb.AppendFormat("&password={0}",model.UserPwd);//861127
          sb.Append("&tempAuthorizeCode=" + mToken);
          return sb.ToString();
      }

    }
}
