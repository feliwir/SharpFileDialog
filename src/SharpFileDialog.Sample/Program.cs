using System;

namespace SharpFileDialog.Sample
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            using (var dirDialog = new DirectoryDialog("Select a directory"))
                dirDialog.Open(result => OpenDirectory(result.FileName));

            Console.ReadKey();

            using (var openDialog = new OpenFileDialog("This is a test dialog"))
            {
                var filter = "Text files(*.txt) | *.txt | Png files(*.png) | *.png | All files(*.*) | *.*";
                openDialog.Open(result => OpenFile(result.FileName), filter);
            }

            Console.ReadKey();

            using (var saveDialog = new SaveFileDialog("Another test for saving"))
            {
                var filter = "Text files(*.txt) | *.txt | Png files(*.png) | *.png | All files(*.*) | *.*";
                saveDialog.DefaultFileName = "filename.txt";
                saveDialog.Save(result => SaveFile(result.FileName), filter);
            }

            Console.ReadKey();
        }

        private static void OpenDirectory(string filename)
        {
            Console.WriteLine("Opening directory: {0}", filename);
        }

        private static void OpenFile(string filename)
        {
            Console.WriteLine("Opening file: {0}", filename);
        }

        private static void SaveFile(string filename)
        {
            Console.WriteLine("Saving file: {0}", filename);
        }
    }
}
