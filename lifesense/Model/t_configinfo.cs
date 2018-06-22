using System;
namespace lifesense.Model
{
	/// <summary>
	/// t_configinfo:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class t_configinfo
	{
		public t_configinfo()
		{}
		#region Model
		private string _keyid;
		private string _keyvalue= "NULL";
		/// <summary>
		/// 
		/// </summary>
		public string keyId
		{
			set{ _keyid=value;}
			get{return _keyid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string KeyValue
		{
			set{ _keyvalue=value;}
			get{return _keyvalue;}
		}
		#endregion Model

	}
}

