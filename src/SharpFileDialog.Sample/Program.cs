using System;

namespace SharpFileDialog.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            var openDialog = new OpenFileDialog();
            var filter = "Text files(*.txt) | *.txt | All files(*.*) | *.*";
            openDialog.Open(filter, result => OpenFile(result.FileName));

            Console.ReadKey();

            var saveDialog = new SaveFileDialog();
            openDialog.Open(filter, result => SaveFile(result.FileName));

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
