using System;
using System.Collections.Generic;
using System.Text;

namespace EVLlib.Mail
{
    public class MailParameters
    {
        public string SenderName;
        public string SenderEmail;
        public string RecipientName;
        public string RecipientEmail;
        public string Subject;
        public string Body;
        public string AttachmentPath = null;

        public MailParameters()
        {

        }

        public MailParameters(string senderName, string senderEmail, string recipientName,
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
