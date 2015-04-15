using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using System.Reflection;

namespace Commons.DLL
{
    public class DllInvoke
    {
        /// <summary>
        /// 动态调用DLL
        /// </summary>
        /// <param name="dllName">dll名称</param>
        /// <param name="classFullName">dll中class的名称</param>
        /// <param name="methodName">需要调用的方法名</param>
        /// <param name="parameters">参数数组</param>
        /// <param name="result">调用方法的返回值</param>
        /// <returns>true:调用成功;false:调用失败</returns>
        public static bool Invoke(string dllName, string classFullName, string methodName, object[] parameters, out object result)
        {
            string strDllPath = Application.StartupPath + "\\DLLS\\" + dllName;
            result = false;
            //try
            //{
            Assembly m_Assembly = System.Reflection.Assembly.LoadFrom(strDllPath);
            if (m_Assembly == null)
            {
                MessageBox.Show("DLL不存在");
                result = false;
                return false;
            }
            Type m_Type = m_Assembly.GetType(classFullName);
            if (m_Type == null)
            {
                MessageBox.Show("DLL对象不存在");
                result = false;
                return false;
            }

            MethodInfo method = m_Type.GetMethod(methodName);
            Object m_Object = Activator.CreateInstance(m_Type); ;
            result = method.Invoke(m_Object, parameters);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            return true;

        }

        public static bool GetProperty(string dllName,
                                       string classFullName,
                                       string PropertyName,
                                       ref object ReturnValue
                                       )
        {
            string strDllPath = Application.StartupPath + "\\DLLS\\" + dllName;
            //try
            //{
            Assembly m_Assembly = System.Reflection.Assembly.LoadFrom(strDllPath);
            if (m_Assembly == null)
            {
                MessageBox.Show("DLL不存在");
                return false;
            }

            Type m_Type = m_Assembly.GetType(classFullName);
            if (m_Type == null)
            {
                MessageBox.Show("DLL对象不存在");
                return false;
            }

            //因为是静态调用,所以不需要创建Object 
            ReturnValue = m_Type.InvokeMember(PropertyName,
                                              BindingFlags.DeclaredOnly | BindingFlags.Public |
                                              BindingFlags.Static | BindingFlags.GetProperty,
                                              null,
                                              null,
                                              null);

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //    return false;
            //}

            return true;

        }
    }
}
