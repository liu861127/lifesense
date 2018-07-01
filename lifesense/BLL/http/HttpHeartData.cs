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
 public   class HttpHeartData : HttpBaseData
    {
     private WebClient webClient;
     private lifesense.Model.t_userinfo mUserModel;
     private string mSyncDay;
     public HttpHeartData(AcessTokenandOpendid model, lifesense.Model.t_userinfo userModel, String syncDay)
       {
           base.mAcessTokenandOpendid = model;
           webClient = WebClient.instance;
           this.mUserModel = userModel;
           this.mSyncDay = syncDay;
       }

     public HeartrateData getHeartrateData(string day)
       {

           lifesense.BLL.http.RequestParam.RequestParam requestModel = new RequestParam.RequestParam();
           requestModel.openid = mAcessTokenandOpendid.opendid;
           requestModel.day = day;
           string param2 = JsonConvert.SerializeObject(requestModel);
           return getHeartrateDataExt(param2);
       }

     private HeartrateData getHeartrateDataExt(string param2)
       {
           string param = Consts.GET_HEART_DATA + getParams();
           try
           {
               String sleepInfo = webClient.Post(param, param2, CONTENT_TYPE);
               HeartrateData data = JsonConvert.DeserializeObject<HeartrateData>(sleepInfo);
               if (data == null || data.heartrate==null)
               {
                   data = new HeartrateData();
                   Heartrate heartrate = new Heartrate();
                   data.heartrate = heartrate;
               }
               return data;
           }
           catch (Exception ex)
           {
               if (currentTryRunNum == TRY_AGAIN_MUN)
               {
                   FailRequestManager.mInstance.saveInFailList(mUserModel.UserID, TimeParser.GetTime(mSyncDay), param, (ex == null ? "" : ex.Message));
                   return null;
               }
               else
               {
                   currentTryRunNum++;
                   return getHeartrateDataExt(param2);
               }
           }
       }

    }
}
