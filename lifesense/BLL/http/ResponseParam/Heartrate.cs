using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lifesense.BLL.http.ResponseParam
{
   public class Heartrate
    {
       public string heartrate { get; set; }
       public int fastHeartRate { get; set; }
       public int slowHeartRate { get; set; }
       public int restingHeartRate { get; set; }
    }
}
