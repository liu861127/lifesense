using System;
using System.Collections.Generic;
using System.Text;
using NPOI.HSSF.UserModel;
using System.Data;
using System.IO;
using NPOI.HPSF;
using System.Web;
using NPOI.SS.UserModel;
using System.Collections;
using NPOI.XSSF.UserModel;
using System.Text.RegularExpressions;
using NPOI.SS.Formula.Eval;
using NPOI.SS.Util;

namespace Common
{
    public class ExcelHelper
    {
        private static WriteLog wl = new WriteLog();
        static HSSFWorkbook hssfworkbook;
        static int HeadRowCount = 0;//��ͷ���ݼ������

        #region ��datatable�н����ݵ�����excel

        public static MemoryStream ExportDT(DataTable dtSource, string strHeaderText, string strFilePath)
        {
            InitializeWorkbook(strFilePath);

            string sNa = hssfworkbook.GetSheetName(0);
            ISheet sheet1 = hssfworkbook.GetSheet(sNa);
            sheet1.PrintSetup.PaperSize = 9;
            ICellStyle style = hssfworkbook.CreateCellStyle();
            style.BorderBottom = NPOI.SS.UserModel.BorderStyle.NONE;// CellBorderType.THIN;
            style.BorderLeft = NPOI.SS.UserModel.BorderStyle.NONE; //CellBorderType.THIN;
            style.BorderRight = NPOI.SS.UserModel.BorderStyle.NONE; //CellBorderType.THIN;
            style.BorderTop = NPOI.SS.UserModel.BorderStyle.NONE; //CellBorderType.THIN;

            #region ��ѯ��Ľṹ��Ϣ�����

            DataTable dtGetRowTable = new DataTable();
            dtGetRowTable = dtSource;

            #endregion
            int DataRowCount = dtGetRowTable.Rows.Count;

            #region ��������������⴦���и�ʽ
            HSSFCellStyle dateStyle = hssfworkbook.CreateCellStyle() as HSSFCellStyle;
            // CellType.Numeric;
            dateStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.NONE; ; //CellBorderType.THIN;
            dateStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.NONE; //CellBorderType.THIN;
            dateStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.NONE; // CellBorderType.THIN;
            dateStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.NONE; // CellBorderType.THIN;
            HSSFDataFormat format = hssfworkbook.CreateDataFormat() as HSSFDataFormat;
            dateStyle.DataFormat = format.GetFormat("yyyy-mm-dd");
            #endregion
            #region �������ֶνṹ��Ϣ����

            CreateDataMethod(sheet1, style, dtGetRowTable, HeadRowCount, dateStyle);
            #endregion

            using (MemoryStream ms = new MemoryStream())
            {
                hssfworkbook.Write(ms);
                ms.Flush();
                ms.Position = 0;
                return ms;
            }
        }

        static void InitializeWorkbook(string strFilePath)
        {
            //FileStream file = new FileStream("C:\\Book1.xls", FileMode.Open, FileAccess.Read);
            FileStream file = new FileStream(strFilePath + "\\PrintFields\\TemplateFile.xls", FileMode.Open, FileAccess.Read);
            hssfworkbook = new HSSFWorkbook(file);
        }

        #region ��Ԫ����������ͨ����
        private static int CreateDataMethod(ISheet sheet1, ICellStyle style, DataTable dtGetRowTable, int rowIndex, HSSFCellStyle dateStyle)
        {
            IRow cFootRow = sheet1.CreateRow(0);
            for (int j = 0; j < dtGetRowTable.Columns.Count; j++)
            {
                ICell cFootCell = cFootRow.CreateCell(j, CellType.STRING);
                cFootCell.SetCellValue(dtGetRowTable.Columns[j].ColumnName);
                cFootCell.CellStyle = style;
            }
            rowIndex = rowIndex + 1;

            foreach (DataRow row in dtGetRowTable.Rows)
            {
                #region �������
                HSSFRow dataRow = sheet1.CreateRow(rowIndex) as HSSFRow;
                foreach (DataColumn column in dtGetRowTable.Columns)
                {
                    HSSFCell newCell = dataRow.CreateCell(column.Ordinal) as HSSFCell;
                    string drValue = row[column].ToString();
                    #region �ֶ����ʹ���
                    switch (column.DataType.ToString())
                    {
                        case "System.String": //�ַ�������
                            newCell.SetCellValue(drValue);
                            newCell.CellStyle = style;
                            break;
                        case "System.DateTime": //��������
                            //DateTime dateV;
                            //DateTime.TryParse(drValue, out dateV);
                            //newCell.SetCellValue(dateV);
                            //newCell.CellStyle = dateStyle; //��ʽ����ʾ
                            newCell.SetCellValue(drValue);
                            newCell.CellStyle = style;
                            break;
                        case "System.Boolean": //������
                            bool boolV = false;
                            bool.TryParse(drValue, out boolV);
                            newCell.SetCellValue(boolV);
                            newCell.CellStyle = style;
                            break;
                        case "System.Int16": //����
                        case "System.Int32":
                        case "System.Int64":
                        case "System.Byte":
                            int intV = 0;
                            int.TryParse(drValue, out intV);
                            newCell.SetCellValue(intV);
                            newCell.CellStyle = style;
                            break;
                        case "System.Decimal": //������
                        case "System.Double":
                            double doubV = 0;
                            double.TryParse(drValue, out doubV);
                            newCell.SetCellValue(doubV);
                            newCell.CellStyle = style;
                            break;
                        case "System.DBNull": //��ֵ����
                            newCell.SetCellValue("");
                            newCell.CellStyle = style;
                            break;
                        default:
                            newCell.SetCellValue(drValue);
                            newCell.CellStyle = style;
                            break;
                    }
                    #endregion
                    #region ��������հ���
                    //int iLastCell = sheet1.GetRow(3).LastCellNum + 1 - dtGetRowTable.Columns.Count;
                    //for (int i = 1; i < iLastCell; i++)
                    //{
                    //    HSSFCell newNullCell = dataRow.CreateCell(newCell.ColumnIndex + i) as HSSFCell;
                    //    newNullCell.SetCellValue("");
                    //    newNullCell.CellStyle = style;
                    //}
                    #endregion

                }

                #endregion
                rowIndex++;
            }
            return rowIndex;
        }
        #endregion

        /// <summary>
        /// DataTable������Excel��MemoryStream
        /// </summary>
        /// <param name="dtSource">ԴDataTable</param>
        /// <param name="strHeaderText">��ͷ�ı�</param>
        public static MemoryStream ExportDT1(DataTable dtSource, string strHeaderText)
        {
            HSSFWorkbook workbook = new HSSFWorkbook();
            HSSFSheet sheet = workbook.CreateSheet() as HSSFSheet;
            sheet.PrintSetup.Scale = 100;
            sheet.PrintSetup.PaperSize = 9;
            sheet.SetZoom(1, 1);
            //sheet.PrintSetup.FitHeight

            #region �һ��ļ� ������Ϣ

            //{
            //    DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            //    dsi.Company = "http://www.yongfa365.com/";
            //    workbook.DocumentSummaryInformation = dsi;

            //    SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            //    si.Author = "������"; //���xls�ļ�������Ϣ
            //    si.ApplicationName = "NPOI���Գ���"; //���xls�ļ�����������Ϣ
            //    si.LastAuthor = "������2"; //���xls�ļ���󱣴�����Ϣ
            //    si.Comments = "˵����Ϣ"; //���xls�ļ�������Ϣ
            //    si.Title = "NPOI����"; //���xls�ļ�������Ϣ
            //    si.Subject = "NPOI����Demo"; //����ļ�������Ϣ
            //    si.CreateDateTime = DateTime.Now;
            //    workbook.SummaryInformation = si;
            //}

            #endregion

            HSSFCellStyle dateStyle = workbook.CreateCellStyle() as HSSFCellStyle;
            HSSFDataFormat format = workbook.CreateDataFormat() as HSSFDataFormat;
            dateStyle.DataFormat = format.GetFormat("yyyy-mm-dd");

            //ȡ���п�
            int[] arrColWidth = new int[dtSource.Columns.Count];
            foreach (DataColumn item in dtSource.Columns)
            {
                arrColWidth[item.Ordinal] = Encoding.GetEncoding(936).GetBytes(item.ColumnName.ToString()).Length;
            }
            for (int i = 0; i < dtSource.Rows.Count; i++)
            {
                for (int j = 0; j < dtSource.Columns.Count; j++)
                {
                    int intTemp = Encoding.GetEncoding(936).GetBytes(dtSource.Rows[i][j].ToString()).Length;
                    if (intTemp > arrColWidth[j])
                    {
                        arrColWidth[j] = intTemp;
                    }
                }
            }
            int rowIndex = 0;

            foreach (DataRow row in dtSource.Rows)
            {
                #region �½�������ͷ�������ͷ����ʽ

                if (rowIndex == 65535 || rowIndex == 0)
                {
                    if (rowIndex != 0)
                    {
                        sheet = workbook.CreateSheet() as HSSFSheet;
                        sheet.PrintSetup.Scale = 100;
                        sheet.PrintSetup.PaperSize = 9;
                    }

                    #region ��ͷ����ʽ

                    {
                        HSSFRow headerRow = sheet.CreateRow(0) as HSSFRow;
                        headerRow.HeightInPoints = 25;
                        headerRow.CreateCell(0).SetCellValue(strHeaderText);

                        HSSFCellStyle headStyle = workbook.CreateCellStyle() as HSSFCellStyle;
                        headStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER;
                        HSSFFont font = workbook.CreateFont() as HSSFFont;
                        font.FontHeightInPoints = 11;
                        font.Boldweight = 70;
                        headStyle.SetFont(font);

                        headerRow.GetCell(0).CellStyle = headStyle;
                        sheet.AddMergedRegion(new Region(0, 0, 0, dtSource.Columns.Count - 1));
                        //headerRow.Dispose();
                    }

                    #endregion


                    #region ��ͷ����ʽ

                    {
                        HSSFRow headerRow = sheet.CreateRow(1) as HSSFRow;


                        HSSFCellStyle headStyle = workbook.CreateCellStyle() as HSSFCellStyle;
                        headStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER;
                        HSSFFont font = workbook.CreateFont() as HSSFFont;
                        font.FontHeightInPoints = 10;
                        font.Boldweight = 700;
                        headStyle.SetFont(font);


                        //foreach (DataColumn column in dtSource.Columns)
                        //{
                        //    headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
                        //    headerRow.GetCell(column.Ordinal).CellStyle = headStyle;

                        //    //�����п������Ҷ����10���ַ��ĳ���
                        //    sheet.SetColumnWidth(column.Ordinal, (arrColWidth[column.Ordinal] + 10) * 256);


                        //}
                        //headerRow.Dispose();
                    }

                    #endregion

                    rowIndex = 2;
                }

                #endregion

                #region �������

                HSSFRow dataRow = sheet.CreateRow(rowIndex) as HSSFRow;
                foreach (DataColumn column in dtSource.Columns)
                {
                    HSSFCell newCell = dataRow.CreateCell(column.Ordinal) as HSSFCell;

                    string drValue = row[column].ToString();

                    switch (column.DataType.ToString())
                    {
                        case "System.String": //�ַ�������
                            double result;
                            if (isNumeric(drValue, out result))
                            {

                                double.TryParse(drValue, out result);
                                newCell.SetCellValue(result);
                                break;
                            }
                            else
                            {
                                newCell.SetCellValue(drValue);
                                break;
                            }

                        case "System.DateTime": //��������
                            DateTime dateV;
                            DateTime.TryParse(drValue, out dateV);
                            newCell.SetCellValue(dateV);

                            newCell.CellStyle = dateStyle; //��ʽ����ʾ
                            break;
                        case "System.Boolean": //������
                            bool boolV = false;
                            bool.TryParse(drValue, out boolV);
                            newCell.SetCellValue(boolV);
                            break;
                        case "System.Int16": //����
                        case "System.Int32":
                        case "System.Int64":
                        case "System.Byte":
                            int intV = 0;
                            int.TryParse(drValue, out intV);
                            newCell.SetCellValue(intV);
                            break;
                        case "System.Decimal": //������
                        case "System.Double":
                            double doubV = 0;
                            double.TryParse(drValue, out doubV);
                            newCell.SetCellValue(doubV);
                            break;
                        case "System.DBNull": //��ֵ����
                            newCell.SetCellValue("");
                            break;
                        default:
                            newCell.SetCellValue("");
                            break;
                    }

                }

                #endregion

                rowIndex++;
            }
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;

                //sheet.Dispose();
                //workbook.Dispose();

                return ms;
            }
        }

        /// <summary>
        /// DataTable������Excel��MemoryStream
        /// </summary>
        /// <param name="dtSource">ԴDataTable</param>
        /// <param name="strHeaderText">��ͷ�ı�</param>
        static void ExportDTI(DataTable dtSource, string strHeaderText, FileStream fs)
        {
            XSSFWorkbook workbook = new XSSFWorkbook();
            XSSFSheet sheet = workbook.CreateSheet() as XSSFSheet;

            #region �һ��ļ� ������Ϣ

            //{
            //    DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            //    dsi.Company = "http://www.yongfa365.com/";
            //    workbook.DocumentSummaryInformation = dsi;

            //    SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            //    si.Author = "������"; //���xls�ļ�������Ϣ
            //    si.ApplicationName = "NPOI���Գ���"; //���xls�ļ�����������Ϣ
            //    si.LastAuthor = "������2"; //���xls�ļ���󱣴�����Ϣ
            //    si.Comments = "˵����Ϣ"; //���xls�ļ�������Ϣ
            //    si.Title = "NPOI����"; //���xls�ļ�������Ϣ
            //    si.Subject = "NPOI����Demo"; //����ļ�������Ϣ
            //    si.CreateDateTime = DateTime.Now;
            //    workbook.SummaryInformation = si;
            //}

            #endregion

            XSSFCellStyle dateStyle = workbook.CreateCellStyle() as XSSFCellStyle;
            XSSFDataFormat format = workbook.CreateDataFormat() as XSSFDataFormat;
            dateStyle.DataFormat = format.GetFormat("yyyy-mm-dd");

            //ȡ���п�
            int[] arrColWidth = new int[dtSource.Columns.Count];
            foreach (DataColumn item in dtSource.Columns)
            {
                arrColWidth[item.Ordinal] = Encoding.GetEncoding(936).GetBytes(item.ColumnName.ToString()).Length;
            }
            for (int i = 0; i < dtSource.Rows.Count; i++)
            {
                for (int j = 0; j < dtSource.Columns.Count; j++)
                {
                    int intTemp = Encoding.GetEncoding(936).GetBytes(dtSource.Rows[i][j].ToString()).Length;
                    if (intTemp > arrColWidth[j])
                    {
                        arrColWidth[j] = intTemp;
                    }
                }
            }
            int rowIndex = 0;

            foreach (DataRow row in dtSource.Rows)
            {
                #region �½�������ͷ�������ͷ����ʽ

                if (rowIndex == 0)
                {
                    #region ��ͷ����ʽ
                    //{
                    //    XSSFRow headerRow = sheet.CreateRow(0) as XSSFRow;
                    //    headerRow.HeightInPoints = 25;
                    //    headerRow.CreateCell(0).SetCellValue(strHeaderText);

                    //    XSSFCellStyle headStyle = workbook.CreateCellStyle() as XSSFCellStyle;
                    //    headStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER;
                    //    XSSFFont font = workbook.CreateFont() as XSSFFont;
                    //    font.FontHeightInPoints = 20;
                    //    font.Boldweight = 700;
                    //    headStyle.SetFont(font);

                    //    headerRow.GetCell(0).CellStyle = headStyle;

                    //    //sheet.AddMergedRegion(new Region(0, 0, 0, dtSource.Columns.Count - 1));
                    //    //headerRow.Dispose();
                    //}

                    #endregion


                    #region ��ͷ����ʽ

                    //{
                    //    XSSFRow headerRow = sheet.CreateRow(0) as XSSFRow;


                    //    XSSFCellStyle headStyle = workbook.CreateCellStyle() as XSSFCellStyle;
                    //    headStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER;
                    //    XSSFFont font = workbook.CreateFont() as XSSFFont;
                    //    font.FontHeightInPoints = 10;
                    //    font.Boldweight = 700;
                    //    headStyle.SetFont(font);


                    //    foreach (DataColumn column in dtSource.Columns)
                    //    {
                    //        headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
                    //        headerRow.GetCell(column.Ordinal).CellStyle = headStyle;

                    //        //�����п�
                    //        sheet.SetColumnWidth(column.Ordinal, (arrColWidth[column.Ordinal] + 1) * 256);

                    //    }
                    //    //headerRow.Dispose();
                    //}

                    #endregion

                    //rowIndex = 1;
                }

                #endregion

                #region �������

                XSSFRow dataRow = sheet.CreateRow(rowIndex) as XSSFRow;
                foreach (DataColumn column in dtSource.Columns)
                {
                    XSSFCell newCell = dataRow.CreateCell(column.Ordinal) as XSSFCell;

                    string drValue = row[column].ToString();

                    switch (column.DataType.ToString())
                    {
                        case "System.String": //�ַ�������
                            double result;
                            if (isNumeric(drValue, out result))
                            {

                                double.TryParse(drValue, out result);
                                newCell.SetCellValue(result);
                                break;
                            }
                            else
                            {
                                newCell.SetCellValue(drValue);
                                break;
                            }

                        case "System.DateTime": //��������
                            DateTime dateV;
                            DateTime.TryParse(drValue, out dateV);
                            newCell.SetCellValue(dateV);

                            newCell.CellStyle = dateStyle; //��ʽ����ʾ
                            break;
                        case "System.Boolean": //������
                            bool boolV = false;
                            bool.TryParse(drValue, out boolV);
                            newCell.SetCellValue(boolV);
                            break;
                        case "System.Int16": //����
                        case "System.Int32":
                        case "System.Int64":
                        case "System.Byte":
                            int intV = 0;
                            int.TryParse(drValue, out intV);
                            newCell.SetCellValue(intV);
                            break;
                        case "System.Decimal": //������
                        case "System.Double":
                            double doubV = 0;
                            double.TryParse(drValue, out doubV);
                            newCell.SetCellValue(doubV);
                            break;
                        case "System.DBNull": //��ֵ����
                            newCell.SetCellValue("");
                            break;
                        default:
                            newCell.SetCellValue("");
                            break;
                    }

                }

                #endregion

                rowIndex++;
            }
            workbook.Write(fs);
            fs.Close();
        }

        /// <summary>
        /// DataTable������Excel�ļ�
        /// </summary>
        /// <param name="dtSource">ԴDataTable</param>
        /// <param name="strHeaderText">��ͷ�ı�</param>
        /// <param name="strFileName">����λ��</param>
        public static void ExportDTtoExcel(DataTable dtSource, string strHeaderText, string strFileName)
        {
            string[] temp = strFileName.Split('.');

            if (temp[temp.Length - 1] == "xls" && dtSource.Columns.Count < 256 && dtSource.Rows.Count < 65536)
            {
                using (MemoryStream ms = ExportDT(dtSource, strHeaderText, ""))
                {
                    using (FileStream fs = new FileStream(strFileName, FileMode.Create, FileAccess.Write))
                    {
                        byte[] data = ms.ToArray();
                        fs.Write(data, 0, data.Length);
                        fs.Flush();
                    }
                }
            }
            else
            {
                if (temp[temp.Length - 1] == "xls")
                    strFileName = strFileName + "x";

                using (FileStream fs = new FileStream(strFileName, FileMode.Create, FileAccess.Write))
                {
                    ExportDTI(dtSource, strHeaderText, fs);

                }

            }
        }
        #endregion

        #region ��excel�н����ݵ�����datatable
        /// <summary>��ȡexcel
        /// Ĭ�ϵ�һ��Ϊ��ͷ
        /// </summary>
        /// <param name="strFileName">excel�ĵ�·��</param>
        /// <returns></returns>
        public static DataTable ImportExceltoDt(string strFileName)
        {
            DataTable dt = new DataTable();
            IWorkbook wb;
            using (FileStream file = new FileStream(strFileName, FileMode.Open, FileAccess.Read))
            {
                wb = WorkbookFactory.Create(file);
            }
            ISheet sheet = wb.GetSheetAt(0);
            dt = ImportDt(sheet, 0, true);
            return dt;
        }

        /// <summary>
        /// ��ȡexcel
        /// </summary>
        /// <param name="strFileName">excel�ļ�·��</param>
        /// <param name="sheet">��Ҫ������sheet</param>
        /// <param name="HeaderRowIndex">��ͷ�����кţ�-1��ʾû����ͷ</param>
        /// <returns></returns>
        public static DataTable ImportExceltoDt(string strFileName, string SheetName, int HeaderRowIndex)
        {
            HSSFWorkbook workbook;
            IWorkbook wb;
            using (FileStream file = new FileStream(strFileName, FileMode.Open, FileAccess.Read))
            {
                wb = WorkbookFactory.Create(file);
            }
            ISheet sheet = wb.GetSheet(SheetName);
            DataTable table = new DataTable();
            table = ImportDt(sheet, HeaderRowIndex, true);
            //ExcelFileStream.Close();
            workbook = null;
            sheet = null;
            return table;
        }

        /// <summary>
        /// ��ȡexcel
        /// </summary>
        /// <param name="strFileName">excel�ļ�·��</param>
        /// <param name="sheet">��Ҫ������sheet���</param>
        /// <param name="HeaderRowIndex">��ͷ�����кţ�-1��ʾû����ͷ</param>
        /// <returns></returns>
        public static DataTable ImportExceltoDt(string strFileName, int SheetIndex, int HeaderRowIndex)
        {
            HSSFWorkbook workbook;
            IWorkbook wb;
            using (FileStream file = new FileStream(strFileName, FileMode.Open, FileAccess.Read))
            {
                wb = WorkbookFactory.Create(file);
            }
            ISheet isheet = wb.GetSheetAt(SheetIndex);
            DataTable table = new DataTable();
            table = ImportDt(isheet, HeaderRowIndex, true);
            //ExcelFileStream.Close();
            workbook = null;
            isheet = null;
            return table;
        }

        /// <summary>
        /// ��ȡexcel
        /// </summary>
        /// <param name="strFileName">excel�ļ�·��</param>
        /// <param name="sheet">��Ҫ������sheet</param>
        /// <param name="HeaderRowIndex">��ͷ�����кţ�-1��ʾû����ͷ</param>
        /// <returns></returns>
        public static DataTable ImportExceltoDt(string strFileName, string SheetName, int HeaderRowIndex, bool needHeader)
        {
            HSSFWorkbook workbook;
            IWorkbook wb;
            using (FileStream file = new FileStream(strFileName, FileMode.Open, FileAccess.Read))
            {
                wb = WorkbookFactory.Create(file);
            }
            ISheet sheet = wb.GetSheet(SheetName);
            DataTable table = new DataTable();
            table = ImportDt(sheet, HeaderRowIndex, needHeader);
            //ExcelFileStream.Close();
            workbook = null;
            sheet = null;
            return table;
        }

        /// <summary>
        /// ��ȡexcel
        /// </summary>
        /// <param name="strFileName">excel�ļ�·��</param>
        /// <param name="sheet">��Ҫ������sheet���</param>
        /// <param name="HeaderRowIndex">��ͷ�����кţ�-1��ʾû����ͷ</param>
        /// <returns></returns>
        public static DataTable ImportExceltoDt(string strFileName, int SheetIndex, int HeaderRowIndex, bool needHeader)
        {
            HSSFWorkbook workbook;
            IWorkbook wb;
            using (FileStream file = new FileStream(strFileName, FileMode.Open, FileAccess.Read))
            {
                wb = WorkbookFactory.Create(file);
            }
            ISheet sheet = wb.GetSheetAt(SheetIndex);
            DataTable table = new DataTable();
            table = ImportDt(sheet, HeaderRowIndex, needHeader);
            //ExcelFileStream.Close();
            workbook = null;
            sheet = null;
            return table;
        }

        /// <summary>
        /// ���ƶ�sheet�е����ݵ�����datatable��
        /// </summary>
        /// <param name="sheet">��Ҫ������sheet</param>
        /// <param name="HeaderRowIndex">��ͷ�����кţ�-1��ʾû����ͷ</param>
        /// <returns></returns>
        static DataTable ImportDt(ISheet sheet, int HeaderRowIndex, bool needHeader)
        {
            DataTable table = new DataTable();
            IRow headerRow;
            int cellCount;
            try
            {
                if (HeaderRowIndex < 0 || !needHeader)
                {
                    headerRow = sheet.GetRow(0);
                    cellCount = headerRow.LastCellNum;

                    for (int i = headerRow.FirstCellNum; i <= cellCount; i++)
                    {
                        DataColumn column = new DataColumn(Convert.ToString(i));
                        table.Columns.Add(column);
                    }
                }
                else
                {
                    headerRow = sheet.GetRow(HeaderRowIndex);
                    cellCount = headerRow.LastCellNum;

                    for (int i = headerRow.FirstCellNum; i <= cellCount; i++)
                    {
                        if (headerRow.GetCell(i) == null)
                        {
                            if (table.Columns.IndexOf(Convert.ToString(i)) > 0)
                            {
                                DataColumn column = new DataColumn(Convert.ToString("�ظ�����" + i));
                                table.Columns.Add(column);
                            }
                            else
                            {
                                //DataColumn column = new DataColumn(Convert.ToString(i));
                                //table.Columns.Add(column);
                            }

                        }
                        else if (table.Columns.IndexOf(headerRow.GetCell(i).ToString()) > 0)
                        {
                            DataColumn column = new DataColumn(Convert.ToString("�ظ�����" + i));
                            table.Columns.Add(column);
                        }
                        else
                        {
                            DataColumn column = new DataColumn(headerRow.GetCell(i).ToString());
                            table.Columns.Add(column);
                        }
                    }
                }
                int rowCount = sheet.LastRowNum;
                for (int i = (HeaderRowIndex + 1); i <= sheet.LastRowNum; i++)
                {
                    try
                    {
                        IRow row;
                        if (sheet.GetRow(i) == null)
                        {
                            row = sheet.CreateRow(i);
                        }
                        else
                        {
                            row = sheet.GetRow(i);
                        }

                        DataRow dataRow = table.NewRow();

                        for (int j = row.FirstCellNum; j <= cellCount; j++)
                        {
                            try
                            {
                                if (row.GetCell(j) != null)
                                {
                                    switch (row.GetCell(j).CellType)
                                    {
                                        case CellType.STRING:
                                            string str = row.GetCell(j).StringCellValue;
                                            if (str != null && str.Length > 0)
                                            {
                                                dataRow[j] = str.ToString();
                                            }
                                            else
                                            {
                                                dataRow[j] = null;
                                            }
                                            break;
                                        case CellType.NUMERIC:
                                            if (DateUtil.IsCellDateFormatted(row.GetCell(j)))
                                            {
                                                dataRow[j] = DateTime.FromOADate(row.GetCell(j).NumericCellValue);
                                            }
                                            else
                                            {
                                                dataRow[j] = Convert.ToDouble(row.GetCell(j).NumericCellValue);
                                            }
                                            break;
                                        case CellType.BOOLEAN:
                                            dataRow[j] = Convert.ToString(row.GetCell(j).BooleanCellValue);
                                            break;
                                        case CellType.ERROR:
                                            dataRow[j] = ErrorEval.GetText(row.GetCell(j).ErrorCellValue);
                                            break;
                                        case CellType.FORMULA:
                                            switch (row.GetCell(j).CachedFormulaResultType)
                                            {
                                                case CellType.STRING:
                                                    string strFORMULA = row.GetCell(j).StringCellValue;
                                                    if (strFORMULA != null && strFORMULA.Length > 0)
                                                    {
                                                        dataRow[j] = strFORMULA.ToString();
                                                    }
                                                    else
                                                    {
                                                        dataRow[j] = null;
                                                    }
                                                    break;
                                                case CellType.NUMERIC:
                                                    dataRow[j] = Convert.ToString(row.GetCell(j).NumericCellValue);
                                                    break;
                                                case CellType.BOOLEAN:
                                                    dataRow[j] = Convert.ToString(row.GetCell(j).BooleanCellValue);
                                                    break;
                                                case CellType.ERROR:
                                                    dataRow[j] = ErrorEval.GetText(row.GetCell(j).ErrorCellValue);
                                                    break;
                                                default:
                                                    dataRow[j] = "";
                                                    break;
                                            }
                                            break;
                                        default:
                                            dataRow[j] = "";
                                            break;
                                    }
                                }
                            }
                            catch (Exception exception)
                            {
                                wl.WriteLogs(exception.ToString());
                            }
                        }
                        table.Rows.Add(dataRow);
                    }
                    catch (Exception exception)
                    {
                        wl.WriteLogs(exception.ToString());
                    }
                }
            }
            catch (Exception exception)
            {
                wl.WriteLogs(exception.ToString());
            }
            return table;
        }

        #endregion


        public static void InsertSheet(string outputFile, string sheetname, DataTable dt)
        {
            FileStream readfile = new FileStream(outputFile, FileMode.Open, FileAccess.Read);
            IWorkbook hssfworkbook = WorkbookFactory.Create(readfile);
            //HSSFWorkbook hssfworkbook = new HSSFWorkbook(readfile);
            int num = hssfworkbook.GetSheetIndex(sheetname);
            ISheet sheet1;
            if (num >= 0)
                sheet1 = hssfworkbook.GetSheet(sheetname);
            else
            {
                sheet1 = hssfworkbook.CreateSheet(sheetname);
            }


            try
            {
                if (sheet1.GetRow(0) == null)
                {
                    sheet1.CreateRow(0);
                }
                for (int coluid = 0; coluid < dt.Columns.Count; coluid++)
                {
                    if (sheet1.GetRow(0).GetCell(coluid) == null)
                    {
                        sheet1.GetRow(0).CreateCell(coluid);
                    }

                    sheet1.GetRow(0).GetCell(coluid).SetCellValue(dt.Columns[coluid].ColumnName);
                }
            }
            catch (Exception ex)
            {
                wl.WriteLogs(ex.ToString());
                throw;
            }


            for (int i = 1; i <= dt.Rows.Count; i++)
            {
                try
                {
                    if (sheet1.GetRow(i) == null)
                    {
                        sheet1.CreateRow(i);
                    }
                    for (int coluid = 0; coluid < dt.Columns.Count; coluid++)
                    {
                        if (sheet1.GetRow(i).GetCell(coluid) == null)
                        {
                            sheet1.GetRow(i).CreateCell(coluid);
                        }

                        sheet1.GetRow(i).GetCell(coluid).SetCellValue(dt.Rows[i - 1][coluid].ToString());
                    }
                }
                catch (Exception ex)
                {
                    wl.WriteLogs(ex.ToString());
                    //throw;
                }
            }
            try
            {
                readfile.Close();

                FileStream writefile = new FileStream(outputFile, FileMode.OpenOrCreate, FileAccess.Write);
                hssfworkbook.Write(writefile);
                writefile.Close();
            }
            catch (Exception ex)
            {
                wl.WriteLogs(ex.ToString());
            }
        }

        #region ����excel�е�����
        /// <summary>
        /// ����Excel���
        /// </summary>
        /// <param name="outputFile">����µ�excel���·��</param>
        /// <param name="sheetname">sheet��</param>
        /// <param name="updateData">����µ�����</param>
        /// <param name="coluid">����µ��к�</param>
        /// <param name="rowid">����µĿ�ʼ�к�</param>
        public static void UpdateExcel(string outputFile, string sheetname, string[] updateData, int coluid, int rowid)
        {
            //FileStream readfile = new FileStream(outputFile, FileMode.Open, FileAccess.Read);
            IWorkbook hssfworkbook = WorkbookFactory.Create(outputFile);
            //HSSFWorkbook hssfworkbook = new HSSFWorkbook(readfile);
            ISheet sheet1 = hssfworkbook.GetSheet(sheetname);
            for (int i = 0; i < updateData.Length; i++)
            {
                try
                {
                    if (sheet1.GetRow(i + rowid) == null)
                    {
                        sheet1.CreateRow(i + rowid);
                    }
                    if (sheet1.GetRow(i + rowid).GetCell(coluid) == null)
                    {
                        sheet1.GetRow(i + rowid).CreateCell(coluid);
                    }

                    sheet1.GetRow(i + rowid).GetCell(coluid).SetCellValue(updateData[i]);
                }
                catch (Exception ex)
                {
                    wl.WriteLogs(ex.ToString());
                    throw;
                }
            }
            try
            {
                //readfile.Close();
                FileStream writefile = new FileStream(outputFile, FileMode.OpenOrCreate, FileAccess.Write);
                hssfworkbook.Write(writefile);
                writefile.Close();
            }
            catch (Exception ex)
            {
                wl.WriteLogs(ex.ToString());
            }

        }

        /// <summary>
        /// ����Excel���
        /// </summary>
        /// <param name="outputFile">����µ�excel���·��</param>
        /// <param name="sheetname">sheet��</param>
        /// <param name="updateData">����µ�����</param>
        /// <param name="coluids">����µ��к�</param>
        /// <param name="rowid">����µĿ�ʼ�к�</param>
        public static void UpdateExcel(string outputFile, string sheetname, string[][] updateData, int[] coluids, int rowid)
        {
            FileStream readfile = new FileStream(outputFile, FileMode.Open, FileAccess.Read);

            HSSFWorkbook hssfworkbook = new HSSFWorkbook(readfile);
            readfile.Close();
            ISheet sheet1 = hssfworkbook.GetSheet(sheetname);
            for (int j = 0; j < coluids.Length; j++)
            {
                for (int i = 0; i < updateData[j].Length; i++)
                {
                    try
                    {
                        if (sheet1.GetRow(i + rowid) == null)
                        {
                            sheet1.CreateRow(i + rowid);
                        }
                        if (sheet1.GetRow(i + rowid).GetCell(coluids[j]) == null)
                        {
                            sheet1.GetRow(i + rowid).CreateCell(coluids[j]);
                        }
                        sheet1.GetRow(i + rowid).GetCell(coluids[j]).SetCellValue(updateData[j][i]);
                    }
                    catch (Exception ex)
                    {
                        wl.WriteLogs(ex.ToString());
                    }
                }
            }
            try
            {
                FileStream writefile = new FileStream(outputFile, FileMode.Create);
                hssfworkbook.Write(writefile);
                writefile.Close();
            }
            catch (Exception ex)
            {
                wl.WriteLogs(ex.ToString());
            }
        }

        /// <summary>
        /// ����Excel���
        /// </summary>
        /// <param name="outputFile">����µ�excel���·��</param>
        /// <param name="sheetname">sheet��</param>
        /// <param name="updateData">����µ�����</param>
        /// <param name="coluid">����µ��к�</param>
        /// <param name="rowid">����µĿ�ʼ�к�</param>
        public static void UpdateExcel(string outputFile, string sheetname, double[] updateData, int coluid, int rowid)
        {
            FileStream readfile = new FileStream(outputFile, FileMode.Open, FileAccess.Read);

            HSSFWorkbook hssfworkbook = new HSSFWorkbook(readfile);
            ISheet sheet1 = hssfworkbook.GetSheet(sheetname);
            for (int i = 0; i < updateData.Length; i++)
            {
                try
                {
                    if (sheet1.GetRow(i + rowid) == null)
                    {
                        sheet1.CreateRow(i + rowid);
                    }
                    if (sheet1.GetRow(i + rowid).GetCell(coluid) == null)
                    {
                        sheet1.GetRow(i + rowid).CreateCell(coluid);
                    }

                    sheet1.GetRow(i + rowid).GetCell(coluid).SetCellValue(updateData[i]);
                }
                catch (Exception ex)
                {
                    wl.WriteLogs(ex.ToString());
                    throw;
                }
            }
            try
            {
                readfile.Close();
                FileStream writefile = new FileStream(outputFile, FileMode.Create, FileAccess.Write);
                hssfworkbook.Write(writefile);
                writefile.Close();
            }
            catch (Exception ex)
            {
                wl.WriteLogs(ex.ToString());
            }

        }

        /// <summary>
        /// ����Excel���
        /// </summary>
        /// <param name="outputFile">����µ�excel���·��</param>
        /// <param name="sheetname">sheet��</param>
        /// <param name="updateData">����µ�����</param>
        /// <param name="coluids">����µ��к�</param>
        /// <param name="rowid">����µĿ�ʼ�к�</param>
        public static void UpdateExcel(string outputFile, string sheetname, double[][] updateData, int[] coluids, int rowid)
        {
            FileStream readfile = new FileStream(outputFile, FileMode.Open, FileAccess.Read);

            HSSFWorkbook hssfworkbook = new HSSFWorkbook(readfile);
            readfile.Close();
            ISheet sheet1 = hssfworkbook.GetSheet(sheetname);
            for (int j = 0; j < coluids.Length; j++)
            {
                for (int i = 0; i < updateData[j].Length; i++)
                {
                    try
                    {
                        if (sheet1.GetRow(i + rowid) == null)
                        {
                            sheet1.CreateRow(i + rowid);
                        }
                        if (sheet1.GetRow(i + rowid).GetCell(coluids[j]) == null)
                        {
                            sheet1.GetRow(i + rowid).CreateCell(coluids[j]);
                        }
                        sheet1.GetRow(i + rowid).GetCell(coluids[j]).SetCellValue(updateData[j][i]);
                    }
                    catch (Exception ex)
                    {
                        wl.WriteLogs(ex.ToString());
                    }
                }
            }
            try
            {
                FileStream writefile = new FileStream(outputFile, FileMode.Create);
                hssfworkbook.Write(writefile);
                writefile.Close();
            }
            catch (Exception ex)
            {
                wl.WriteLogs(ex.ToString());
            }
        }

        #endregion

        public static int GetSheetNumber(string outputFile)
        {
            int number = 0;
            try
            {
                FileStream readfile = new FileStream(outputFile, FileMode.Open, FileAccess.Read);

                HSSFWorkbook hssfworkbook = new HSSFWorkbook(readfile);
                number = hssfworkbook.NumberOfSheets;

            }
            catch (Exception exception)
            {
                wl.WriteLogs(exception.ToString());
            }
            return number;
        }

        public static ArrayList GetSheetName(string outputFile)
        {
            ArrayList arrayList = new ArrayList();
            try
            {
                FileStream readfile = new FileStream(outputFile, FileMode.Open, FileAccess.Read);

                HSSFWorkbook hssfworkbook = new HSSFWorkbook(readfile);
                for (int i = 0; i < hssfworkbook.NumberOfSheets; i++)
                {
                    arrayList.Add(hssfworkbook.GetSheetName(i));
                }
            }
            catch (Exception exception)
            {
                wl.WriteLogs(exception.ToString());
            }
            return arrayList;
        }

        public static bool isNumeric(String message, out double result)
        {
            Regex rex = new Regex(@"^[-]?\d+[.]?\d*$");
            result = -1;
            if (rex.IsMatch(message))
            {
                result = double.Parse(message);
                return true;
            }
            else
                return false;

        }
    }
}
