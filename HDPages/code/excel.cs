using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;

namespace YDCode
{
    public class excel
    {
        /// <summary>  
        /// 导出速度最快  
        /// </summary>  
        /// <param name="list"><列名，数据></param>  
        /// <param name="filepath"></param>  
        /// <returns></returns>  
        public bool NewExport(List<DictionaryEntry> list, string filepath)
        {
            bool bSuccess = true;
            Microsoft.Office.Interop.Excel.Application appexcel = new Microsoft.Office.Interop.Excel.Application();
            System.Reflection.Missing miss = System.Reflection.Missing.Value;
            appexcel = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook workbookdata = null;
            Microsoft.Office.Interop.Excel.Worksheet worksheetdata = null;
            Microsoft.Office.Interop.Excel.Range rangedata;

            workbookdata = appexcel.Workbooks.Add();

            //设置对象不可见  
            appexcel.Visible = false;
            appexcel.DisplayAlerts = false;
            try
            {
                foreach (var lv in list)
                {
                    var keys = lv.Key as List<string>;
                    var values = lv.Value as List<IList<string>>;
                    worksheetdata = (Microsoft.Office.Interop.Excel.Worksheet)workbookdata.Worksheets.Add(miss, workbookdata.ActiveSheet);

                    for (int i = 0; i < keys.Count - 1; i++)
                    {
                        //给工作表赋名称  
                        worksheetdata.Name = keys[0];//列名的第一个数据位表名  
                        worksheetdata.Cells[1, i + 1] = keys[i + 1];
                    }

                    //因为第一行已经写了表头，所以所有数据都应该从a2开始  
                    rangedata = worksheetdata.get_Range("a2", miss);
                    Microsoft.Office.Interop.Excel.Range xlrang = null;

                    //irowcount为实际行数，最大行  
                    int irowcount = values.Count;
                    int iparstedrow = 0, icurrsize = 0;

                    //ieachsize为每次写行的数值，可以自己设置  
                    int ieachsize = 10000;

                    //icolumnaccount为实际列数，最大列数  
                    int icolumnaccount = keys.Count - 1;

                    //在内存中声明一个ieachsize×icolumnaccount的数组，ieachsize是每次最大存储的行数，icolumnaccount就是存储的实际列数  
                    object[,] objval = new object[ieachsize, icolumnaccount];
                    icurrsize = ieachsize;

                    while (iparstedrow < irowcount)
                    {
                        if ((irowcount - iparstedrow) < ieachsize)
                            icurrsize = irowcount - iparstedrow;

                        //用for循环给数组赋值  
                        for (int i = 0; i < icurrsize; i++)
                        {
                            for (int j = 0; j < icolumnaccount; j++)
                            {
                                var v = values[i + iparstedrow][j];
                                objval[i, j] = v != null ? v.ToString() : "";
                            }
                        }
                        string X = "A" + ((int)(iparstedrow + 2)).ToString();
                        string col = "";
                        if (icolumnaccount <= 26)
                        {
                            col = ((char)('A' + icolumnaccount - 1)).ToString() + ((int)(iparstedrow + icurrsize + 1)).ToString();
                        }
                        else
                        {
                            col = ((char)('A' + (icolumnaccount / 26 - 1))).ToString() + ((char)('A' + (icolumnaccount % 26 - 1))).ToString() + ((int)(iparstedrow + icurrsize + 1)).ToString();
                        }
                        xlrang = worksheetdata.get_Range(X, col);
                        xlrang.NumberFormat = "@";
                        // 调用range的value2属性，把内存中的值赋给excel  
                        xlrang.Value2 = objval;
                        iparstedrow = iparstedrow + icurrsize;
                    }
                }
                ((Microsoft.Office.Interop.Excel.Worksheet)workbookdata.Worksheets["Sheet1"]).Delete();
                ((Microsoft.Office.Interop.Excel.Worksheet)workbookdata.Worksheets["Sheet2"]).Delete();
                ((Microsoft.Office.Interop.Excel.Worksheet)workbookdata.Worksheets["Sheet3"]).Delete();
                //保存工作表  
                workbookdata.SaveAs(filepath, miss, miss, miss, miss, miss, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, miss, miss, miss);
                workbookdata.Close(false, miss, miss);
                appexcel.Workbooks.Close();
                appexcel.Quit();

                System.Runtime.InteropServices.Marshal.ReleaseComObject(workbookdata);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(appexcel.Workbooks);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(appexcel);
                GC.Collect();
            }
            catch (Exception ex)
            {
                string ErrorMsg = ex.Message;
                bSuccess = false;
                throw ex;
            }
            finally
            {
                if (appexcel != null)
                {
                    KillSpecialExcel(appexcel);
                }
            }
            return bSuccess;
        }

        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);
        private string KillSpecialExcel(Microsoft.Office.Interop.Excel.Application objExcel)
        {
            try
            {
                if (objExcel != null)
                {
                    int lpdwProcessId;
                    GetWindowThreadProcessId(new IntPtr(objExcel.Hwnd), out lpdwProcessId);

                    System.Diagnostics.Process.GetProcessById(lpdwProcessId).Kill();
                }
            }
            catch (Exception ex)
            {
                return "Delete Excel Process Error:" + ex.Message;
            }
            return "";
        }
    }
}
