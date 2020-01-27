using System;
using System.IO;
using System.Collections.Generic;

namespace file_format
{
    class Program
    {
        static List<FileInfo> FileList= new List<FileInfo>();


        static void Main(string[] args)
        {
            String OutputDir = "C:\\Users\\sina\\Desktop\\output";
            String InputDir = "C:\\Users\\sina\\Desktop\\input";
            String TextFileName = "TALLLOOOOOS.txt";
            String TextInputDir = System.IO.Path.Combine(InputDir, TextFileName);
            String TextOutputDir = System.IO.Path.Combine(OutputDir, TextFileName);

            //load files into dictionary
            foreach(var file in Directory.GetFiles(InputDir))
            {
                FileList.Add(new FileInfo(file));
            }
            //copy to destination
            foreach (var item in FileList)
            {
                File.Copy(item.FullName, TextOutputDir);
                  
                Console.WriteLine(item.Name + "".PadLeft(8) + item.Length + "bytes");
            }
            Console.ReadLine();
        }
    }
}
