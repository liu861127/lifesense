using System;
namespace lifesense.Model
{
	/// <summary>
	/// t_userinfo:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class t_userinfo
	{
		public t_userinfo()
		{}
		#region Model
		private int _id;
		private string _userid= "NULL";
		private string _username= "NULL";
		private string _userpwd= "NULL";
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
		public string UserID
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UserName
		{
			set{ _username=value;}
			get{return _username;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UserPwd
		{
			set{ _userpwd=value;}
			get{return _userpwd;}
		}
		#endregion Model

	}
}

