using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Commons.WinForm;
using System.Collections.Generic;

namespace Commons.WebService
{

    /// <summary>
    ///  利用WebRequest/WebResponse进行WebService调用的类
    /// </summary>
    public class WebSvcCaller
    {
        //<webServices>
        //  <protocols>
        //    <add name="HttpGet"/>
        //    <add name="HttpPost"/>
        //  </protocols>
        //</webServices>
        private static Hashtable _xmlNamespaces = new Hashtable();//缓存xmlNamespace，避免重复调用GetNamespace
        /// <summary>
        /// 需要WebService支持Post调用
        /// </summary>
        public static XmlDocument QueryPostWebService(String URL, String MethodName, Hashtable Pars)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(URL + "/" + MethodName);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            SetWebRequest(request);
            byte[] data = EncodePars(Pars);
            WriteRequestData(request, data);
            return ReadXmlResponse(request.GetResponse());
        }

        /// <summary>
        /// 需要WebService支持Get调用
        /// </summary>
        public static XmlDocument QueryGetWebService(String URL, String MethodName, Hashtable Pars)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(URL + "/" + MethodName + "?" + ParsToString(Pars));
            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded";
            SetWebRequest(request);
            return ReadXmlResponse(request.GetResponse());
        }


        /// <summary>
        /// 通用WebService调用(Soap),参数Pars为String类型的参数名、参数值
        /// </summary>
        public static XmlDocument QuerySoapWebService(String URL, String MethodName, Hashtable Pars)
        {
            if (_xmlNamespaces.ContainsKey(URL))
            {
                return QuerySoapWebService(URL, MethodName, Pars, _xmlNamespaces[URL].ToString());
            }
            else
            {
                return QuerySoapWebService(URL, MethodName, Pars, GetNamespace(URL));
            }
        }

        public static String QuerySoapWebService(Hashtable Pars, String URL, String MethodName)
        {
            List<PosMsg> result = new List<PosMsg>();
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(URL);
            request.Method = "POST";
            request.ContentType = "text/xml; charset=utf-8";
            SetWebRequest(request);
            byte[] data = data = EncodeParsToSoap(Pars, MethodName);
            WriteRequestData(request, data);
            XmlDocument doc = new XmlDocument(), doc2 = new XmlDocument();

            WebResponse res;
            try
            {
                res = (WebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                res = (WebResponse)ex.Response;
            }

            doc = ReadXmlResponse(res);
            XmlElement root = doc.DocumentElement;
            string nameSpace = root.NamespaceURI;

            XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);

            nsmgr.AddNamespace("soapenv", nameSpace);
            nsmgr.AddNamespace("e", "\"http://ofbiz.apache.org/service\"");

            //XmlNode xe = doc.SelectSingleNode("/soapenv:Envelope/soapenv:Body/" + MethodName + "Response/map-Map/map-Entry/map-Value/std-String", nsmgr);
            XmlNodeList nList = doc.SelectNodes("/soapenv:Envelope/soapenv:Body/" + MethodName + "Response/map-Map/map-Entry", nsmgr);
            foreach (XmlNode xe in nList)
            {
                PosMsg m = new PosMsg();
                XmlNode xekey = xe.SelectSingleNode("map-Key/std-String", nsmgr);
                XmlNode xeValue = xe.SelectSingleNode("map-Value/std-String", nsmgr);
                m.Key = xekey.Attributes["value"].Value;
                m.Value = xeValue!=null&&xeValue.Attributes["value"] != null ? xeValue.Attributes["value"].Value : "";
                result.Add(m);
            }
            for (int i = 0; i < result.Count; i++)
            {
                if (!result[i].Value.Equals("error"))
                {
                    return result[i].Value;
                }
            }
            return "";
            //return xe.Attributes["value"].Value;
        }
        public static List<PosMsg> QuerySoapWebServiceList(Hashtable Pars, String URL, String MethodName)
        {
            List<PosMsg> result = new List<PosMsg>();
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(URL);
            request.Method = "POST";
            request.ContentType = "text/xml; charset=utf-8";
            SetWebRequest(request);
            byte[] data = data = EncodeParsToSoap(Pars, MethodName);
            WriteRequestData(request, data);
            XmlDocument doc = new XmlDocument(), doc2 = new XmlDocument();

            WebResponse res;
            try
            {
                res = (WebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                res = (WebResponse)ex.Response;
            }

            doc = ReadXmlResponse(res);
            XmlElement root = doc.DocumentElement;
            string nameSpace = root.NamespaceURI;

            XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);

            nsmgr.AddNamespace("soapenv", nameSpace);
            nsmgr.AddNamespace("e", "\"http://ofbiz.apache.org/service\"");

            XmlNodeList nList = doc.SelectNodes("/soapenv:Envelope/soapenv:Body/" + MethodName + "Response/map-Map/map-Entry", nsmgr);
            foreach (XmlNode xe in nList)
            {
                PosMsg m = new PosMsg();
                XmlNode xekey = xe.SelectSingleNode("map-Key/std-String", nsmgr);
                XmlNode xeValue = xe.SelectSingleNode("map-Value/std-String", nsmgr);
                m.Key = xekey.Attributes["value"].Value;
                m.Value = xeValue.Attributes["value"].Value;
                result.Add(m);
            }
            
            return result;
        }
        private static byte[] EncodeParsToSoap(Hashtable Pars, String keyName)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml("<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:ser=\"http://ofbiz.apache.org/service/\"></soapenv:Envelope>");
            XmlElement soapBody = doc.CreateElement("soapenv", "Body", "http://schemas.xmlsoap.org/soap/envelope/");
            XmlElement soapMethodA = doc.CreateElement(keyName);
            XmlElement soapMethod = doc.CreateElement("map-Map");
            XmlElement soapMethodEntry = doc.CreateElement("map-Entry");
            XmlElement soapMethodEntryKey = doc.CreateElement("map-Key");
            XmlElement soapMethodEntryValue = doc.CreateElement("map-Value");

            if (Pars != null)
            {
                if (Pars.Count > 0)
                {
                    foreach (string k in Pars.Keys)
                    {
                        XmlElement soapPar = doc.CreateElement("std-String");
                        soapPar.SetAttribute("value", k);
                        soapMethodEntryKey.AppendChild(soapPar);
                        XmlElement soapParK = doc.CreateElement("std-String");
                        soapParK.SetAttribute("value", Pars[k].ToString());
                        //soapParK.InnerXml = ObjectToSoapXml(k);
                        soapMethodEntryValue.AppendChild(soapParK);
                    }
                    soapMethodEntry.AppendChild(soapMethodEntryKey);
                    soapMethodEntry.AppendChild(soapMethodEntryValue);
                    soapMethod.AppendChild(soapMethodEntry);
                    soapMethodA.AppendChild(soapMethod);
                }
            }
            soapBody.AppendChild(soapMethodA);
            doc.DocumentElement.AppendChild(soapBody);
            return Encoding.UTF8.GetBytes(doc.OuterXml);
        }



        private static XmlDocument QuerySoapWebService(String URL, String MethodName, Hashtable Pars, string XmlNs)
        {
            _xmlNamespaces[URL] = XmlNs;//加入缓存，提高效率
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(URL);
            request.Method = "POST";
            request.ContentType = "text/xml; charset=utf-8";
            request.Headers.Add("SOAPAction", "\"" + XmlNs + (XmlNs.EndsWith("/") ? "" : "/") + MethodName + "\"");
            SetWebRequest(request);
            byte[] data = EncodeParsToSoap(Pars, XmlNs, MethodName);
            WriteRequestData(request, data);
            XmlDocument doc = new XmlDocument(), doc2 = new XmlDocument();

            WebResponse res;
            try
            {
                res = (WebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                res = (WebResponse)ex.Response;
            }

            doc = ReadXmlResponse(res);
            //doc = ReadXmlResponse(request.GetResponse());
            XmlNamespaceManager mgr = new XmlNamespaceManager(doc.NameTable);
            mgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");
            String RetXml = doc.SelectSingleNode("//soap:Body/*/*", mgr).InnerXml;
            doc2.LoadXml("<root>" + RetXml + "</root>");
            AddDelaration(doc2);
            return doc2;
        }
        private static string GetNamespace(String URL)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL + "?WSDL");
            SetWebRequest(request);
            WebResponse response = request.GetResponse();
            StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(sr.ReadToEnd());
            sr.Close();
            return doc.SelectSingleNode("//@targetNamespace").Value;
        }
        private static byte[] EncodeParsToSoap(Hashtable Pars, String XmlNs, String MethodName)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml("<soap:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\"></soap:Envelope>");
            AddDelaration(doc);
            XmlElement soapBody = doc.CreateElement("soap", "Body", "http://schemas.xmlsoap.org/soap/envelope/");
            XmlElement soapMethod = doc.CreateElement(MethodName);
            soapMethod.SetAttribute("xmlns", XmlNs);
            foreach (string k in Pars.Keys)
            {
                XmlElement soapPar = doc.CreateElement(k);
                soapPar.InnerXml = ObjectToSoapXml(Pars[k]);
                soapMethod.AppendChild(soapPar);
            }
            soapBody.AppendChild(soapMethod);
            doc.DocumentElement.AppendChild(soapBody);
            return Encoding.UTF8.GetBytes(doc.OuterXml);
        }
        private static string ObjectToSoapXml(object o)
        {
            XmlSerializer mySerializer = new XmlSerializer(o.GetType());
            MemoryStream ms = new MemoryStream();
            mySerializer.Serialize(ms, o);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(Encoding.UTF8.GetString(ms.ToArray()));
            if (doc.DocumentElement != null)
            {
                return doc.DocumentElement.InnerXml;
            }
            else
            {
                return o.ToString();
            }
        }
        private static void SetWebRequest(HttpWebRequest request)
        {
            request.Credentials = CredentialCache.DefaultCredentials;
            request.Timeout = 600000;
        }
        private static void WriteRequestData(HttpWebRequest request, byte[] data)
        {
            request.ContentLength = data.Length;
            Stream writer = request.GetRequestStream();
            writer.Write(data, 0, data.Length);
            writer.Close();
        }
        private static byte[] EncodePars(Hashtable Pars)
        {
            return Encoding.UTF8.GetBytes(ParsToString(Pars));
        }
        private static String ParsToString(Hashtable Pars)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string k in Pars.Keys)
            {
                if (sb.Length > 0)
                {
                    sb.Append("&");
                }
                //sb.Append(HttpUtility.UrlEncode(k) + "=" + HttpUtility.UrlEncode(Pars[k].ToString()));
            }
            return sb.ToString();
        }
        private static XmlDocument ReadXmlResponse(WebResponse response)
        {
            StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            String retXml = sr.ReadToEnd();
            retXml = retXml.Replace("xmlns=\"http://ofbiz.apache.org/service/\"", " ");
            sr.Close();
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(retXml);
            return doc;
        }
        private static void AddDelaration(XmlDocument doc)
        {
            XmlDeclaration decl = doc.CreateXmlDeclaration("1.0", "utf-8", null);
            doc.InsertBefore(decl, doc.DocumentElement);
        }
    }
    public class PosMsg
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
