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
    public HttpSportData(AcessTokenandOpendid model)
       {
           base.mAcessTokenandOpendid = model;
           webClient = WebClient.instance;
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
           try
           {
               string param = Consts.GET_SPORT_DATA + getParams();
               String sleepInfo = webClient.Post(param, param2, CONTENT_TYPE);
               return JsonConvert.DeserializeObject<SportData>(sleepInfo);
           }
           catch (Exception ex)
           {
               return null;
           }
       }

    }
}
