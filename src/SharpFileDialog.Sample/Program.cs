using System;
using System.Threading;

namespace SharpFileDialog.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            var dialog = new OpenFileDialog();
            dialog.Open("*.txt",result => Console.Write(result));

            Console.ReadKey();
            Thread.Sleep(10000);
        }
    }
}
