using MySql.Data.MySqlClient;
using OnlineShoppingAPI.Models;
using OnlineShoppingAPI.Models.POCO;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web;
using System.Web.Caching;

namespace OnlineShoppingAPI.Business_Logic
{
    public class BLCart
    {
        #region Private Fields

        /// <summary>
        /// _dbFactory is used to store the reference of database connection.
        /// </summary>
        private readonly IDbConnectionFactory _dbFactory;

        #endregion

        #region Constructors

        /// <summary>
        /// Static constructor is used to initialize _dbfactory for future reference.
        /// </summary>
        /// <exception cref="ApplicationException">If database can't connect then this exception shows.</exception>
        public BLCart()
        {
            // Getting data connection from Application state
            _dbFactory = HttpContext.Current.Application["DbFactory"] as IDbConnectionFactory;

            // If database can't be connect.
            if (_dbFactory == null)
            {
                throw new ApplicationException("IDbConnectionFactory not found in Application state.");
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds a product to the user's cart based on the specified product information.
        /// </summary>
        /// <param name="objProduct">Product information to be added to the cart.</param>
        /// <returns>
        /// HttpResponseMessage indicating the success or failure of adding the product to the cart.
        /// </returns>
        public HttpResponseMessage Add(CRT01 objProduct)
        {
            try
            {
                // Check if the product data is null.
                if (objProduct == null)
                {
                    return BLHelper.ResponseMessage(HttpStatusCode.PreconditionFailed,
                        "Product data is null.");
                }

                using (var db = _dbFactory.OpenDbConnection())
                {
                    // Retrieve the corresponding source product from the database.
                    PRO02 sourceProduct = db.Single(db.From<PRO02>()
                        .Where(product => product.O02F01 == objProduct.T01F03));

                    // If the source product is not found, return NotFound response.
                    if (sourceProduct == null)
                    {
                        return new HttpResponseMessage(HttpStatusCode.NotFound);
                    }

                    // Check if the quantity in the cart exceeds the available quantity of
                    // the source product.
                    if (sourceProduct.O02F05 < objProduct.T01F04)
                    {
                        return BLHelper.ResponseMessage(HttpStatusCode.PreconditionFailed,
                            "Product can't be bought because the quantity can't be satisfied.");
                    }

                    // Calculate the total price of the product in the cart.
                    objProduct.T01F05 = sourceProduct.O02F04;

                    // Insert the product into the cart.
                    db.Insert(objProduct);

                    // Return a success response.
                    return BLHelper.ResponseMessage(HttpStatusCode.Created,
                        "Product added successfully to cart.");
                }
            }
            catch (Exception ex)
            {
                // Log the exception and return an appropriate response
                BLHelper.LogError(ex);
                return BLHelper.ResponseMessage(HttpStatusCode.InternalServerError,
                    "An error occurred while adding item to cart.");
            }
        }

        /// <summary>
        /// Deletes an item from the customer's cart based on the specified cartId.
        /// </summary>
        /// <param name="cartId">The unique identifier of the item in the cart.</param>
        /// <returns>
        /// HttpResponseMessage indicating the success or failure of deleting the item from the cart.
        /// </returns>
        public HttpResponseMessage Delete(int cartId)
        {
            try
            {
                // Check if the provided cartId is invalid (negative or zero).
                if (cartId <= 0)
                {
                    return BLHelper.ResponseMessage(HttpStatusCode.BadRequest,
                        "Id can't be negative nor zero.");
                }

                using (var db = _dbFactory.OpenDbConnection())
                {
                    // Retrieve the item from the cart based on the cartId.
                    CRT01 objItem = db.SingleById<CRT01>(cartId);

                    // If the item is not found, return NotFound response.
                    if (objItem == null)
                    {
                        return BLHelper.ResponseMessage(HttpStatusCode.NotFound,
                            "Item not found.");
                    }

                    // Delete the item from the cart.
                    db.DeleteById<CRT01>(cartId);

                    // Return a success response.
                    return BLHelper.ResponseMessage(HttpStatusCode.OK,
                        "Item deleted successfully from your cart.");
                }
            }
            catch (Exception ex)
            {
                // Log the exception and return an appropriate response
                BLHelper.LogError(ex);
                return BLHelper.ResponseMessage(HttpStatusCode.InternalServerError,
                    "An error occurred while deleting item from your cart.");
            }
        }


        /// <summary>
        /// Generates an OTP (One-Time Password) and sends it to the customer's registered email.
        /// </summary>
        /// <param name="customerId">The unique identifier of the customer.</param>
        /// <returns>
        /// HttpResponseMessage indicating the success or failure of generating and sending the OTP.
        /// </returns>
        public HttpResponseMessage Generate(int customerId)
        {
            try
            {
                // Check if the customerId is valid (greater than zero).
                if (customerId <= 0)
                {
                    return BLHelper.ResponseMessage(HttpStatusCode.PreconditionFailed,
                        "Customer Id can't be null or negative.");
                }

                using (var db = _dbFactory.OpenDbConnection())
                {
                    // Generate a random OTP.
                    Random random = new Random();
                    string otp = random.Next(0, 999999).ToString("000000");

                    // Retrieve the customer's email address from the database.
                    string email = db.SingleById<CUS01>(customerId)?.S01F03;

                    // If the customer's email is not found, return NotFound response.
                    if (string.IsNullOrEmpty(email))
                    {
                        return BLHelper.ResponseMessage(HttpStatusCode.NotFound,
                            "Customer email not found.");
                    }

                    // Send the OTP to the customer's registered email.
                    SendEmail(email, otp);

                    // Cache the OTP with the customer's email as the key for future validation.
                    BLHelper.ServerCache.Add(
                        key: email,
                        value: otp,
                        dependencies: null,
                        absoluteExpiration: DateTime.Now.AddMinutes(5),
                        slidingExpiration: TimeSpan.Zero,
                        priority: CacheItemPriority.Default,
                        onRemoveCallback: null);
                }

                // Return a success response.
                return BLHelper.ResponseMessage(HttpStatusCode.OK,
                    "OTP sent to customer's registered email.");
            }
            catch (Exception ex)
            {
                // Log the exception and return an appropriate response
                BLHelper.LogError(ex);
                return BLHelper.ResponseMessage(HttpStatusCode.InternalServerError,
                    "An error occurred while generating OTP.");
            }
        }

        /// <summary>
        /// Retrieves the list of items in the customer's cart based on the specified customerId.
        /// </summary>
        /// <param name="customerId">The unique identifier of the customer.</param>
        /// <returns>
        /// List of CRT01 objects representing the items in the customer's cart, or null if an 
        /// error occurs.
        /// </returns>
        public List<CRT01> Get(int customerId)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    // Retrieve the list of items in the customer's cart based on the customerId.
                    return db.Where<CRT01>("T01F02", customerId) ?? new List<CRT01>();
                }
            }
            catch (Exception ex)
            {
                // Log the exception and return an appropriate response
                BLHelper.LogError(ex);
                return null;
            }
        }

        /// <summary>
        /// Verifies the provided OTP (One-Time Password) and initiates the purchase of items 
        /// in the customer's cart.
        /// </summary>
        /// <param name="customerId">The unique identifier of the customer.</param>
        /// <param name="otp">The OTP entered by the customer for verification.</param>
        /// <returns>
        /// HttpResponseMessage indicating the success or failure of OTP verification and the 
        /// subsequent purchase process.
        /// </returns>
        public HttpResponseMessage VerifyAndBuy(int customerId, string otp)
        {
            try
            {
                // Check if the provided customerId is invalid (negative or zero).
                if (customerId <= 0)
                {
                    return BLHelper.ResponseMessage(HttpStatusCode.PreconditionFailed,
                        "Id can't be negative nor zero.");
                }

                using (var db = _dbFactory.OpenDbConnection())
                {
                    // Retrieve the customer's email address from the database.
                    string email = db.SingleById<CUS01>(customerId)?.S01F03;

                    // Retrieve the existing OTP from the cache based on the customer's email.
                    string existingOTP = BLHelper.ServerCache.Get(email)?.ToString();

                    // If no OTP is generated for buying items, return NotFound response.
                    if (existingOTP == null)
                    {
                        return BLHelper.ResponseMessage(HttpStatusCode.NotFound,
                            "OTP isn't generated for buying items.");
                    }

                    // Check if the provided OTP matches the existing OTP for verification.
                    if (!existingOTP.Equals(otp))
                    {
                        return BLHelper.ResponseMessage(HttpStatusCode.BadRequest, "Incorrect OTP.");
                    }

                    // Call the BuyAllItems method to initiate the purchase process.
                    _ = BuyAllItems(customerId);

                    // Remove the OTP from the cache after successful verification.
                    BLHelper.ServerCache.Remove(email);

                    // Return a success response.
                    return BLHelper.ResponseMessage(HttpStatusCode.OK,
                        "Items bought successfully.");
                }
            }
            catch (Exception ex)
            {
                // Log the exception and return an appropriate response
                BLHelper.LogError(ex);
                return BLHelper.ResponseMessage(HttpStatusCode.InternalServerError,
                    "An error occurred while verifying OTP for buying.");
            }
        }

        /// <summary>
        /// An method that return a cart info of specific customer.
        /// </summary>
        /// <param name="id">customer id for finding items in his cart.</param>
        /// <returns>An dynamic list which contains the cart items which user have in his/her cart.</returns>
        public dynamic GetCartInfo(int id)
        {
            try
            {
                dynamic lstCartItems = new List<dynamic>();

                // Creating a MySqlConnection to connect to the database
                using (MySqlConnection _connection = new MySqlConnection(
                    HttpContext.Current.Application["MySQLConnection"] as string))
                {
                    // Using MySqlCommand to execute SQL command
                    using (MySqlCommand cmd = new MySqlCommand(@"SELECT 
                                                                    crt01.T01F01 AS 'Id',
                                                                    pro02.O02F02 AS 'Product Name',
                                                                    crt01.T01F04 AS 'Quantity',
                                                                    (crt01.T01F05 * crt01.T01F04) AS 'Price'
                                                                FROM
                                                                    crt01
                                                                        INNER JOIN
                                                                    pro02 ON crt01.T01F03 = pro02.O02F01
                                                                WHERE
                                                                    crt01.T01F02 = @Id;",
                                                                    _connection))
                    {
                        _connection.Open();
                        cmd.Parameters.AddWithValue("@Id", id);

                        // Using MySqlDataReader to read data from the executed command
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Mapping the data from reader to PRO02 object and adding it to the list
                                lstCartItems.Add(new
                                {
                                    Id = (int)reader[0],
                                    ProductName = (string)reader[1],
                                    Quantity = (int)reader[2],
                                    Price = (long)reader[3]
                                });
                            }
                        }
                    }
                }

                return lstCartItems;
            }
            catch (Exception ex)
            {
                BLHelper.LogError(ex);
                return null;
            }
        }

        /// <summary>
        /// Buy single item from user's cart.
        /// </summary>
        /// <param name="cartId">Cart id for finding that item in cart table.</param>
        /// <returns>HttpResponseMessage if user successfully buy that item or another status code
        /// messages for specific conditions failed during buying that item.</returns>
        public HttpResponseMessage BuyItem(int cartId)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    // Retrieve the item in the cart.
                    CRT01 objCart = db.Single<CRT01>(c => c.T01F01 == cartId);

                    // If the customer has nothing in the cart, return NotFound response.
                    if (objCart == null)
                    {
                        return BLHelper.ResponseMessage(HttpStatusCode.NotFound,
                            "Customer has nothing in their cart.");
                    }

                    CUS01 objCustomer = db.SingleById<CUS01>(objCart.T01F02);

                    // Create an instance of BLRecord to add records for the purchased items.
                    BLRecord objRecord = new BLRecord();

                    List<CRT01> lstCart = new List<CRT01>()
                    {
                        objCart
                    };

                    // Call the AddRecords method to add records for all items in the cart.
                    return objRecord.BuyCartItems(lstCart, objCustomer.S01F03);
                }
            }
            catch (Exception ex)
            {
                // Log the exception and return an appropriate response
                BLHelper.LogError(ex);
                return BLHelper.ResponseMessage(HttpStatusCode.InternalServerError,
                        "An error occurred while buying all items in the cart.");
            }
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Buys all items in the customer's cart and adds corresponding records to the system.
        /// </summary>
        /// <param name="customerId">The unique identifier of the customer.</param>
        /// <returns>
        /// HttpResponseMessage indicating the success or failure of buying all items in the cart.
        /// </returns>
        private HttpResponseMessage BuyAllItems(int customerId)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    // Retrieve the list of items in the customer's cart.
                    List<CRT01> lstItems = db.Where<CRT01>("T01F02", customerId);

                    // If the customer has nothing in the cart, return NotFound response.
                    if (lstItems == null)
                    {
                        return BLHelper.ResponseMessage(HttpStatusCode.NotFound,
                            "Customer has nothing in their cart.");
                    }

                    CUS01 objCustomer = db.SingleById<CUS01>(customerId);

                    // Create an instance of BLRecord to add records for the purchased items.
                    BLRecord objRecord = new BLRecord();

                    // Call the AddRecords method to add records for all items in the cart.
                    return objRecord.BuyCartItems(lstItems, objCustomer.S01F03);
                }
            }
            catch (Exception ex)
            {
                // Log the exception and return an appropriate response
                BLHelper.LogError(ex);
                return BLHelper.ResponseMessage(HttpStatusCode.InternalServerError,
                        "An error occurred while buying all items in the cart.");
            }
        }

        /// <summary>
        /// Sends an email with the generated OTP to the specified email address.
        /// </summary>
        /// <param name="email">The recipient's email address.</param>
        /// <param name="otp">The generated OTP (One-Time Password).</param>
        private void SendEmail(string email, string otp)
        {
            try
            {
                // Initialize SMTP client with Office 365 settings.
                SmtpClient smtpClient = new SmtpClient("smtp.office365.com", 587);
                smtpClient.Credentials = HttpContext.Current
                    .Application["Credentials"] as NetworkCredential;

                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.EnableSsl = true;

                // Create a mail message with sender, recipient, subject, and body.
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("deeppatel2513@outlook.com", "Deep Patel");
                mailMessage.To.Add(new MailAddress(email));

                mailMessage.Subject = "OTP for Buying";
                mailMessage.Body = $"OTP for buying items in your cart: {otp}";

                // Send the mail message.
                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                BLHelper.LogError(ex);
                // You might want to rethrow the exception if it needs further handling at a higher level.
            }
        }

        #endregion
    }
}