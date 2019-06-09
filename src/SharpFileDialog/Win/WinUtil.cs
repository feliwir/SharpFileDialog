using System;
using System.Collections.Generic;
using System.Text;

namespace SharpFileDialog.Win
{
    internal class WinUtil
    {
        /// <summary>
        /// Converts our filter string to a winapi filter string
        /// </summary>
        /// <param name="filter">The filter string as expected by SharpFileDialog</param>
        /// <returns></returns>
        internal static string ConvertFilter(string filterString)
        {
            var filterArray = filterString.Split('|');
            if (filterArray.Length < 2)
                return null;

            string result = "";

            foreach(var part in filterArray)
            {
                result += part.Trim();
                result += "\0";
            }

            return result;
        }
    }
}
