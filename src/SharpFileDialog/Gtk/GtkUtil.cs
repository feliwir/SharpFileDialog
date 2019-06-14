using System.Collections.Generic;
using Gtk;

namespace SharpFileDialog.Gtk
{
    struct Filter
    {
        public string Name;
        public string Pattern;
    }

    static internal class GtkUtil
    {
        static bool _initialized = false;

        internal static void Initialize()
        {
            if (_initialized)
                return;

            Application.Init();
            _initialized = true;
        }

        /// <summary>
        /// Converts our filter string to a GtkSharp filter list
        /// </summary>
        /// <param name="filterString">The filter string as expected by SharpFileDialog</param>
        /// <returns></returns>
        internal static List<Filter> ConvertFilter(string filterString)
        {
            var filterArray = filterString.Split('|');

            if (filterArray.Length < 2 || (filterArray.Length % 2 != 0))
                return null;

            var results = new List<Filter>();

            for (int i = 0; i < filterArray.Length; i += 2)
            {
                Filter filter;
                filter.Name = filterArray[i].Trim();
                filter.Pattern = filterArray[i + 1].Trim();

                results.Add(filter);
            }

            return results;
        }
    }
}