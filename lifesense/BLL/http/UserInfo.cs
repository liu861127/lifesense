using ConsoleLifesense;
using lifesense.BLL.http.consts;
using Maticsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using lifesense.BLL.http.config;
using Newtonsoft.Json.Linq;
using lifesense.BLL.http.ResponseParam;

namespace lifesense.BLL.http
{
    public  class UserInfo
    {
       private  String mAuthorizeCode;
       public UserInfo(String authorizeCode)
        {
            this.mAuthorizeCode = authorizeCode;
        }

       public AcessTokenandOpendid getUserInfo()
       {
           WebClient webClient = WebClient.instance;
           try
           {
               String param = Consts.GET_ACCESS_TOKEN_AND_OPENID + getParams();
               String userInfo = webClient.Post(param, "");

               return getAcessToken(userInfo);
           }
           catch (Exception ex)
           {
               return null;

           }
       }

       private AcessTokenandOpendid getAcessToken(String userInfo)
       {
           AcessTokenandOpendid model = new AcessTokenandOpendid();
           JObject jsonObj = (JObject)JsonConvert.DeserializeObject(userInfo);
           //JavaScriptObject jsonObj = JavaScriptConvert.DeserializeObject<JavaScriptObject>(userInfo);
           if (jsonObj["acessToken"] != null)
           {
               model.acessToken = jsonObj["acessToken"].ToString();
               model.opendid = jsonObj["openId"].ToString();
          
           }
           return model;
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
