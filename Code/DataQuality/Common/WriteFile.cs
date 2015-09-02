﻿using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace DataQuality.Common
{
    /// <summary>
    /// 写入文件，将错误信息写入doc/excel/dataGridView中
    /// </summary>
    class WriteFile
    {
        /// <summary>
        /// 向DataGridView中写入检查出的错误数据
        /// </summary>
        /// <param name="resultList">错误数据列表</param>
        /// <param name="dgv">要写入的目标DataGridView</param>
        /// <returns>是否写入成功</returns>
        public bool WriteDataGridView(List<ResultEntity> resultList,DataGridView dgv)
        {
            //如果错误数据列表条目大于0，向dgv写入数据
            if(resultList.Count>0)
            {
                dgv.Rows.Add(resultList.Count);
                for(int i=0;i<resultList.Count;i++)
                {
                    ResultEntity re=resultList[i];
                    dgv.Rows[i].Cells["cmXH"].Value = (i+1).ToString();
                    dgv.Rows[i].Cells["cmCGMC"].Value=re.Cgmc;
                    dgv.Rows[i].Cells["cmGZLX"].Value = re.Gzlx;
                    dgv.Rows[i].Cells["cmGZBH"].Value = re.Gzbh;
                    dgv.Rows[i].Cells["cmGZMC"].Value = re.Gzmc;
                    dgv.Rows[i].Cells["cmCWMS"].Value = re.Cwms;
                    dgv.Rows[i].Cells["cmHH"].Value = re.Hh;
                    dgv.Rows[i].Cells["cmCWDJ"].Value = re.Cwdj;
                    dgv.Rows[i].Cells["cmJCRQ"].Value = re.Jcrq;
                }
            }
            return false;
        }
        /// <summary>
        /// 向word文档写入检查出的错误数据
        /// </summary>
        /// <param name="resultList">错误数据列表</param>
        /// <param name="path">要写入的目标doc文档路径</param>
        /// <returns></returns>
        public bool WriteDoc(List<ResultEntity> resultList,string path)
        {
            return false;
        }

        /// <summary>
        /// 向excel文档写入检查出的错误数据
        /// </summary>
        /// <param name="resultList">错误数据列表</param>
        /// <param name="path">要写入的目标excel文档路径</param>
        /// <returns></returns>
        public bool WriteXls(string path)
        {
            Microsoft.Office.Interop.Excel.Application excelApp = null;//引用excel对象
            Microsoft.Office.Interop.Excel.Workbook workBook = null;//引用工作簿
            Microsoft.Office.Interop.Excel.Worksheet sheetMuLu = null;//引用工作表-目录完整性
            try
            {
                excelApp = new Microsoft.Office.Interop.Excel.Application();
                workBook = excelApp.Workbooks.Open(ComMsg.xlsPath, Missing.Value, Missing.Value,
                    Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                    Missing.Value, Missing.Value);
                //成果目录相关
                sheetMuLu = (Worksheet)workBook.Worksheets[1];
                var mulu = from p in ComMsg.ResultShow
                           where p.Gzlx.Equals("数据完整性")
                           select p;
                List<ResultEntity> muluList = mulu.ToList();
                for (int i = 0; i < muluList.Count; i++)
                {
                    sheetMuLu.Cells[1][i + 2] = (i + 1).ToString();//第一列，第(i+2)行
                    sheetMuLu.Cells[2][i + 2] = muluList[i].Cgmc;
                    sheetMuLu.Cells[3][i + 2] = muluList[i].Gzlx;
                    sheetMuLu.Cells[4][i + 2] = muluList[i].Gzbh;
                    sheetMuLu.Cells[5][i + 2] = muluList[i].Gzmc;
                    sheetMuLu.Cells[6][i + 2] = muluList[i].Cwms;
                    sheetMuLu.Cells[7][i + 2] = muluList[i].Hh;
                    sheetMuLu.Cells[8][i + 2] = muluList[i].Cwdj;
                    sheetMuLu.Cells[9][i + 2] = muluList[i].Jcrq;
                }
                /***************************此处预留其它检查类型******************************************/



                /*********************************************************************/

                //保存写入的数据
                sheetMuLu.SaveAs(ComMsg.xlsPath, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                excelApp.Quit();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return true;
        }
    }
}