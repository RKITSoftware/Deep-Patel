using MySql.Data.MySqlClient;
using OnlineShoppingAPI.Extension;
using OnlineShoppingAPI.Models;
using OnlineShoppingAPI.Models.POCO;
using System;
using System.Collections.Generic;
using System.Configuration;
using static OnlineShoppingAPI.BL.Common.BLHelper;

namespace OnlineShoppingAPI.DL
{
    /// <summary>
    /// DB for <see cref="PFT01"/> Model.
    /// </summary>
    public class DBPFT01
    {
        #region Private Fields

        /// <summary>
        /// Connection string for the database connection.
        /// </summary>
        private readonly string _connectionString;

        /// <summary>
        /// <see cref="MySqlConnection"/> for execute MySql Queries.
        /// </summary>
        private readonly MySqlConnection _connection;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the DBPFT01 class with default connection settings.
        /// </summary>
        public DBPFT01()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
            _connection = new MySqlConnection(_connectionString);
        }

        #endregion

        #region Public Method

        /// <summary>
        /// Gets of last 10 years data
        /// </summary>
        /// <param name="response">Response indicating the outcome of this method.</param>
        public void GetData(out Response response)
        {
            List<decimal> lstData = new List<decimal>();

            try
            {
                _connection.Open();
                int currentYear = DateTime.Now.Year;

                for (int year = currentYear - 9; year <= currentYear; year++)
                {
                    string query = string.Format(@"SELECT 
                                                       SUM(T01F03) AS 'Profit' 
                                                   FROM 
                                                       pft01 
                                                   WHERE T01F02 LIKE '__-__-{0}'",
                                                   year.ToString("0000"));

                    MySqlCommand command = new MySqlCommand(query, _connection);
                    object result = command.ExecuteScalar();

                    if (result != DBNull.Value && result != null)
                    {
                        decimal profit = Convert.ToDecimal(result);
                        lstData.Add(profit);
                    }
                    else
                    {
                        lstData.Add(0);
                    }
                }

                response = OkResponse("Success.");
                response.Data = lstData;
            }
            catch (Exception ex)
            {
                ex.LogException();
                response = ISEResponse();
            }
        }

        /// <summary>
        /// Gets the data of months of this year.
        /// </summary>
        /// <param name="year">Current year</param>
        /// <param name="response">Response indicating the outcome of this method.</param>
        public void GetData(int year, out Response response)
        {
            List<decimal> lstData = new List<decimal>();

            try
            {
                _connection.Open();

                for (int month = 1; month <= 12; month++)
                {
                    string query = string.Format(@"SELECT 
                                                       SUM(T01F03) AS 'Profit' 
                                                   FROM 
                                                       pft01 
                                                   WHERE T01F02 LIKE '__-{0}-{1}'",
                                                       month.ToString("00"),
                                                       year.ToString("0000"));

                    MySqlCommand command = new MySqlCommand(query, _connection);
                    object result = command.ExecuteScalar();

                    if (result != DBNull.Value && result != null)
                    {
                        decimal profit = Convert.ToDecimal(result);
                        lstData.Add(profit);
                    }
                    else
                    {
                        lstData.Add(0);
                    }
                }

                response = OkResponse("Success.");
                response.Data = lstData;
            }
            catch (Exception ex)
            {
                ex.LogException();
                response = ISEResponse();
            }
        }

        /// <summary>
        /// Gets the day wise data of this running month.
        /// </summary>
        /// <param name="month">Current month.</param>
        /// <param name="year">Current year.</param>
        /// <param name="response">Response indicating the outcome of this method.</param>
        public void GetData(int month, int year, out Response response)
        {
            int days = DateTime.DaysInMonth(year, month);
            List<decimal> lstData = new List<decimal>();

            try
            {
                _connection.Open();

                for (int day = 1; day <= days; day++)
                {
                    string query = string.Format(@"SELECT 
                                                       SUM(T01F03) AS 'Profit' 
                                                   FROM 
                                                       pft01 
                                                   WHERE T01F02 LIKE '{0}-{1}-{2}'",
                                                       day.ToString("00"),
                                                       month.ToString("00"),
                                                       year.ToString("0000"));

                    MySqlCommand command = new MySqlCommand(query, _connection);
                    object result = command.ExecuteScalar();

                    if (result != DBNull.Value && result != null)
                    {
                        decimal profit = Convert.ToDecimal(result);
                        lstData.Add(profit);
                    }
                    else
                    {
                        lstData.Add(0);
                    }
                }

                response = OkResponse("Success.");
                response.Data = lstData;
            }
            catch (Exception ex)
            {
                ex.LogException();
                response = ISEResponse();
            }
        }

        #endregion
    }
}