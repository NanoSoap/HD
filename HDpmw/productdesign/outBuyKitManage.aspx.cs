using FineUIPro;
using HDBusiness;
using HDPages.productLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;

namespace HDpmw.productdesign
{
    public partial class outBuyKitManage:ImagePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                setPageInit();
                setDropListInit();
            }
        }
        void setDropListInit()
        {
            string[] otypes = new xparams().getparamData("h011").Split(',');
            otype.DataSource = otypes;
            otype.DataBind();
        }

        protected void btnPhoto_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length > 0)
            {
                string strID = Grid1.DataKeys[Grid1.SelectedRowIndexArray[0]][0].ToString();
                photoWindow.Hidden = !photoWindow.Hidden;
                imageText.Text = "<img width=\"100%\" height=\"100%\" src=\"" + ResolveUrl(IMAGEPATH + new pd_outbuykit().getPhotoFileName(strID)) + "\" />";
            }
            else
            {
                Alert.Show("请选择记录！");

            }
        }


        private void BindGrid()
        {
            string stroname = Foname.Text.Trim();

            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("oname", stroname);

            int intPageindex = Convert.ToInt32(CurPage.Text.Trim());
            int intPagesize = Convert.ToInt32(GridPageSize.Text.Trim());
            string strSort = Grid1.SortField;
            string strSortDirection = Grid1.SortDirection;

            pd_outbuykit ok = new pd_outbuykit();
            DataTable dt = ok.getBindDataAsdt(dic, strSort, strSortDirection, intPagesize, intPageindex);
            DataTable dt1 = ok.getBindDataAsdt(dic, strSort, strSortDirection);
            TotalPage.Text = dt1.Rows.Count.ToString();

            Grid1.DataSource = dt;
            Grid1.DataBind();
        }

        #region 分页-0

        private void setPageInit()
        {
            GridPageSize.Text = "21";
            CurPage.Text = "";
            TotalPage.Text = "";
            MemoTxt.Text = "";
        }

        protected void setPageContent(int intType)
        {
            int intPagesize = Convert.ToInt32(GridPageSize.Text.Trim());

            if (intType == 1)
            {
                CurPage.Text = "1";
                BindGrid();
                double intTotalPage = Math.Ceiling(Convert.ToDouble(Convert.ToDouble(TotalPage.Text.Trim()) / intPagesize));
                MemoTxt.Text = "第 1 页 共 " + intTotalPage.ToString() + " 页 " + TotalPage.Text.Trim() + " 条数据";
            }

            if (intType == 2)
            {
                int intCurPage;

                if (int.TryParse(CurPage.Text.Trim(), out intCurPage))
                {
                    intCurPage--;
                    if (intCurPage > 0)
                    {
                        CurPage.Text = intCurPage.ToString();
                        double intTotalPage = Math.Ceiling(Convert.ToDouble(Convert.ToDouble(TotalPage.Text.Trim()) / intPagesize));
                        MemoTxt.Text = "第 " + intCurPage.ToString() + " 页 共 " + intTotalPage.ToString() +
                            " 页 " + TotalPage.Text.Trim() + " 条数据";
                        BindGrid();
                    }
                }
            }

            if (intType == 3)
            {
                int intCurPage;

                if (int.TryParse(CurPage.Text.Trim(), out intCurPage))
                {
                    intCurPage++;
                    double intTotalPage = Math.Ceiling(Convert.ToDouble(Convert.ToDouble(TotalPage.Text.Trim()) / intPagesize));
                    if (intCurPage < intTotalPage + 1)
                    {
                        CurPage.Text = intCurPage.ToString();

                        MemoTxt.Text = "第 " + intCurPage.ToString() + " 页 共 " + intTotalPage.ToString() +
                            " 页 " + TotalPage.Text.Trim() + " 条数据";
                        BindGrid();
                    }
                }
            }

            if (intType == 4)
            {
                double intTotalPage = Math.Ceiling(Convert.ToDouble(Convert.ToDouble(TotalPage.Text.Trim()) / intPagesize));
                CurPage.Text = intTotalPage.ToString();
                MemoTxt.Text = "终页 共 " + intTotalPage.ToString() + " 页 " + TotalPage.Text.Trim() + " 条数据";
                BindGrid();
            }

            if (intType == 5)
            {
                int intCurPage;

                if (int.TryParse(CurPage.Text.Trim(), out intCurPage))
                {
                    double intTotalPage = Math.Ceiling(Convert.ToDouble(Convert.ToDouble(TotalPage.Text.Trim()) / intPagesize));
                    if (intCurPage < intTotalPage + 1 && intCurPage > 0)
                    {
                        CurPage.Text = intCurPage.ToString();

                        MemoTxt.Text = "第 " + intCurPage.ToString() + " 页 共 " + intTotalPage.ToString() +
                            " 页 " + TotalPage.Text.Trim() + " 条数据";
                        BindGrid();
                    }
                }
            }
        }

        protected void FirstPage_Click(object sender, EventArgs e)
        {
            setPageContent(1);
        }

        protected void PrePage_Click(object sender, EventArgs e)
        {
            setPageContent(2);
        }

        protected void NextPage_Click(object sender, EventArgs e)
        {
            setPageContent(3); ;
        }

        protected void LastPage_Click(object sender, EventArgs e)
        {
            setPageContent(4); ;
        }

        protected void GoPage_Click(object sender, EventArgs e)
        {
            setPageContent(5); ;
        }

        protected void SubNumber_Click(object sender, EventArgs e)
        {
            int intGridPageSize;

            if (int.TryParse(GridPageSize.Text.Trim(), out intGridPageSize))
            {
                if (intGridPageSize > 6)
                {
                    intGridPageSize--;
                    GridPageSize.Text = intGridPageSize.ToString();
                }
            }
        }

        protected void UpNumber_Click(object sender, EventArgs e)
        {
            int intGridPageSize;

            if (int.TryParse(GridPageSize.Text.Trim(), out intGridPageSize))
            {
                if (intGridPageSize < 36)
                {
                    intGridPageSize++;
                    GridPageSize.Text = intGridPageSize.ToString();
                }
            }
        }
        protected void Grid1_Sort(object sender, FineUIPro.GridSortEventArgs e)
        {
            setPageContent(1);
        }

        #endregion

        #region 主窗口菜单

        protected void btnFind_Click(object sender, EventArgs e)
        {
            setPageContent(1);
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            erase();
            neweditWindow.Title = "外购件新增界面";
            neweditWindow.Hidden = false;
        }

        private void erase()
        {
            editID.Text = "";
            oname.Text = "";
            ocode.Text = "";
            specification.Text = "";
            imgPhoto.ImageUrl = DEFAULT_IMAGEPATH;
            filePhoto.Reset();

        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            edit();
        }

        void edit()
        {
            int[] intRowindexarray = Grid1.SelectedRowIndexArray;

            if (intRowindexarray.Length < 1)
            {
                Alert.Show("请选择编辑记录");

                return;
            }

            initinfo(Grid1.DataKeys[intRowindexarray[0]][0].ToString().Trim());
            neweditWindow.Title = "外购件编辑界面";
            neweditWindow.Hidden = false;
        }
        private void initinfo(string strID)
        {
            pd_outbuykit ok = new pd_outbuykit();
            DataTable dt = ok.getEditdata(strID);

            DataRow r = dt.Rows[0];

            editID.Text = strID;
            oname.Text = r["oname"].ToString().Trim();
            ocode.Text = r["ocode"].ToString().Trim();
            specification.Text = r["specification"].ToString().Trim();
            otype.Text = r["otype"].ToString();
            imgPhoto.ImageUrl = ok.getPhotoFileName(strID) == "" ? DEFAULT_IMAGEPATH : IMAGEPATH + ok.getPhotoFileName(strID);

        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int[] intRowindexarray = Grid1.SelectedRowIndexArray;

            if (intRowindexarray.Length > 0)
            {
                pd_outbuykit ok = new pd_outbuykit();
                object[] keys = Grid1.DataKeys[intRowindexarray[0]];
                string strID = keys[0].ToString();

                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("isdelid", "2");
                string photoID = ok.getPhotoID(strID);
                if (photoID != "")
                {
                    // 返回与指定虚拟路径相对应的物理路径即绝对路径
                    string filePath = Server.MapPath(IMAGEPATH+ ok.getPhotoFileName(strID));
                    // 删除该文件
                    System.IO.File.Delete(filePath);
                    int intresultPhoto = new pd_photo().delete("pd_photo", "ID", ok.getPhotoID(strID));
                }
                int intresult = ok.delete("pd_outbuykit", "ID", strID);

                setPageContent(1);

                Alert alert = new Alert();

                if (intresult > 0)
                {
                    alert.Icon = Icon.Information;
                    alert.Message = "成功移除数据";
                }
                else
                {
                    alert.MessageBoxIcon = MessageBoxIcon.Error;
                    alert.Message = "数据移除失败";
                }


                alert.Show();
            }
            else
            {
                Alert.Show("请选择记录！");

            }
        }

        #endregion

        #region 编辑窗口按钮
        protected void btnSave_Click(object sender, EventArgs e)
        {

            Dictionary<string, string> dicOutBuykit = initDatadicOutBuykit();
            Dictionary<string, string> dicPhoto = initDatadicPhoto();
            string strID = editID.Text.ToString().Trim();
            pd_outbuykit ok = new pd_outbuykit();
            pd_photo ph = new pd_photo();
            int intresultRecord = 0;
            int intresultPhoto = 0;

            if (strID == "")
            {
                string standardKitID = Guid.NewGuid().ToString();
                dicOutBuykit.Add("ID", standardKitID);
                dicOutBuykit.Add("isdelid", "1");
                dicPhoto.Add("ID", Guid.NewGuid().ToString());
                dicPhoto.Add("isdelid", "1");
                dicPhoto.Add("pid", standardKitID);
                string str_ocode = dicOutBuykit["ocode"].ToString().Trim();
                string str_oname = dicOutBuykit["oname"].ToString().Trim();
                if ((ok.isExistdata("pd_outbuykit", "ocode", str_ocode, "ocode").Trim() != "") || (ok.isExistdata("pd_outbuykit", "oname", str_oname, "oname").Trim() != ""))
                {
                    Alert.Show(" 该外购件名称或代码已经存在!");
                }
                else
                {
                    intresultRecord = ok.add(dicOutBuykit, "pd_outbuykit");
                    intresultPhoto = 1;
                    if ((filePhoto.HasFile) && (intresultRecord == 1))
                    {
                        intresultPhoto = ph.add(dicPhoto, "pd_photo");
                    }
                }
            }
            else
            {
                intresultRecord = ok.update(dicOutBuykit, "pd_outbuykit", "ID", strID);
                //exist photo->update || else ->add
                string photoID = ok.getPhotoID(strID).Trim();
                if (intresultRecord == 1)
                {
                    if (photoID != "")

                    {
                        // 返回与指定虚拟路径相对应的物理路径即绝对路径
                        string filePath = Server.MapPath(IMAGEPATH+ ok.getPhotoFileName(strID));
                        // 删除该文件
                        System.IO.File.Delete(filePath);
                        intresultPhoto = ph.update(dicPhoto, "pd_photo", "ID", photoID);
                    }
                    else
                    {
                        dicPhoto.Add("ID", Guid.NewGuid().ToString());
                        dicPhoto.Add("isdelid", "1");
                        dicPhoto.Add("pid", strID);
                        intresultPhoto = ph.add(dicPhoto, "pd_photo");

                    }
                }
            }

            if (CurPage.Text.Trim() == "")
            {
                setPageContent(1);
            }
            else
            {
                setPageContent(5);
            }

            Alert alert = new Alert();

            if ((intresultRecord == 1) && (intresultPhoto == 1))
            {
                alert.Icon = Icon.Information;
                alert.Message = "数据保存成功";
                imgPhoto.Reset();
                filePhoto.Reset();
            }
            else if (intresultRecord == 0)
            {
                alert.MessageBoxIcon = MessageBoxIcon.Error;
                alert.Message = "数据保存失败";
                filePhoto.Reset();
                imgPhoto.Reset();
            }
            else if (intresultPhoto == 0)
            {
                alert.MessageBoxIcon = MessageBoxIcon.Error;
                alert.Message = "图片保存失败";
                filePhoto.Reset();
                imgPhoto.Reset();
            }


            alert.Show();
        }

        private Dictionary<string, string> initDatadicPhoto()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("pname", oname.Text.Trim());
            dic.Add("pcode", ocode.Text.Trim());
            dic.Add("filename", str_filename.Text);
            dic.Add("filetype", getSuffix(imgPhoto.ImageUrl));
            dic.Add("operater", SessionUserName);
            dic.Add("systemdate", DateTime.Now.ToString());
            return dic;
        }

        private Dictionary<string, string> initDatadicOutBuykit()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("oname", oname.Text.ToString().Trim());
            dic.Add("ocode", ocode.Text.ToString().Trim());
            dic.Add("specification", specification.Text.ToString().Trim());
            dic.Add("otype", otype.SelectedValue);
            dic.Add("operater", SessionUserName);
            dic.Add("systemdate", DateTime.Now.ToString());

            return dic;
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            string strID = editID.Text.ToString().Trim();

            if (strID.Trim() == "")
            {
                erase();
            }
            else
            {
                initinfo(strID);
            }
        }
        #endregion

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

        }

        protected void filePhoto_FileSelected(object sender, EventArgs e)
        {
            if (filePhoto.HasFile)
            {
                string fileName = filePhoto.ShortFileName;

                if (!YDCode.commonLib.ValidateImgType(fileName))
                {
                    // 清空文件上传控件
                    filePhoto.Reset();

                    Alert.Show("无效的文件类型！");
                    return;
                }



                fileName = DateTime.Now.Ticks.ToString() + "." + getSuffix(fileName);

                filePhoto.SaveAs(Server.MapPath(IMAGEPATH + fileName));

                imgPhoto.ImageUrl = IMAGEPATH + fileName;
                str_filename.Text = fileName;

                // 清空文件上传组件（上传后要记着清空，否则点击提交表单时会再次上传！！）
                //filePhoto.Reset();
            }

        }

        //private bool ValidFileType(string fileName)
        //{
        //    xparams x = new xparams();
        //    string[] suffixs = x.getparamData("H008").Split(',');
        //    bool valid = false;


        //    foreach (var item in suffixs)
        //    {
        //        if (item.Equals(getSuffix(fileName)))
        //        {
        //            valid = true;
        //        }
        //    }
        //    return valid;
        //}

        //将文件名转换为大写并返回后缀名
        string getSuffix(string s)
        {
            string[] filenameSplit = s.ToUpper().Split('.');
            return filenameSplit[filenameSplit.Length - 1];
        }

        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            edit();
        }
    }
}