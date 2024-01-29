using FileHandlingAPI.Business_Logic;
using FileHandlingAPI.Models;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace FileHandlingAPI.Controllers
{
    /// <summary>
    /// Student Controller which handles api endpoints of student.
    /// </summary>
    [RoutePrefix("api/CLStudent")]
    public class CLStudentController : ApiController
    {
        /// <summary>
        /// GET :- api/CLStudent/GetAllStudentData
        /// </summary>
        /// <returns>All Student Data with Ok Response</returns>
        [HttpGet]
        [Route("GetAllStudentData")]
        public IHttpActionResult GetAllStudentData() => Ok(BLStudent.GetAllStudent());

        /// <summary>
        /// GET :- api/CLStudent/GetStudentById/1
        /// </summary>
        /// <param name="id">Student Id</param>
        /// <returns>Student specific by Id</returns>
        [HttpGet]
        [Route("GetStudentById/{id}")]
        public IHttpActionResult GetStudentById([FromUri] int id) => Ok(BLStudent.GetStudentById(id));

        /// <summary>
        /// POST :- api/CLStudent/AddStudent
        /// </summary>
        /// <param name="objStudent">Student Data</param>
        /// <returns>Ok response</returns>
        [HttpPost]
        [Route("AddStudent")]
        public IHttpActionResult Addstudent([FromBody] STU01 objStudent) => Ok(BLStudent.CreateStudent(objStudent));

        /// <summary>
        /// DELETE :- api/CLStudent/DeleteStudent/1
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("DeleteStudent/{id}")]
        public IHttpActionResult DeleteStudent(int id) => Ok(BLStudent.DeleteStudent(id));

        /// <summary>
        /// GET :- api/CLStudent/FileWrite
        /// Write the student data into file.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("FileWrite")]
        public IHttpActionResult WriteStudentDataIntoFile() => Ok(BLStudent.WriteData());

        /// <summary>
        /// GET :- api/CLStudent/download
        /// For download a file.
        /// </summary>
        /// <returns>bakcup File</returns>
        [HttpGet]
        [Route("download")]
        public HttpResponseMessage DownloadFile() => BLStudent.DownloadFile();

        /// <summary>
        /// POST :- api/CLStudent/upload
        /// Upload your data to server
        /// </summary>
        /// <returns>String response</returns>
        [HttpPost]
        [Route("upload")]
        public async Task<string> UploadFileAsync()
        {
            var ctx = HttpContext.Current;
            var root = ctx.Server.MapPath("~/Upload Data");
            var provider = new MultipartFormDataStreamProvider(root);

            try
            {
                await Request.Content.ReadAsMultipartAsync(provider);

                foreach (var file in provider.FileData)
                {
                    var name = file.Headers.ContentDisposition.FileName;
                    name = name.Trim('"');

                    var localFileName = file.LocalFileName;
                    var fileP = Path.Combine(root, name);

                    File.Copy(localFileName, fileP, true);
                }

                return "File written successfully";
            }
            catch (Exception ex)
            {
                return $"Error :- {ex.Message}";
            }
        }

        /// <summary>
        /// POST :- api/CLStudent/fillData
        /// Filling the student list
        /// </summary>
        /// <param name="day"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("FillData")]
        public HttpResponseMessage FillStudentDataToList(string day, string month, string year) => BLStudent.FillData(day, month, year);

        /// <summary>
        /// GET :- api/CLStudent/GetDirectoryInfo
        /// Getting the directory information of specified path
        /// </summary>
        /// <param name="path">path of directory</param>
        /// <returns>Directory information</returns>
        [HttpGet]
        [Route("GetDirectoryInfo")]
        public IHttpActionResult GetDirInfo(string path)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(path);

            // Creating a Directory
            // dirInfo.Create();

            // Creating sub directories in Deep Patel
            // dirInfo.CreateSubdirectory("College Work");
            // dirInfo.CreateSubdirectory("Internship Work");

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

            return Ok(dirs);
        }

        /// <summary>
        /// GET :- api/CLStudent/GetFileInfo
        /// Performing methods and operation of FileInfo classes.
        /// </summary>
        /// <param name="path">path of file</param>
        /// <returns>File information</returns>
        [HttpGet]
        [Route("GetFileInfo")]
        public IHttpActionResult GetFileInfo(string path)
        {
            FileInfo fileInfo = new FileInfo(path);

            using (StreamWriter writer = fileInfo.CreateText())
            {
                writer.WriteLine("File Created");
            }

            using(StreamWriter writer = fileInfo.AppendText())
            {
                writer.Write("Append text");
            }

            //using (StreamReader reader = fileInfo.OpenText())
            //{
            //    return Ok(reader.ReadToEnd());
            //}

            // Delete a file
            // fileInfo.Delete();

            // Copy a file
            // fileInfo.CopyTo(path2);

            // Move a file
            // fileInfo.MoveTo(path2);

            //Console.WriteLine($"File name is {fileInfo.Name}");
            //Console.WriteLine($"File creation time is {fileInfo.CreationTime.ToLongTimeString()}");
            //Console.WriteLine($"File last access time is {fileInfo.LastAccessTime.ToLongDateString()}");
            //Console.WriteLine($"File length is {fileInfo.Length.ToString()} Bytes");
            //Console.WriteLine($"File extension is {fileInfo.Extension}");
            //Console.WriteLine($"File exists: {fileInfo.Exists}");
            //Console.WriteLine($"File last write time is {fileInfo.LastWriteTime}");

            return Ok(fileInfo);
        }
    }
}
