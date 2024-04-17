using MySql.Data.MySqlClient;
using OnlineShoppingAPI.Models.POCO;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace OnlineShoppingAPI.DL
{
    /// <summary>
    /// DB for <see cref="PFT01"/> Model.
    /// </summary>
    public class DBPFT01Context
    {
        #region Private Fields

        /// <summary>
        /// <see cref="MySqlConnection"/> for execute MySql Queries.
        /// </summary>
        private readonly MySqlConnection _connection;

        /// <summary>
        /// Connection string for the database connection.
        /// </summary>
        private readonly string _connectionString;

        #endregion Private Fields

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the DBPFT01 class with default connection settings.
        /// </summary>
        public DBPFT01Context()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
            _connection = new MySqlConnection(_connectionString);
        }

        #endregion Constructor

        #region Public Method

        /// <summary>
        /// Gets of last 10 years data
        /// </summary>
        /// <returns>List of last 10 years profit data.</returns>
        public List<decimal> GetYearData()
        {
            List<decimal> lstData = new List<decimal>();
            int currentYear = DateTime.Now.Year;

            try
            {
                _connection.Open();

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
                        lstData.Add(0);
                }
            }
            finally { _connection.Close(); }

            return lstData;
        }

        /// <summary>
        /// Gets the data of months of this year.
        /// </summary>
        /// <param name="year">Current year</param>
        /// <returns>List of this year's months profit data.</returns>
        public List<decimal> GetMonthData(int year)
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
                        lstData.Add(0);
                }
            }
            finally { _connection.Close(); }

            return lstData;
        }

        /// <summary>
        /// Gets the day wise data of this running month.
        /// </summary>
        /// <param name="month">Current month.</param>
        /// <param name="year">Current year.</param>
        /// <returns>List of this month's daywise profit.</returns>
        public List<decimal> GetDayWiseData(int month, int year)
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
                        lstData.Add(0);
                }
            }
            finally { _connection.Close(); }

            return lstData;
        }

        #endregion Public Method
    }
}