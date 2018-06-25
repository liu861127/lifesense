using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Maticsoft.Common;
using lifesense.BLL.http.consts;
using lifesense.BLL.http.config;
using System.Collections;
using Maticsoft.Common;

namespace lifesense.BLL.http
{
    public class Token
    {
       public Token()
       {

       }

       public String getTempAuthorizeCode()
       {
           WebClient webClient = WebClient.instance;
           try
           {
               String param = Consts.GET_USER_TOKEN_URL + getParams();
               String data = webClient.GetHtml(param);
               return data;
           }catch(Exception ex){
               String msg = ex.ToString();
               return msg;
           }

       }

       private String getParams()
       {
           StringBuilder sb = new StringBuilder();
           String appId= AppConfig.getAPPid();
           sb.Append("?app_id=" + appId);
           String scope = "";
           sb.Append("&scope=" + scope);
           String state = "12345678";
           sb.Append("&state=" + state);
           String responseType = "code";
           sb.Append("&response_type=" + responseType);
           String time = TimeParser.GetTimeStamp(DateTime.Now);
           sb.Append("&timestamp="+time);


           List<System.String> array = new List<System.String>();
           string APPsecret = AppConfig.getAPPsecret();// "ef1a5ab65c0c26747d85fc0832a9d5548e1c9cb7";
           array.Add(APPsecret);
           array.Add(appId);
           array.Add(responseType);
           array.Add(time);
           array.Add(state);
           array.Add(scope);
           sb.Append("&checksum=" + SHAUtils.getSHACode(array.ToArray()));
           return sb.ToString();
       }


    }
}
