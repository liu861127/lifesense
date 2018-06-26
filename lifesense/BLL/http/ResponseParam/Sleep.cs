using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lifesense.BLL.http.ResponseParam
{
   public class Sleep
    {
        int totalTime {get;set;}
        int depthTime {get;set;}
        int shallowTime {get;set;}
        int consciousTime {get;set;}
       long startTime {get;set;}
       //{"sleep":{"totalTime":0,"depthTime":0,"shallowTime":0,"consciousTime":0,"startTime":0}}
    }
}
