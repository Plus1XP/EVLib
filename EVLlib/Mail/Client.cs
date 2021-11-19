using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace EVLlib.Mail
{
    public class Client
    {
        public ServerSettings Server;
        public MessageField Field;

        public Client()
        {

        }

        /// <summary>
        ///  Initializes client for sending emails via STMP.
        /// </summary>
        /// <param name="serverSettings">Initializes a new instance of the Server Settings class with the specified settings.</param>
        public Client(ServerSettings serverSettings)
        {
            Server = serverSettings;
        }

        /// <summary>
        ///  Initializes client for sending emails via STMP.
        /// </summary>
        /// <param name="host">Email servers host address.</param>
        /// <param name="port">Email servers port.</param>
        /// <param name="enableSsl">Specifies whether the email server requires SSL.</param>
        public Client(string host, int port, bool enableSsl)
        {
            Server = new ServerSettings();
            Server.Host = host;
            Server.Port = port;
            Server.UseDefaultCredentials = true;
            Server.EnableSsl = enableSsl;
        }

        /// <summary>
        ///  Initializes client for sending emails via STMP.
        /// </summary>
        /// <param name="host">Email servers host address.</param>
        /// <param name="port">Email servers port.</param>
        /// <param name="username">Username to login to email Server.</param>
        /// <param name="password">Password to login to email server.</param>
        /// <param name="enableSsl">Specifies whether the email server requires SSL.</param>
        public Client(string host, int port, string username, string password, bool enableSsl)
        {
            Server = new ServerSettings();
            Server.Host = host;
            Server.Port = port;
            Server.UseDefaultCredentials = false;
            Server.Username = username;
            Server.Password = password;
            Server.EnableSsl = enableSsl;
        }
    }
}
