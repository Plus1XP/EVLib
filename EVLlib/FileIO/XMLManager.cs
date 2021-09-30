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
