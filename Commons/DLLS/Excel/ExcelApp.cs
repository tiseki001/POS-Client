using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Commons.Data
{
    #region "Excel Application"
    /// <summary>
    /// Excel后期绑定的操作类
    /// </summary>
    public class ExcelApp
    {
        object m_ExcelApp;

        public ExcelApp()
        {
            Type objExcelType = Type.GetTypeFromProgID("Excel.Application");
            if (objExcelType == null)
            {
                throw new Exception("未发现Excel程序");
            }
            m_ExcelApp = Activator.CreateInstance(objExcelType);
            if (m_ExcelApp == null)
            {
                throw new Exception("启用Excel程序失败!");
            }
        }

        public bool Visible
        {
            get
            {
                object objVisible =
                m_ExcelApp.GetType().InvokeMember("Visible", BindingFlags.GetProperty, null,
                m_ExcelApp, null);
                if (objVisible is Boolean)
                    return (bool)objVisible;
                else
                    throw new Exception("调用方法失败!");
            }
            set
            {
                object[] Parameters = new object[1] { value };
                m_ExcelApp.GetType().InvokeMember("Visible", BindingFlags.SetProperty, null,
                m_ExcelApp, Parameters);
            }
        }

        public Workbooks Workbooks
        {
            get
            {
                object workbooks = m_ExcelApp.GetType().InvokeMember("Workbooks",
                BindingFlags.GetProperty, null, m_ExcelApp, null);
                if (workbooks == null)
                    throw new Exception("查询工作簿时失败!");
                else
                    return new Workbooks(workbooks);
            }
        }

        public void Quit()
        {
            m_ExcelApp.GetType().InvokeMember("Quit", BindingFlags.InvokeMethod, null,
                            m_ExcelApp, null);
        }

        public void Dispose()
        {
            if (m_ExcelApp != null)
            {
                this.Workbooks.Dispose();
                Marshal.ReleaseComObject(m_ExcelApp);
                m_ExcelApp = null;
            }

            GC.Collect();
        }
    }

    public class Workbooks
    {
        private object m_Workbooks;
        private string m_ExcelFileName;

        public Workbooks(object workbooks)
        {
            m_Workbooks = workbooks;
        }

        public void Open(string ExcelFileName)
        {
            m_ExcelFileName = ExcelFileName;
            object[] Parameters = new object[1] { ExcelFileName };
            m_Workbooks.GetType().InvokeMember("Open", BindingFlags.InvokeMethod, null,
            m_Workbooks, Parameters);
        }

        public Workbook this[int index]
        {
            get
            {
                object[] Parameters = new object[1] { index };
                object workbook = m_Workbooks.GetType().InvokeMember("Item",
                BindingFlags.GetProperty, null, m_Workbooks, Parameters);
                if (workbook == null)
                    throw new Exception("获取工作薄时出现错误!");
                else
                    return new Workbook(workbook);
            }
        }

        public void Close()
        {
            m_Workbooks.GetType().InvokeMember("Close", BindingFlags.InvokeMethod, null,
            m_Workbooks, null);
        }

        public void Dispose()
        {
            if (m_Workbooks != null)
            {
                // this[1].Dispose();
                Marshal.ReleaseComObject(m_Workbooks);
                m_Workbooks = null;
            }
        }
    }

    public class Workbook
    {
        private object m_Workbook;

        public Workbook(object workbook)
        {
            m_Workbook = workbook;
        }

        public Worksheets Worksheets
        {
            get
            {
                object worksheets = m_Workbook.GetType().InvokeMember("Worksheets",
                System.Reflection.BindingFlags.GetProperty, null, m_Workbook, null);
                if (worksheets == null)
                    throw new Exception("获取工作表集合时失败!");
                else
                    return new Worksheets(worksheets);
            }
        }

        public void SaveAs(string FileName)
        {
            object[] Parameters = new object[1] { FileName };
            m_Workbook.GetType().InvokeMember("SaveAs", BindingFlags.InvokeMethod, null, m_Workbook, Parameters);
        }

        public void Save()
        {
            m_Workbook.GetType().InvokeMember("Save", BindingFlags.InvokeMethod, null, m_Workbook, null);
        }

        public void Close()
        {
            m_Workbook.GetType().InvokeMember("Close", BindingFlags.InvokeMethod, null, m_Workbook, null);
        }

        public void Dispose()
        {
            if (m_Workbook != null)
            {
                this.Worksheets.Dispose();
                Marshal.ReleaseComObject(m_Workbook);
                m_Workbook = null;
            }
        }
    }

    public class Worksheets
    {
        private object m_Worksheets;

        public Worksheets(object worksheets)
        {
            m_Worksheets = worksheets;
        }

        public Worksheet this[int index]
        {
            get
            {
                object[] Parameters = new object[1] { index };
                object worksheet = m_Worksheets.GetType().InvokeMember("Item",
                System.Reflection.BindingFlags.GetProperty, null, m_Worksheets, Parameters);
                if (worksheet == null)
                    throw new Exception("获取工作表时出现错误!");
                else
                    return new Worksheet(worksheet);
            }
        }

        public void Dispose()
        {
            if (m_Worksheets != null)
            {
                //this[1].Dispose();
                Marshal.ReleaseComObject(m_Worksheets);
                m_Worksheets = null;
            }
        }
    }

    public class Worksheet
    {
        private object m_Worksheet;

        public Worksheet(object worksheet)
        {
            m_Worksheet = worksheet;
        }

        public Range this[int row, int col, int rows, int cols]
        {
            get
            {
                object[] Parameters = new Object[2] { row, col };
                object cell1 = m_Worksheet.GetType().InvokeMember("Cells",
                System.Reflection.BindingFlags.GetProperty, null, m_Worksheet, Parameters);

                object cell2 = m_Worksheet.GetType().InvokeMember("Cells", BindingFlags.GetProperty, null
                                        , m_Worksheet, new Object[] { row + rows - 1, col + cols - 1 });
                object range;

                if (cell1 == null || cell2 == null)
                    throw new Exception("获取单元格失败!");
                else
                {
                    range = m_Worksheet.GetType().InvokeMember("Range", BindingFlags.GetProperty, null
                                    , m_Worksheet, new Object[] { cell1, cell2 });

                    if (range == null)
                        throw new Exception("获取区域失败!");
                }

                return new Range(range);
            }
        }

        public Range this[int row, int col]
        {
            get
            {
                object[] Parameters;
                object cell;

                Parameters = new Object[2] { row, col };

                cell = m_Worksheet.GetType().InvokeMember("Cells", BindingFlags.GetProperty, null, m_Worksheet, Parameters);

                if (cell == null)
                    throw new Exception("获取单元格失败!");

                return new Range(cell);
            }
        }

        public object Copy()
        {
            object wsheet;

            wsheet = m_Worksheet.GetType().InvokeMember("Copy", BindingFlags.InvokeMethod, null, m_Worksheet, new object[] { m_Worksheet });

            return wsheet;
        }

        public object Name
        {
            get
            {
                object wsName;

                wsName = m_Worksheet.GetType().InvokeMember("Name", BindingFlags.GetProperty, null, m_Worksheet, null);

                return wsName;
            }
            set
            {
                object[] Parameters;

                Parameters = new Object[1] { value };
                m_Worksheet.GetType().InvokeMember("Name", BindingFlags.SetProperty, null, m_Worksheet, Parameters);
            }
        }


        public void Dispose()
        {
            if (m_Worksheet != null)
            {
                Marshal.ReleaseComObject(m_Worksheet);
                m_Worksheet = null;
            }
        }
    }

    public class Range
    {
        private object m_Range;

        public Range(object Range)
        {
            m_Range = Range;
        }

        public object Value
        {
            get
            {
                object result = m_Range.GetType().InvokeMember("Value",
                System.Reflection.BindingFlags.GetProperty, null, m_Range, null);
                return result;
            }
            set
            {
                object[] Parameters = new Object[1] { value };
                m_Range.GetType().InvokeMember("Value", System.Reflection.BindingFlags.SetProperty,
                null, m_Range, Parameters);
            }
        }

        public object Formula
        {
            get
            {
                object result = m_Range.GetType().InvokeMember("Formula",
                System.Reflection.BindingFlags.GetProperty, null, m_Range, null);
                return result;
            }
            set
            {
                object[] Parameters = new Object[1] { value };
                m_Range.GetType().InvokeMember("Formula", System.Reflection.BindingFlags.SetProperty,
                null, m_Range, Parameters);
            }
        }

        public object Cell
        {
            get
            {
                object result = m_Range.GetType().InvokeMember("Cell",
                System.Reflection.BindingFlags.GetProperty, null, m_Range, null);
                return result;
            }
            set
            {
                object[] Parameters = new Object[1] { value };
                m_Range.GetType().InvokeMember("Cell", System.Reflection.BindingFlags.SetProperty,
                null, m_Range, Parameters);
            }
        }
        public object NumberFormatLocal
        {
            get
            {
                object numberFormatlocal;

                numberFormatlocal = m_Range.GetType().InvokeMember("NumberFormatLocal", BindingFlags.GetProperty, null, m_Range, null);

                return numberFormatlocal;
            }
            set
            {
                object[] Parameters;

                Parameters = new Object[1] { value };
                m_Range.GetType().InvokeMember("NumberFormatLocal", BindingFlags.SetProperty, null, m_Range, Parameters);
            }
        }
        public void Dispose()
        {
            if (m_Range != null)
            {
                Marshal.ReleaseComObject(m_Range);
                m_Range = null;
            }
        }
    }
    #endregion
}
