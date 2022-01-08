using System;
using System.Linq;

namespace EVLib.ConsoleTools
{
    public class TextUI
    {
        /// <summary>
        /// Reads a string (letters) from the console using Console.ReadLine.
        /// </summary>
        /// <remarks>
        /// Method will loop until a valid string is entered.
        /// (Unacceptable values includes non-letters and whitespaces).
        /// </remarks>
        /// <param name="DisplayText">String to be displayed before user input.</param>
        /// <returns>A String in lowercase.</returns>
        public string GetTextResponse(string DisplayText)
        {
            string text;
            do
            {
                this.PrintBlankLine();
                this.Print(DisplayText);
                text = this.GetResponse();
            } while (!text.All(char.IsLetter) || text.All(char.IsWhiteSpace));

            return text.ToLower();
        }

        /// <summary>
        /// Reads a String (single letter) from the console using Console.ReadLine.
        /// </summary>
        /// <remarks>
        /// Method will loop until a valid character is entered.
        /// (Unacceptable values includes non-letters and whitespaces).
        /// </remarks>
        /// <param name="DisplayText">String to be displayed before user input.</param>
        /// <returns>A Char in lowercase.</returns>
        public char GetCharResponse(string DisplayText)
        {
            char letter;
            do
            {
                this.PrintBlankLine();
                this.Print(DisplayText);
            } while (!char.TryParse(this.GetResponse(), out letter) || !char.IsLetter(letter) || char.IsWhiteSpace(letter));

            return char.ToLower(letter);
        }

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
        /// Captures line by using the Console.ReadLine Method.
        /// </summary>
        /// <returns>String of characters from input stream.</returns>
        public string GetResponse()
        {
            return Console.ReadLine();
        }

        /// <summary>
        /// Waits for user input by using the Console.ReadKey Method.
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
        /// <param name="title">Text to be displayed in the console title bar.</param>
        public void SetConsoleTitle(string title)
        {
            Console.Title = title;
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
        /// Prints a string Aligned to the center of the console. Can be used with decoration if used inside menu or header.
        /// </summary>
        /// <param name="text">Content to center</param>
        public void PrintToCenterScreen(string text)
        {
            Console.WriteLine(string.Format("{0," + ((Console.WindowWidth / 2) + (text.Length / 2)) + "}", text));
        }

        /// <summary>
        /// Prints a blank line using the newline string.
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
