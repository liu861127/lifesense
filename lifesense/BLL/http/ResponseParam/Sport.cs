using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lifesense.BLL.http.ResponseParam
{
    public class Sport
    {
        public int stepCount { get; set; }
        public double calorie { get; set; }
        public double distance { get; set; }
    }

    public class SportData
    {
        public Sport sport { get; set; }
    } 
}
