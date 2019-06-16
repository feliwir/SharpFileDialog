using System.Collections.Generic;

namespace SharpFileDialog.Zenity
{
    internal class ZenityUtil
    {
        /// <summary>
        /// Converts our filter string to a zenity filter string
        /// </summary>
        /// <param name="filterString">The filter string as expected by SharpFileDialog</param>
        /// <returns></returns>
        internal static List<string> ConvertFilter(string filterString)
        {
            var filterArray = filterString.Split('|');

            if (filterArray.Length < 2 || (filterArray.Length % 2 != 0))
                return null;

            var results = new List<string>();

            for (int i = 0; i < filterArray.Length; i += 2)
            {
                string piece = filterArray[i].Trim() + "|" + filterArray[i + 1].Trim();
                results.Add(piece);
            }

            return results;
        }
    }
}
