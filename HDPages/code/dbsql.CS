using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Configuration;

namespace YDCode
{
    /// <summary>
    ///数据库访问静态基础类
    ///copyright (c) 2013 yksoft
    /// </summary>
    public static class DBSQL
    {
        #region 初始化连接字符串

        public static string connstr = System.Configuration.ConfigurationManager.ConnectionStrings["HDPMWDbConntion"].ToString();

        #endregion

        #region 执行通用sql语句

        /// 返回影响的记录数量
        /// <param name="sqltext">sql语句</param>
        /// <returns>影响的记录数</returns>
        public static int ExecutesqlTointrow(string sqltext,string connstr)
        {
            int rows = -1;

            using (SqlConnection conn = new SqlConnection(connstr))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    using (SqlCommand cmd = new SqlCommand(sqltext, conn))
                    {
                        try
                        {
                            cmd.Transaction = trans;
                            cmd.CommandTimeout = 30;
                            rows = cmd.ExecuteNonQuery();
                            trans.Commit();
                        }
                        catch (SqlException ex)
                        {
                            trans.Rollback();
                            throw ex;
                        }
                        finally
                        {
                            if (conn.State.ToString() != "Closed")
                            {
                                conn.Close();
                            }
                        }
                    }
                }
            }

            return rows;
        }

        /// 返回影响的记录数量
        /// <param name="sqltext">sql语句</param>
        /// <returns>影响的记录数</returns>
        /// <param name="times">时间长度</param>
        public static int ExecutesqlTointrowBytime(string sqltext, int times,string connstr)
        {
            int rows = -1;

            using (SqlConnection conn = new SqlConnection(connstr))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    using (SqlCommand cmd = new SqlCommand(sqltext, conn))
                    {
                        try
                        {
                            cmd.Transaction = trans;
                            cmd.CommandTimeout = times;
                            rows = cmd.ExecuteNonQuery();
                            trans.Commit();
                        }
                        catch (SqlException ex)
                        {
                            trans.Rollback();
                            throw ex;
                        }
                        finally
                        {
                            if (conn.State.ToString() != "Closed")
                            {
                                conn.Close();
                            }
                        }
                    }
                }
            }

            return rows;
        }

        /// <summary>
        /// 获取当前插入key,该语句最好与插入语句同时执行
        /// IDENT_CURRENT 返回为某个会话和用域中的指定表生成的最新标识值。
        /// @@IDENTITY 返回为跨所有作用域的当前会话中的某个表生成的最新标识值。
        /// SCOPE_IDENTITY 返回为当前会话和当前作用域中的某个表生成的最新标识值。
        /// </summary>
        /// <returns></returns>
        public static int ExecutesqlReturnkey(string sqltext,string strtablename,string connstr)
        {
            int seed = -1;

            using (SqlConnection conn = new SqlConnection(connstr))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    using (SqlCommand cmd = new SqlCommand(sqltext, conn))
                    {
                        try
                        {
                            cmd.Transaction = trans;
                            cmd.CommandTimeout = 15;

                            if (cmd.ExecuteNonQuery() > -1)
                            {
                                SqlCommand cmd_re = new SqlCommand("select IDENT_CURRENT('" + strtablename + "') AS Current_Identity;", conn);
                                cmd_re.Transaction = trans;
                                cmd_re.CommandTimeout = 15;

                                object obj = cmd_re.ExecuteScalar();
                                if (!(Object.Equals(obj, null) || Object.Equals(obj, System.DBNull.Value))
                                    && System.Int32.TryParse(obj.ToString(), out seed))
                                {
                                    seed = System.Convert.ToInt32(obj);
                                }
                            }

                            trans.Commit();
                        }
                        catch (SqlException ex)
                        {
                            trans.Rollback();
                            throw ex;
                        }
                        finally
                        {
                            if (conn.State.ToString() != "Closed")
                            {
                                conn.Close();
                            }
                        }
                    }
                }
            }

            return seed;
        }

        /// 按顺序执行多条sql语句
        /// <param name="sqltext">sql语句数组</param>
        /// <returns>返回是否全部成功执行</returns>
        /// 备注：多条语句可以合并用分号分开
        public static int ExecutesqlMutriTobool(string[] sqltext,string connstr)
        {
            int row=0;

            using (SqlConnection conn = new SqlConnection(connstr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                SqlTransaction trans = conn.BeginTransaction();
                cmd.Transaction = trans;

                try
                {
                    for (int i = 0; i < sqltext.Length; i++)
                    {
                        if (sqltext[i] != null && sqltext[i].Trim() != "")
                        {
                            cmd.CommandText = sqltext[i].Trim();
                            row+=cmd.ExecuteNonQuery();

                            if (row < 1)
                            {//
                                row = 1;
                            }
                        }
                    }
                    trans.Commit();
                }
                catch(SqlException ex)
                {
                    trans.Rollback();
                    throw ex;
                }
                finally
                {
                    if (conn.State.ToString() != "Closed")
                    {
                        conn.Close();
                    }
                }
            }

            return row;
        }

        /// <param name="sqltext">sql语句</param>
        /// <returns>返回字符串类型值,空字符表示没找到或空值</returns>
        public static string ExecutesqlTostring(string sqltext,string connstr)
        {
            string str = "";

            using (SqlConnection conn = new SqlConnection(connstr))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    using (SqlCommand cmd = new SqlCommand(sqltext, conn))
                    {
                        try
                        {
                            cmd.Transaction = trans;
                            cmd.CommandTimeout = 15;
                            object obj = cmd.ExecuteScalar();
                            if (!(Object.Equals(obj,null) || Object.Equals(obj,System.DBNull.Value)))
                            {
                                str = obj.ToString().Trim();
                            }

                            trans.Commit();
                        }
                        catch (SqlException ex)
                        {
                            trans.Rollback();
                            throw ex;
                        }
                        finally
                        {
                            if (conn.State.ToString() != "Closed")
                            {
                                conn.Close();
                            }
                        }
                    }
                }
            }

            return str;
        }

        /// <param name="sqltext">sql语句</param>
        /// <returns>返回整型值,-1表示没找到</returns>
        public static int ExecutesqlToint(string sqltext,string connstr)
        {
            int count =-1;

            using (SqlConnection conn = new SqlConnection(connstr))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    using (SqlCommand cmd = new SqlCommand(sqltext, conn))
                    {
                        try
                        {
                            cmd.Transaction = trans;
                            cmd.CommandTimeout = 15;
                            object obj = cmd.ExecuteScalar();
                            if (!(Object.Equals(obj, null) || Object.Equals(obj, System.DBNull.Value)) 
                                && System.Int32.TryParse(obj.ToString(),out count))
                            {
                                count=System.Convert.ToInt32(obj);
                            }

                            trans.Commit();
                        }
                        catch (SqlException ex)
                        {
                            trans.Rollback();
                            throw ex;
                        }
                        finally
                        {
                            if (conn.State.ToString() != "Closed")
                            {
                                conn.Close();
                            }
                        }
                    }
                }
            }

            return count;
        }

        /// <param name="sqltext">sql语句</param>
        /// <returns>返回对象,默认null</returns>
        public static object ExecutesqlToobj(string sqltext,string connstr)
        {
            object obj = null;

            using (SqlConnection conn = new SqlConnection(connstr))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    using (SqlCommand cmd = new SqlCommand(sqltext, conn))
                    {
                        try
                        {
                            cmd.Transaction = trans;
                            cmd.CommandTimeout = 15;
                            obj = cmd.ExecuteScalar();
 
                            trans.Commit();
                        }
                        catch (SqlException ex)
                        {
                            trans.Rollback();
                            throw ex;
                        }
                        finally
                        {
                            if (conn.State.ToString() != "Closed")
                            {
                                conn.Close();
                            }
                        }
                    }
                }
            }

            return obj;
        }

        /// <param name="sqltext">sql语句</param>
        /// <returns>返回对象单条语句执行是否成功</returns>
        public static int ExecutesqlTobool(string sqltext,string connstr)
        {
            int row = 0;

            using (SqlConnection conn = new SqlConnection(connstr))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    using (SqlCommand cmd = new SqlCommand(sqltext, conn))
                    {
                        try
                        {
                            cmd.Transaction = trans;
                            cmd.CommandTimeout = 15;

                            row = cmd.ExecuteNonQuery();

                            trans.Commit();
                        }
                        catch (SqlException ex)
                        {
                            trans.Rollback();
                            throw ex;
                        }
                        finally
                        {
                            if (conn.State.ToString() != "Closed")
                            {
                                conn.Close();
                            }
                        }
                    }
                }
            }

            return row;
        }

        /// <param name="sqltext">sql语句</param>
        /// <returns>返回数据结果集</returns>
        public static DataSet ExecutesqlTods(string sqltext,string connstr)
        {
            DataSet ds = new DataSet();

            using (SqlConnection conn = new SqlConnection(connstr))
            {
                conn.Open();

                try
                {
                    SqlDataAdapter sda = new SqlDataAdapter(sqltext, conn);

                    sda.Fill(ds);
                }
                catch (SqlException ex)
                {

                    throw new Exception(ex.Message);
                }
                finally
                {
                    if (conn.State.ToString() != "Closed")
                    {
                        conn.Close();
                    }
                }
                    
            }

            return ds;
        }

        /// <param name="sqltext">sql语句</param>
        /// <returns>返回数据表</returns>
        public static DataTable ExecutesqlTodt(string sqltext,string connstr)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(connstr))
            {
                conn.Open();

                try
                {
                    SqlDataAdapter sda = new SqlDataAdapter(sqltext, conn);

                    sda.Fill(dt);
                }
                catch (SqlException ex)
                {

                    throw new Exception(ex.Message);
                }
                finally
                {
                    if (conn.State.ToString() != "Closed")
                    {
                        conn.Close();
                    }
                }

            }

            return dt;
        }

        /// 带参数的sql语句，单参数
        /// <param name="sqltext">sql语句</param>
        /// <param name="sb">大字段参数，@filedata为sqltext参数</param>
        /// insert into table ("text") values (@filedata) 
        /// <returns>返回大字段参数是否写成功</returns>
        public static bool ExecuteBinarysqlTobool(byte[] sb, string sqltext,string connstr)
        {
            bool isok = false;

            System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(connstr);

            try
            {
                conn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(sqltext, conn);

                cmd.Parameters.Add("@filedata", SqlDbType.Binary, 8000, "text").Value = sb;

                if (cmd.ExecuteNonQuery() > -1)
                {
                    isok = true;
                }

            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                throw (ex);
            }
            finally
            {
                if (conn.State.ToString() != "Closed")
                {
                    conn.Close();
                }
            }

            return isok;
        }

        /// 带参数的sql语句，单参数
        /// <param name="sqltext">sql语句</param>
        /// <returns>返回大字段参数</returns>
        public static System.Data.SqlTypes.SqlBinary ExecuteBinarysqlToBinary(string sqltext,string connstr)
        {
            System.Data.SqlTypes.SqlBinary sb = new System.Data.SqlTypes.SqlBinary();

            System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(connstr);

            try
            {
                System.Data.SqlClient.SqlCommand scomm = new SqlCommand(sqltext, conn);

                conn.Open();

                System.Data.SqlClient.SqlDataReader sr = scomm.ExecuteReader();

                if (sr != null && sr.HasRows)
                {
                    sr.Read();
                    sb = sr.GetSqlBinary(0);
                }

                sr.Close();

            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                throw (ex);
            }
            finally
            {
                if (conn.State.ToString() != "Closed")
                {
                    conn.Close();
                }
            }
            return sb;
        }

        /// 执行带参数的sql语句，多参数
        /// <param name="sqltext">sql语句</param>
        /// <param name="cmdparams">参数数组</param>
        /// <returns>返回受影响的行数</returns>
        public static int ExecuteparamssqlToint(string sqltext,string connstr,params SqlParameter[] cmdparams)
        {
            int rows=-1;

            using (SqlConnection conn = new SqlConnection(connstr))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    using (SqlCommand cmd = new SqlCommand(sqltext, conn))
                    {
                        try
                        {
                            cmd.Transaction = trans;
                            cmd.CommandTimeout = 30;
                            PrepareCommand(cmd,conn,null,sqltext,cmdparams);
                            rows = cmd.ExecuteNonQuery();
                            trans.Commit();
                        }
                        catch (SqlException ex)
                        {
                            trans.Rollback();
                            throw ex;
                        }
                        finally
                        {
                            if (conn.State.ToString() != "Closed")
                            {
                                conn.Close();
                            }
                        }
                    }
                }
            }

            return rows;
        }

        #endregion
        
        #region 执行存储过程
        
        /// <summary>
        /// 执行存储过程,限时30秒
        /// </summary>
        /// <param name="strprocname">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="tablename">dataset结果表名</param>
        /// <returns></returns>
        public static DataSet ExecuteprocedureTods(string storedProcname, IDataParameter[] parameters, string tablename,string connstr)
        {
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                DataSet ds = new DataSet();
                conn.Open();

                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = BuildQueryCommand(conn, storedProcname, parameters);
                sda.SelectCommand.CommandTimeout = 30;

                sda.Fill(ds, tablename);

                conn.Close();

                return ds;
            }
        }

        /// <summary>
        /// 执行存储过程,限时30秒
        /// </summary>
        /// <param name="strprocname">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="tablename">dataset结果表名</param>
        /// <param name="startRecord">记录开始</param>
        /// <param name="recordCount">记录数</param>
        /// <returns></returns>
        public static DataSet ExecuteprocedureTods(string storedProcname, IDataParameter[] parameters, string tablename, int startRecord, int recordCount,string connstr)
        {
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                DataSet ds = new DataSet();
                conn.Open();

                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = BuildQueryCommand(conn, storedProcname, parameters);
                sda.SelectCommand.CommandTimeout = 30;

                sda.Fill(ds, startRecord, recordCount,tablename);

                conn.Close();

                return ds;
            }
        }

        /// <summary>
        /// 执行存储过程,限时30秒
        /// </summary>
        /// <param name="strprocname">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="rows">返回值</param>
        /// <returns>返回受影响行数</returns>
        public static int ExecuteprocedureTods(string storedProcname, IDataParameter[] parameters, out int rows,string connstr)
        {
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                int result;
                conn.Open();

                SqlCommand cmd = BuildCommand(conn, storedProcname, parameters);
                rows = cmd.ExecuteNonQuery();

                result = (int)cmd.Parameters["ReturnValue"].Value;
                conn.Close();

                return result;
            }
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 准备sql命令参数
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="conn"></param>
        /// <param name="trans"></param>
        /// <param name="cmdText"></param>
        /// <param name="cmdParms"></param>
        public static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, string cmdText, SqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = CommandType.Text;//cmdType;
            if (cmdParms != null)
            {


                foreach (SqlParameter parameter in cmdParms)
                {
                    if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&
                        (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    cmd.Parameters.Add(parameter);
                }
            }
        }

        /// <summary>
        /// 构建s返回一个sqlcommand对象（返回一个结果集，不是一个整数值）
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="storedProcname"></param>
        /// <param name="parameters"></param>
        /// <returns>sqlcommand</returns>
        public static SqlCommand BuildQueryCommand(SqlConnection conn, string storedProcname, IDataParameter[] parameters)
        {
            SqlCommand cmd = new SqlCommand(storedProcname, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            foreach (SqlParameter parameter in parameters)
            {
                if (parameter != null)
                {
                    if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.ReturnValue) ||
                        (parameter.Value==null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    cmd.Parameters.Add(parameter);
                }
            }
            return cmd;
        }
        
        /// <summary>
        /// 创建sqlcommand对象实例（返回一个整数值）
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="storedProcname"></param>
        /// <param name="parameters"></param>
        /// <returns>sqlcommand对象实例</returns>
        public static SqlCommand BuildCommand(SqlConnection conn, string storedProcname, IDataParameter[] parameters)
        {
            SqlCommand cmd = BuildQueryCommand(conn, storedProcname, parameters);
            cmd.Parameters.Add(new SqlParameter("ReturnValue",SqlDbType.Int,4,ParameterDirection.ReturnValue,false,0,0,string.Empty,DataRowVersion.Default,null));

            return cmd;
        }

        #endregion

    }

    #region 开窗函数

    //OVER 子句 (Transact-SQL) 确定在应用关联的开窗函数之前，行集的分区和排序。
    //    PARTITION BY
    //    将结果集分为多个分区。开窗函数分别应用于每个分区，并为每个分区重新启动计算。
    //SELECT SalesOrderID, ProductID, OrderQty
    //    ,SUM(OrderQty) OVER(PARTITION BY SalesOrderID) AS 'Total'
    //    ,AVG(OrderQty) OVER(PARTITION BY SalesOrderID) AS 'Avg'
    //    ,COUNT(OrderQty) OVER(PARTITION BY SalesOrderID) AS 'Count'
    //    ,MIN(OrderQty) OVER(PARTITION BY SalesOrderID) AS 'Min'
    //    ,MAX(OrderQty) OVER(PARTITION BY SalesOrderID) AS 'Max'
    //    FROM Sales.SalesOrderDetail 
    //    WHERE SalesOrderID IN(43659,43664);

    //递归 CTE 结构必须至少包含一个定位点成员和一个递归成员。以下伪代码显示了包含一个定位点成员和一个递归成员的简单递归 CTE 的组件。
    //WITH cte_name ( column_name [,...n] ) 
    //AS 
    //( 
    //CTE_query_definition –- Anchor member is defined. 
    //UNION ALL 
    //CTE_query_definition –- Recursive member is defined referencing cte_name. 
    //) 
    //-- Statement using the CTE 
    //SELECT * 
    //FROM cte_name     
    //递归执行的语义如下：
    //将 CTE 表达式拆分为定位点成员和递归成员。
    //运行定位点成员，创建第一个调用或基准结果集 (T0)。
    //运行递归成员，将 Ti 作为输入，将 Ti+1 作为输出。
    //重复步骤 3，直到返回空集。
    //返回结果集。这是对 T0 到 Tn 执行 UNION ALL 的结果。

    //WITH DirectReports (ManagerID, EmployeeID, Title, DeptID, Level)
    //AS
    //(
    //-- Anchor member definition
    //    SELECT e.ManagerID, e.EmployeeID, e.Title, edh.DepartmentID, 
    //        0 AS Level
    //    FROM HumanResources.Employee AS e
    //    INNER JOIN HumanResources.EmployeeDepartmentHistory AS edh
    //        ON e.EmployeeID = edh.EmployeeID AND edh.EndDate IS NULL
    //    WHERE ManagerID IS NULL
    //    UNION ALL
    //-- Recursive member definition
    //    SELECT e.ManagerID, e.EmployeeID, e.Title, edh.DepartmentID,
    //        Level + 1
    //    FROM HumanResources.Employee AS e
    //    INNER JOIN HumanResources.EmployeeDepartmentHistory AS edh
    //        ON e.EmployeeID = edh.EmployeeID AND edh.EndDate IS NULL
    //    INNER JOIN DirectReports AS d
    //        ON e.ManagerID = d.EmployeeID
    //)
    //-- Statement that executes the CTE
    //SELECT ManagerID, EmployeeID, Title, Level
    //FROM DirectReports
    //INNER JOIN HumanResources.Department AS dp
    //    ON DirectReports.DeptID = dp.DepartmentID
    //WHERE dp.GroupName = N'Research and Development' OR Level = 0;

    #endregion

    #region 新排序函数

    //RANK (Transact-SQL) 返回结果集的分区内每行的排名。行的排名是相关行之前的排名数加一。

    //DENSE_RANK (Transact-SQL) 返回结果集分区中行的排名，在排名中没有任何间断。行的排名等于所讨论行之前的所有排名数加一。

    //ROW_NUMBER (Transact-SQL) 返回结果集分区内行的序列号，每个分区的第一行从 1 开始。
    //    SELECT FirstName, LastName, ROW_NUMBER() OVER(PARTITION BY PostalCode ORDER BY SalesYTD DESC) AS 'Row Number', SalesYTD, PostalCode 
    //    FROM Sales.vSalesPerson
    //    WHERE TerritoryName IS NOT NULL AND SalesYTD <> 0;

    //NTILE (Transact-SQL) 将有序分区中的行分发到指定数目的组中。各个组有编号，编号从一开始。对于每一个行，NTILE 将返回此行所属的组的编号。

    #endregion

    #region LINQ to list

    //筛选
    //List<string> stcdList = stcdArray.ToList<string>()
    //            .FindAll(new Predicate<string>(stcd => stcd.Contains("stcd_")))
    //            .Select(stcd => stcd.Substring(5))
    //            .ToList<string>();

    //拼接
    //List<string> suggestion = new List<string>();
    //    string sg_font_details = suggestion.Aggregate((s1, s2) => string.Format("{0},{1}", s1, s2)).ToString();

    //分组统计
    //public class HomeStat
    //{
    //    public string adnm { get; set; }
    //    public string ProCount { get; set; }
    //    public string countCompare { get; set; }
    //    public string perCount { get; set; }
    //    public string dayCount { get; set; }
    //    public string perDayCount { get; set; }
    //}
    //List<HomeStat> chsList = new List<HomeStat>() {... };
    //var query = hsList.GroupBy(t => t.adnm).Select(p => new
    //                {
    //                    adnm = p.Key,
    //                    dayCount = p.Sum(x => double.Parse(x.dayCount)),
    //                    proCount = p.Select(x => x.projectID).Distinct().Count(),
    //                    perCount = p.Sum(x => double.Parse(x.perCount)),
    //                    perDayCount = p.Sum(x => double.Parse(x.perDayCount))
    //                });

    //分组拼接
    //public class Service
    //{
    //    public string Month { get; set; }
    //    public string Content { get; set; }
    //}
    //List<Service> serList = new List<Service>(){...};
    //var query = serList.GroupBy(t => t.Month).Select(g => g.Aggregate((s1, s2) =>
    //            new Service { Month = g.Key, Content = s1.Content + "," + s2.Content }))
    //            .OrderByDescending(t => t.Month);

    #endregion
}