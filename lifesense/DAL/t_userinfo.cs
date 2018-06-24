using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;//Please add references
namespace lifesense.DAL
{
	/// <summary>
	/// 数据访问类:t_userinfo
	/// </summary>
	public partial class t_userinfo
	{
		public t_userinfo()
		{}
		#region  BasicMethod



		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(lifesense.Model.t_userinfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into t_userinfo(");
			strSql.Append("UserID,UserName,UserPwd)");
			strSql.Append(" values (");
			strSql.Append("@UserID,@UserName,@UserPwd)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.VarChar,50),
					new SqlParameter("@UserName", SqlDbType.VarChar,50),
					new SqlParameter("@UserPwd", SqlDbType.VarChar,50)};
			parameters[0].Value = model.UserID;
			parameters[1].Value = model.UserName;
			parameters[2].Value = model.UserPwd;

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
			if (obj == null)
			{
				return 0;
			}
			else
			{
				return Convert.ToInt32(obj);
			}
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(lifesense.Model.t_userinfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update t_userinfo set ");
			strSql.Append("UserID=@UserID,");
			strSql.Append("UserName=@UserName,");
			strSql.Append("UserPwd=@UserPwd");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.VarChar,50),
					new SqlParameter("@UserName", SqlDbType.VarChar,50),
					new SqlParameter("@UserPwd", SqlDbType.VarChar,50),
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = model.UserID;
			parameters[1].Value = model.UserName;
			parameters[2].Value = model.UserPwd;
			parameters[3].Value = model.ID;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from t_userinfo ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		/// <summary>
		/// 批量删除数据
		/// </summary>
		public bool DeleteList(string IDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from t_userinfo ");
			strSql.Append(" where ID in ("+IDlist + ")  ");
			int rows=DbHelperSQL.ExecuteSql(strSql.ToString());
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public lifesense.Model.t_userinfo GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ID,UserID,UserName,UserPwd from t_userinfo ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			lifesense.Model.t_userinfo model=new lifesense.Model.t_userinfo();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				return DataRowToModel(ds.Tables[0].Rows[0]);
			}
			else
			{
				return null;
			}
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public lifesense.Model.t_userinfo DataRowToModel(DataRow row)
		{
			lifesense.Model.t_userinfo model=new lifesense.Model.t_userinfo();
			if (row != null)
			{
				if(row["ID"]!=null && row["ID"].ToString()!="")
				{
					model.ID=int.Parse(row["ID"].ToString());
				}
				if(row["UserID"]!=null)
				{
					model.UserID=row["UserID"].ToString();
				}
				if(row["UserName"]!=null)
				{
					model.UserName=row["UserName"].ToString();
				}
				if(row["UserPwd"]!=null)
				{
					model.UserPwd=row["UserPwd"].ToString();
				}
			}
			return model;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ID,UserID,UserName,UserPwd ");
			strSql.Append(" FROM t_userinfo ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" ID,UserID,UserName,UserPwd ");
			strSql.Append(" FROM t_userinfo ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM t_userinfo ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			object obj = DbHelperSQL.GetSingle(strSql.ToString());
			if (obj == null)
			{
				return 0;
			}
			else
			{
				return Convert.ToInt32(obj);
			}
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby );
			}
			else
			{
				strSql.Append("order by T.ID desc");
			}
			strSql.Append(")AS Row, T.*  from t_userinfo T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
		}

        
         ///<summary>
         ///分页获取数据列表
         ///</summary>
        public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@tblName", SqlDbType.VarChar, 255),
                    new SqlParameter("@fldName", SqlDbType.VarChar, 255),
                    new SqlParameter("@PageSize", SqlDbType.Int),
                    new SqlParameter("@PageIndex", SqlDbType.Int),
                    new SqlParameter("@IsReCount", SqlDbType.Bit),
                    new SqlParameter("@OrderType", SqlDbType.Bit),
                    new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
                    };
            parameters[0].Value = "t_userinfo";
            parameters[1].Value = "ID";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }

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
            return DbHelperSQL.ExecuteSqlPager(GetDataSql, OrderField, pageIndex, pageSize);
        }
        
        public  DataSet ExecuteSqlPager(string GetDataSql, string OrderField, int pageIndex, int pageSize, params SqlParameter[] parameters)
        {
            return DbHelperSQL.ExecuteSqlPager(GetDataSql, OrderField, pageIndex, pageSize,parameters);
        }
		#endregion  ExtensionMethod
	}
}

