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
            string OutputDir = "C:\\Users\\sina\\Desktop\\output";
            string InputDir = "C:\\Users\\sina\\Desktop\\input";
            string TextFileName = "TALLLOOOOOS.txt";
            string TextInputPath = System.IO.Path.Combine(InputDir, TextFileName);
            string TextOutputPath = System.IO.Path.Combine(OutputDir, TextFileName);

            byte[] buffer = new byte[256];
            int b_size = buffer.Length;
            FileStream InputStream = File.OpenRead(TextInputPath);

            FileStream OutputStream = File.OpenWrite(TextOutputPath);
            

            //Console.WriteLine(test +" ---- "+ NumberOfChunks);
            
    
            while(InputStream.Position != InputStream.Length)
            {
                Console.Write("InputStream: "+""+InputStream.Position + "-");
                InputStream.Read(buffer, 0, b_size);
                OutputStream.Write(buffer, 0, b_size);
                Console.WriteLine(InputStream.Position);
                Console.WriteLine("OutPutStream: "+""+ OutputStream.Position);
                

            }  

            OutputStream.Dispose();
            InputStream.Dispose();
            Console.ReadLine();
        }
    }
}

