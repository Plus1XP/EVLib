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
