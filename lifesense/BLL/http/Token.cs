using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Maticsoft.Common;
using lifesense.BLL.http.consts;
using lifesense.BLL.http.config;
using System.Collections;

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
            return webClient.Post(Consts.GET_USER_TOKEN_URL + getParams(), "");
        }

        private String getParams()
        {
            StringBuilder sb = new StringBuilder();
            String appId = AppConfig.getAPPid();
            sb.Append("?app_id=" + appId);
            sb.Append("&scope=");
            sb.Append("&state=");
            String responseType = "code";
            sb.Append("&response_typ=" + responseType);
            String time = new DateTime().ToLongTimeString();
            sb.Append("&timestamp=" + time);

            ArrayList array = new ArrayList();
            array.Add(appId);
            array.Add(responseType);
            array.Add(time);
            sb.Append("&checksun=" + SHAUtils.getSHACode(array));

            return sb.ToString();
        }




    }
}
