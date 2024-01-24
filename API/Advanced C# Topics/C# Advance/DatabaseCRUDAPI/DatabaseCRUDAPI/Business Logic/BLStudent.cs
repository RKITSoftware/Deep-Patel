using DatabaseCRUDAPI.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Net.Http;

namespace DatabaseCRUDAPI.Business_Logic
{
    /// <summary>
    /// Helper class for student controller
    /// </summary>
    public class BLStudent
    {
        /// <summary>
        /// Inserting data into the college database 
        /// </summary>
        /// <param name="objStudent">Student data to insert</param>
        /// <returns>Create response</returns>
        public static HttpResponseMessage InsertData(STU01 objStudent)
        {
            MySqlConnection _connection = new MySqlConnection("Server=localhost;Port=3306;Database=college;User Id=Admin;Password=gs@123;");

            try
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = _connection;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "INSERT INTO STU01 (U01F02, U01F03) VALUES (@Name, @Age)";

                    cmd.Parameters.AddWithValue("@Name", objStudent.U01F02);
                    cmd.Parameters.AddWithValue("@Age", objStudent.U01F03);

                    _connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch(Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(ex.Message)
                };
            }
            finally
            {
                _connection.Close();
            }

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("Data added successfully.")
            };
        }

        /// <summary>
        /// Reading the data from college database and add it to list of students.
        /// </summary>
        /// <returns>List of students</returns>
        public static List<STU01> ReadData()
        {
            MySqlConnection _connection = new MySqlConnection("Server=localhost;Port=3306;Database=college;User Id=Admin;Password=gs@123;");
            List<STU01> lstStudent = new List<STU01>();

            try
            {
                using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM STU01;", _connection))
                {
                    _connection.Open();

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lstStudent.Add(new STU01()
                            {
                                U01F01 = (int)reader[0],
                                U01F02 = (string)reader[1],
                                U01F03 = (int)reader[2]
                            });
                        }
                    }

                    _connection.Clone();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                _connection.Close();
            }

            return lstStudent;
        }

        /// <summary>
        /// Updating a student information using student id
        /// </summary>
        /// <param name="objStudent">Student information</param>
        /// <returns>Update response</returns>
        public static HttpResponseMessage UpdateStudent(STU01 objStudent)
        {
            MySqlConnection _connection = new MySqlConnection("Server=localhost;Port=3306;Database=college;User Id=Admin;Password=gs@123;");

            try
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = _connection;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "UPDATE STU01 " +
                                        "SET U01F02=@Name, U01F03=@Age W" +
                                        "HERE U01F01=@Id";

                    cmd.Parameters.AddWithValue("@Id", objStudent.U01F01);
                    cmd.Parameters.AddWithValue("@Name", objStudent.U01F02);
                    cmd.Parameters.AddWithValue("@Age", objStudent.U01F03);

                    _connection.Open();
                    cmd.ExecuteNonQuery();
                    _connection.Clone();
                }
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(ex.Message)
                };
            }
            finally
            {
                _connection.Close();
            }

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("Data updated successfully.")
            };
        }

        /// <summary>
        /// Deleting a record in the college database using student id
        /// </summary>
        /// <param name="id">Student id</param>
        /// <returns>Delete response</returns>
        public static HttpResponseMessage DeleteStudent(int id)
        {
            MySqlConnection _connection = new MySqlConnection("Server=localhost;Port=3306;Database=college;User Id=Admin;Password=gs@123;");

            try
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = _connection;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "DELETE FROM STU01 " +
                                        "WHERE U01F01=@Id";

                    cmd.Parameters.AddWithValue("@Id", id);

                    _connection.Open();
                    cmd.ExecuteNonQuery();
                    _connection.Clone();
                }
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(ex.Message)
                };
            }
            finally
            {
                _connection.Close();
            }

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("Data deleted successfully.")
            };
        }
    }
}