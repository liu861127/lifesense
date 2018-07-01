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
public    class HttpSportData : HttpBaseData
    {
    private WebClient webClient;
    private lifesense.Model.t_userinfo mUserModel;
    private string mSyncDay;
    public HttpSportData(AcessTokenandOpendid model, lifesense.Model.t_userinfo userModel, String syncDay)
       {
           base.mAcessTokenandOpendid = model;
           webClient = WebClient.instance;
           this.mUserModel = userModel;
           this.mSyncDay = syncDay;
       }

    public SportData getSportData(String day)
       {
           lifesense.BLL.http.RequestParam.RequestParam requestModel = new RequestParam.RequestParam();
           requestModel.openid = mAcessTokenandOpendid.opendid;
           requestModel.day = day;
           string param2 = JsonConvert.SerializeObject(requestModel);
           return getSportDataExt(param2);
       }

       private SportData getSportDataExt(string param2)
       {
           string param = Consts.GET_SPORT_DATA + getParams();
           try
           {
               String sleepInfo = webClient.Post(param, param2, CONTENT_TYPE);
               return JsonConvert.DeserializeObject<SportData>(sleepInfo);
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
                   return getSportDataExt(param2);
               }
           }
       }

    }
}
