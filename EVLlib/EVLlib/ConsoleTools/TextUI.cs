using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EVLlib.ConsoleTools
{
    class TextUI
    {
        /// <summary>
        /// Resets the console colour to default.
        /// </summary>
        public void ResetTextColour()
        {
            Console.ResetColor();
        }

        /// <summary>
        /// Sets the console title bar text.
        /// </summary>
        /// <param name="text">Text to be displayed in the console title bar.</param>
        public void SetConsoleTitle(string text)
        {
            Console.Title = text;
        }

        /// <summary>
        /// Prints a string to the console.
        /// </summary>
        /// <param name="text">String value to be printed</param>
        public void Print(string text)
        {
            Console.WriteLine(text);
        }

        /// <summary>
        /// Align content to center for console. Can be used with decoration if used inside menu or header
        /// </summary>
        /// <param name="text">Content to center</param>
        /// <returns>Center aligned text</returns>
        public void PrintToCenterScreen(string text)
        {
            Console.WriteLine(string.Format("{0," + ((Console.WindowWidth / 2) + (text.Length / 2)) + "}", text));
        }

        /// <summary>
        /// Gets the newline string.
        /// </summary>
        /// <remarks>
        /// Uses Environment.NewLine (\r\n).
        /// </remarks>
        public void PrintBlankLine()
        {
            Console.Write(Environment.NewLine);
        }

        /// <summary>
        /// Clears console.
        /// </summary>
        public void ClearScreen()
        {
            Console.Clear();
        }
    }
}
