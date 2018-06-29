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
    public class HttpSleepData : HttpBaseData
    {
       private WebClient webClient;
       public HttpSleepData(AcessTokenandOpendid model)
       {
           base.mAcessTokenandOpendid = model;
           webClient = WebClient.instance;
       }

       public SleepData getSleepData(String day)
       {

           lifesense.BLL.http.RequestParam.RequestParam requestModel = new RequestParam.RequestParam();
           requestModel.openid = mAcessTokenandOpendid.opendid;
           requestModel.day = day; 
           string param2 = JsonConvert.SerializeObject(requestModel);
           return getSleepDataExt(param2);
       }

       private SleepData getSleepDataExt(string param2)
       {
           try
           {
               string param = Consts.GET_SLEEP_DATA + getParams();
               String sleepInfo = webClient.Post(param, param2, CONTENT_TYPE);
               return JsonConvert.DeserializeObject<SleepData>(sleepInfo);
           }
           catch (Exception ex)
           {
               return null;
           }
       }

    }
}
