using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;
namespace lifesense.DAL
{
	/// <summary>
	/// 数据访问类:t_sleepinfo
	/// </summary>
	public partial class t_sleepinfo
	{
		public t_sleepinfo()
		{}
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from t_sleepinfo");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(lifesense.Model.t_sleepinfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into t_sleepinfo(");
            strSql.Append("UserID,SleepingTime,WakingTime,LongSleepNum,ShallowSleepNum,WakeUpLong,WakingNum)");
            strSql.Append(" values (");
            strSql.Append("@UserID,@SleepingTime,@WakingTime,@LongSleepNum,@ShallowSleepNum,@WakeUpLong,@WakingNum)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.NVarChar,100),
					new SqlParameter("@SleepingTime", SqlDbType.DateTime),
					new SqlParameter("@WakingTime", SqlDbType.DateTime),
					new SqlParameter("@LongSleepNum", SqlDbType.Decimal,9),
					new SqlParameter("@ShallowSleepNum", SqlDbType.Decimal,9),
					new SqlParameter("@WakeUpLong", SqlDbType.Decimal,9),
					new SqlParameter("@WakingNum", SqlDbType.Int,4)};
            parameters[0].Value = model.UserID;
            parameters[1].Value = model.SleepingTime;
            parameters[2].Value = model.WakingTime;
            parameters[3].Value = model.LongSleepNum;
            parameters[4].Value = model.ShallowSleepNum;
            parameters[5].Value = model.WakeUpLong;
            parameters[6].Value = model.WakingNum;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
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
        public bool Update(lifesense.Model.t_sleepinfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update t_sleepinfo set ");
            strSql.Append("UserID=@UserID,");
            strSql.Append("SleepingTime=@SleepingTime,");
            strSql.Append("WakingTime=@WakingTime,");
            strSql.Append("LongSleepNum=@LongSleepNum,");
            strSql.Append("ShallowSleepNum=@ShallowSleepNum,");
            strSql.Append("WakeUpLong=@WakeUpLong,");
            strSql.Append("WakingNum=@WakingNum");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.NVarChar,100),
					new SqlParameter("@SleepingTime", SqlDbType.DateTime),
					new SqlParameter("@WakingTime", SqlDbType.DateTime),
					new SqlParameter("@LongSleepNum", SqlDbType.Decimal,9),
					new SqlParameter("@ShallowSleepNum", SqlDbType.Decimal,9),
					new SqlParameter("@WakeUpLong", SqlDbType.Decimal,9),
					new SqlParameter("@WakingNum", SqlDbType.Int,4),
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.UserID;
            parameters[1].Value = model.SleepingTime;
            parameters[2].Value = model.WakingTime;
            parameters[3].Value = model.LongSleepNum;
            parameters[4].Value = model.ShallowSleepNum;
            parameters[5].Value = model.WakeUpLong;
            parameters[6].Value = model.WakingNum;
            parameters[7].Value = model.ID;

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
        public bool Delete(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from t_sleepinfo ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

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
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from t_sleepinfo ");
            strSql.Append(" where ID in (" + IDlist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
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
        public lifesense.Model.t_sleepinfo GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,UserID,SleepingTime,WakingTime,LongSleepNum,ShallowSleepNum,WakeUpLong,WakingNum from t_sleepinfo ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

            lifesense.Model.t_sleepinfo model = new lifesense.Model.t_sleepinfo();
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
        public lifesense.Model.t_sleepinfo DataRowToModel(DataRow row)
        {
            lifesense.Model.t_sleepinfo model = new lifesense.Model.t_sleepinfo();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["UserID"] != null)
                {
                    model.UserID = row["UserID"].ToString();
                }
                if (row["SleepingTime"] != null && row["SleepingTime"].ToString() != "")
                {
                    model.SleepingTime = DateTime.Parse(row["SleepingTime"].ToString());
                }
                if (row["WakingTime"] != null && row["WakingTime"].ToString() != "")
                {
                    model.WakingTime = DateTime.Parse(row["WakingTime"].ToString());
                }
                if (row["LongSleepNum"] != null && row["LongSleepNum"].ToString() != "")
                {
                    model.LongSleepNum = decimal.Parse(row["LongSleepNum"].ToString());
                }
                if (row["ShallowSleepNum"] != null && row["ShallowSleepNum"].ToString() != "")
                {
                    model.ShallowSleepNum = decimal.Parse(row["ShallowSleepNum"].ToString());
                }
                if (row["WakeUpLong"] != null && row["WakeUpLong"].ToString() != "")
                {
                    model.WakeUpLong = decimal.Parse(row["WakeUpLong"].ToString());
                }
                if (row["WakingNum"] != null && row["WakingNum"].ToString() != "")
                {
                    model.WakingNum = int.Parse(row["WakingNum"].ToString());
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
            strSql.Append("select ID,UserID,SleepingTime,WakingTime,LongSleepNum,ShallowSleepNum,WakeUpLong,WakingNum ");
            strSql.Append(" FROM t_sleepinfo ");
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
            strSql.Append(" ID,UserID,SleepingTime,WakingTime,LongSleepNum,ShallowSleepNum,WakeUpLong,WakingNum ");
            strSql.Append(" FROM t_sleepinfo ");
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
            strSql.Append("select count(1) FROM t_sleepinfo ");
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
                strSql.Append("order by T.ID desc");
            }
            strSql.Append(")AS Row, T.*  from t_sleepinfo T ");
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
            parameters[0].Value = "t_sleepinfo";
            parameters[1].Value = "ID";
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

