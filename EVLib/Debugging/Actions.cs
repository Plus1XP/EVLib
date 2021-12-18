﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace EVLib.Debugging
{
    public static class Actions
    {
        /// <summary>
        /// Times how long it takes to perform an action.
        /// </summary>
        /// <remarks>
        /// 
        /// Example:
        /// Time(()=>
        /// {    
        ///     ActionToMeasure();    
        /// });
        /// </remarks>
        /// <param name="action">Method with no parameters.</param>
        public static void Time(Action action)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            action();
            timer.Stop();
            Debug.Print("Duration:" + timer.Elapsed.ToString("mm\\:ss\\.ff"));
        }
    }
}
