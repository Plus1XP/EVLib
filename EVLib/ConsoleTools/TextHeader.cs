using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace EVLib.ConsoleTools
{
    public class TextHeader : TextUI
    {
        private string title;
        private string version;
        private string release;
        private string contact;
        private string border;
        private string dismiss;

        public TextHeader()
        {
            this.title = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductName;
            this.version = $"Version {FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion}";
            this.release = File.GetLastWriteTime(Assembly.GetExecutingAssembly().Location).ToString("dd MMMM yyyy");
            this.contact = "http://www.github.com/Plus1XP";
            this.border = $"+ {new string('-', Console.WindowWidth / 2)} +";
            this.dismiss = "Press any key to dismiss...";
        }

        /// <summary>
        /// Application header, also sets the console title.
        /// </summary>
        public void PrintHeader()
        {
            this.SetConsoleTitle(this.title);
            this.PrintBlankLine();
            this.ChangeTextColour(ConsoleColor.Cyan);
            this.PrintToCenterScreen(this.title);
            this.ChangeTextColour(ConsoleColor.White);
            this.PrintToCenterScreen(this.version);
            this.PrintToCenterScreen(this.release);
            this.PrintToCenterScreen(this.contact);
            this.PrintToCenterScreen(this.border);
            this.PrintBlankLine();
            this.PrintToCenterScreen(this.dismiss);
            this.ResetTextColour();
            this.AwaitResponse();
        }

        /// <summary>
        /// Application header, also sets the console title.
        /// </summary>
        /// <param name="contact">Authors contact information.</param>
        public void PrintHeader(string contact)
        {
            this.contact = contact;

            this.SetConsoleTitle(this.title);
            this.PrintBlankLine();
            this.ChangeTextColour(ConsoleColor.Cyan);
            this.PrintToCenterScreen(this.title);
            this.ChangeTextColour(ConsoleColor.White);
            this.PrintToCenterScreen(this.version);
            this.PrintToCenterScreen(this.release);
            this.PrintToCenterScreen(this.contact);
            this.PrintToCenterScreen(this.border);
            this.PrintBlankLine();
            this.PrintToCenterScreen(this.dismiss);
            this.ResetTextColour();
            this.AwaitResponse();
        }

        /// <summary>
        /// Application header, also sets the console title.
        /// </summary>
        /// <param name="contact">Authors contact information.</param>
        /// <param name="dismiss">Message to dismiss header.</param>
        public void PrintHeader(string contact, string dismiss)
        {
            this.contact = contact;
            this.dismiss = dismiss;

            this.SetConsoleTitle(this.title);
            this.PrintBlankLine();
            this.ChangeTextColour(ConsoleColor.Cyan);
            this.PrintToCenterScreen(this.title);
            this.ChangeTextColour(ConsoleColor.White);
            this.PrintToCenterScreen(this.version);
            this.PrintToCenterScreen(this.release);
            this.PrintToCenterScreen(this.contact);
            this.PrintToCenterScreen(this.border);
            this.PrintBlankLine();
            this.PrintToCenterScreen(this.dismiss);
            this.ResetTextColour();
            this.AwaitResponse();
        }

        /// <summary>
        /// Application header, also sets the console title.
        /// </summary>
        /// <param name="title">Title of application.</param>
        /// <param name="version">Version of application.</param>
        /// <param name="release">Date of build.</param>
        public void PrintHeader(string title, string version, string release)
        {
            this.title = title;
            this.version = version;
            this.release = release;

            this.SetConsoleTitle(this.title);
            this.PrintBlankLine();
            this.ChangeTextColour(ConsoleColor.Cyan);
            this.PrintToCenterScreen(this.title);
            this.ChangeTextColour(ConsoleColor.White);
            this.PrintToCenterScreen(this.version);
            this.PrintToCenterScreen(this.release);
            this.PrintToCenterScreen(this.contact);
            this.PrintToCenterScreen(this.border);
            this.PrintBlankLine();
            this.PrintToCenterScreen(this.dismiss);
            this.ResetTextColour();
            this.AwaitResponse();
        }

        /// <summary>
        /// Application header, also sets the console title.
        /// </summary>
        /// <param name="title">Title of application.</param>
        /// <param name="version">Version of application.</param>
        /// <param name="release">Date of build.</param>
        /// <param name="contact">Authors contact information.</param>
        public void PrintHeader(string title, string version, string release, string contact)
        {
            this.title = title;
            this.version = version;
            this.release = release;
            this.contact = contact;

            this.SetConsoleTitle(this.title);
            this.PrintBlankLine();
            this.ChangeTextColour(ConsoleColor.Cyan);
            this.PrintToCenterScreen(this.title);
            this.ChangeTextColour(ConsoleColor.White);
            this.PrintToCenterScreen(this.version);
            this.PrintToCenterScreen(this.release);
            this.PrintToCenterScreen(this.contact);
            this.PrintToCenterScreen(this.border);
            this.PrintBlankLine();
            this.PrintToCenterScreen(this.dismiss);
            this.ResetTextColour();
            this.AwaitResponse();
        }

        /// <summary>
        /// Application header, also sets the console title.
        /// </summary>
        /// <param name="title">Title of application.</param>
        /// <param name="version">Version of application.</param>
        /// <param name="release">Date of build.</param>
        /// <param name="contact">Authors contact information.</param>
        /// <param name="dismiss">Message to dismiss header.</param>
        public void PrintHeader(string title, string version, string release, string contact, string dismiss)
        {
            this.title = title;
            this.version = version;
            this.release = release;
            this.contact = contact;
            this.dismiss = dismiss;

            this.SetConsoleTitle(this.title);
            this.PrintBlankLine();
            this.ChangeTextColour(ConsoleColor.Cyan);
            this.PrintToCenterScreen(this.title);
            this.ChangeTextColour(ConsoleColor.White);
            this.PrintToCenterScreen(this.version);
            this.PrintToCenterScreen(this.release);
            this.PrintToCenterScreen(this.contact);
            this.PrintToCenterScreen(this.border);
            this.PrintBlankLine();
            this.PrintToCenterScreen(this.dismiss);
            this.ResetTextColour();
            this.AwaitResponse();
        }
    }
}
