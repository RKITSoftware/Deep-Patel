using Newtonsoft.Json;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.XPath;

namespace DataSerializationDemo
{
    internal class Program
    {
        /// <summary>
        /// Convert operation on class object
        /// </summary>
        public static void JsonConvertDemoOfClass()
        {
            STU01 objStudent = new STU01()
            {
                Id = 1,
                Name = "Deep Patel",
                Age = 20
            };

            // Serializing object to json data
            string jsonData = JsonConvert.SerializeObject(objStudent);
            Console.WriteLine(jsonData);

            // Deserialize json data to object
            STU01 objstudent2 = JsonConvert.DeserializeObject<STU01>(jsonData);
            Console.WriteLine($"{objstudent2.Id} {objstudent2.Name} {objstudent2.Age}");
        }

        /// <summary>
        /// Convert operation of list of student object
        /// </summary>
        public static void JsonConvertDemoOfList()
        {
            List<STU01> lstStudent = new List<STU01>()
            {
                new STU01 { Id = 1, Name = "Deep", Age = 20 },
                new STU01 { Id = 2, Name = "Jeet", Age = 20 }
            };

            // Serializing object to json data
            string jsonData = JsonConvert.SerializeObject(lstStudent);
            Console.WriteLine(jsonData);

            // Deserialize json data to object
            List<STU01> objstudent2 = JsonConvert.DeserializeObject < List<STU01>>(jsonData);

            foreach (STU01 obj in objstudent2)
            {
                Console.WriteLine($"{obj.Id} {obj.Name} {obj.Age}");
            }
        }

        /// <summary>
        /// Convert operation on class object
        /// </summary>
        public static void XmlConvertDemoOfClass()
        {
            XmlDocument objXmlDocument = new XmlDocument();
            XPathNavigator xNav = objXmlDocument.CreateNavigator();
            
            STU01 objStudent = new STU01()
            {
                Id = 1,
                Name = "Deep Patel",
                Age = 20
            };

            XmlSerializer x = new XmlSerializer(objStudent.GetType());
            using(var xs = xNav.AppendChild())
            {
                x.Serialize(xs, objStudent);
            }

            Console.WriteLine(objXmlDocument.OuterXml);

            STU01 objStudent2 = (STU01)x.Deserialize(new StringReader(objXmlDocument.OuterXml));
            Console.WriteLine(objStudent2.Id);
            Console.WriteLine(objStudent2.Name);
            Console.WriteLine(objStudent2.Age);
        }

        static void Main(string[] args)
        {
            // Program.JsonConvertDemoOfClass();
            // Program.JsonConvertDemoOfList();
            Program.XmlConvertDemoOfClass();
        }
    }
}