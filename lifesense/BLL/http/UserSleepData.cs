using lifesense.BLL.http.config;
using lifesense.BLL.http.consts;
using lifesense.BLL.http.ResponseParam;
using Maticsoft.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lifesense.BLL.http
{
   public class UserData
    {
       private string accessToken = string.Empty;
       private string opendid = string.Empty;
       public UserData(acessTokenandOpendid model)
       {
           this.accessToken = model.acessToken;
           this.opendid = model.opendid;
       }
       public bool GetUserData()
       {
           bool bolResult = false;
           WebClient webClient = WebClient.instance;
           try
           {
               lifesense.BLL.http.RequestParam.RequestParam requestModel = new RequestParam.RequestParam();
               requestModel.opendId = opendid;
               requestModel.day = DateTime.Now.ToString("yyyy-MM-dd");
               string param = Consts.GET_SLEEP_DATA + getParams();
               String userInfo = webClient.Post(param, JsonConvert.SerializeObject(requestModel));
            
           }
           catch (Exception ex)
           {
               

           }
           return bolResult;
       }
       private string getParams()
       {
           String appId = AppConfig.getAPPid();
           String time = TimeParser.GetTimeStamp(DateTime.Now);
           List<System.String> array = new List<System.String>();
           string APPsecret = AppConfig.getAPPsecret();
           array.Add(APPsecret);
           array.Add(appId);
           array.Add(this.accessToken);
           array.Add(time);
           String checkSum = SHAUtils.getSHACode(array.ToArray());

           StringBuilder sb = new StringBuilder();
           sb.Append("?app_id=" + appId);
           sb.Append("&acess_token=" + this.accessToken);
           sb.Append("&timestamp=" + time);
           sb.Append("&checksum=" + checkSum);

           return sb.ToString();
       }
    }
}
