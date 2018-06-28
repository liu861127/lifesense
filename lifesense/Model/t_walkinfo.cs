using System;
namespace lifesense.Model
{

    /// <summary>
    /// t_walkinfo:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class t_walkinfo
    {
        public t_walkinfo()
        { }
        #region Model
        private int _id;
        private string _userid = "";
        private DateTime? _measuretime = Convert.ToDateTime(null);
        private decimal? _stepnum = 0.0m;
        private decimal? _calorie = 0.0m;
        private decimal? _mileage = 0.0m;
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
        public DateTime? MeasureTime
        {
            set { _measuretime = value; }
            get { return _measuretime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? StepNum
        {
            set { _stepnum = value; }
            get { return _stepnum; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? Calorie
        {
            set { _calorie = value; }
            get { return _calorie; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? Mileage
        {
            set { _mileage = value; }
            get { return _mileage; }
        }
        #endregion Model

    }


}

