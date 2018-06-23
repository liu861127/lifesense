using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace lifesense.Common
{
   public class ExportExcel
    {
        #region 针对GridView
        //导出excel
        public static void GetExportExcel(GridView grdList, string ExcelName)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.Charset = "UTF-8";
            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;fileName=" + ExcelName);
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            ClearControls(grdList);
            System.Globalization.CultureInfo myCItrad = new System.Globalization.CultureInfo("ZH-CN", true);
            System.IO.StringWriter oStringWriter = new System.IO.StringWriter(myCItrad);
            HtmlTextWriter html = new HtmlTextWriter(oStringWriter);
            grdList.RenderControl(html);
            HttpContext.Current.Response.Output.Write(System.Text.RegularExpressions.Regex.Replace(oStringWriter.ToString(), @"(\<a\s+[^\>]*\>)|(\</a\>)", ""));
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }
        /// <summary>
        /// 清除控件中的所有控件，以便导出Excel
        /// </summary>
        /// <param name="control"></param>
        private static void ClearControls(Control control)
        {
            for (int i = control.Controls.Count - 1; i >= 0; i--)
            {
                ClearControls(control.Controls[i]);
            }

            if (!(control is TableCell))
            {
                if (control.GetType().GetProperty("SelectedItem") != null)
                {
                    LiteralControl literal = new LiteralControl();
                    control.Parent.Controls.Add(literal);
                    try
                    {
                        literal.Text = (string)control.GetType().GetProperty("SelectedItem").GetValue(control, null);
                    }
                    catch
                    {
                    }
                    control.Parent.Controls.Remove(control);
                }
                else if (control.GetType().GetProperty("Text") != null)
                {
                    LiteralControl literal = new LiteralControl();
                    control.Parent.Controls.Add(literal);
                    literal.Text = (string)control.GetType().GetProperty("Text").GetValue(control, null);
                    control.Parent.Controls.Remove(control);
                }
            }
            return;
        }
        //导出excel
        public static void GetExportExcel(DataList daList, string ExcelName)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.Charset = "UTF-8";
            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;fileName=" + ExcelName);
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
            HtmlTextWriter html = new HtmlTextWriter(oStringWriter);
            daList.RenderControl(html);
            HttpContext.Current.Response.Output.Write(System.Text.RegularExpressions.Regex.Replace(oStringWriter.ToString(), @"(\<a\s+[^\>]*\>)|(\</a\>)", ""));
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }
        /// <summary>
        /// dtData是要导出为Excel的DataTable,FileName是要导出的Excel文件名(不加.xls)
        /// </summary>
        /// <param name="dtData"></param>
        /// <param name="FileName"></param>
        public static void DataTable3Excel(System.Data.DataTable dtData, String FileName, string UserName, int Cells)
        {
            if (dtData != null)
            {
                GridView gv = new GridView();
                gv.AllowPaging = false;
                gv.DataSource = dtData;
                gv.DataBind();
                gv = getExcelAtteributes(gv, Cells);
                GetExportExcel(gv, FileName + ".xls");
            }
        }
        //设置不用科学计算显示
        private static GridView getExcelAtteributes(GridView gv, int Cells)
        {
            for (int i = 0; i < gv.Rows.Count; i++)
            {
                gv.Rows[i].Cells[Cells].Attributes.Add("style", "vnd.ms-excel.numberformat:@;");
            }
            return gv;
        }

        // dtData是要导出为Excel的DataTable
        public static void DataTableExcel(DataTable dtData, String FileName)
        {
            if (dtData != null)
            {
                GridView gv = new GridView();
                gv.AllowPaging = false;
                gv.DataSource = dtData;
                gv.DataBind();
                GetExportExcel(gv, FileName);
            }
        }
        #endregion
    }
}
