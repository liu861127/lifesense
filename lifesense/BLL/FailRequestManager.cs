using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lifesense.BLL
{
 public  class FailRequestManager
    {
     public static FailRequestManager mInstance = new FailRequestManager();
 

     private lifesense.BLL.t_failrequestInfo failRequestInfo;
     private FailRequestManager()
     {
         failRequestInfo = new t_failrequestInfo();
     }
     public bool saveInFailList(string UserID, DateTime WriteTime, string Url, string Reason)
     {
        List<lifesense.Model.t_failrequestInfo> failList= failRequestInfo.GetModelList("UserID='" + UserID + "' and WriteTime='" + WriteTime + "'");
        if (failList != null && failList.Count > 0)
        {
            failRequestInfo.Delete(UserID, WriteTime);

        }
        lifesense.Model.t_failrequestInfo model = getFailRequestInfoModel(UserID, WriteTime, Url, Reason);
        return failRequestInfo.Add(model);

     }

     public bool deleteFromFialList(string UserID, DateTime WriteTime)
     {
         return failRequestInfo.Delete(UserID, WriteTime);
     }

     private lifesense.Model.t_failrequestInfo getFailRequestInfoModel(string UserID, DateTime WriteTime, string Url, string Reason)
     {
         lifesense.Model.t_failrequestInfo model = new Model.t_failrequestInfo();
         model.UserID = UserID;
         model.WriteTime = WriteTime;
         model.Url = Url;
         model.Reason = Reason;
         return model;
     }

    }
}
