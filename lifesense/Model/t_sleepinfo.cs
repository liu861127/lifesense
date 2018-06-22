using System;
namespace lifesense.Model
{
	/// <summary>
	/// t_sleepinfo:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class t_sleepinfo
	{
		public t_sleepinfo()
		{}
		#region Model
		private int _id;
		private DateTime? _sleepingtime= Convert.ToDateTime(null);
		private DateTime? _wakingtime= Convert.ToDateTime(null);
		private decimal? _longsleepnum=null;
		private decimal? _shallowsleepnum=null;
		private decimal? _wakeuplong=null;
		private int? _wakingnum;
		/// <summary>
		/// 
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? SleepingTime
		{
			set{ _sleepingtime=value;}
			get{return _sleepingtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? WakingTime
		{
			set{ _wakingtime=value;}
			get{return _wakingtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? LongSleepNum
		{
			set{ _longsleepnum=value;}
			get{return _longsleepnum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? ShallowSleepNum
		{
			set{ _shallowsleepnum=value;}
			get{return _shallowsleepnum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? WakeUpLong
		{
			set{ _wakeuplong=value;}
			get{return _wakeuplong;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? WakingNum
		{
			set{ _wakingnum=value;}
			get{return _wakingnum;}
		}
		#endregion Model

	}
}

