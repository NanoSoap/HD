using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Data;

namespace YDCode
{
    /// <summary>
    ///基础简化sql语句组合
    /// </summary>
    public class createSql
    {
        #region 构造函数
        public createSql()
        {
        }
        #endregion

        #region 创建sql基础语句

        public static string getAlldatByID(string tablename,string strID)
        {
            string sqltext = "select * from " + tablename + " where cast(ID as varchar(36))='" + strID + "' ";

            return sqltext;
        }

        public static string isExistsdata(string tablename,string fieldname,string fieldvalue,string strresult)
        {
            string sqltext = "select "+ strresult + " from " + tablename + " where "+fieldname+"='" + fieldvalue + "' ";

            return sqltext;
        }

        public static string isExistsdatabycondition(string tablename, Dictionary<string, string> dic,string strresult)
        {
            string sqltext = "";
            int flag = 0;

            if (dic != null)
            {
                sqltext = "select " + strresult + " from " + tablename + " where ";

                foreach (KeyValuePair<string, string> kv in dic)
                {
                    sqltext += kv.Value.ToString().Trim();

                    if (flag < dic.Keys.Count - 1)
                    {
                        sqltext += " and ";
                        flag++;
                    }
                }
            }

            return sqltext;
        }

        public static string[] CreatetableSql(string tablename)
        {
            string[] sqltext =new string[3];

            sqltext[0] = "CREATE TABLE HDSC_MetaData.[dbo].[" + tablename + "] ([guid] [nvarchar](50) NOT NULL," +
	                "[text] [nchar](10) NOT NULL,[code] [nchar](10) NOT NULL,"+
	                "[delid] [bit] NOT NULL) ON [PRIMARY]";

            sqltext[1] = "ALTER TABLE HDSC_MetaData.[dbo].["+tablename+"] ADD  CONSTRAINT [DF_" + tablename + "_guid]  DEFAULT (newid()) FOR [guid]";

            sqltext[2] = "ALTER TABLE HDSC_MetaData.[dbo].["+tablename+"] ADD  CONSTRAINT [DF_" + tablename + "_delid]  DEFAULT ((1)) FOR [delid]";

            return sqltext;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="Has">数据值对hastable</param>
        /// <param name="tablename">插入表名</param>
        /// <returns></returns>
        public static string InsertSql(Dictionary<string,string> Has,string tablename)
        {
            string sqltext = "";
            string str = "";

            if (Has != null)
            {
                sqltext = "Insert " + tablename + " (";

                string strValue = "";

                foreach (KeyValuePair<string,string> d in Has)
                {///处理单引号问题
                    sqltext += d.Key.ToString() + ",";
                    str = d.Value.ToString();
                    str = str.Replace("'", "''");
                    strValue += "'" + str + "',";
                }

                sqltext = sqltext.Substring(0, sqltext.Length - 1) + ") values (";

                sqltext += strValue.Substring(0, strValue.Length - 1) + ")";
            }

            return sqltext;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="Has">数据值对hastable</param>
        /// <param name="tablename">插入表名</param>
        /// <returns></returns>
        public static string InsertOutputfieldSql(Dictionary<string, string> Has, string tablename,string returnfieldname)
        {
            string sqltext = "";
            string str = "";

            if (Has != null)
            {
                sqltext = "Insert " + tablename + " (";

                string strValue = "";

                foreach (KeyValuePair<string, string> d in Has)
                {///处理单引号问题
                    sqltext += d.Key.ToString() + ",";
                    str = d.Value.ToString();
                    str = str.Replace("'", "''");
                    strValue += "'" + str + "',";
                }

                sqltext = sqltext.Substring(0, sqltext.Length - 1) + ") OUTPUT INSERTED." + returnfieldname + " values (";

                sqltext += strValue.Substring(0, strValue.Length - 1) + ")";
            }

            return sqltext;
        }

        /// <summary>
        /// 插入数据的语句返回guid值
        /// </summary>
        /// <param name="Has">数据值对hastable</param>
        /// <param name="tablename">插入表名</param>
        /// <returns></returns>
        public static string InsertoutputSql(Dictionary<string, string> Has, string tablename)
        {
            string sqltext = "";
            string str = "";

            if (Has != null)
            {
                sqltext = "Insert " + tablename + " (";

                string strValue = "";

                foreach (KeyValuePair<string, string> d in Has)
                {///处理单引号问题
                    sqltext += d.Key.ToString() + ",";
                    str = d.Value.ToString();
                    str = str.Replace("'", "''");
                    strValue += "'" + str + "',";
                }

                sqltext = sqltext.Substring(0, sqltext.Length - 1) + ") OUTPUT INSERTED.guid values (";

                sqltext += strValue.Substring(0, strValue.Length - 1) + ")";
            }

            return sqltext;
        }

        /// <summary>
        /// 依据标识符进行更新
        /// </summary>
        /// <param name="Has"></param>
        /// <param name="strcode">标识符</param>
        /// <param name="tablename">目标表</param>
        /// <returns></returns>
        public static string updsql(Dictionary<string, string> Has, string tablename, string strfieldname, string strfieldvalue,bool isGUID)
        {
            string sqltext = "";
            string str = "";

            if (Has != null && strfieldvalue != "")
            {
                sqltext = "Update " + tablename + " Set ";

                string strValue = "";

                foreach (KeyValuePair<string, string> d in Has)
                {
                    if (d.Key.ToString().Trim() != strfieldname.Trim())
                    {
                        str = d.Value.ToString();
                        str = str.Replace("'", "''");
                        strValue += d.Key.ToString() + "='" + str + "',";
                    }
                }

                if(isGUID)
                {
                    strfieldname = "cast(" + strfieldname + " as varchar(36))";
                }

                sqltext = sqltext + strValue.Substring(0, strValue.Length - 1) + " " +
                        " where "+strfieldname+"='" + strfieldvalue.Trim() + "' ";
            }

            return sqltext;
        }

        public static string updsqlbycondition(Dictionary<string, string> Has, string tablename, string strcondition)
        {
            string sqltext = "";
            string str = "";

            if (Has != null && strcondition != "")
            {
                sqltext = "Update " + tablename + " Set ";

                string strValue = "";

                foreach (KeyValuePair<string, string> d in Has)
                {
                        str = d.Value.ToString();
                        str = str.Replace("'", "''");
                        strValue += d.Key.ToString() + "='" + str + "',";
                }

                sqltext = sqltext + strValue.Substring(0, strValue.Length - 1) + " " +
                        " where " + strcondition + "' ";
            }

            return sqltext;
        }

        /// <summary>
        /// 依据关键字段进行删除
        /// </summary>
        /// <param name="strfieldvalue"></param>
        /// <param name="tablename"></param>
        /// <param name="strfieldname">字段名</param>
        /// <returns></returns>
        public static string DeleteDatasql(string tablename, string strfieldname, string strfieldvalue)
        {
            string sqltext = "delete from " + tablename + " where " + strfieldname + "='" + strfieldvalue + "' ";

            return sqltext;
        }

        /// <summary>
        /// 依据关键字段进行删除
        /// </summary>
        /// <param name="strfieldvalue"></param>
        /// <param name="tablename"></param>
        /// <param name="strfieldname">字段名</param>
        /// <returns></returns>
        public static string DeleteOutputDatasql(string tablename, string strfieldname, string strfieldvalue)
        {
            string sqltext = "delete from " + tablename + " OUTPUT deleted.guid where " + strfieldname + "='" + strfieldvalue + "' ";

            return sqltext;
        }

        //单表更新插入,删除指定条件数据，新插入
        public static string[] geteditsqlarray(string strtablename, string strfieldname, string strfieldvalue,
                string[] strfields,System.Data.DataTable insdt)
        {
            System.Collections.ArrayList sqltextlist = new System.Collections.ArrayList();
            Dictionary<string, string> has = new Dictionary<string, string>();

            foreach (DataRow r in insdt.Rows)
            {
                has.Clear();

                for (int i = 0; i < strfields.Length; i++)
                {
                    has.Add(strfields[i], r[strfields[i]].ToString().Trim());
                }

                sqltextlist.Add(InsertSql(has, strtablename));
            }

            string[] sqlarray = new string[sqltextlist.Count+1];

            sqlarray[0] = DeleteDatasql(strtablename, strfieldname, strfieldvalue);

            for (int j = 1; j < sqltextlist.Count+1; j++)
            {
                sqlarray[j] = sqltextlist[j-1].ToString();
            }

            return sqlarray;
        }

        //单表更新,只生产批量插入语句
        public static string[] getinsrtsqlarray(string strtablename,string[] strfields, System.Data.DataTable insdt)
        {
            System.Collections.ArrayList sqltextlist = new System.Collections.ArrayList();
            Dictionary<string, string> has = new Dictionary<string, string>();

            foreach (DataRow r in insdt.Rows)
            {
                has.Clear();

                for (int i = 0; i < strfields.Length; i++)
                {
                    has.Add(strfields[i], r[strfields[i]].ToString().Trim());
                }

                sqltextlist.Add(InsertSql(has, strtablename));
            }

            string[] sqlarray = new string[sqltextlist.Count];

            for (int j = 0; j < sqltextlist.Count ; j++)
            {
                sqlarray[j] = sqltextlist[j].ToString();
            }

            return sqlarray;
        }

        public static string[] getinsrtsqlarray(string strtablename, List<Dictionary<string, string>> listdic)
        {
            string[] sqlarray = null;
            int i = 0;

            if (listdic != null)
            {
                sqlarray = new string[listdic.Count];

                foreach (Dictionary<string, string> dic in listdic)
                {
                    sqlarray[i] = InsertSql(dic, strtablename);
                    i++;
                }
            }

            return sqlarray;
        }

        public static string[] getupdatesqlarray(string strtablename, List<Dictionary<string, string>> listdic, string fieldname, bool isGUID)
        {
            string[] sqlarray = null;
            int i = 0;

            if (listdic != null)
            {
                sqlarray = new string[listdic.Count];

                foreach (Dictionary<string, string> dic in listdic)
                {
                    sqlarray[i] = updsql(dic, strtablename, fieldname, dic[fieldname].ToString().Trim(), isGUID);
                    i++;
                }
            }

            return sqlarray;
        }

        public static string DeleteDatasql(string tablename, Dictionary<string, string> dic)
        {
            string sqltext = "";
            int flag = 0;

            if (dic != null)
            {
                sqltext = "delete from " + tablename + " where ";

                foreach(KeyValuePair<string,string> kv in dic)
                {
                    sqltext +=kv.Key.ToString().Trim()+"='"+kv.Value.ToString().Trim()+"' ";

                    if (flag < dic.Keys.Count-1)
                    {
                        sqltext += " and ";
                        flag++;
                    }
                }
            }

            return sqltext;
        }

        public static string DeleteDatasqlbycondition(string tablename, Dictionary<string,string> dic)
        {
            string sqltext = "";
            int flag = 0;

            if (dic != null)
            {
                sqltext = "delete from " + tablename + " where ";

                foreach (KeyValuePair<string, string> kv in dic)
                {
                    sqltext += kv.Value.ToString().Trim();

                    if (flag < dic.Keys.Count - 1)
                    {
                        sqltext += " and ";
                        flag++;
                    }
                }
            }

            return sqltext;
        }

        //依据传入数据批量生成删除语句
        public static string[] getdeletesqlarray(string strtablename, List<Dictionary<string, string>> listdic)
        {
            string[] sqlarray=null;
            int i = 0;

            if (listdic != null)
            {
                sqlarray=new string[listdic.Count];

                foreach (Dictionary<string,string> dic in listdic)
                {
                    sqlarray[i] = DeleteDatasql(strtablename, dic);
                    i++;
                }
            }

            return sqlarray;
        }

        #endregion

        #region 防sql注入攻击检查

        // net user
        // xp_cmdshell
        // /add
        // exec master.dbo.xp_cmdshell
        // select
        // count
        // Asc
        // char
        // mid
        // '
        // :
        // insert
        // delete from
        // drop table
        // update
        // truncate
        // from
        // =


        public string[] sqlenum={ "net user","xp_cmdshell","/add","exec master.dbo.xp_cmdshell","select","count","Asc",
             "char","mid","'","insert","delete from","drop table","update","truncate","from","=" };

        /// <summary>
        /// 字符串校验
        /// </summary>
        /// <param name="strtext"></param>
        /// <returns></returns>
        public static string checktext(string[] strtext,string[] sqlenum)
        {

            foreach (string str in sqlenum)
            {
                foreach (string strcheck in strtext)
                {
                    if (strcheck.IndexOf(str) > -1)
                    {
                        return "警告：" + str + "非法sql字符出现！";
                    }
                }
            }

            return "OK";
        }
             
        #endregion
    }
}