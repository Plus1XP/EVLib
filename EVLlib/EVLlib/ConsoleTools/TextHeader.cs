using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;

namespace EVLlib.ConsoleTools
{
    class TextHeader : TextUI
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
    }
}
