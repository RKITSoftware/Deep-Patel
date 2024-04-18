﻿using OnlineShoppingAPI.BL.Master.Interface;
using OnlineShoppingAPI.DL;
using OnlineShoppingAPI.Models;
using OnlineShoppingAPI.Models.POCO;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using static OnlineShoppingAPI.BL.Common.BLHelper;

namespace OnlineShoppingAPI.BL.Master.Service
{
    /// <summary>
    /// Service implementation of <see cref="IPFT01Service/>.
    /// </summary>
    public class BLPFT01Handler : IPFT01Service
    {
        #region Private Fields

        /// <summary>
        /// Orm Lite Connection.
        /// </summary>
        private readonly IDbConnectionFactory _dbFactory;

        /// <summary>
        /// Database Context for Mysql queries of <see cref="BLPFT01Handler"/>.
        /// </summary>
        private readonly DBPFT01Context _dbPFT01Context;

        #endregion Private Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BLPFT01Handler"/> class.
        /// </summary>
        public BLPFT01Handler()
        {
            _dbPFT01Context = new DBPFT01Context();
            _dbFactory = HttpContext.Current.Application["DbFactory"] as IDbConnectionFactory;
        }

        #endregion Constructors

        #region Public Methods

        /// <summary>
        /// Retrieves profit data for each day of current running month.
        /// </summary>
        /// <returns>Success response if no error occur else response with error message.</returns>
        public Response GetDayWiseData()
        {
            int month = DateTime.Now.Month;
            int year = DateTime.Now.Year;

            List<decimal> lstData = _dbPFT01Context.GetDayWiseData(month, year);

            if (lstData == null)
            {
                return NoContentResponse();
            }

            return OkResponse("", lstData);
        }

        /// <summary>
        /// Retrieves profit data for each month of current year.
        /// </summary>
        /// <returns>Success response if no error occur else response with error message.</returns>
        public Response GetMonthData()
        {
            int year = DateTime.Now.Year;
            List<decimal> lstData = _dbPFT01Context.GetMonthData(year);

            if (lstData == null)
            {
                return NoContentResponse();
            }

            return OkResponse("", lstData);
        }

        /// <summary>
        /// Retrieves aggregated profit data for last 10 year.
        /// </summary>
        /// <returns>Success response if no error occur else response with error message.</returns>
        public Response GetYearData()
        {
            List<decimal> lstData = _dbPFT01Context.GetYearData();

            if (lstData == null)
            {
                return NoContentResponse();
            }

            return OkResponse("", lstData);
        }

        /// <summary>
        /// Updates profit information based on the provided product and quantity of the today.
        /// </summary>
        /// <param name="objPRO02">The product object for which profit is being updated.</param>
        /// <param name="quantity">The quantity of product being sold.</param>
        public void UpdateProfit(PRO02 objPRO02, int quantity)
        {
            string currentDate = DateTime.Now.ToString("dd-MM-yyyy");
            decimal profitChange = (objPRO02.O02F04 - objPRO02.O02F03) * quantity;

            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                PFT01 objProfit = db.Single<PFT01>(p => p.T01F02 == currentDate);

                if (objProfit == null)
                {
                    db.Insert(new PFT01()
                    {
                        T01F02 = currentDate,
                        T01F03 = profitChange
                    });
                    return;
                }

                objProfit.T01F03 += profitChange;
                db.Update(objProfit);
            }
        }

        #endregion Public Methods
    }
}