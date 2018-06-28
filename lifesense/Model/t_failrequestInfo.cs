using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lifesense.Model
{
    /// <summary>
    /// t_failrequestInfo:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class t_failrequestInfo
    {
        public t_failrequestInfo()
        { }
        #region Model
        private string _userid;
        private DateTime _writetime;
        private string _url;
        private string _reason;
        /// <summary>
        /// 
        /// </summary>
        public string UserID
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime WriteTime
        {
            set { _writetime = value; }
            get { return _writetime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Url
        {
            set { _url = value; }
            get { return _url; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Reason
        {
            set { _reason = value; }
            get { return _reason; }
        }
        #endregion Model

    }
}
