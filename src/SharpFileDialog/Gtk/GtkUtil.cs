using System;
using System.Threading.Tasks;
using Gtk;

namespace SharpFileDialog.Gtk
{
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
    }
}