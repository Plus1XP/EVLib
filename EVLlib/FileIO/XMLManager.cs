using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace EVLlib.FileIO
{
    class XMLManager : FileManager
    {
        public XMLManager()
        {

        }

        /// <summary>
        /// Sets boolean value to attribute in XML node.
        /// </summary>
        /// <param name="xmlFileLocation">Path to XML file.</param>
        /// <param name="nodePath">XML node path.</param>
        /// <param name="nodeName">XML node name.</param>
        /// <param name="nodeValue">XML node value.</param>
        /// <param name="attributeName">XML Attribute name.</param>
        /// <param name="attributeValue">Boolean value to set XML attribute value.</param>
        public void SetNodeAttributeValueFromBool(string xmlFileLocation, string nodePath, string nodeName, string nodeValue, string attributeName, bool attributeValue)
        {
            XmlDocument xmlDoc = this.LoadXmlDocument(xmlFileLocation);
            XmlNodeList nodeList = this.LoadNodeList(xmlDoc, nodePath);
            XmlNode node = this.GetNodeFromNodeList(nodeList, nodeName, nodeValue);
            this.SetNodeAttributeFromBool(node, attributeName, attributeValue);
            this.SaveXmlDocument(xmlDoc, xmlFileLocation);
        }

        /// <summary>
        /// Sets integer value to attribute in XML node.
        /// </summary>
        /// <param name="xmlFileLocation">Path to XML file.</param>
        /// <param name="nodePath">XML node path.</param>
        /// <param name="nodeName">XML node name.</param>
        /// <param name="nodeValue">XML node value.</param>
        /// <param name="attributeName">XML Attribute name.</param>
        /// <param name="attributeValue">Integer value to set XML attribute value.</param>
        public void SetNodeAttributeValueFromInt(string xmlFileLocation, string nodePath, string nodeName, string nodeValue, string attributeName, int attributeValue)
        {
            XmlDocument xmlDoc = this.LoadXmlDocument(xmlFileLocation);
            XmlNodeList nodeList = this.LoadNodeList(xmlDoc, nodePath);
            XmlNode node = this.GetNodeFromNodeList(nodeList, nodeName, nodeValue);
            this.SetNodeAttributeFromInt(node, attributeName, attributeValue);
            this.SaveXmlDocument(xmlDoc, xmlFileLocation);
        }

        /// <summary>
        /// Loads XML document from file.
        /// </summary>
        /// <param name="xmlFileLoaction">Path to XML file.</param>
        /// <returns>XML Document.</returns>
        public XmlDocument LoadXmlDocument(string xmlFileLoaction)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(this.ReadStringFromFile(xmlFileLoaction));

            return xmlDoc;
        }

        /// <summary>
        /// Loads XML node list from XML document.
        /// </summary>
        /// <param name="xmlDoc">XML Document to load.</param>
        /// <param name="nodePath">Path to XML file.</param>
        /// <returns>XML node list.</returns>
        public XmlNodeList LoadNodeList(XmlDocument xmlDoc, string nodePath)
        {
            XmlNodeList nodeList;
            nodeList = xmlDoc.SelectNodes(nodePath);

            return nodeList;
        }

        /// <summary>
        /// Saves XML document to file.
        /// </summary>
        /// <param name="xmlDoc">XML document to save.</param>
        /// <param name="xmlFileLoaction">Path to XML file.</param>
        public void SaveXmlDocument(XmlDocument xmlDoc, string xmlFileLoaction)
        {
            xmlDoc.Save(xmlFileLoaction);
        }

        /// <summary>
        /// Gets a single node from an XML node list.
        /// </summary>
        /// <param name="nodeList">XML node list.</param>
        /// <param name="nodeName">XML node name.</param>
        /// <param name="nodeValue">XML node value.</param>
        /// <returns>Single node.</returns>
        public XmlNode GetNodeFromNodeList(XmlNodeList nodeList, string nodeName, string nodeValue)
        {
            XmlNode element = null;

            foreach (XmlNode node in nodeList)
            {
                if (this.GetNodeAttributeValueAsString(node, nodeName).Equals(nodeValue))
                {
                    element = node;
                }
            }

            return element;
        }

        /// <summary>
        /// Gets attribute from XML node list.
        /// </summary>
        /// <param name="nodeList">XML node list.</param>
        /// <param name="nodeName">XML node name.</param>
        /// <param name="nodeValue">XML node value.</param>
        /// <param name="attributeName">XML attribute name.</param>
        /// <returns>XML attribute.</returns>
        public XmlAttribute GetAttributeFromNodeList(XmlNodeList nodeList, string nodeName, string nodeValue, string attributeName)
        {
            XmlAttribute elementAttribute = null;

            foreach (XmlNode node in nodeList)
            {
                if (this.GetNodeAttributeValueAsString(node, nodeName).Equals(nodeValue))
                {
                    elementAttribute = this.GetNodeAttribute(node, attributeName);
                }
            }

            return elementAttribute;
        }

        /// <summary>
        /// Gets attribute string value from XML attribute.
        /// </summary>
        /// <param name="attribute">XML attribute.</param>
        /// <returns>XML attribute value as string.</returns>
        public string GetAttributeValueAsString(XmlAttribute attribute)
        {
            return attribute.Value;
        }

        /// <summary>
        /// Gets attribute integer value from XML attribute.
        /// </summary>
        /// <param name="attribute">XML attribute.</param>
        /// <returns>XML attribute value as integer.</returns>
        public int GetAttributeValueAsInt(XmlAttribute attribute)
        {
            if (attribute == null)
            {
                return 15;
            }

            return Convert.ToInt32(attribute.Value);
        }

        /// <summary>
        /// Gets attribute boolean vlaue from XML attribute.
        /// </summary>
        /// <param name="attribute">XML attribute.</param>
        /// <returns>XML attribute value as boolean.</returns>
        public bool GetAttributeValueAsBool(XmlAttribute attribute)
        {
            if (attribute == null)
            {
                return true;
            }

            return Convert.ToBoolean(attribute.Value);
        }

        /// <summary>
        /// Gets a single node attribute string value.
        /// </summary>
        /// <param name="node">XML node.</param>
        /// <param name="attributeName">XML attribute name.</param>
        /// <returns>XML attribute value as string.</returns>
        private string GetNodeAttributeValueAsString(XmlNode node, string attributeName)
        {
            return this.GetNodeAttribute(node, attributeName).Value;
        }

        /// <summary>
        /// Gets a single node attribute integer value.
        /// </summary>
        /// <param name="node">XML Node.</param>
        /// <param name="attributeName">XML attribute name.</param>
        /// <returns>XML attribute value as integer.</returns>
        private int GetNodeAttributeValueAsInt(XmlNode node, string attributeName)
        {
            return Convert.ToInt32(this.GetNodeAttribute(node, attributeName).Value);
        }

        /// <summary>
        /// Gets a single node attribute boolean value.
        /// </summary>
        /// <param name="node">XML node.</param>
        /// <param name="attributeName">XML attribute name.</param>
        /// <returns>XML attribute value as boolean.</returns>
        public bool GetNodeAttributeValueAsBool(XmlNode node, string attributeName)
        {
            return Convert.ToBoolean(this.GetNodeAttribute(node, attributeName).Value);
        }

        /// <summary>
        /// Gets a single node attribute.
        /// </summary>
        /// <param name="node">XML node.</param>
        /// <param name="attributeName">XML attribute name.</param>
        /// <returns>XML attribute.</returns>
        public XmlAttribute GetNodeAttribute(XmlNode node, string attributeName)
        {
            return node.Attributes?[attributeName];
        }

        /// <summary>
        /// Sets a single node attribute value from string.
        /// </summary>
        /// <param name="node">XML node.</param>
        /// <param name="attributeName">XML attribute name.</param>
        /// <param name="attributeValue">string value to set XML attribute node.</param>
        private void SetNodeAttributeFromString(XmlNode node, string attributeName, string attributeValue)
        {
            this.SetNodeAttribute(node, attributeName, attributeValue);
        }

        /// <summary>
        /// Sets a single node attribute value from integer.
        /// </summary>
        /// <param name="node">XML node.</param>
        /// <param name="attributeName">XML attribute name.</param>
        /// <param name="attributeValue">integer value to set XML attribute node.</param>
        private void SetNodeAttributeFromInt(XmlNode node, string attributeName, int attributeValue)
        {
            this.SetNodeAttribute(node, attributeName, Convert.ToString(attributeValue));
        }

        /// <summary>
        /// Sets a single node attribute value from boolean.
        /// </summary>
        /// <param name="node">XML node.</param>
        /// <param name="attributeName">XML attribute name.</param>
        /// <param name="attributeValue">boolean value to set XML attribute node.</param>
        public void SetNodeAttributeFromBool(XmlNode node, string attributeName, bool attributeValue)
        {
            this.SetNodeAttribute(node, attributeName, Convert.ToString(attributeValue));
        }

        /// <summary>
        /// Sets a single node attribute.
        /// </summary>
        /// <param name="node">XML node.</param>
        /// <param name="attributeName">XML attribute name.</param>
        /// <param name="attributeValue">value to set XML attribute node.</param>
        private void SetNodeAttribute(XmlNode node, string attributeName, string attributeValue)
        {
            if (node != null)
            {
                node.Attributes[attributeName].Value = attributeValue;
            }
        }
    }
}
