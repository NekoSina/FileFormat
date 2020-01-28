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
            String TextInputPath = System.IO.Path.Combine(InputDir, TextFileName);
            String TextOutputPath = System.IO.Path.Combine(OutputDir, TextFileName);

            byte[] buffer = new byte[256];
            int b_size = buffer.Length;//512
            long NumberOfChunks;
            FileStream InputStream = File.OpenRead(TextInputPath);
            if(!File.Exists(TextOutputPath))
            {
                File.Create(TextOutputPath);
            }
            FileStream OutputStream = File.OpenWrite(TextOutputPath);
            
            //

            long test = InputStream.Length;//657
            if(test % b_size != 0)
            {
                NumberOfChunks = test/b_size +1;
            }
            else
            {
                NumberOfChunks = test/b_size;
            }
            

            //Console.WriteLine(test +" ---- "+ NumberOfChunks);
            
    
            for(int i = 0 ; i < NumberOfChunks; i++)
            {
                Console.Write("InputStream: "+""+InputStream.Position + "-");
                InputStream.Read(buffer, 0, b_size);
                OutputStream.Write(buffer, 0, b_size);
                Console.WriteLine(InputStream.Position);
                Console.WriteLine("OutPutStream: "+""+ OutputStream.Position);
                
                Console.ReadLine();
            }  


            Console.ReadLine();
        }
    }
}

