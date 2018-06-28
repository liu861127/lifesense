using Maticsoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace lifesense.DAL
{
    /// <summary>
    /// 数据访问类:t_failrequestInfo
    /// </summary>
    public partial class t_failrequestInfo
    {
        public t_failrequestInfo()
        { }
        #region  BasicMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string UserID, DateTime WriteTime, string Url, string Reason)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from t_failrequestInfo");
            strSql.Append(" where UserID=@UserID and WriteTime=@WriteTime and Url=@Url and Reason=@Reason ");
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.NVarChar,50),
					new SqlParameter("@WriteTime", SqlDbType.Date,8),
					new SqlParameter("@Url", SqlDbType.NVarChar,50),
					new SqlParameter("@Reason", SqlDbType.NVarChar,500)			};
            parameters[0].Value = UserID;
            parameters[1].Value = WriteTime;
            parameters[2].Value = Url;
            parameters[3].Value = Reason;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(lifesense.Model.t_failrequestInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into t_failrequestInfo(");
            strSql.Append("UserID,WriteTime,Url,Reason)");
            strSql.Append(" values (");
            strSql.Append("@UserID,@WriteTime,@Url,@Reason)");
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.NVarChar,50),
					new SqlParameter("@WriteTime", SqlDbType.Date,8),
					new SqlParameter("@Url", SqlDbType.NVarChar,50),
					new SqlParameter("@Reason", SqlDbType.NVarChar,500)};
            parameters[0].Value = model.UserID;
            parameters[1].Value = model.WriteTime;
            parameters[2].Value = model.Url;
            parameters[3].Value = model.Reason;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
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
        /// 更新一条数据
        /// </summary>
        public bool Update(lifesense.Model.t_failrequestInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update t_failrequestInfo set ");
            strSql.Append("UserID=@UserID,");
            strSql.Append("WriteTime=@WriteTime,");
            strSql.Append("Url=@Url,");
            strSql.Append("Reason=@Reason");
            strSql.Append(" where UserID=@UserID and WriteTime=@WriteTime and Url=@Url and Reason=@Reason ");
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.NVarChar,50),
					new SqlParameter("@WriteTime", SqlDbType.Date,8),
					new SqlParameter("@Url", SqlDbType.NVarChar,50),
					new SqlParameter("@Reason", SqlDbType.NVarChar,500)};
            parameters[0].Value = model.UserID;
            parameters[1].Value = model.WriteTime;
            parameters[2].Value = model.Url;
            parameters[3].Value = model.Reason;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
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
        public bool Delete(string UserID, DateTime WriteTime, string Url, string Reason)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from t_failrequestInfo ");
            strSql.Append(" where UserID=@UserID and WriteTime=@WriteTime and Url=@Url and Reason=@Reason ");
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.NVarChar,50),
					new SqlParameter("@WriteTime", SqlDbType.Date,8),
					new SqlParameter("@Url", SqlDbType.NVarChar,50),
					new SqlParameter("@Reason", SqlDbType.NVarChar,500)			};
            parameters[0].Value = UserID;
            parameters[1].Value = WriteTime;
            parameters[2].Value = Url;
            parameters[3].Value = Reason;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
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
        public lifesense.Model.t_failrequestInfo GetModel(string UserID, DateTime WriteTime, string Url, string Reason)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 UserID,WriteTime,Url,Reason from t_failrequestInfo ");
            strSql.Append(" where UserID=@UserID and WriteTime=@WriteTime and Url=@Url and Reason=@Reason ");
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.NVarChar,50),
					new SqlParameter("@WriteTime", SqlDbType.Date,8),
					new SqlParameter("@Url", SqlDbType.NVarChar,50),
					new SqlParameter("@Reason", SqlDbType.NVarChar,500)			};
            parameters[0].Value = UserID;
            parameters[1].Value = WriteTime;
            parameters[2].Value = Url;
            parameters[3].Value = Reason;

            lifesense.Model.t_failrequestInfo model = new lifesense.Model.t_failrequestInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
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
        public lifesense.Model.t_failrequestInfo DataRowToModel(DataRow row)
        {
            lifesense.Model.t_failrequestInfo model = new lifesense.Model.t_failrequestInfo();
            if (row != null)
            {
                if (row["UserID"] != null)
                {
                    model.UserID = row["UserID"].ToString();
                }
                if (row["WriteTime"] != null && row["WriteTime"].ToString() != "")
                {
                    model.WriteTime = DateTime.Parse(row["WriteTime"].ToString());
                }
                if (row["Url"] != null)
                {
                    model.Url = row["Url"].ToString();
                }
                if (row["Reason"] != null)
                {
                    model.Reason = row["Reason"].ToString();
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select UserID,WriteTime,Url,Reason ");
            strSql.Append(" FROM t_failrequestInfo ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" UserID,WriteTime,Url,Reason ");
            strSql.Append(" FROM t_failrequestInfo ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM t_failrequestInfo ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
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
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.Reason desc");
            }
            strSql.Append(")AS Row, T.*  from t_failrequestInfo T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /*
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
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
            parameters[0].Value = "t_failrequestInfo";
            parameters[1].Value = "Reason";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  BasicMethod
        #region  ExtensionMethod

        #endregion  ExtensionMethod
    }
}
