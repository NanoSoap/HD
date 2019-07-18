using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YDCode;

namespace HDBusiness
{
    public class baseBusiness
    {
        public int update(Dictionary<string, string> dic,string strTablename, string strfieldname, string strfieldvalue)
        {
            return DBSQL.ExecutesqlTobool(createSql.updsql(dic, strTablename, strfieldname, strfieldvalue, true), DBSQL.connstr);
        }

        public int updatebycondition(Dictionary<string, string> dic,string strTablename, string strcondition)
        {
            return DBSQL.ExecutesqlTobool(createSql.updsqlbycondition(dic, strTablename, strcondition), DBSQL.connstr);
        }

        public int updateMutrli(List<Dictionary<string, string>> diclist, string strTablename,string strfieldname)
        {
            return DBSQL.ExecutesqlMutriTobool(createSql.getupdatesqlarray(strTablename, diclist, strfieldname, true), DBSQL.connstr);
        }

        public int add(Dictionary<string, string> dic,string strTablename)
        {
            return DBSQL.ExecutesqlTobool(createSql.InsertSql(dic, strTablename), DBSQL.connstr);
        }

        public int addMutri(List<Dictionary<string, string>> diclist,string strTablename)
        {
            return DBSQL.ExecutesqlMutriTobool(createSql.getinsrtsqlarray(strTablename, diclist), DBSQL.connstr);
        }

        public int delete(string strTablename, string strfieldname, string strfieldvalue)
        {
            return DBSQL.ExecutesqlTobool(createSql.DeleteDatasql(strTablename, strfieldname, strfieldvalue), DBSQL.connstr);
        }

        public int deletebycondition(string strTablename, Dictionary<string, string> dic)
        {
            return DBSQL.ExecutesqlTobool(createSql.DeleteDatasqlbycondition(strTablename,dic), DBSQL.connstr);
        }

        public int deleteMutri(string strTablename, List<Dictionary<string, string>> listdic)
        {
            return DBSQL.ExecutesqlMutriTobool(createSql.getdeletesqlarray(strTablename, listdic), DBSQL.connstr);
        }

        public int deleteMutri(List<Dictionary<string, string>> listdic,List<string> listTablename)
        {
            string[] sqltext = new string[listTablename.Count];

            for(int i= 0;i < listTablename.Count;i++)
            {
                sqltext[i]= createSql.DeleteDatasqlbycondition(listTablename[i].ToString(), listdic[i]);
            }

            return DBSQL.ExecutesqlMutriTobool(sqltext, DBSQL.connstr);
        }

        public string isExistdata(string strTablename, string strfieldname, string strfieldnamevalue, string strresult)
        {
            return DBSQL.ExecutesqlTostring(createSql.isExistsdata(strTablename, strfieldname, strfieldnamevalue, strresult), DBSQL.connstr);
        }

        public string isExistdata(string strTablename, Dictionary<string, string> dic, string strresult)
        {
            return DBSQL.ExecutesqlTostring(createSql.isExistsdatabycondition(strTablename, dic, strresult), DBSQL.connstr);
        }

        public int ExecMutri(string[] sqltext)
        {
            return DBSQL.ExecutesqlMutriTobool(sqltext, DBSQL.connstr);
        }
    }
}
