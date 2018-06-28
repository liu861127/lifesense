using System;
namespace lifesense.Model
{
	/// <summary>
	/// t_heartrateinfo:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
    [Serializable]
    public partial class t_heartrateinfo
    {
        public t_heartrateinfo()
        { }
        #region Model
        private int _id;
        private string _userid;
        private DateTime? _starttime = Convert.ToDateTime(null);
        private string _heartrate = "";
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
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
        public DateTime? StartTime
        {
            set { _starttime = value; }
            get { return _starttime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string HeartRate
        {
            set { _heartrate = value; }
            get { return _heartrate; }
        }
        #endregion Model
    }
}

