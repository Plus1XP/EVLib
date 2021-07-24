using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EVLlib.ConsoleTools
{
    class TextUI
    {
        /// <summary>
        /// Reads a String (numbers) from the console using Console.ReadLine.
        /// </summary>
        /// <remarks>
        /// Method will loop until a valid number is entered.
        /// (Unacceptable values include non-numeric or Values outside of the Min/Max Scope).
        /// </remarks>
        /// <param name="DisplayText">String to be displayed before user input.</param>
        /// <param name="minSelection">The minimum Int to be accepted</param>
        /// <param name="maxSelection">the Maximum Int to be accepted</param>
        /// <returns>A Int</returns>
        public int GetNumericResponse(string DisplayText, int minSelection, int maxSelection)
        {
            int selection;
            do
            {
                this.PrintBlankLine();
                this.Print(DisplayText);
            } while (!int.TryParse(this.GetResponse(), out selection) || !(selection >= minSelection && selection <= maxSelection));

            return selection;
        }

        /// <summary>
        /// Executes Console.ReadLine Method.
        /// </summary>
        /// <returns>String of characters from input stream.</returns>
        public string GetResponse()
        {
            return Console.ReadLine();
        }

        /// <summary>
        /// Executes the Console.ReadKey Method.
        /// </summary>
        public void AwaitResponse()
        {
            Console.ReadKey();
        }

        /// <summary>
        /// Sets the foreground colour of the console.
        /// </summary>
        /// <param name="colour">The enum of a specified colour.</param>
        public void ChangeTextColour(ConsoleColor colour)
        {
            Console.ForegroundColor = colour;
        }

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
