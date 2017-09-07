using FileProviders.Zip;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var provider = new ZipFileProvider("static.zip");
            var fileInfo = provider.GetFileInfo("/favicon.ico");
            Console.WriteLine(fileInfo.Name);
        }
    }
}
