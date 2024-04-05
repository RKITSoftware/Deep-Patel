using DatabaseCRUDAPI.Models.POCO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace DatabaseCRUDAPI.DAL
{
    /// <summary>
    /// Data Access Layer for CRUD operations related to student data.
    /// </summary>
    public class DBSTU01
    {
        #region Private Fields

        /// <summary>
        /// Database connection using <see cref="MySqlConnection"/>
        /// </summary>
        private readonly MySqlConnection _connection;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the DBSTU01 class with default connection settings.
        /// </summary>
        public DBSTU01()
        {
            _connection = new MySqlConnection(
                "Server=localhost;Port=3306;Database=college;User Id=Admin;Password=gs@123;");
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates a new student record.
        /// </summary>
        /// <param name="objStudent">The student object to be created.</param>
        /// <returns>True if the operation succeeds, otherwise false.</returns>
        public bool Create(STU01 objStudent)
        {
            try
            {
                string query = string.Format(
                    format: @"INSERT INTO 
                                        STU01 (U01F02, U01F03, U01F04, U01F05) 
                                  VALUES ('{0}', {1}, '{2}', '{3}');",
                    objStudent.U01F02,
                    objStudent.U01F03,
                    objStudent.U01F04.ToString("yyyy-MM-dd HH:mm:ss"),
                    objStudent.U01F05.ToString("yyyy-MM-dd HH:mm:ss"));

                MySqlCommand command = new MySqlCommand(query, _connection);

                _connection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                _connection.Close();
            }

            return true;
        }

        /// <summary>
        /// Updates an existing student record.
        /// </summary>
        /// <param name="objStudent">The student object to be updated.</param>
        /// <returns>True if the operation succeeds, otherwise false.</returns>
        public bool Update(STU01 objStudent)
        {
            try
            {
                string query = string.Format(@"UPDATE STU01 
                                               SET 
                                                   U01F02 = '{1}',
                                                   U01F03 = {2},
                                                   U01F05 = '{3}'
                                               WHERE
                                                   U01F01 = {0}",
                                               objStudent.U01F01,
                                               objStudent.U01F02,
                                               objStudent.U01F03,
                                               objStudent.U01F05.ToString("yyyy-MM-dd HH:mm:ss"));

                MySqlCommand cmd = new MySqlCommand(query, _connection);

                _connection.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                _connection.Close();
            }

            return true;
        }

        /// <summary>
        /// Retrieves all student data.
        /// </summary>
        /// <returns>A list of student objects.</returns>
        public List<STU01> GetAllSTU01Data()
        {
            List<STU01> lstSTU01 = new List<STU01>();

            try
            {
                string query = string.Format(@"SELECT 
                                                    U01F01, U01F02, U01F03, U01F04, U01F05
                                                FROM
                                                    STU01;");

                MySqlCommand cmd = new MySqlCommand(query, _connection);

                _connection.Open();

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    string format = "MM/dd/yyyy HH:mm:ss";
                    while (reader.Read())
                    {
                        DateTime.TryParseExact(reader.GetMySqlDateTime(3).ToString(), format,
                            CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime createdAtTime);

                        DateTime.TryParseExact(reader.GetMySqlDateTime(4).ToString(), format,
                            CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime updatedAtTime);

                        lstSTU01.Add(new STU01()
                        {
                            U01F01 = reader.GetInt32(0),
                            U01F02 = reader.GetString(1),
                            U01F03 = reader.GetInt32(2),
                            U01F04 = createdAtTime,
                            U01F05 = updatedAtTime
                        });
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

            return lstSTU01;
        }

        /// <summary>
        /// Deletes a student record by ID.
        /// </summary>
        /// <param name="id">The ID of the student record to be deleted.</param>
        /// <returns>True if the operation succeeds, otherwise false.</returns>
        public bool Delete(int id)
        {
            try
            {
                string query = string.Format(@"DELETE FROM STU01 
                                               WHERE
                                                   U01F01 = {0}", id);

                MySqlCommand cmd = new MySqlCommand(query, _connection);

                _connection.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                _connection.Close();
            }

            return true;
        }

        #endregion
    }
}
