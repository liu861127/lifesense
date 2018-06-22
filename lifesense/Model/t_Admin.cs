using System;
namespace lifesense.Model
{
	/// <summary>
	/// t_Admin:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class t_Admin
	{
		public t_Admin()
		{}
		#region Model
		private int _id;
		private string _loginname;
		private string _loginpwd;
		private bool _isuse= true;
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
		public string LoginName
		{
			set{ _loginname=value;}
			get{return _loginname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string LoginPwd
		{
			set{ _loginpwd=value;}
			get{return _loginpwd;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool IsUse
		{
			set{ _isuse=value;}
			get{return _isuse;}
		}
		#endregion Model

	}
}

