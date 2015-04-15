using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Commons.XML
{
    public class XMLBase
    {
        private string m_filePath = "";
        private XmlDocument m_xmlDoc = new XmlDocument();
        private string m_fileName = "";

        public XMLBase(string fileName)
        {
            m_fileName = fileName;
            m_filePath = GetXMLFile(fileName);
            m_xmlDoc = GetDoc(fileName);
        }
        /// <summary>
        /// 取得xml文件路径
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public string GetXMLFile(string fileName)
        {
            string strFile="";
            try
            {
                strFile = AppDomain.CurrentDomain.BaseDirectory +@"XML\"+fileName;//System.Deployment.Application.ApplicationDeployment.CurrentDeployment.DataDirectory + "\\" + fileName;
            }
            catch
            {
                strFile = AppDomain.CurrentDomain.BaseDirectory + fileName;
            }

            return strFile;
        }

        /// <summary>
        /// 获得XmlDoc
        /// </summary>
        /// <param name="strFileName"></param>
        /// <returns></returns>
        public XmlDocument GetDoc(string strFileName)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(m_filePath);
            return xmlDoc;
        }

        /// <summary>
        /// 获得节点对象
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <param name="strNodePath"></param>
        /// <returns></returns>
        public XmlNode GetNodeObject(string strNodePath)
        {
            XmlNode xn = null;
            try
            {
                xn = m_xmlDoc.SelectSingleNode(strNodePath);
                XmlElement e = (XmlElement)xn;
                //判断是否存在指定节点
                if (e == null)
                {
                    //如果不存在返回null
                    xn = null;
                }
            }
            catch
            {
                throw;
            }

            //返回
            return xn;
        }

        /// <summary>
        /// 获得节点属性值
        /// </summary>
        /// <param name="xn"></param>
        /// <param name="strAttrName"></param>
        /// <returns></returns>
        public string GetNodeValue(XmlNode xn, string strAttrName)
        {
            string strNodeValue = null;
            try
            {
                //如果传入节点为null,返回null
                if (xn == null)
                {
                    return null;
                }
                XmlElement xe = (XmlElement)xn;
                strNodeValue = xe.GetAttribute(strAttrName);
            }
            catch
            {
                return null;
            }

            return strNodeValue;
        }

        /// <summary>
        /// 设置节点属性值
        /// </summary>
        /// <param name="xn"></param>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public void SetNodeValue(XmlNode xn, string element, string value)
        {
            try
            {
                //如果传入节点为null,返回null
                if (xn == null)
                {
                    return;
                }
                XmlElement xe = (XmlElement)xn;
                xe.SetAttribute(element, value);
                m_xmlDoc.Save(m_filePath);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 获取服务配置信息
        /// </summary>
        /// <param name="rootPath"></param>
        /// <param name="timerType"></param>
        /// <returns></returns>
        public List<ServiceModel> GetServiceConfig(string rootPath, string timerType)
        {
            List<ServiceModel> list = new List<ServiceModel>();
            try
            {
                XmlNode rootNode = GetNodeObject(rootPath);
                foreach (XmlNode xn in rootNode.ChildNodes)
                {
                    ServiceModel smodel = new ServiceModel();
                    smodel.Url = GetNodeValue(xn, "WebUrl");
                    foreach (XmlNode xn1 in xn.ChildNodes)
                    {
                        if (GetNodeValue(xn1, "TimerType") != null && GetNodeValue(xn1, "TimerType").Equals(timerType))
                        {
                            EventModel emodel = new EventModel();
                            emodel.ClassName = GetNodeValue(xn1, "ClassName");
                            emodel.FunctionName = GetNodeValue(xn1, "FunctionName");
                            emodel.TimerType = GetNodeValue(xn1, "TimerType");
                            string[] strArray = GetNodeValue(xn1, "TimerInfo").Split(',');
                            for (int i = 0; i < strArray.Length; i++)
                            {
                                emodel.TimerInfo.Add(strArray[i]);
                            }
                            smodel.EventList.Add(emodel);
                        }

                    }
                    list.Add(smodel);
                }
            }
            catch
            {

                throw;
            }

            return list;
        }
        
        public string ToString()
        {
            return m_xmlDoc.InnerXml;
        }

    }

    public class GetData
    {
        /// <summary>
        /// 获得webservices地址和方法名
        /// </summary>
        /// <param name="nodeIpAddress">IP地址节点</param>
        /// <param name="attributeIpAddress">ip地址节点的属性</param>
        /// <param name="nodeNetWorkType">内外或外网的节点</param>
        /// <param name="attributeNetWork">内外或外网的节点的属性</param>
        /// <param name="nodeRoute">路径地址</param>
        /// <param name="attributeRoute">路径地址属性</param>
        /// <param name="attributeFunction">方法名的属性</param>
        /// <param name="fileName">文件名称</param>
        /// <param name="url">返回的URL</param>
        /// <param name="functionName">返回的方法名</param>
        public static void GetUrl(string nodeIpAddress, string attributeIpAddress, string nodeNetWorkType, string attributeNetWork, string nodeRoute, string attributeRoute, string attributeFunction, string fileName, out string url, out string functionName)
        {
            try
            {
                XMLBase xmlBase = new XMLBase(fileName);
                XMLBase xmlUrl = new XMLBase("WebUrl.xml");
                //获得网络类型：内外或外网
                string urlType = xmlBase.GetNodeValue(xmlBase.GetNodeObject(nodeNetWorkType), attributeNetWork);
                //获得Ip地址
                string urlIp = xmlUrl.GetNodeValue(xmlUrl.GetNodeObject(nodeIpAddress + "/" + urlType), attributeIpAddress);
                //获得服务路径
                string urlRoute = xmlBase.GetNodeValue(xmlBase.GetNodeObject(nodeRoute), attributeRoute);
                //拼接webservices地址
                url = urlIp + urlRoute;
                //获得服务方法名称
                functionName = xmlBase.GetNodeValue(xmlBase.GetNodeObject(attributeNetWork), attributeFunction);

            }
            catch (Exception)
            {
                url = null;
                functionName = null;
            }
        }
        
        /// <summary>
        /// 获得webservices地址和方法名
        /// </summary>
        /// <param name="nodeIpAddress">IP地址节点</param>
        /// <param name="attributeIpAddress">ip地址节点的属性</param>
        /// <param name="nodeNetWorkType">内外或外网的节点</param>
        /// <param name="attributeNetWork">内外或外网的节点的属性</param>
        /// <param name="nodeRoute">路径地址</param>
        /// <param name="attributeRoute">路径地址属性</param>
        /// <param name="attributeFunction">方法名的属性</param>
        /// <param name="attributeDefault">默认值属性名</param>
        /// <param name="fileName">文件名称</param>
        /// <param name="url">返回的URL</param>
        /// <param name="functionName">返回的方法名</param>
        /// <param name="defaultValue">返回的默认值</param>
        public static void GetUrl(string nodeIpAddress, string attributeIpAddress, string nodeNetWorkType, string attributeNetWork, string nodeRoute, string attributeRoute, string attributeFunction, string attributeDefault, string fileName, out string url, out string functionName, out string defaultValue)
        {
            try
            {
                XMLBase xmlBase = new XMLBase(fileName);
                XMLBase xmlUrl = new XMLBase("WebUrl.xml");
                //获得网络类型：内外或外网
                string urlType = xmlBase.GetNodeValue(xmlBase.GetNodeObject(nodeNetWorkType), attributeNetWork);
                //获得Ip地址
                string urlIp = xmlUrl.GetNodeValue(xmlUrl.GetNodeObject(nodeIpAddress + "/" + urlType), attributeIpAddress);
                //获得服务路径
                string urlRoute = xmlBase.GetNodeValue(xmlBase.GetNodeObject(nodeRoute), attributeRoute);
                //拼接webservices地址
                url = urlIp + urlRoute;
                //获得服务方法名称
                functionName = xmlBase.GetNodeValue(xmlBase.GetNodeObject(nodeNetWorkType), attributeFunction);
                //获得默认值数据
                defaultValue = xmlBase.GetNodeValue(xmlBase.GetNodeObject(nodeNetWorkType), attributeDefault);
            }
            catch (Exception ex)
            {
                url = null;
                functionName = null;
                defaultValue = null;
                throw ex;
            }
        }

        /// <summary>
        /// 获得webservices地址和方法名
        /// </summary>
        /// <param name="routeNote">services地址节点名</param>
        /// <param name="functionNote">方法节点名</param>
        /// <param name="url">webservices地址</param>
        /// <param name="functionName">方法名</param>
        /// <param name="defaultValue">默认值</param>
        public static void GetUrl(string routeNote,string functionNote, out string url, out string functionName, out string defaultValue)
        {
            try
            {
                XMLBase xmlBase = new XMLBase("Config.xml");
                XMLBase xmlUrl = new XMLBase("WebUrl.xml");
                //获得网络类型：内外或外网
                string urlType = xmlBase.GetNodeValue(xmlBase.GetNodeObject("ServiceConfig/" + routeNote + "/" + functionNote), "Type");
                //获得Ip地址
                string urlIp = xmlUrl.GetNodeValue(xmlUrl.GetNodeObject("ServiceConfig/URL/" + urlType), "IpAddress");
                //获得服务路径
                string urlRoute = xmlBase.GetNodeValue(xmlBase.GetNodeObject("ServiceConfig/"+routeNote), "WebUrl");
                //拼接webservices地址
                url = urlIp + urlRoute;
                //获得服务方法名称
                functionName = xmlBase.GetNodeValue(xmlBase.GetNodeObject("ServiceConfig/" + routeNote + "/" + functionNote), "FunctionName");
                //获得默认值数据
                defaultValue = xmlBase.GetNodeValue(xmlBase.GetNodeObject("ServiceConfig/" + routeNote + "/" + functionNote), "Default");
            }
            catch (Exception ex)
            {
                url = null;
                functionName = null;
                defaultValue = null;
                throw ex;
            }
        }

        /// <summary>
        /// 获得webservices地址
        /// </summary>
        /// <param name="routeNote">services地址节点名</param>
        /// <param name="functionNote">方法节点名</param>
        /// <param name="url">webservices地址</param>
        /// <param name="functionName">方法名</param>
        public static void GetUrl(string routeNote, string functionNote, out string url, out string functionName)
        {
            try
            {
                XMLBase xmlBase = new XMLBase("Config.xml");
                XMLBase xmlUrl = new XMLBase("WebUrl.xml");
                //获得网络类型：内外或外网
                string urlType = xmlBase.GetNodeValue(xmlBase.GetNodeObject("ServiceConfig/" + routeNote + "/" + functionNote), "Type");
                //获得Ip地址
                string urlIp = xmlUrl.GetNodeValue(xmlUrl.GetNodeObject("ServiceConfig/URL/" + urlType), "IpAddress");
                //获得服务路径
                string urlRoute = xmlBase.GetNodeValue(xmlBase.GetNodeObject("ServiceConfig/" + routeNote), "WebUrl");
                //拼接webservices地址
                url = urlIp + urlRoute;
                //获得服务方法名称
                functionName = xmlBase.GetNodeValue(xmlBase.GetNodeObject("ServiceConfig/" + routeNote + "/" + functionNote), "FunctionName");
            }
            catch (Exception)
            {
                url = null;
                functionName = null;
            }
        }

        /// <summary>
        /// 获得Defual标示
        /// </summary>
        /// <param name="routeNote"></param>
        /// <param name="functionNote"></param>
        /// <param name="mag"></param>
        public static void GetUrl(string routeNote, string functionNote, out string mag)
        {
            try
            {
                XMLBase xmlBase = new XMLBase("Config.xml");
                //获得网络类型：内外或外网
                mag = xmlBase.GetNodeValue(xmlBase.GetNodeObject("ServiceConfig/" + routeNote + "/" + functionNote), "Default");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获得webservices地址
        /// </summary>
        /// <param name="routeNote">services地址节点名</param>
        /// <param name="url">webservices地址</param>
        public static void GetUrl(string routeNote, out string url)
        {
            try
            {
                XMLBase xmlBase = new XMLBase("Config.xml");
                XMLBase xmlUrl = new XMLBase("WebUrl.xml");
                //获得网络类型：内外或外网
                string urlType = xmlBase.GetNodeValue(xmlBase.GetNodeObject("ServiceConfig/" + routeNote ), "Type");
                //获得Ip地址
                string urlIp = xmlUrl.GetNodeValue(xmlUrl.GetNodeObject("ServiceConfig/URL/" + urlType), "IpAddress");
                //获得服务路径
                string urlRoute = xmlBase.GetNodeValue(xmlBase.GetNodeObject("ServiceConfig/" + routeNote), "WebUrl");
                //拼接webservices地址
                url = urlIp + urlRoute;
                //获得服务方法名称
            }
            catch (Exception ex)
            {
                url = null;
                throw ex;
            }
        }
    }
}
