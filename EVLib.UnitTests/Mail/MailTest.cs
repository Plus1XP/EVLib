﻿using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EVLib.Mail.Tests
{
    [Ignore]
    [TestClass]
    public class ClientTest
    {
        ServerSettings serverSettings = new ServerSettings()
        {
            Host = "smtp.host.com",
            Port = 587,
            Username = "username@host.com",
            Password = "StrongPassword1",
            EnableSsl = true
        };

        MessageField messageField = new MessageField()
        {
            SenderName = "Sender",
            SenderEmail = "sender@host.com",
            RecipientName = "Recipient",
            RecipientEmail = "recipient@host.com",
            Subject = "Test Message",
            Body = "Testing mail-flow"
        };

        [TestMethod]
        public void SendTestValues()
        {
            Client mail = new Client();
            mail.Server = this.serverSettings;
            mail.Field = this.messageField;
            string actual = mail.Send();

            string expected = "Mail Sent!";
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void SendTestObjects()
        {
            Client mail = new Client(this.serverSettings);
            string actual = mail.Send(this.messageField);

            string expected = "Mail Sent!";
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void SendTestParameters()
        {
            Client mail = new Client("smtp.host.com", 587, "username@host.com", "StrongPassword1", true);
            string actual = mail.Send("Sender", "sender@host.com", "Recipient", "recipient@host.com",
                "Test Message", "Testing mail-flow");

            string expected = "Mail Sent!";
            Assert.AreEqual(actual, expected);
        }
    }
}
