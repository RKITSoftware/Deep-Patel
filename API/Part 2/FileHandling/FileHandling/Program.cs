using System;
using System.IO;
using System.Text;

namespace FileHandling
{
    internal class Program
    {
        /// <summary>
        /// Checking a file if it exists or not in PC.
        /// </summary>
        public static void CheckingFileExist()
        {
            // Verbatim literal
            string path = @"C:\Users\DEEP\source\repos\FileHandling\FileHandling\Demo.txt";

            // Handling Exception if file not Exists.
            try
            {
                if (File.Exists(path))
                {
                    Console.WriteLine("Demo.txt Exists");
                }
                else
                {
                    throw new Exception("File does not exist.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Reading file data
        /// </summary>
        public static void ReadingDataFromFile()
        {
            string path = @"C:\Users\DEEP\source\repos\FileHandling\FileHandling\Demo.txt";
            string data = File.ReadAllText(path: path);

            // Reading the text file.
            Console.WriteLine(data);
        }

        /// <summary>
        /// Creating a Copy of Existing File.
        /// </summary>
        public static void CreateCopyOfFile()
        {
            string path1 = @"C:\Users\DEEP\source\repos\FileHandling\FileHandling\Demo.txt";
            string path2 = @"C:\Users\DEEP\source\repos\FileHandling\FileHandling\Data\CopyOfFile.txt";

            // Copying a file data into new file.
            File.Copy(sourceFileName: path1, destFileName: path2);

            // Overwriting a file.
            File.Copy(sourceFileName: path1, destFileName: path2, overwrite: true);
        }

        /// <summary>
        /// DirectoryInfo Class Properties and Methods
        /// </summary>
        public static void DirectoryInfoClass()
        {
            string path = @"E:\Deep Patel";
            string path2 = @"E:\Deep Patel 1";

            DirectoryInfo dirInfo = new DirectoryInfo(path);
            DirectoryInfo dirInfo2 = new DirectoryInfo(path2);

            // Creating a Directory
            dirInfo.Create();

            // Creating sub directories in Deep Patel
            dirInfo.CreateSubdirectory("College Work");
            dirInfo.CreateSubdirectory("Internship Work");

            // Moving Souce directory data to Destination directory.
            // dirInfo.MoveTo(destDirName: path2);

            // Deleting Empty directory
            // dirInfo2.Delete();

            // Deleting all files in Directory
            // dirInfo2.Delete(recursive: true);

            // Get all Directories
            DirectoryInfo[] dirs = dirInfo.GetDirectories();

            foreach (var dir in dirs)
            {
                // Some Properties of DirectoryInfo

                // Console.WriteLine(dir.FullName);
                // Console.WriteLine(dir.LastAccessTime);
                // Console.WriteLine(dir.CreationTime);
                // Console.WriteLine(dir.Attributes);
                // Console.WriteLine(dir.Parent);
                // Console.WriteLine(dir.Root);
                // Console.WriteLine(dir.LastWriteTime);

                Console.WriteLine($"Directory {dir.Name} contains {dir.GetFiles().Length} files.");
            }

            // GetFiles method
        }

        /// <summary>
        /// FileStream Class Methods and Properties
        /// </summary>
        public static void FileStreamClass()
        {
            string path = @"E:\MyFile.txt";
            string data = "Hello World ";

            // FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);
            // FileStream fs = new FileStream(path, FileMode.Truncate, FileAccess.Write);
            FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.Read);

            // Write 68 ASCII value into the file.
            // fs.WriteByte(value: 65); // D

            // Writing a string data into file.
            byte[] writeData = Encoding.UTF8.GetBytes(s: data);
            fs.Write(array: writeData, offset: 0, count: data.Length);

            // Closing the File
            fs.Close();

            // using Function Example
            // using(FileStream fs1 = new FileStream(path, FileMode.Create))
            // {
            //     Console.WriteLine("File Created");
            // }
        }

        /// <summary>
        /// StreamWriter Class Methods and Properties
        /// </summary>
        public static void StreamWriterClass()
        {
            string path = @"E:\Deep Patel.txt";

            using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                using (StreamWriter writer = new StreamWriter(fs, Encoding.UTF8))
                {
                    // writer.WriteLine("Statement 1");
                    // writer.WriteLine("Statement 2");
                    // writer.WriteLine("Statement 3");

                    int[] myArr = { 1, 2, 3, 4, 5 };
                    foreach (int value in myArr)
                    {
                        writer.Write($"{value} ");
                    }
                }
            }
        }

        /// <summary>
        /// StreamReader Class Methods and Properties
        /// </summary>
        public static void StreamReaderClass()
        {
            string path = @"E:\MyFile.txt";

            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader reader = new StreamReader(fs))
                {
                    // string line = reader.ReadLine();
                    // Console.WriteLine(line);

                    // string line = "";
                    // while((line = reader.ReadLine()) != null)
                    // {
                    //     Console.WriteLine(line);
                    // }

                    // string[] names = new string[5];
                    // for (int i = 0; i < names.Length; i++)
                    // {
                    //     names[i] = reader.ReadLine();
                    // }

                    // foreach (string name in names)
                    // {
                    //     Console.WriteLine(name);
                    // }

                    string data = reader.ReadToEnd();
                    Console.WriteLine(data);
                }
            }
        }

        static void Main(string[] args)
        {
            // CheckingFileExist();
            // ReadingDataFromFile();
            // CreateCopyOfFile();
            // DirectoryInfoClass();
            // FileStreamClass();
            // StreamWriterClass();
            StreamReaderClass();
        }
    }
}
