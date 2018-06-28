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
       private string userId = string.Empty;
       public UserData(AcessTokenandOpendid model, string userId)
       {
           this.accessToken = model.acessToken;
           this.openid = model.opendid;
           this.userId = userId;
       }
       public bool GetUserData()
       {
           bool bolResult = false;
           WebClient webClient = WebClient.instance;
           lifesense.BLL.http.RequestParam.RequestParam requestModel = new RequestParam.RequestParam();
           requestModel.openid = openid;
           requestModel.day = DateTime.Now.AddDays(-13).ToString("yyyy-MM-dd");
           string param = string.Empty;
           string param2 = string.Empty;
           string userInfo = string.Empty;
           try
           {

               param = Consts.GET_SLEEP_DATA + getParams();
               param2 = JsonConvert.SerializeObject(requestModel);
               userInfo = webClient.Post(param, param2, "application/json");
               SleepData sleepModel = null;
               sleepModel = JsonConvert.DeserializeObject<SleepData>(userInfo);
               lifesense.Model.t_sleepinfo sleepInfoModel = new lifesense.Model.t_sleepinfo();
               sleepInfoModel.UserID = userId;
               sleepInfoModel.SleepingTime = GetTime(sleepModel.sleep.startTime.ToString());
               sleepInfoModel.LongSleepNum = sleepModel.sleep.depthTime;
               sleepInfoModel.ShallowSleepNum = sleepModel.sleep.shallowTime;
               sleepInfoModel.WakeUpLong = sleepModel.sleep.consciousTime;
               sleepInfoModel.WakingTime = Convert.ToDateTime(sleepInfoModel.SleepingTime).AddMinutes(sleepModel.sleep.totalTime- sleepModel.sleep.consciousTime);

               t_sleepinfo sleepinfoBll = new t_sleepinfo();
              if (sleepinfoBll.Add(sleepInfoModel)<1)
              {

              }

           }
           catch (Exception ex)
           {

           }
           try
           {
               param = Consts.GET_SPORT_DATA + getParams();
               userInfo = webClient.Post(param, param2, "application/json");
               SportData sportModel = null;
               sportModel = JsonConvert.DeserializeObject<SportData>(userInfo);

               lifesense.Model.t_walkinfo walkInfoModel = new lifesense.Model.t_walkinfo();
               walkInfoModel.UserID = userId;
               walkInfoModel.MeasureTime =Convert.ToDateTime (requestModel.day);
               walkInfoModel.StepNum = sportModel.sport.stepCount;
               walkInfoModel.Mileage = Convert.ToDecimal (sportModel.sport.distance);
               walkInfoModel.Calorie = Convert.ToDecimal (sportModel.sport.calorie);

               t_walkinfo walkinfoBll = new t_walkinfo();
               if (walkinfoBll.Add(walkInfoModel) < 1)
               {

               }
           }
           catch (Exception ex)
           {

           }
           try
           {
               param = Consts.GET_HEART_DATA + getParams();
               userInfo = webClient.Post(param, param2, "application/json");
               HeartrateData heartrateModel = null;
               heartrateModel = JsonConvert.DeserializeObject<HeartrateData>(userInfo);
               if (heartrateModel.heartrate != null)
               {
                   lifesense.Model.t_heartrateinfo heartrateInfoModel = new lifesense.Model.t_heartrateinfo();
                   heartrateInfoModel.UserID = userId;
                   heartrateInfoModel.StartTime = Convert.ToDateTime(requestModel.day);
                   heartrateInfoModel.HeartRate = heartrateModel.heartrate.heartrate;
                   t_heartrateinfo heartrateBll = new t_heartrateinfo();
                   if (heartrateBll.Add(heartrateInfoModel) < 1)
                   {

                   }
               }

           }
           catch (Exception ex)
           {


           }
           return bolResult;
       }
       /// <summary>
       /// 时间戳转为C#格式时间
       /// </summary>
       /// <param name="timeStamp">Unix时间戳格式</param>
       /// <returns>C#格式时间</returns>
       public static DateTime GetTime(string timeStamp)
       {
           DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));

           long lTime = long.Parse(timeStamp + "0000");
           TimeSpan toNow = new TimeSpan(lTime);
           return dtStart.Add(toNow);
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
