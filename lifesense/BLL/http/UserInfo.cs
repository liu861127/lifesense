using ConsoleLifesense;
using lifesense.BLL.http.consts;
using Maticsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using lifesense.BLL.http.config;

namespace lifesense.BLL.http
{
    public  class UserInfo
    {
       private  String mAuthorizeCode;
       public UserInfo(String authorizeCode)
        {
            this.mAuthorizeCode = authorizeCode;
        }

       public String getUserInfo()
       {
           WebClient webClient = WebClient.instance;
           try
           {
               String param = Consts.GET_ACCESS_TOKEN_AND_OPENID + getParams();
               String userInfo = webClient.Post(param, "");
               return userInfo;
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
               String authorizeCode = value.Substring(start, 41);
               return authorizeCode;
           }
           return "";
       }

       private String getParams()
       {
 
           String appId = AppConfig.getAPPid();
           String grantType = "authorization_code";
           String code = mAuthorizeCode;
           String time = TimeParser.GetTimeStamp(DateTime.Now);
           List<System.String> array = new List<System.String>();
           string APPsecret = AppConfig.getAPPsecret();
           array.Add(APPsecret);
           array.Add(appId);
           array.Add(grantType);
           array.Add(code);
           array.Add(time);
           String checkSum = SHAUtils.getSHACode(array.ToArray());

           StringBuilder sb = new StringBuilder();
           sb.Append("?app_id=" + appId);
           sb.Append("&grant_type="+grantType);
           sb.Append("&code="+code);
           sb.Append("&timestamp="+time);
           sb.Append("&checksum="+checkSum);

           return sb.ToString();
       }
    }
}
