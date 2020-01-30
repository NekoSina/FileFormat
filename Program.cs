using System;
using System.IO;
using System.Collections.Generic;
using System.IO.Compression;



namespace file_format
{
    

    class Program
    {

        static List<string> FileList= new List<string>();

        static void Main(string[] args)
        {
        
        string outputDir = "C:\\Users\\sina\\Desktop\\output";
        string InputDir = "C:\\Users\\sina\\Desktop\\input";
        string File1Name = "Program.cs";
        string File2Name = "face.jpg";
        string OutputFileName = "life.nko";
        string InputPath = System.IO.Path.Combine(InputDir, File1Name);
        string InputPath2 = System.IO.Path.Combine(InputDir, File2Name);
        string OutputPath = System.IO.Path.Combine(outputDir, OutputFileName);
        
        //FileList.Add(InputPath);
        //FileList.Add(InputPath2);
        //Pack(OutputPath, FileList);
        UnPack(OutputPath, Path.Combine(InputDir,"answer"));
        Console.WriteLine($"OutputPath = {OutputPath} \nInputDir = {Path.Combine(InputDir,"answer")}");
        Console.ReadLine();
        
        }
        private static void UnPack(string MergedFile, string OutputPath)
        {
            using(var MergedStream = File.OpenRead(MergedFile)) // first pool
            using (var decompressionStream = new BrotliStream(MergedStream,CompressionMode.Decompress))
            using(BinaryReader reader = new BinaryReader(decompressionStream)) // to read bytes from the merged file
            {
                var FileCount = reader.ReadInt32();
                for(int i = 0; i < FileCount; i++)
                {
                    long FileSize = reader.ReadInt64();
                    string FileName = reader.ReadString();
                    Console.WriteLine($"size: {FileSize}name: {FileName}");
                    using var OutputStream = File.OpenWrite(Path.Combine(OutputPath,FileName));//second pool
                    while(OutputStream.Position!= FileSize)
                    {
                        var BytesLeft = FileSize - OutputStream.Position;
                        var BufferSize = Math.Min(BytesLeft, 1024*1024*2);
                        var buffer = new byte[BufferSize];
                        decompressionStream.Read(buffer, 0, (int)BufferSize);
                        OutputStream.Write(buffer, 0, (int)BufferSize);     
                    }
                    
                }
                
            }
        }
        private static void Copy(string inputFile,string outputFile)
        {
            using (var inputStream = File.OpenRead(inputFile))
            using (var outputStream = File.OpenWrite(outputFile))
            
            {
                while (inputStream.Position != inputStream.Length)
                {
                    var buffer = new byte[1024*1024];
                    var bytesRead = inputStream.Read(buffer, 0, buffer.Length);
                    if (bytesRead < buffer.Length)
                    {
                        var newBuffer = new byte[bytesRead];
                        for (int i = 0; i < bytesRead; i++)
                        {
                            newBuffer[i] = buffer[i];
                        }
                        buffer = newBuffer;
                    }
                    
                    outputStream.Write(buffer, 0, buffer.Length);
                }
            }
        }

        private static void Pack(string OutPut, List<string> FileList)
        {
            using (var outputStream = File.OpenWrite(OutPut))
            using (var compressionStream = new BrotliStream(outputStream,CompressionLevel.Optimal))
            {
                using (BinaryWriter writer = new BinaryWriter(compressionStream))
                {

                writer.Write(FileList.Count);
                foreach (var item in FileList)
                {
                    using (var inputStream = File.OpenRead(item))
                    {
                     writer.Write(inputStream.Length);
                     writer.Write(Path.GetFileName(item));//is the path to the file
                     while (inputStream.Position != inputStream.Length)
                         {
                             var buffer = new byte[1024*1024];
                             var bytesRead = inputStream.Read(buffer, 0, buffer.Length);
                             if (bytesRead < buffer.Length)
                             {
                                 var newBuffer = new byte[bytesRead];
                                     for (int i = 0; i < bytesRead; i++)
                                     {
                                         newBuffer[i] = buffer[i];
                                     }
                                 buffer = newBuffer;
                              }       
                              compressionStream.Write(buffer, 0, buffer.Length);
                          }
                    }
                }  
                }
            }
        }
    }
}