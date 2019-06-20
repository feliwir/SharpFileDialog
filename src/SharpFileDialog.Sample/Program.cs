using System;

namespace SharpFileDialog.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            var dirDialog = new DirectoryDialog("Select a directory");
            dirDialog.Open(result => OpenDirectory(result.FileName));

            Console.ReadKey();

            var openDialog = new OpenFileDialog("This is a test dialog");
            var filter = "Text files(*.txt) | *.txt | Png files(*.png) | *.png | All files(*.*) | *.*";
            openDialog.Open(result => OpenFile(result.FileName), filter);

            Console.ReadKey();

            var saveDialog = new SaveFileDialog("Another test for saving");
            filter = "Text files(*.txt) | *.txt | Png files(*.png) | *.png | All files(*.*) | *.*";
            saveDialog.DefaultFileName = "filename.txt";
            saveDialog.Save(result => SaveFile(result.FileName), filter);

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
