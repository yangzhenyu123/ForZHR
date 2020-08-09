using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExcelTools.Handle
{
    internal class MergeExcelHandle
    {
        /// <summary>
        /// 获取用户复制为结果excel的模板
        /// </summary>
        /// <returns></returns>
        private static string GetCopyModelExcelPath()
        {
            return Application.StartupPath.TrimEnd('\\') + '\\' + "合并模板.xlsx";
        }

        /// <summary>
        /// 获取读取table结构的excel模板
        /// </summary>
        /// <returns></returns>
        private static string GetTableModelExcelPath()
        {
            return Application.StartupPath.TrimEnd('\\') + '\\' + "合并Row模板.xlsx";
        }

        public static string MergeExcel(ToolMainForm mainForm, List<string> excelPathList, string resultFilePath)
        {
            StringBuilder msg = new StringBuilder();

            string modelExcelPath = GetCopyModelExcelPath();
            System.IO.File.Copy(modelExcelPath, resultFilePath);
            modelExcelPath = GetTableModelExcelPath();
            DataTable resultTable = BulidResultTable(modelExcelPath);
            foreach (var item in excelPathList)
            {
                Application.DoEvents();
                List<DataTable> tempTables = GetTableListFromExcel(mainForm, item, ref msg);
                AppendDataToResultTable(resultTable, tempTables);
            }
            NPOIHandle.TableToExistExcel(resultTable, resultFilePath);
            return msg.ToString();
        }


        private static List<DataTable> GetTableListFromExcel(ToolMainForm mainForm, string excelPath, ref StringBuilder msg)
        {
            List<DataTable> dts = NPOIHandle.ExcelAllSheetToDataTable(excelPath, true, ref msg);
            return dts;
        }

        private static DataTable BulidResultTable(string modelExcelPath)
        {
            DataTable dt = NPOIHandle.ExcelFirstSheetToDataTable(modelExcelPath, true);
            dt.Rows.Clear();
            return dt;
        }


        private static void AppendDataToResultTable(DataTable resultTable, List<DataTable> tempTables)
        {
            if (tempTables == null || tempTables.Count <= 0)
            {
                return;
            }
            foreach (var tempTable in tempTables)
            {
                Application.DoEvents();

                int columnCount = GetColumnCount();
                columnCount = (columnCount > tempTable.Columns.Count ? tempTable.Columns.Count : columnCount);

                if (tempTable != null && tempTable.Rows != null && tempTable.Rows.Count > 0)
                {
                    int rowcount = tempTable.Rows.Count;
                    for (int i = 0; i < rowcount; i++)
                    {
                        Application.DoEvents();

                        DataRow tempRow = tempTable.Rows[i];
                        bool rowFlag = false;
                        //验证数据是否为空行
                        for (int j = 0; j < columnCount; j++)
                        {
                            Application.DoEvents();
                            if (tempRow[j] != null && tempRow[j].ToString().Length > 0)
                            {
                                rowFlag = true;
                            }
                        }

                        if (rowFlag)
                        {
                            DataRow resultRow = resultTable.NewRow();

                            //将数据添加到resultTable
                            for (int j = 0; j < columnCount; j++)
                            {
                                resultRow[j] = tempRow[j];
                                Application.DoEvents();
                            }
                            resultTable.Rows.Add(resultRow);
                        }
                    }

                }
            }
        }



        internal static int GetColumnCount()
        {
            int columnCount = 62;
            try
            {
                int.TryParse(ConfigurationManager.AppSettings["ColumnCount"], out columnCount);
            }
            catch
            {
            }
            return columnCount;
        }



        internal static List<string> SheetBackName()
        {
            List<string> backNames = new List<string>();
            try
            {
                backNames = ConfigurationManager.AppSettings["SheetBackName"].ToString().Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).ToList();
            }
            catch
            {
            }
            return backNames;
        }

        internal static bool VerifySheetName(string sheetName)
        {
            bool flag = true;
            try
            {
                sheetName = sheetName.ToLower();
                List<string> backNames = SheetBackName();
                foreach (var item in backNames)
                {
                    if (sheetName.Contains(item.ToLower()))
                    {
                        flag = false;
                        return flag;
                    }
                }
            }
            catch
            {
            }
            return flag;
        }

    }
}
