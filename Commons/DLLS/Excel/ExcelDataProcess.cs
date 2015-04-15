using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;

namespace Commons.Data
{
  public class ExcelDataProcess
    {
        /*********************************************************** 
         * 选择Excel文件，并从中读取数据
         *
         *  iStartRow：开始行
         *  iStartCol：开始列
         ****** *****************************************************/
        //public System.Array  getExcelDataFromFile(int iStartRow, int iStartCol)
        //{
        //    string sFileName;

        //    sFileName = getFile();

        //    return getExcelDataArea(sFileName, iStartRow, iStartCol);

        //}

       
       public DataTable getExcelDataFromFile(int iStartRow, int iStartCol)
       {
           string sFileName;

           sFileName = getFile();

           return getExcelDataArea(sFileName, iStartRow, iStartCol);

       }
       /* 
         * 选择Excel文件
         */
        private string getFile()
        {
            string fName;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "c:\\";//注意这里写路径时要用c:\\而不是c:\  
            openFileDialog.Filter = "文本文件|*.*|C#文件|*.cs|所有文件|*.*";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                fName = openFileDialog.FileName;
                return fName;
            }
            else
            {
                return null;
            }
        }

        /*********************************************************** 
         * 从Excel文件中读取数据
         * 
         *  filePath：文件名
         *  iStartRow：开始行
         *  iStartCol：开始列
         ***********************************************************/
        //public System.Array getExcelDataArea(string filePath, int iStartRow, int iStartCol)
        //{

        //    ExcelApp app = null;

        //    try
        //    {
        //        DataTable dtExcel = new DataTable();
        //        //Open  Excel 文件
        //        app = new ExcelApp();
        //        app.Visible = false;
        //        app.Workbooks.Open(filePath);
                
        //        //获得Sheet
        //        Worksheets sheets = app.Workbooks[1].Worksheets;
        //        Worksheet datasheet = sheets[1];

        //        if (null == datasheet)
        //        {
        //            return null;
        //        }

        //        //check rows，假定第一列不能为空
        //        int iRow = iStartRow - 1;
        //        int iRows;

        //        do
        //        {
        //            iRow = iRow + 1;
        //        }
        //        while (datasheet[iRow, iStartCol].Value != null);
        //        iRows = iRow - 1;

        //        //check Cols，假定第一行不能为空
        //        int iCol = iStartCol - 1;
        //        int iCols;

        //        do
        //        {
        //            iCol = iCol + 1;

        //        }
        //        while (datasheet[iStartRow, iCol].Value != null);
        //        iCols = iCol - 1;

        //        //读取有数据的范围
        //        Range range = datasheet[iStartRow, iStartCol, iRows, iCols];

        //        Array values = null;
        //        values = (System.Array)range.Value;
        //        //if (values.GetLength(0)>0)
        //        //{
        //        //    for (int i = 1; i < values.GetLength(0);i++ )
        //        //    {
        //        //        DataRow dr = dtExcel.NewRow();
        //        //        int j = 1;
        //        //        foreach (object obj in values[i])
        //        //        {
        //        //            dr[i][j] = obj;
        //        //            j++;
        //        //        }
        //        //        dtExcel.Rows.Add(dr);
        //        //    }
        //        //}
                
        //        //dtExcel.NewRow()
                
        //        //    stest= values.GetValue(i,1).ToString();

        //        app.Quit();
        //        //return dtExcel;
        //       // DataRow drSource = m_dtMaterielDistributionBasicInfor.NewRow();
        //        //返回获得的数据
        //         return values;
        //    }
        //    catch (System.Exception ex)
        //    {
        //        //throw;
        //        app.Quit();
        //        return null;

        //    }
        //}
      //
      public DataTable  getExcelDataArea(string filePath, int iStartRow, int iStartCol)
      {

          ExcelApp app = null;

          try
          {
              DataTable dtExcel = new DataTable();
              //Open  Excel 文件
              app = new ExcelApp();
              app.Visible = false;
              app.Workbooks.Open(filePath);
              DataRow dr = null;
              //获得Sheet
              Worksheets sheets = app.Workbooks[1].Worksheets;
              Worksheet datasheet = sheets[1];

              if (null == datasheet)
              {
                  return null;
              }

              //check rows，假定第一列不能为空
              int iRow = iStartRow - 1;
              int iRows;

              do
              {
                  iRow = iRow + 1;
              }
              while (datasheet[iRow, iStartCol].Value != null);
              iRows = iRow - 1;

              //check Cols，假定第一行不能为空
              int iCol = iStartCol - 1;
              int iCols;

              do
              {
                  iCol = iCol + 1;

              }
              while (datasheet[iStartRow, iCol].Value != null);
              iCols = iCol - 1;

              //读取有数据的范围
              Range range = datasheet[iStartRow, iStartCol, iRows, iCols];

              Array values = null;
              values = (System.Array)range.Value;
              if (values.GetLength(0) > 0)
              {
                  for (int i = 1; i <= values.GetLength(0); i++)
                  {
                      if (values.GetLength(1)>0)
                      {
                          //m_dtMaterielDistributionBasicInfor.Columns.Add("物料编码", System.Type.GetType("System.String"));
                          if (i>1)
                          {
                              dr = dtExcel.NewRow();
                          }
                          
                          for (int j = 1; j <=values.GetLength(1);j++)
                          {
                              if (i==1)
                              {
                                  dtExcel.Columns.Add(Convert.ToString(values.GetValue(i, j)), System.Type.GetType("System.String"));
                              } 
                              else
                              {
                                  //DataRow dr = dtExcel.NewRow();
                                   if (values.GetValue(i, j)!= null)
                                   {
                                       dr[j-1] = values.GetValue(i, j);
                                   }
                              }
                            
                          }
                          if (i>1)
                          {
                              dtExcel.Rows.Add(dr);
                          }
                      }
                  }
              }
              //if (values.GetLength(0)>0)
              //{
              //    for (int i = 1; i < values.GetLength(0);i++ )
              //    {
              //        DataRow dr = dtExcel.NewRow();
              //        int j = 1;
              //        foreach (object obj in values[i])
              //        {
              //            dr[i][j] = obj;
              //            j++;
              //        }
              //        dtExcel.Rows.Add(dr);
              //    }
              //}

              //dtExcel.NewRow()

              //    stest= values.GetValue(i,1).ToString();

              app.Quit();
              return dtExcel;
              // DataRow drSource = m_dtMaterielDistributionBasicInfor.NewRow();
              //返回获得的数据
              //return values;
          }
          catch (System.Exception ex)
          {
              //throw;
              app.Quit();
              return null;

          }
      }
    }
}
