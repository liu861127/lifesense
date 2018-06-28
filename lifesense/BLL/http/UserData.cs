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
       private string openid = string.Empty;
       public UserData(AcessTokenandOpendid model)
       {
           this.accessToken = model.acessToken;
           this.openid = model.opendid;
       }
       public bool GetUserData()
       {
           bool bolResult = false;
           WebClient webClient = WebClient.instance;
<<<<<<< HEAD:lifesense/BLL/http/UserData.cs

                   lifesense.BLL.http.RequestParam.RequestParam requestModel = new RequestParam.RequestParam();
                   requestModel.openid = openid;
                   requestModel.day = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                   string param = Consts.GET_SLEEP_DATA + getParams();
                   string param2 = JsonConvert.SerializeObject(requestModel);
                   String userInfo = webClient.Post(param, param2, "application/json");
                   SleepData sleepModel = JsonConvert.DeserializeObject<SleepData>(userInfo);
=======
           try
           {
               lifesense.BLL.http.RequestParam.RequestParam requestModel = new RequestParam.RequestParam();
               requestModel.openid = openid;
               requestModel.day = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
               string param = Consts.GET_SLEEP_DATA + getParams();
               string param2 = JsonConvert.SerializeObject(requestModel);
               String userInfo = webClient.Post(param, param2, "application/json");
               SleepData sleepModel = null;
               sleepModel = JsonConvert.DeserializeObject<SleepData>(userInfo);
>>>>>>> f82e63a5b8210a8077030bc4084f7347c33ff73f:lifesense/BLL/http/UserSleepData.cs

               param = Consts.GET_SPORT_DATA + getParams();
               userInfo = webClient.Post(param, param2, "application/json");
               SportData sportModel = null;
               sportModel = JsonConvert.DeserializeObject<SportData>(userInfo);

               param = Consts.GET_HEART_DATA + getParams();
               userInfo = webClient.Post(param, param2, "application/json");
               HeartrateData heartrateModel = null;
               heartrateModel = JsonConvert.DeserializeObject<HeartrateData>(userInfo);

<<<<<<< HEAD:lifesense/BLL/http/UserData.cs
                   param = Consts.GET_HEART_DATA + getParams();
                   userInfo = webClient.Post(param, param2, "application/json");
                   HeartrateData heartrateModel = JsonConvert.DeserializeObject<HeartrateData>(userInfo);
               
=======
           }
           catch (Exception ex)
           {


           }
>>>>>>> f82e63a5b8210a8077030bc4084f7347c33ff73f:lifesense/BLL/http/UserSleepData.cs
           return bolResult;
       }
       private DateTime getStartTimeByMinute()
       {
           DateTime startTime = DateTime.MinValue;

           return startTime;
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
