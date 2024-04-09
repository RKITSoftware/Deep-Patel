﻿using OnlineShoppingAPI.BL.Interface;
using OnlineShoppingAPI.DL;
using OnlineShoppingAPI.Extension;
using OnlineShoppingAPI.Models;
using OnlineShoppingAPI.Models.POCO;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.Web;

namespace OnlineShoppingAPI.BL.Service
{
    /// <summary>
    /// Service implementation of <see cref="IPFT01Service/>.
    /// </summary>
    public class BLPFT01 : IPFT01Service
    {
        #region Private Fields

        /// <summary>
        /// Database Context for Mysql queries of <see cref="BLPFT01"/>.
        /// </summary>
        private readonly DBPFT01 _context;

        /// <summary>
        /// Orm Lite Connection.
        /// </summary>
        private readonly IDbConnectionFactory _dbFactory;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BLPFT01"/> class.
        /// </summary>
        public BLPFT01()
        {
            _context = new DBPFT01();
            _dbFactory = HttpContext.Current.Application["DbFactory"] as IDbConnectionFactory;
        }

        #endregion

        /// <summary>
        /// Retrieves profit data for each day of current running month.
        /// </summary>
        /// <param name="response"><see cref="Response"/> indicating the outcome of the operation.</param>
        public void GetDayWiseData(out Response response)
        {
            int month = DateTime.Now.Month;
            int year = DateTime.Now.Year;

            _context.GetData(month, year, out response);
        }

        /// <summary>
        /// Retrieves profit data for each month of current year.
        /// </summary>
        /// <param name="response"><see cref="Response"/> indicating the outcome of the operation.</param>
        public void GetMonthData(out Response response)
        {
            int year = DateTime.Now.Year;
            _context.GetData(year, out response);
        }

        /// <summary>
        /// Retrieves aggregated profit data for last 10 year.
        /// </summary>
        /// <param name="response"><see cref="Response"/> indicating the outcome of the operation.</param>
        public void GetYearData(out Response response) => _context.GetData(out response);

        /// <summary>
        /// Updates profit information based on the provided product and quantity of the today.
        /// </summary>
        /// <param name="objPRO02">The product object for which profit is being updated.</param>
        /// <param name="quantity">The quantity of product being sold.</param>
        public void UpdateProfit(PRO02 objPRO02, int quantity)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    string currentDate = DateTime.Now.ToString("dd-MM-yyyy");
                    PFT01 objProfit = db.Single<PFT01>(p => p.T01F02 == currentDate);

                    if (objProfit != null)
                    {
                        objProfit.T01F03 += (objPRO02.O02F04 - objPRO02.O02F03) * quantity;
                        db.Update(objProfit);
                    }
                    else
                    {
                        db.Insert(new PFT01()
                        {
                            T01F02 = currentDate,
                            T01F03 = (objPRO02.O02F04 - objPRO02.O02F03) * quantity
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                ex.LogException();
            }
        }
    }
}
