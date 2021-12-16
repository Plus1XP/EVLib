using System;
using System.Collections.Generic;
using System.Text;

namespace EVLlib.Mail
{
    public class ServerSettings
    {
        public string Host;
        public int Port;
        public string Username;
        public string Password;
        public bool UseDefaultCredentials = false;
        public bool EnableSsl;

        /// <summary>
        /// Provides the properties required to send an email.
        /// </summary>
        public ServerSettings()
        {

        }

        /// <summary>
        /// Provides the properties required to send an email.
        /// </summary>
        /// <param name="host">Email servers host address.</param>
        /// <param name="port">Email servers port.</param>
        /// <param name="username">Username to login to email Server.</param>
        /// <param name="password">Password to login to email server.</param>
        /// <param name="enableSsl">Specifies whether the email server requires SSL.</param>
        public ServerSettings(string host, int port, string username, string password, bool enableSsl)
        {
            Host = host;
            Port = port;
            UseDefaultCredentials = false;
            Username = username;
            Password = password;
            EnableSsl = enableSsl;
        }

        /// <summary>
        /// Provides the properties required to send an email.
        /// </summary>
        /// <param name="host">Email servers host address.</param>
        /// <param name="port">Email servers port.</param>
        /// <param name="enableSsl">Specifies whether the email server requires SSL.</param>
        public ServerSettings(string host, int port, bool enableSsl)
        {
            Host = host;
            Port = port;
            UseDefaultCredentials = true;
            EnableSsl = enableSsl;
        }
    }
}
