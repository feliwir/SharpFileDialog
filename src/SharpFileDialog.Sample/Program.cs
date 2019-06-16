using System;

namespace SharpFileDialog.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            var openDialog = new OpenFileDialog("This is a test dialog");
            var filter = "Text files(*.txt) | *.txt | Png files(*.png) | *.png | All files(*.*) | *.*";
            openDialog.Open(filter, result => OpenFile(result.FileName));

            Console.ReadKey();

            var saveDialog = new SaveFileDialog("Another test for saving");
            filter = "Text files(*.txt) | *.txt | Png files(*.png) | *.png | All files(*.*) | *.*";
            saveDialog.Save(filter, result => SaveFile(result.FileName));

            Console.ReadKey();
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
