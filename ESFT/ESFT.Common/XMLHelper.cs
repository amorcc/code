using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Xml;

namespace ESFT.Common
{
    /// <summary>
    /// XML操作：
    /// 参照手册http://www.w3school.com.cn/xpath/xpath_operators.asp
    /// </summary>
    public class XMLHelper
    {
        #region 构造函数
        public XMLHelper()
        {
            // OpenXML();
        }

        public XMLHelper(string iFilePath)
            : this()
        {
            this.filePath = iFilePath;
        }
        #endregion

        #region 对象定义

        private XmlDocument xmlDoc = new XmlDocument();
        XmlNode xmlnode;
        XmlElement xmlelem;

        #endregion

        #region 属性定义
        private string errorMess;

        public string ErrorMess
        {
            get { return this.errorMess; }
            set { this.errorMess = value; }
        }

        private string filePath;

        public string FilePath
        {
            set { this.filePath = value; }
            get { return this.filePath; }
        }

        #endregion

        #region 创建XML操作对象
        /// <summary>
        /// 创建XML操作对象
        /// </summary>
        public void OpenXML()
        {
            try
            {
                if (!string.IsNullOrEmpty(this.filePath))
                {
                    this.xmlDoc.Load(this.filePath);
                }
                else
                {
                    // 默认路径 c:\ejiang.xml
                    this.filePath = string.Format(@"c:\ejiang.xml");
                    this.xmlDoc.Load(this.filePath);
                }
            }
            catch (Exception ex)
            {
                this.ErrorMess = ex.ToString();
            }
        }
        #endregion

        #region 创建Xml 文档
        /// <summary>
        /// 创建一个带有根节点的Xml 文件
        /// </summary>
        /// <param name="FileName">Xml 文件名称</param>
        /// <param name="rootName">根节点名称</param>
        /// <param name="Encode">编码方式:gb2312，UTF-8 等常见的</param>
        /// <param name="DirPath">保存的目录路径</param>
        /// <returns></returns>
        public bool CreatexmlDocument(string FileName, string rootName, string Encode)
        {
            try
            {
                XmlDeclaration xmldecl;
                xmldecl = this.xmlDoc.CreateXmlDeclaration("1.0", Encode, null);
                this.xmlDoc.AppendChild(xmldecl);
                this.xmlelem = this.xmlDoc.CreateElement(string.Empty, rootName, string.Empty);
                this.xmlDoc.AppendChild(this.xmlelem);
                this.xmlDoc.Save(FileName);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region 得到表
        /// <summary>
        /// 得到表  
        /// </summary>
        /// <returns></returns>
        public DataTable GetData()
        {
            DataSet ds = new DataSet();

            FileStream fs = new System.IO.FileStream(this.filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            ds.ReadXml(fs);
            if (ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region 读取指定节点的指定属性值
        /// <summary>
        /// 功能:
        /// 读取指定节点的指定属性值
        /// </summary>
        /// <param name="strNode">节点名称(相对路径：//+节点名称)</param>
        /// <param name="strAttribute">此节点的属性</param>
        /// <returns></returns>
        public string GetXmlNodeValue(string strNode, string strAttribute)
        {
            string strReturn = string.Empty;
            try
            {
                // 根据指定路径获取节点
                XmlNode xmlNode = this.xmlDoc.SelectSingleNode(strNode);

                // 获取节点的属性，并循环取出需要的属性值
                XmlAttributeCollection xmlAttr = xmlNode.Attributes;

                for (int i = 0; i < xmlAttr.Count; i++)
                {
                    if (xmlAttr.Item(i).Name == strAttribute)
                    {
                        strReturn = xmlAttr.Item(i).Value;
                    }
                }
            }
            catch (XmlException xmle)
            {
                throw xmle;
            }

            return strReturn;
        }
        #endregion

        #region 读取指定节点的值
        /// <summary>
        /// 功能:
        /// 读取指定节点的值
        /// </summary>
        /// <param name="strNode">节点名称</param>
        /// <returns></returns>
        public string GetXmlNodeValue(string strNode)
        {
            string strReturn = string.Empty;
            try
            {
                ////根据路径获取节点
                XmlNode xmlNode = this.xmlDoc.SelectSingleNode(strNode);
                strReturn = xmlNode.InnerText;
            }
            catch (XmlException xmle)
            {
                System.Console.WriteLine(xmle.Message);
            }

            return strReturn;
        }
        #endregion

        #region 获取XML文件的根元素
        /// <summary>
        /// 获取XML文件的根元素
        /// </summary>
        public XmlNode GetXmlRoot()
        {
            return this.xmlDoc.DocumentElement;
        }
        #endregion

        #region 获取XML节点值
        /// <summary>
        /// 获取XML节点值
        /// </summary>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        public string GetNodeValue(string nodeName)
        {
            this.xmlDoc = new XmlDocument();
            this.xmlDoc.Load(this.filePath);

            XmlNodeList xnl = this.xmlDoc.ChildNodes;
            foreach (XmlNode xnf in xnl)
            {
                XmlElement xe = (XmlElement)xnf;
                XmlNodeList xnf1 = xe.ChildNodes;
                foreach (XmlNode xn2 in xnf1)
                {
                    if (xn2.InnerText == nodeName)
                    {
                        XmlElement xe2 = (XmlElement)xn2;
                        return xe2.GetAttribute("value");
                    }
                }
            }

            return null;
        }
        #endregion

        #region 设置节点值
        /// <summary>
        /// 功能:
        /// 设置节点值
        /// </summary>
        /// <param name="strNode">节点的名称</param>
        /// <param name="newValue">节点值</param>
        public void SetXmlNodeValue(string xmlNodePath, string xmlNodeValue)
        {
            try
            {
                // 根据指定路径获取节点
                XmlNode xmlNode = this.xmlDoc.SelectSingleNode(xmlNodePath);

                // 设置节点值
                xmlNode.InnerText = xmlNodeValue;
            }
            catch (XmlException xmle)
            {
                throw xmle;
            }
        }
        #endregion

        #region 添加父节点

        /// <summary>
        /// 在根节点下添加父节点
        /// </summary>
        public void AddParentNode(string parentNode)
        {
            XmlNode root = this.GetXmlRoot();
            XmlNode parentXmlNode = this.xmlDoc.CreateElement(parentNode);

            root.AppendChild(parentXmlNode);
        }
        #endregion

        #region 向一个已经存在的父节点中插入一个子节点
        /// <summary>
        /// 向一个已经存在的父节点中插入一个子节点
        /// </summary>
        public void AddChildNode(string parentNodePath, string childNodePath)
        {
            XmlNode parentXmlNode = this.xmlDoc.SelectSingleNode(parentNodePath);
            XmlNode childXmlNode = this.xmlDoc.CreateElement(childNodePath);

            parentXmlNode.AppendChild(childXmlNode);
        }
        #endregion

        #region 向一个节点添加属性
        /// <summary>
        /// 向一个节点添加属性
        /// </summary>
        public void AddAttribute(string NodePath, string NodeAttribute)
        {
            XmlAttribute nodeAttribute = this.xmlDoc.CreateAttribute(NodeAttribute);
            XmlNode nodePath = this.xmlDoc.SelectSingleNode(NodePath);
            nodePath.Attributes.Append(nodeAttribute);
        }
        #endregion

        #region 插入一个节点和它的若干子节点
        /// <summary>
        /// 插入一个节点和它的若干子节点
        /// </summary>
        /// <param name="NewNodeName">插入的节点名称</param>
        /// <param name="HasAttributes">此节点是否具有属性，True 为有，False 为无</param>
        /// <param name="fatherNode">此插入节点的父节点</param>
        /// <param name="htAtt">此节点的属性，Key 为属性名，Value 为属性值</param>
        /// <param name="htSubNode"> 子节点的属性， Key 为Name,Value 为InnerText</param>
        /// <returns>返回真为更新成功，否则失败</returns>
        public bool InsertNode(string NewNodeName, bool HasAttributes, string fatherNode, Hashtable htAtt, Hashtable htSubNode)
        {
            try
            {
                this.xmlDoc.Load(this.filePath);
                XmlNode root = this.xmlDoc.SelectSingleNode(fatherNode);
                this.xmlelem = this.xmlDoc.CreateElement(NewNodeName);

                // 若此节点有属性，则先添加属性
                if (htAtt != null && HasAttributes)
                {
                    this.SetAttributes(this.xmlelem, htAtt);
                    this.AddNodes(this.xmlelem.Name, this.xmlDoc, this.xmlelem, htSubNode); // 添加完此节点属性后，再添加它的子节点和它们的InnerText
                }
                else
                {
                    this.AddNodes(this.xmlelem.Name, this.xmlDoc, this.xmlelem, htSubNode); // 若此节点无属性，那么直接添加它的子节点
                }

                root.AppendChild(this.xmlelem);
                this.xmlDoc.Save(this.filePath);
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        #endregion

        #region 设置节点属性
        /// <summary>
        /// 设置节点属性
        /// </summary>
        /// <param name="xe">节点所处的Element</param>
        /// <param name="htAttribute">节点属性，Key 代表属性名称，Value 代表属性值</param>
        public void SetAttributes(XmlElement xe, Hashtable htAttribute)
        {
            foreach (DictionaryEntry de in htAttribute)
            {
                xe.SetAttribute(de.Key.ToString(), de.Value.ToString());
            }
        }
        #endregion

        #region 增加子节点到根节点下
        /// <summary>
        /// 增加子节点到根节点下
        /// </summary>
        /// <param name="rootNode">上级节点名称</param>
        /// <param name="xmlDoc">Xml 文档</param>
        /// <param name="rootXe">父根节点所属的Element</param>
        /// <param name="SubNodes">子节点属性，Key 为Name 值，Value 为InnerText 值</param>
        public void AddNodes(string rootNode, XmlDocument xmlDoc, XmlElement rootXe, Hashtable SubNodes)
        {
            foreach (DictionaryEntry de in SubNodes)
            {
                this.xmlnode = this.xmlDoc.SelectSingleNode(rootNode);
                XmlElement subNode = this.xmlDoc.CreateElement(de.Key.ToString());
                subNode.InnerText = de.Value.ToString();
                rootXe.AppendChild(subNode);
            }
        }
        #endregion

        #region 设置节点的属性值
        /// <summary>
        /// 功能:
        /// 设置节点的属性值
        /// </summary>
        /// <param name="xmlNodePath">节点名称</param>
        /// <param name="xmlNodeAttribute">属性名称</param>
        /// <param name="xmlNodeAttributeValue">属性值</param>
        public void SetXmlNodeValue(string xmlNodePath, string xmlNodeAttribute, string xmlNodeAttributeValue)
        {
            try
            {
                // 根据指定路径获取节点
                XmlNode xmlNode = this.xmlDoc.SelectSingleNode(xmlNodePath);

                // 获取节点的属性，并循环取出需要的属性值
                XmlAttributeCollection xmlAttr = xmlNode.Attributes;
                for (int i = 0; i < xmlAttr.Count; i++)
                {
                    if (xmlAttr.Item(i).Name == xmlNodeAttribute)
                    {
                        xmlAttr.Item(i).Value = xmlNodeAttributeValue;
                        break;
                    }
                }
            }
            catch (XmlException xmle)
            {
                throw xmle;
            }
        }

        #endregion

        #region 更新节点
        /// <summary>
        /// 更新节点
        /// </summary>
        /// <param name="fatherNode">需要更新节点的上级节点</param>
        /// <param name="htAtt">需要更新的属性表，Key 代表需要更新的属性，Value 代表更新后的值</param>
        /// <param name="htSubNode">需要更新的子节点的属性表，Key 代表需要更新的子节点名字Name,Value 代表更新后的值InnerText</param>
        /// <returns>返回真为更新成功，否则失败</returns>
        public bool UpdateNode(string fatherNode, Hashtable htAtt, Hashtable htSubNode)
        {
            try
            {
                this.xmlDoc = new XmlDocument();
                this.xmlDoc.Load(this.filePath);
                XmlNodeList root = this.xmlDoc.SelectSingleNode(fatherNode).ChildNodes;
                this.UpdateNodes(root, htAtt, htSubNode);
                this.xmlDoc.Save(this.filePath);
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 根据编号更新某个节点
        /// </summary>
        /// <param name="iParentNode">要更新的节点的父节点</param>
        /// <param name="iNode">要更新的节点</param>
        /// <param name="iId">要更新的节点的编号</param>
        /// <param name="iValue">要更新的值</param>
        /// <returns>返回真为更新成功，否则失败</returns>
        public bool UpdateNode(string iParentNode, string iNode, string iId, string iValue)
        {
            try
            {
                this.xmlDoc = new XmlDocument();
                this.xmlDoc.Load(this.filePath);
                XmlNodeList root = this.xmlDoc.SelectSingleNode(iParentNode).ChildNodes;
                for (int i = 0; i < root.Count; i++)
                {
                    if (root[i].SelectSingleNode("Id").InnerText == iId)
                    {
                        root[i].SelectSingleNode(iNode).InnerText = iValue;
                        break;
                    }
                }

                this.xmlDoc.Save(this.filePath);
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        #endregion

        #region 更新节点属性和子节点InnerText 值
        /// <summary>
        /// 更新节点属性和子节点InnerText 值
        /// </summary>
        /// <param name="root">根节点名字</param>
        /// <param name="htAtt">需要更改的属性名称和值</param>
        /// <param name="htSubNode">需要更改InnerText 的子节点名字和值</param>
        public void UpdateNodes(XmlNodeList root, Hashtable htAtt, Hashtable htSubNode)
        {
            foreach (XmlNode xn in root)
            {
                this.xmlelem = (XmlElement)xn;

                // 如果节点如属性，则先更改它的属性
                if (this.xmlelem.HasAttributes)
                {
                    // 遍历属性哈希表
                    foreach (DictionaryEntry de in htAtt)
                    {
                        // 如果节点有需要更改的属性
                        if (this.xmlelem.HasAttribute(de.Key.ToString()))
                        {
                            this.xmlelem.SetAttribute(de.Key.ToString(), de.Value.ToString()); // 则把哈希表中相应的值Value 赋给此属性Key
                        }
                    }
                }

                // 如果有子节点，则修改其子节点的InnerText
                if (this.xmlelem.HasChildNodes)
                {
                    XmlNodeList xnl = this.xmlelem.ChildNodes;
                    foreach (XmlNode xn1 in xnl)
                    {
                        XmlElement xe = (XmlElement)xn1;
                        foreach (DictionaryEntry de in htSubNode)
                        {
                            // htSubNode 中的key 存储了需要更改的节点名称，
                            if (xe.Name == de.Key.ToString())
                            {
                                xe.InnerText = de.Value.ToString(); // htSubNode中的Value存储了Key 节点更新后的数据
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region 删除一个节点的属性
        /// <summary>
        /// 删除一个节点的属性
        /// </summary>
        public void DeleteAttribute(string NodePath, string NodeAttribute, string NodeAttributeValue)
        {
            XmlNodeList nodePath = this.xmlDoc.SelectSingleNode(NodePath).ChildNodes;

            foreach (XmlNode xn in nodePath)
            {
                XmlElement xe = (XmlElement)xn;

                if (xe.GetAttribute(NodeAttribute) == NodeAttributeValue)
                {
                    xe.RemoveAttribute(NodeAttribute); // 删除属性
                }
            }
        }

        #endregion

        #region 删除一个节点
        /// <summary>
        /// 删除一个节点
        /// </summary>
        public void DeleteXmlNode(string tempXmlNode)
        {
            XmlNode xmlNodePath = this.xmlDoc.SelectSingleNode(tempXmlNode);
            xmlNodePath.ParentNode.RemoveChild(xmlNodePath);
        }

        /// <summary>
        /// 删除指定节点
        /// </summary>
        public void DeleteXmlNode(string iNodeName, string iId)
        {
            XmlNodeList xmlNodes = this.xmlDoc.SelectSingleNode(iNodeName).ChildNodes;
            foreach (XmlNode node in xmlNodes)
            {
                if (node.SelectSingleNode("Id").InnerText == iId)
                {
                    node.ParentNode.RemoveChild(node);
                    break;
                }
            }

            this.SavexmlDocument();
        }
        #endregion

        #region 删除指定节点下的子节点
        /// <summary>
        /// 删除指定节点下的子节点
        /// </summary>
        /// <param name="fatherNode">制定节点</param>
        /// <returns>返回真为更新成功，否则失败</returns>
        public bool DeleteNodes(string fatherNode)
        {
            try
            {
                this.xmlDoc = new XmlDocument();
                this.xmlDoc.Load(this.filePath);
                this.xmlnode = this.xmlDoc.SelectSingleNode(fatherNode);
                this.xmlnode.RemoveAll();
                this.xmlDoc.Save(this.filePath);
                return true;
            }
            catch (XmlException xe)
            {
                throw new XmlException(xe.Message);
            }
        }
        #endregion

        #region 私有函数

        private string functionReturn(XmlNodeList xmlList, int i, string nodeName)
        {
            string node = xmlList[i].ToString();
            string rusultNode = string.Empty;
            for (int j = 0; j < i; j++)
            {
                if (node == nodeName)
                {
                    rusultNode = node.ToString();
                }
                else
                {
                    if (xmlList[j].HasChildNodes)
                    {
                        this.functionReturn(xmlList, j, nodeName);
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return rusultNode;
        }

        #endregion

        #region 保存XML文件
        /// <summary>
        /// 功能: 
        /// 保存XML文件
        /// </summary>
        public void SavexmlDocument()
        {
            try
            {
                this.xmlDoc.Save(this.filePath);
            }
            catch (XmlException xmle)
            {
                throw xmle;
            }
        }

        /// <summary>
        /// 功能: 保存XML文件
        /// </summary>
        /// <param name="tempXMLFilePath"></param>
        public void SavexmlDocument(string tempXMLFilePath)
        {
            try
            {
                this.xmlDoc.Save(tempXMLFilePath);
            }
            catch (XmlException xmle)
            {
                throw xmle;
            }
        }
        #endregion

        /// <summary>
        /// 创建节点
        /// </summary>
        /// <param name="nodeName">节点名称</param>
        /// <returns></returns>
        public XmlNode CreateNode(string nodeName)
        {
            XmlNode node = this.xmlDoc.CreateNode(XmlNodeType.Element, nodeName, string.Empty);
            return node;
        }

        /// <summary>
        /// 创建元素
        /// </summary>
        /// <param name="eleName">名称</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public XmlElement CreateElement(string eleName, string value)
        {
            XmlElement element = this.xmlDoc.CreateElement(eleName);
            element.InnerText = value;
            return element;
        }

        #region 替换文本
        /// <summary>
        /// 根据XML文件替换字符串
        /// </summary>
        /// <param name="iStr">要替换的字符串</param>
        /// <param name="iNodeName">XML中要替换的节点名称</param>
        /// <param name="iAttributeName">XML中要替换的节点属性</param>
        /// <returns></returns>
        public string ReplaceFace(string iStr, string iNodeName, string iAttributeName)
        {
            this.OpenXML();
            string newStr = iStr;
            XmlNodeList xmlNodeList = this.xmlDoc.GetElementsByTagName(iNodeName);
            for (int i = 0; i < xmlNodeList.Count; i++)
            {
                newStr = newStr.Replace(xmlNodeList[i].Attributes[iAttributeName].Value, xmlNodeList[i].InnerXml);
            }

            return newStr;
        }
        #endregion

        #region 判断节点是否存在
        /// <summary>
        /// 根据编号判断节点是否存在
        /// </summary>
        /// <param name="iParentNode">要判断的节点的父节点</param>
        /// <param name="iId">要判断的节点的编号</param>
        /// <returns>返回真为存在，否则不存在</returns>
        public bool IsExsit(string iParentNode, string iId)
        {
            bool isExsit = false;
            try
            {
                this.xmlDoc = new XmlDocument();
                this.xmlDoc.Load(this.filePath);
                XmlNodeList root = this.xmlDoc.SelectSingleNode(iParentNode).ChildNodes;
                for (int i = 0; i < root.Count; i++)
                {
                    if (root[i].SelectSingleNode("Id").InnerText == iId)
                    {
                        isExsit = true;
                        break;
                    }
                }

                return isExsit;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        #endregion
    }
}
