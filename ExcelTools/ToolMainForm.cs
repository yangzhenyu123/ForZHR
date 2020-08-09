using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using ExcelTools.Handle;

namespace ExcelTools
{
    public partial class ToolMainForm : Form
    {

        public static bool ProcessorStartFlag = false;
        public static bool ProcessorFinishFlag = false;
        public List<string> excelPathList = new List<string>();


        public ToolMainForm()
        {
            InitializeComponent();
            timer1.Start();
        }

        private void btn_chooseFolder_Click(object sender, EventArgs e)
        {
            excelPathList = new List<string>();
           
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txt_folderPath.Text = folderBrowserDialog1.SelectedPath;

                string path = txt_folderPath.Text;
                DirectoryInfo dic = new DirectoryInfo(path);
                var files = dic.GetFiles("*.xls");
                if (files != null)
                {
                    StringBuilder strb = new StringBuilder();
                    foreach (var item in files)
                    {
                        excelPathList.Add(item.FullName);
                        strb.Append(item.FullName + "\r\n");
                    }
                    txt_excelList.Text = strb.ToString();
                }
                ClearProcessorBar();
                

            }
        }

        private void btn_startMerge_Click(object sender, EventArgs e)
        {
            try
            {

                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    string resultFilePath = folderBrowserDialog1.SelectedPath.TrimEnd('\\') + "\\合并结果" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

                    if (excelPathList != null && excelPathList.Count > 0)
                    {
                        SetProcessorBarState(false);
                        ShowResultToTxt("");
                        Application.DoEvents();
                        string msg = MergeExcelHandle.MergeExcel(this, excelPathList, resultFilePath);
                        DealMergeExcelMsg(msg);
                    }
                    Application.DoEvents();
                    SetProcessorBarState(true);
                    MessageBox.Show("执行完成");

                }
                else
                {
                    MessageBox.Show("请选择保存结果的文件夹");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("程序异常，请联系开发者排查");
                SetProcessorBarState(true);
                ClearProcessorBar();
                return;
            }



        }


        private void DealMergeExcelMsg(string msg)
        {
            if (!string.IsNullOrEmpty(msg))
            {
                string[] arr = msg.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                if (arr != null && arr.Length > 0)
                {
                    string resultTxt = "处理如下sheet出现问题，请手动核验：\r\n" + msg.Replace(",", "\r\n");
                    ShowResultToTxt(resultTxt);
                }
            }
        }

        internal void ShowResultToTxt(string value)
        {
            txt_result.Text = value;
        }



        #region 辅助功能

        private void txt_excelList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.A)
            {
                ((TextBox)sender).SelectAll();
            }
        }

        private void txt_result_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.A)
            {
                ((TextBox)sender).SelectAll();
            }
        }

        private void ToolMainForm_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (ProcessorStartFlag)
            {
                if (processBar1.Value < processBar1.Maximum)
                {
                    Application.DoEvents();
                    processBar1.Value = processBar1.Value + 1;
                }
                else
                {
                    Application.DoEvents();
                    processBar1.Value = 0;
                }
            }
            if (ProcessorFinishFlag)
            {
                Application.DoEvents();
                processBar1.Value = processBar1.Maximum;
            }
        }


        internal void SetProcessorBarState(bool finish)
        {
            ProcessorStartFlag = !finish;
            ProcessorFinishFlag = finish;
        }

        internal void ClearProcessorBar()
        {
            ProcessorStartFlag = false;
            ProcessorFinishFlag = false;
            processBar1.Value = 0;
        }

        #endregion


    }
}
