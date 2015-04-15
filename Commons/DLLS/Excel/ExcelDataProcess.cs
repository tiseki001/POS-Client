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
         * ѡ��Excel�ļ��������ж�ȡ����
         *
         *  iStartRow����ʼ��
         *  iStartCol����ʼ��
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
         * ѡ��Excel�ļ�
         */
        private string getFile()
        {
            string fName;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "c:\\";//ע������д·��ʱҪ��c:\\������c:\  
            openFileDialog.Filter = "�ı��ļ�|*.*|C#�ļ�|*.cs|�����ļ�|*.*";
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
         * ��Excel�ļ��ж�ȡ����
         * 
         *  filePath���ļ���
         *  iStartRow����ʼ��
         *  iStartCol����ʼ��
         ***********************************************************/
        //public System.Array getExcelDataArea(string filePath, int iStartRow, int iStartCol)
        //{

        //    ExcelApp app = null;

        //    try
        //    {
        //        DataTable dtExcel = new DataTable();
        //        //Open  Excel �ļ�
        //        app = new ExcelApp();
        //        app.Visible = false;
        //        app.Workbooks.Open(filePath);
                
        //        //���Sheet
        //        Worksheets sheets = app.Workbooks[1].Worksheets;
        //        Worksheet datasheet = sheets[1];

        //        if (null == datasheet)
        //        {
        //            return null;
        //        }

        //        //check rows���ٶ���һ�в���Ϊ��
        //        int iRow = iStartRow - 1;
        //        int iRows;

        //        do
        //        {
        //            iRow = iRow + 1;
        //        }
        //        while (datasheet[iRow, iStartCol].Value != null);
        //        iRows = iRow - 1;

        //        //check Cols���ٶ���һ�в���Ϊ��
        //        int iCol = iStartCol - 1;
        //        int iCols;

        //        do
        //        {
        //            iCol = iCol + 1;

        //        }
        //        while (datasheet[iStartRow, iCol].Value != null);
        //        iCols = iCol - 1;

        //        //��ȡ�����ݵķ�Χ
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
        //        //���ػ�õ�����
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
              //Open  Excel �ļ�
              app = new ExcelApp();
              app.Visible = false;
              app.Workbooks.Open(filePath);
              DataRow dr = null;
              //���Sheet
              Worksheets sheets = app.Workbooks[1].Worksheets;
              Worksheet datasheet = sheets[1];

              if (null == datasheet)
              {
                  return null;
              }

              //check rows���ٶ���һ�в���Ϊ��
              int iRow = iStartRow - 1;
              int iRows;

              do
              {
                  iRow = iRow + 1;
              }
              while (datasheet[iRow, iStartCol].Value != null);
              iRows = iRow - 1;

              //check Cols���ٶ���һ�в���Ϊ��
              int iCol = iStartCol - 1;
              int iCols;

              do
              {
                  iCol = iCol + 1;

              }
              while (datasheet[iStartRow, iCol].Value != null);
              iCols = iCol - 1;

              //��ȡ�����ݵķ�Χ
              Range range = datasheet[iStartRow, iStartCol, iRows, iCols];

              Array values = null;
              values = (System.Array)range.Value;
              if (values.GetLength(0) > 0)
              {
                  for (int i = 1; i <= values.GetLength(0); i++)
                  {
                      if (values.GetLength(1)>0)
                      {
                          //m_dtMaterielDistributionBasicInfor.Columns.Add("���ϱ���", System.Type.GetType("System.String"));
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
              //���ػ�õ�����
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
