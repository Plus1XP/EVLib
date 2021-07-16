using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EVLlib.ConsoleTools
{
    class TextUI
    {
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
