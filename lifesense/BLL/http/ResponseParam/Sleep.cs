using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lifesense.BLL.http.ResponseParam
{
    public class Sleep
    {
        public int totalTime { get; set; }
        public int depthTime { get; set; }
        public int shallowTime { get; set; }
        public int consciousTime { get; set; }
        public long startTime { get; set; }
    }

    public class SleepData
    {
        public Sleep sleep { get; set; }
    }  
}
