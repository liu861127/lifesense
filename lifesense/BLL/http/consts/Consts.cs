using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lifesense.BLL.http.consts
{
    public class Consts
    {
        public const String BASE_URL = "https://open.lifesense.com/openapi_service/";
       //获取用户授权token
       public const String GET_USER_TOKEN_URL = BASE_URL + "oauth2/authorize";
       //乐心用户身份验证
       public const String CHECK_USER = BASE_URL + "oauth2/authenticate";
        //根据授权码获取Access Token 和OpenID
       public const String GET_ACCESS_TOKEN_AND_OPENID = BASE_URL + "oauth2/access_token";
       //获取睡眠数据
       public const String GET_SLEEP_DATA = BASE_URL + "business/getSleep";
       //获取运动
       public const String GET_SPORT_DATA = BASE_URL + "business/getSport";
        //获取心率
       public const String GET_HEART_DATA = BASE_URL + "business/getHeartrate";

    }
}
