using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.RVMessage
{
    public static class XmlSerialization
    {

        private static void XmlSerializeInternal(Stream stream, object o, Encoding encoding)
        {
            if (o == null)
                throw new ArgumentNullException("o");
            if (encoding == null)
                throw new ArgumentNullException("encoding");

            XmlSerializer serializer = new XmlSerializer(o.GetType());
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = false;
            settings.NewLineChars = "\r\n";
            settings.Encoding = encoding;
            settings.IndentChars = "    ";

            using (XmlWriter writer = XmlWriter.Create(stream, settings))
            {
                serializer.Serialize(writer, o, ns);
                writer.Close();
            }
        }

        /// <summary>
        /// 将一个对象序列化为XML字符串
        /// </summary>
        /// <param name="o">要序列化的对象</param>
        /// <param name="encoding">编码方式</param>
        /// <returns>序列化产生的XML字符串</returns>
        public static string XmlSerialize(object o)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                XmlSerializeInternal(stream, o, Encoding.UTF8);

                stream.Position = 0;
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    return reader.ReadToEnd();
                }
            }
        }
        /// <summary>
        /// 从XML字符串中反序列化对象
        /// </summary>
        /// <typeparam name="T">结果对象类型</typeparam>
        /// <param name="s">包含对象的XML字符串</param>
        /// <param name="encoding">编码方式</param>
        /// <returns>反序列化得到的对象</returns>
        public static T XmlDeserialize<T>(string s)
        {
            if (string.IsNullOrEmpty(s))
                throw new ArgumentNullException("s");

            XmlSerializer mySerializer = new XmlSerializer(typeof(T));
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(s)))
            {
                using (StreamReader sr = new StreamReader(ms, Encoding.UTF8))
                {
                    return (T)mySerializer.Deserialize(sr);
                }
            }
        }

        /// <summary>
        /// 序列化不包含xml定义
        /// </summary>
        /// <typeparam name="R"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string SerializeOnlyBody<R>(R t)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                XmlSerializer serializer = new XmlSerializer(t.GetType());

                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add("", "");

                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = false;
                settings.NewLineChars = "\r\n";
                settings.Encoding = Encoding.UTF8;
                settings.OmitXmlDeclaration = true;

                using (XmlWriter writer = XmlWriter.Create(stream, settings))
                {
                    serializer.Serialize(writer, t, ns);
                    writer.Close();
                    stream.Position = 0;
                    using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
        }

        /// <summary> 
        /// 反序列化Body
        /// </summary> 
        public static T DeserializeBody<T>(string s) where T : class
        {
            System.Xml.XmlDocument xd = new System.Xml.XmlDocument();
            xd.LoadXml(s);

            var body = xd.SelectSingleNode("Message").SelectSingleNode("Body");
            return ConvertNode<T>(body);

        }
        /// <summary> 
        /// 反序列化DataLayer
        /// </summary> 
        public static T DeserializeDataLayer<T>(string s) where T : class
        {
            System.Xml.XmlDocument xd = new System.Xml.XmlDocument();
            xd.LoadXml(s);

            var dataLayer = xd.SelectSingleNode("DataLayer");
            return ConvertNode<T>(dataLayer);

        }
        public static T DeserializeDataLayers<T>(string s) where T : class
        {
            System.Xml.XmlDocument xd = new System.Xml.XmlDocument();
            xd.LoadXml(s);

            var dataLayer = xd.SelectSingleNode("Message").SelectSingleNode("DataLayer");
            return ConvertNode<T>(dataLayer);

        }
        /// <summary> 
        /// 反序列化为对象 
        /// </summary> 
        public static T Deserialize<T>(string s) where T : class
        {
            System.Xml.XmlDocument xd = new System.Xml.XmlDocument();
            xd.LoadXml(s);

            if (typeof(T) == typeof(Header))
            {
                var header = xd.SelectSingleNode("Message").SelectSingleNode("Header");
                return ConvertNode<T>(header);
            }
            else if (typeof(T) == typeof(DataLayer))
            {
                var dataLayer = xd.SelectSingleNode("Message").SelectSingleNode("DataLayer");
                return ConvertNode<T>(dataLayer);
            }
            else if (typeof(T) == typeof(Return))
            {
                var ret = xd.SelectSingleNode("Message").SelectSingleNode("Return");
                return ConvertNode<T>(ret);
            }
            else
            {
                var body = xd.SelectSingleNode("Message").SelectSingleNode("DataLayer");
                //if (name != null)
                //{
                //    if (name == "LABEL_INFO_REQUEST_R")
                //    {
                //        return ConvertNode<T>(body, true);
                //    }
                //}
                return ConvertNode<T>(body);
            }
        }

        /// <summary> 
        /// 反序列化Header和Return
        /// </summary> 
        public static void DeserializeHeaderAndReturn(string s, out Header header, out Return ret, out string msgName)
        {
            System.Xml.XmlDocument xd = new System.Xml.XmlDocument();
            Return ss = null;
            xd.LoadXml(s);
            var temp = xd.SelectSingleNode("Message").SelectSingleNode("Header");
            header = ConvertNode<Header>(temp);
            var temp2 = xd.SelectSingleNode("Message").SelectSingleNode("Return");
            if (temp2 != null)
            {
                ss = ConvertNode<Return>(temp2);
            }
            msgName = xd.SelectSingleNode("Message").SelectSingleNode("Name").InnerText;
            ret = ss;

        }

        private static T ConvertNode<T>(XmlNode node) where T : class
        {
            MemoryStream stm = new MemoryStream();

            StreamWriter stw = new StreamWriter(stm);
            stw.Write(node.OuterXml);
            stw.Flush();

            stm.Position = 0;

            XmlSerializer ser = new XmlSerializer(typeof(T));
            T result = (ser.Deserialize(stm) as T);
            //if (xd.SelectSingleNode("Message").SelectSingleNode("Name").Value.ToString() == "LABEL_INFO_REQUEST_R")
            //{

            //}
            return result;
        }

        private static T ConvertNode<T>(XmlNode node, bool isLabel) where T : class
        {
            MemoryStream stm = new MemoryStream();

            StreamWriter stw = new StreamWriter(stm);
            stw.Write(node.OuterXml);
            stw.Flush();

            stm.Position = 0;

            XmlSerializer ser = new XmlSerializer(typeof(T));
            T result = (ser.Deserialize(stm) as T);
            //if (xd.SelectSingleNode("Message").SelectSingleNode("Name").Value.ToString() == "LABEL_INFO_REQUEST_R")
            //{

            //}
            if (isLabel)
            {

            }
            return result;
        }

        /// <summary>
        /// 序列化成R
        /// </summary>
        /// <typeparam name="R">Message类名</typeparam>
        /// <param name="obj">Message字符串</param>
        /// <param name="body">Body 字符串</param>
        /// <returns></returns>
        public static string XmlSerializeMessage<R>(R obj, string body) where R : new()
        {
            var msg = XmlSerialize(obj);
            msg = msg.Replace("<DataLayer />", body);
            return msg;
        }

        /// <summary>
        /// 转换成能换行的xml
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static string ToXmlFormat(this string xml)
        {
            if (string.IsNullOrEmpty(xml))
                return "";
            using (MemoryStream ms = new MemoryStream())
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml);
                doc.Save(ms);

                StreamReader reader = new StreamReader(ms, Encoding.UTF8);
                reader.BaseStream.Seek(0, SeekOrigin.Begin);
                return reader.ReadToEnd();
            }

        }
    }
}
