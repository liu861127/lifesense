using lifesense.BLL.http.config;
using lifesense.BLL.http.ResponseParam;
using Maticsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lifesense.BLL.http
{
public abstract    class HttpBaseData
    {
    //重试的总次数
    protected int TRY_AGAIN_MUN = 2;
    //当前重试次数
    protected int currentTryRunNum = 0;
    protected const string CONTENT_TYPE = "application/json";
    public AcessTokenandOpendid mAcessTokenandOpendid;

    protected string getParams()
    {
        String appId = AppConfig.getAPPid();
        String time = TimeParser.GetTimeStamp(DateTime.Now);
        List<System.String> array = new List<System.String>();
        string APPsecret = AppConfig.getAPPsecret();
        array.Add(APPsecret);
        array.Add(appId);
        array.Add(mAcessTokenandOpendid.acessToken);
        array.Add(time);
        String checkSum = SHAUtils.getSHACode(array.ToArray());

        StringBuilder sb = new StringBuilder();
        sb.Append("?app_id=" + appId);
        sb.Append("&acess_token=" + mAcessTokenandOpendid.acessToken);
        sb.Append("&timestamp=" + time);
        sb.Append("&checksum=" + checkSum);

        return sb.ToString();
    }
    }
}
