using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using lifesense.Model;
using System.Data.SqlClient;
namespace lifesense.BLL
{
	/// <summary>
	/// t_userinfo
	/// </summary>
	public partial class t_userinfo
	{
		private readonly lifesense.DAL.t_userinfo dal=new lifesense.DAL.t_userinfo();
		public t_userinfo()
		{}
		#region  BasicMethod

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(lifesense.Model.t_userinfo model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(lifesense.Model.t_userinfo model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int ID)
		{
			
			return dal.Delete(ID);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string IDlist )
		{
			return dal.DeleteList(Maticsoft.Common.PageValidate.SafeLongFilter(IDlist,0) );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public lifesense.Model.t_userinfo GetModel(int ID)
		{
			
			return dal.GetModel(ID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public lifesense.Model.t_userinfo GetModelByCache(int ID)
		{
			
			string CacheKey = "t_userinfoModel-" + ID;
			object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(ID);
					if (objModel != null)
					{
						int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
						Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (lifesense.Model.t_userinfo)objModel;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return dal.GetList(Top,strWhere,filedOrder);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<lifesense.Model.t_userinfo> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<lifesense.Model.t_userinfo> DataTableToList(DataTable dt)
		{
			List<lifesense.Model.t_userinfo> modelList = new List<lifesense.Model.t_userinfo>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				lifesense.Model.t_userinfo model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = dal.DataRowToModel(dt.Rows[n]);
					if (model != null)
					{
						modelList.Add(model);
					}
				}
			}
			return modelList;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetAllList()
		{
			return GetList("");
		}

		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			return dal.GetRecordCount(strWhere);
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			return dal.GetListByPage( strWhere,  orderby,  startIndex,  endIndex);
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		//public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		//{
			//return dal.GetList(PageSize,PageIndex,strWhere);
		//}

		#endregion  BasicMethod
		#region  ExtensionMethod
        /// <summary>
        /// 分页返回数据
        /// </summary>
        /// <param name="GetDataSql">查询语句</param>
        /// <param name="OrderField">排序字段名 如[要排序的列 DESC] </param>
        /// <param name="pageSize">每页要有多少行数据</param>
        /// <param name="pageIndex">当前页</param>  
        /// <returns></returns>
        public DataSet ExecuteSqlPager(string GetDataSql, string OrderField, int pageIndex, int pageSize)
        {
            return dal.ExecuteSqlPager(GetDataSql, OrderField, pageIndex, pageSize);
        }

        public  DataSet ExecuteSqlPager(string GetDataSql, string OrderField, int pageIndex, int pageSize, params SqlParameter[] parameters)
        {
            return dal.ExecuteSqlPager(GetDataSql, OrderField, pageIndex, pageSize, parameters);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetSqlList(string strSql)
        {
            return dal.GetSqlList(strSql);
        }
		#endregion  ExtensionMethod
	}
}

