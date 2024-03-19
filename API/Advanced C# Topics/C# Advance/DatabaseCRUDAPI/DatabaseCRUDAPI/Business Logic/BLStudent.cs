using DatabaseCRUDAPI.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
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
        /// An sql connection for CRUD operation of APIs
        /// </summary>
        private readonly MySqlConnection _connection = new MySqlConnection(
                "Server=localhost;Port=3306;Database=college;User Id=Admin;Password=gs@123;");

        /// <summary>
        /// Inserting data into the college database 
        /// </summary>
        /// <param name="objStudent">Student data to insert</param>
        /// <returns>Create response</returns>
        public HttpResponseMessage InsertData(STU01 objStudent)
        {
            try
            {
                using (MySqlCommand cmd = new MySqlCommand(
                    "INSERT INTO STU01 (U01F02, U01F03) VALUES (@Name, @Age)", _connection))
                {
                    MySqlParameter[] mySqlParameters =
                    {
                        new MySqlParameter("@Name", objStudent.U01F02),
                        new MySqlParameter("@Age", objStudent.U01F03)
                    };
                    cmd.Parameters.AddRange(mySqlParameters);

                    //cmd.Parameters.AddWithValue("@Name", objStudent.U01F02);
                    //cmd.Parameters.AddWithValue("@Age", objStudent.U01F03);

                    _connection.Open();
                    cmd.ExecuteNonQuery();
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
                Content = new StringContent("Data added successfully.")
            };
        }

        /// <summary>
        /// Reading the data from college database and add it to list of students.
        /// </summary>
        /// <returns>List of students</returns>
        public List<STU01> ReadData()
        {
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
                }
            }
            catch (Exception)
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
        public HttpResponseMessage UpdateStudent(STU01 objStudent)
        {
            try
            {
                using (MySqlCommand cmd = new MySqlCommand(@"UPDATE 
                                                                STU01 
                                                            SET 
                                                                U01F02=@Name, 
                                                                U01F03=@Age 
                                                            WHERE U01F01=@Id", _connection))
                {
                    MySqlParameter[] mySqlParameters =
                    {
                        new MySqlParameter("@Id", objStudent.U01F01),
                        new MySqlParameter("@Name", objStudent.U01F02),
                        new MySqlParameter("@Age", objStudent.U01F03)
                    };
                    cmd.Parameters.AddRange(mySqlParameters);

                    _connection.Open();
                    cmd.ExecuteNonQuery();
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
        public HttpResponseMessage DeleteStudent(int id)
        {
            try
            {
                using (MySqlCommand cmd = new MySqlCommand(@"DELETE 
                                                                FROM STU01 
                                                                WHERE U01F01 = @Id", _connection))
                {
                    cmd.Parameters.AddWithValue("@Id", id);

                    _connection.Open();
                    cmd.ExecuteNonQuery();
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