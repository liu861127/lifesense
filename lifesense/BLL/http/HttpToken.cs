using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Maticsoft.Common;
using lifesense.BLL.http.consts;
using lifesense.BLL.http.config;
using System.Collections;
using Maticsoft.Common;
using System.Collections.Specialized;
using System.Web;

namespace lifesense.BLL.http
{
    public class HttpToken : HttpBaseData
    {
        private string mSyncDay;
        private lifesense.Model.t_userinfo mModel;
        public HttpToken(lifesense.Model.t_userinfo model, String syncDay)
       {
           this.mSyncDay = syncDay;
           this.mModel = model;
       }

       public String getTempAuthorizeCode()
       {
           WebClient webClient = WebClient.instance;
           String param = Consts.GET_USER_TOKEN_URL + getParams();
           try
           {
               System.Net.HttpWebResponse httpWebResponse = webClient.getHttpWebResponse(param);
               String tempUrl =httpWebResponse.GetResponseHeader("Location");
               Uri uri = new Uri(tempUrl);  
                string queryString = uri.Query;  
                NameValueCollection col = GetQueryString(queryString);
                return col["tempAuthorizeCode"].ToString();
           }catch(Exception ex){
               if (currentTryRunNum == TRY_AGAIN_MUN)
               {
                   FailRequestManager.mInstance.saveInFailList(mModel.UserID, TimeParser.GetTime(mSyncDay), param, (ex == null ? "" : ex.Message));
                   return null;
               }
               else
               {
                   currentTryRunNum++;
                   return getTempAuthorizeCode();
               }
           }

       }
       public static NameValueCollection GetQueryString(string queryString)
       {
           return GetQueryString(queryString, null, true);
       } 
       /// <summary>  
       /// 将查询字符串解析转换为名值集合.  
       /// </summary>  
       /// <param name="queryString"></param>  
       /// <param name="encoding"></param>  
       /// <param name="isEncoded"></param>  
       /// <returns></returns>  
       public static NameValueCollection GetQueryString(string queryString, Encoding encoding, bool isEncoded)
       {
           queryString = queryString.Replace("?", "");
           NameValueCollection result = new NameValueCollection(StringComparer.OrdinalIgnoreCase);
           if (!string.IsNullOrEmpty(queryString))
           {
               int count = queryString.Length;
               for (int i = 0; i < count; i++)
               {
                   int startIndex = i;
                   int index = -1;
                   while (i < count)
                   {
                       char item = queryString[i];
                       if (item == '=')
                       {
                           if (index < 0)
                           {
                               index = i;
                           }
                       }
                       else if (item == '&')
                       {
                           break;
                       }
                       i++;
                   }
                   string key = null;
                   string value = null;
                   if (index >= 0)
                   {
                       key = queryString.Substring(startIndex, index - startIndex);
                       value = queryString.Substring(index + 1, (i - index) - 1);
                   }
                   else
                   {
                       key = queryString.Substring(startIndex, i - startIndex);
                   }
                   if (isEncoded)
                   {
                       result[MyUrlDeCode(key, encoding)] = MyUrlDeCode(value, encoding);
                   }
                   else
                   {
                       result[key] = value;
                   }
                   if ((i == (count - 1)) && (queryString[i] == '&'))
                   {
                       result[key] = string.Empty;
                   }
               }
           }
           return result;
       }
       public static string MyUrlDeCode(string str, Encoding encoding)
       {
           if (encoding == null)
           {
               Encoding utf8 = Encoding.UTF8;
               //首先用utf-8进行解码                       
               string code = HttpUtility.UrlDecode(str.ToUpper(), utf8);
               //将已经解码的字符再次进行编码.  
               string encode = HttpUtility.UrlEncode(code, utf8).ToUpper();
               if (str == encode)
                   encoding = Encoding.UTF8;
               else
                   encoding = Encoding.GetEncoding("gb2312");
           }
           return HttpUtility.UrlDecode(str, encoding);
       } 
       private String getParams()
       {
           StringBuilder sb = new StringBuilder();
           String appId= AppConfig.getAPPid();
           sb.Append("?app_id=" + appId);
           String scope = "";
           sb.Append("&scope=" + scope);
           String state = "12345678";
           sb.Append("&state=" + state);
           String responseType = "code";
           sb.Append("&response_type=" + responseType);
           String time = TimeParser.GetTimeStamp(DateTime.Now);
           sb.Append("&timestamp="+time);


           List<System.String> array = new List<System.String>();
           string APPsecret = AppConfig.getAPPsecret();
           array.Add(APPsecret);
           array.Add(appId);
           array.Add(responseType);
           array.Add(time);
           array.Add(state);
           array.Add(scope);
           sb.Append("&checksum=" + SHAUtils.getSHACode(array.ToArray()));
           return sb.ToString();
       }


    }
}