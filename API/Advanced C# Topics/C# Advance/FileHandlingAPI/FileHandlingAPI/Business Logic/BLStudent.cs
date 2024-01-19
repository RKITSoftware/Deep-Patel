using FileHandlingAPI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace FileHandlingAPI.Business_Logic
{
    public class BLStudent
    {
        /// <summary>
        /// Student list performs the operation of adding, updating, deleting the student and stores all student data.
        /// </summary>
        private static List<STU01> lstStudent = new List<STU01>();

        /// <summary>
        /// File path of stuent data.
        /// </summary>
        private static string filePath = HttpContext.Current.Server.MapPath("~/Data")
            + "\\Student Data "
            + DateTime.Now.ToString("dd-MM-yyyy")
            + ".txt";

        /// <summary>
        /// For getting all student data
        /// </summary>
        /// <returns>All student data</returns>
        public static List<STU01> GetAllStudent() => lstStudent;

        /// <summary>
        /// For getting student using student id
        /// </summary>
        /// <param name="id">Student id</param>
        /// <returns>Student</returns>
        public static STU01 GetStudentById(int id) => lstStudent.FirstOrDefault(s => s.U01F01 == id);

        /// <summary>
        /// For cretaing a student
        /// </summary>
        /// <param name="objStudent">Student data</param>
        /// <returns>string data</returns>
        public static string CreateStudent(STU01 objStudent)
        {
            STU01 objSTU01 = lstStudent.FirstOrDefault(s => s.U01F01 == objStudent.U01F01);

            if (objSTU01 == null)
            {
                lstStudent.Add(objStudent);
                return "Student Created Successfully.";
            }

            return "Student Already Exists";
        }

        /// <summary>
        /// For deleting a student
        /// </summary>
        /// <param name="id">Student delete id</param>
        /// <returns>Delete response</returns>
        public static string DeleteStudent(int id)
        {
            lstStudent.RemoveAll(s => s.U01F01 == id);
            return "Student deleted successfully.";
        }

        /// <summary>
        /// Create a file with today date
        /// </summary>
        public static void CreateFile()
        {
            if (!File.Exists(filePath))
            {
                File.Create(filePath);
            }
        }

        /// <summary>
        /// Writing data into a file
        /// </summary>
        /// <returns>File written response</returns>
        public static string WriteData()
        {
            CreateFile();

            using(StreamWriter writer = new StreamWriter(filePath)) 
            {
                foreach(STU01 objSTU01 in lstStudent)
                {
                    writer.WriteLine($"{objSTU01.U01F01}, {objSTU01.U01F02}, {objSTU01.U01F03}");
                }
            }
            return "File written successfully.";
        }

        /// <summary>
        /// Download backup file of student data
        /// </summary>
        /// <returns></returns>
        public static HttpResponseMessage DownloadFile()
        {
            if (File.Exists(filePath))
            {
                var dataBytes = File.ReadAllBytes(filePath);
                var dataStream = new MemoryStream(dataBytes);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StreamContent(dataStream);
                response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment") 
                { 

                };
                response.Content.Headers.ContentDisposition.FileName = "Backup of Student Data " + DateTime.Now.ToString("dd-MM-yyyy") + ".txt"; // Set the desired file name
                response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");

                return response;
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
        }

        /// <summary>
        /// GEt the data from older files.
        /// </summary>
        /// <param name="day">Day of data</param>
        /// <param name="month">Month of data</param>
        /// <param name="year">Year of data</param>
        /// <returns>HttpResponseMEssage if files writes then Ok Else NotFound</returns>
        public static HttpResponseMessage FillData(string day, string month, string year)
        {
            string readFilePath = $"{HttpContext.Current.Server.MapPath("~/Data")}\\Student Data {day}-{month}-{year}.txt";
            if (File.Exists(readFilePath))
            {
                lstStudent.Clear();

                using(StreamReader sr = new StreamReader(readFilePath))
                {
                    while(!sr.EndOfStream)
                    {
                        string[] parts = sr.ReadLine().Split(',');

                        lstStudent.Add(new STU01
                        {
                            U01F01 = int.Parse(parts[0]),
                            U01F02 = parts[1],
                            U01F03 = int.Parse(parts[2])
                        });
                    }
                }

                return new HttpResponseMessage(HttpStatusCode.OK);
            }

            return new HttpResponseMessage(HttpStatusCode.NotFound);
        }
    }
}