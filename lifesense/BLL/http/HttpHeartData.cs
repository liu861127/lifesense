﻿using lifesense.BLL.http.config;
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
     public HttpHeartData(AcessTokenandOpendid model)
       {
           base.mAcessTokenandOpendid = model;
           webClient = WebClient.instance;
       }

     public HeartrateData getHeartrateData()
       {

           lifesense.BLL.http.RequestParam.RequestParam requestModel = new RequestParam.RequestParam();
           requestModel.openid = mAcessTokenandOpendid.opendid;
           requestModel.day = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
           string param2 = JsonConvert.SerializeObject(requestModel);
           return getHeartrateData(param2);
       }

     private HeartrateData getHeartrateData(string param2)
       {
           try
           {
               string param = Consts.GET_HEART_DATA + getParams();
               String sleepInfo = webClient.Post(param, param2, CONTENT_TYPE);
               return JsonConvert.DeserializeObject<HeartrateData>(sleepInfo);
           }
           catch (Exception ex)
           {
               return null;
           }
       }

    }
}
