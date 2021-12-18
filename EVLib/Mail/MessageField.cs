using System;
using System.Collections.Generic;
using System.Text;

namespace EVLib.Mail
{
    public class MessageField
    {
        public string SenderName;
        public string SenderEmail;
        public string RecipientName;
        public string RecipientEmail;
        public string Subject;
        public string Body;
        public string AttachmentPath = null;

        /// <summary>
        /// Provides the properties required to compose an email.
        /// </summary>
        public MessageField()
        {

        }

        /// <summary>
        /// Provides the properties required to compose an email.
        /// </summary>
        /// <param name="senderName">Display name of Sender.</param>
        /// <param name="senderEmail">Email address of sender.</param>
        /// <param name="recipientName">Display name of recipient.</param>
        /// <param name="recipientEmail">Email address of recipient.</param>
        /// <param name="subject">Email subject.</param>
        /// <param name="body">Email message body.</param>
        /// <param name="attachmentPath">Email attachment file path.</param>
        public MessageField(string senderName, string senderEmail, string recipientName,
            string recipientEmail, string subject, string body, string attachmentPath = null)
        {
            SenderName = senderName;
            SenderEmail = senderEmail;
            RecipientName = recipientName;
            RecipientEmail = recipientEmail;
            Subject = subject;
            Body = body;
            AttachmentPath = attachmentPath;
        }
    }
}
