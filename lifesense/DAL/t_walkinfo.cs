using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;
namespace lifesense.DAL
{
	/// <summary>
	/// 数据访问类:t_walkinfo
	/// </summary>
	public partial class t_walkinfo
	{
		public t_walkinfo()
		{}
        #region  BasicMethod


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(lifesense.Model.t_walkinfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into t_walkinfo(");
            strSql.Append("UserID,MeasureTime,StepNum,Calorie,Mileage)");
            strSql.Append(" values (");
            strSql.Append("@UserID,@MeasureTime,@StepNum,@Calorie,@Mileage)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.VarChar,50),
					new SqlParameter("@MeasureTime", SqlDbType.DateTime),
					new SqlParameter("@StepNum", SqlDbType.Decimal,9),
					new SqlParameter("@Calorie", SqlDbType.Decimal,9),
					new SqlParameter("@Mileage", SqlDbType.Decimal,9)};
            parameters[0].Value = model.UserID;
            parameters[1].Value = model.MeasureTime;
            parameters[2].Value = model.StepNum;
            parameters[3].Value = model.Calorie;
            parameters[4].Value = model.Mileage;

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
        public bool Update(lifesense.Model.t_walkinfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update t_walkinfo set ");
            strSql.Append("UserID=@UserID,");
            strSql.Append("MeasureTime=@MeasureTime,");
            strSql.Append("StepNum=@StepNum,");
            strSql.Append("Calorie=@Calorie,");
            strSql.Append("Mileage=@Mileage");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.VarChar,50),
					new SqlParameter("@MeasureTime", SqlDbType.DateTime),
					new SqlParameter("@StepNum", SqlDbType.Decimal,9),
					new SqlParameter("@Calorie", SqlDbType.Decimal,9),
					new SqlParameter("@Mileage", SqlDbType.Decimal,9),
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.UserID;
            parameters[1].Value = model.MeasureTime;
            parameters[2].Value = model.StepNum;
            parameters[3].Value = model.Calorie;
            parameters[4].Value = model.Mileage;
            parameters[5].Value = model.ID;

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
            strSql.Append("delete from t_walkinfo ");
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
            strSql.Append("delete from t_walkinfo ");
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
        public lifesense.Model.t_walkinfo GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,UserID,MeasureTime,StepNum,Calorie,Mileage from t_walkinfo ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

            lifesense.Model.t_walkinfo model = new lifesense.Model.t_walkinfo();
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
        public lifesense.Model.t_walkinfo DataRowToModel(DataRow row)
        {
            lifesense.Model.t_walkinfo model = new lifesense.Model.t_walkinfo();
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
                if (row["MeasureTime"] != null && row["MeasureTime"].ToString() != "")
                {
                    model.MeasureTime = DateTime.Parse(row["MeasureTime"].ToString());
                }
                if (row["StepNum"] != null && row["StepNum"].ToString() != "")
                {
                    model.StepNum = decimal.Parse(row["StepNum"].ToString());
                }
                if (row["Calorie"] != null && row["Calorie"].ToString() != "")
                {
                    model.Calorie = decimal.Parse(row["Calorie"].ToString());
                }
                if (row["Mileage"] != null && row["Mileage"].ToString() != "")
                {
                    model.Mileage = decimal.Parse(row["Mileage"].ToString());
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
            strSql.Append("select ID,UserID,MeasureTime,StepNum,Calorie,Mileage ");
            strSql.Append(" FROM t_walkinfo ");
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
            strSql.Append(" ID,UserID,MeasureTime,StepNum,Calorie,Mileage ");
            strSql.Append(" FROM t_walkinfo ");
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
            strSql.Append("select count(1) FROM t_walkinfo ");
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
            strSql.Append(")AS Row, T.*  from t_walkinfo T ");
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
            parameters[0].Value = "t_walkinfo";
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

