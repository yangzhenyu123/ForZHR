using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExcelTools.Handle
{
    public class NPOIHandle
    {
        /// <summary>
        /// 将excel导入到datatable
        /// </summary>
        /// <param name="filePath">excel路径</param>
        /// <param name="isColumnName">第一行是否是列名</param>
        /// <returns>返回datatable</returns>
        public static DataTable ExcelFirstSheetToDataTable(string filePath, bool isColumnName)
        {
            DataTable dataTable = null;
            FileStream fs = null;
            DataColumn column = null;
            DataRow dataRow = null;
            IWorkbook workbook = null;
            ISheet sheet = null;
            IRow row = null;
            ICell cell = null;
            int startRow = 0;
            try
            {
                using (fs = File.OpenRead(filePath))
                {
                    // 2007版本
                    if (filePath.IndexOf(".xlsx") > 0)
                        workbook = new XSSFWorkbook(fs);
                    // 2003版本
                    else if (filePath.IndexOf(".xls") > 0)
                        workbook = new HSSFWorkbook(fs);

                    if (workbook != null)
                    {

                        sheet = workbook.GetSheetAt(0);//读取第一个sheet，当然也可以循环读取每个sheet
                        dataTable = new DataTable();
                        if (sheet != null)
                        {
                            int rowCount = sheet.LastRowNum;//总行数
                            if (rowCount > 0)
                            {
                                IRow firstRow = sheet.GetRow(0);//第一行
                                int cellCount = firstRow.LastCellNum;//列数

                                //构建datatable的列
                                if (isColumnName)
                                {
                                    startRow = 1;//如果第一行是列名，则从第二行开始读取
                                    for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                                    {
                                        Application.DoEvents();
                                        cell = firstRow.GetCell(i);
                                        if (cell != null)
                                        {
                                            if (cell.StringCellValue != null)
                                            {
                                                column = new DataColumn(cell.StringCellValue);
                                                dataTable.Columns.Add(column);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                                    {
                                        column = new DataColumn("column" + (i + 1));
                                        dataTable.Columns.Add(column);
                                    }
                                }

                                //填充行
                                for (int i = startRow; i <= rowCount; ++i)
                                {
                                    Application.DoEvents();

                                    row = sheet.GetRow(i);
                                    if (row == null) continue;

                                    dataRow = dataTable.NewRow();
                                    for (int j = row.FirstCellNum; j < cellCount; ++j)
                                    {
                                        cell = row.GetCell(j);
                                        if (cell == null)
                                        {
                                            dataRow[j] = "";
                                        }
                                        else
                                        {
                                            //CellType(Unknown = -1,Numeric = 0,String = 1,Formula = 2,Blank = 3,Boolean = 4,Error = 5,)
                                            switch (cell.CellType)
                                            {
                                                case CellType.Blank:
                                                    dataRow[j] = "";
                                                    break;
                                                case CellType.Numeric:
                                                    short format = cell.CellStyle.DataFormat;
                                                    //对时间格式（2015.12.5、2015/12/5、2015-12-5等）的处理
                                                    if (format == 14 || format == 31 || format == 57 || format == 58)
                                                        dataRow[j] = cell.DateCellValue;
                                                    else
                                                        dataRow[j] = cell.NumericCellValue;
                                                    break;
                                                case CellType.String:
                                                    dataRow[j] = cell.StringCellValue;
                                                    break;
                                            }
                                        }
                                    }
                                    dataTable.Rows.Add(dataRow);
                                }
                            }
                        }
                    }
                }
                return dataTable;
            }
            catch (Exception)
            {
                if (fs != null)
                {
                    fs.Close();
                }
                return null;
            }
        }


        /// <summary>
        /// 将excel导入到datatable，全部sheet
        /// </summary>
        /// <param name="filePath">excel路径</param>
        /// <param name="isColumnName">第一行是否是列名</param>
        /// <returns>返回datatable</returns>
        public static List<DataTable> ExcelAllSheetToDataTable(string filePath, bool isColumnName, ref StringBuilder errMsg)
        {
            List<DataTable> dataTables = new List<DataTable>();
            FileStream fs = null;
            IWorkbook workbook = null;
            ISheet sheet = null;
            string sheetName = "";
            try
            {

                using (fs = File.OpenRead(filePath))
                {
                    // 2007版本
                    if (filePath.IndexOf(".xlsx") > 0)
                        workbook = new XSSFWorkbook(fs);
                    // 2003版本
                    else if (filePath.IndexOf(".xls") > 0)
                        workbook = new HSSFWorkbook(fs);

                    if (workbook != null)
                    {
                        int sheetCount = workbook.NumberOfSheets;
                        for (int sheetIndex = 0; sheetIndex < sheetCount; sheetIndex++)
                        {

                            try
                            {

                                Application.DoEvents();
                                sheet = workbook.GetSheetAt(sheetIndex);//读取第一个sheet，当然也可以循环读取每个shee
                                sheetName = sheet.SheetName;
                                if (!MergeExcelHandle.VerifySheetName(sheet.SheetName.ToLower()))
                                {
                                    continue;
                                }
                                DataTable dataTable = new DataTable();
                                DataColumn column = null;
                                DataRow dataRow = null;
                                IRow row = null;
                                ICell cell = null;
                                int startRow = 0;

                                if (sheet != null)
                                {
                                    int rowCount = sheet.LastRowNum;//总行数
                                    if (rowCount > 0)
                                    {
                                        Application.DoEvents();
                                        IRow firstRow = sheet.GetRow(0);//第一行
                                        int cellCount = firstRow.LastCellNum;//列数

                                        //构建datatable的列
                                        if (isColumnName)
                                        {
                                            //startRow = 1;//如果第一行是列名，则从第二行开始读取
                                            //for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                                            //{
                                            //    Application.DoEvents();
                                            //    cell = firstRow.GetCell(i);
                                            //    if (cell != null)
                                            //    {
                                            //        if (cell.StringCellValue != null)
                                            //        {
                                            //            column = new DataColumn(cell.StringCellValue);
                                            //            dataTable.Columns.Add(column);
                                            //        }
                                            //    }
                                            //}

                                            //有可能列名称重复，此处也不关注列名，所以就直接列名设置  column+i了
                                            for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                                            {
                                                Application.DoEvents();
                                                column = new DataColumn("column" + (i + 1));
                                                dataTable.Columns.Add(column);
                                            }

                                        }
                                        else
                                        {
                                            for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                                            {
                                                Application.DoEvents();
                                                column = new DataColumn("column" + (i + 1));
                                                dataTable.Columns.Add(column);
                                            }
                                        }

                                        //填充行
                                        for (int i = startRow; i <= rowCount; ++i)
                                        {
                                            Application.DoEvents();

                                            row = sheet.GetRow(i);
                                            if (row == null) continue;

                                            dataRow = dataTable.NewRow();
                                            for (int j = row.FirstCellNum; j < cellCount; ++j)
                                            {
                                                Application.DoEvents();
                                                cell = row.GetCell(j);
                                                if (cell == null)
                                                {
                                                    dataRow[j] = "";
                                                }
                                                else
                                                {
                                                    //CellType(Unknown = -1,Numeric = 0,String = 1,Formula = 2,Blank = 3,Boolean = 4,Error = 5,)
                                                    switch (cell.CellType)
                                                    {
                                                        case CellType.Blank:
                                                            dataRow[j] = "";
                                                            break;
                                                        case CellType.Numeric:
                                                            short format = cell.CellStyle.DataFormat;
                                                            //对时间格式（2015.12.5、2015/12/5、2015-12-5等）的处理
                                                            if (format == 14 || format == 31 || format == 57 || format == 58)
                                                                dataRow[j] = cell.DateCellValue;
                                                            else
                                                                dataRow[j] = cell.NumericCellValue;
                                                            break;
                                                        case CellType.String:
                                                            dataRow[j] = cell.StringCellValue;
                                                            break;
                                                    }
                                                }
                                            }
                                            dataTable.Rows.Add(dataRow);
                                        }
                                    }
                                }

                                dataTables.Add(dataTable);

                            }
                            catch (Exception ex)
                            {
                                errMsg.Append("," + filePath + "--" + sheetName);
                                continue;
                            }
                        }
                    }
                }
                return dataTables;
            }
            catch (Exception ex)
            {
                if (fs != null)
                {
                    fs.Close();
                }
                return dataTables;
            }
        }



        /// <summary>
        /// Datable导出成Excel
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="file">导出路径(包括文件名与扩展名)</param>
        public static void TableToExistExcel(DataTable dt, string file)
        {
            IWorkbook workbook = null;
            string fileExt = Path.GetExtension(file).ToLower();
            FileStream fs1 = null;
            using (fs1 = File.OpenRead(file))
            {
                // 2007版本
                if (fileExt == ".xlsx")
                    workbook = new XSSFWorkbook(fs1);
                // 2003版本
                else if (fileExt == ".xls")
                    workbook = new HSSFWorkbook(fs1);
            }
            if (workbook == null) { return; }
            ISheet sheet = workbook.GetSheetAt(0);

            ////表头
            //IRow row = sheet.CreateRow(0);
            //for (int i = 0; i < dt.Columns.Count; i++)
            //{
            //    Application.DoEvents();
            //    ICell cell = row.CreateCell(i);
            //    cell.SetCellValue(dt.Columns[i].ColumnName);
            //}

            //数据
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Application.DoEvents();
                IRow row1 = sheet.CreateRow(i + 1);
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    Application.DoEvents();
                    ICell cell = row1.CreateCell(j);
                    cell.SetCellValue(dt.Rows[i][j].ToString());
                }
            }

            //转为字节数组
            MemoryStream stream = new MemoryStream();
            workbook.Write(stream);
            var buf = stream.ToArray();

            //保存为Excel文件
            using (FileStream fs = new FileStream(file, FileMode.OpenOrCreate, FileAccess.Write))
            {
                fs.Write(buf, 0, buf.Length);
                fs.Flush();
            }
            Application.DoEvents();
        }


        /// <summary>
        /// 获取单元格类型
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        private static object GetValueType(ICell cell)
        {
            if (cell == null)
                return null;
            switch (cell.CellType)
            {
                case CellType.Blank: //BLANK:
                    return null;
                case CellType.Boolean: //BOOLEAN:
                    return cell.BooleanCellValue;
                case CellType.Numeric: //NUMERIC:
                    return cell.NumericCellValue;
                case CellType.String: //STRING:
                    return cell.StringCellValue;
                case CellType.Error: //ERROR:
                    return cell.ErrorCellValue;
                case CellType.Formula: //FORMULA:
                default:
                    return "=" + cell.CellFormula;

            }
        }

    }
}
