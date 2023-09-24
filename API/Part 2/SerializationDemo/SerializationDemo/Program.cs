using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace SerializationDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Serialization

            string path = @"C:\Users\DEEP\source\repos\SerializationDemo\SerializationDemo\Deep\Data.txt";
            Employee emp = new Employee(1, "Deep");

            FileStream stream = new FileStream(path: path, mode: FileMode.OpenOrCreate);
            BinaryFormatter formatter = new BinaryFormatter();

            // formatter.Serialize(stream, emp);

            // De-Serialization
            Employee emp1 = (Employee)formatter.Deserialize(stream);
            Console.WriteLine(emp1.Id + " " + emp1.Name);

            // Stream Closing
            stream.Close();
        }
    }
}
