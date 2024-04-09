using OnlineShoppingAPI.Models;
using OnlineShoppingAPI.Models.DTO;
using OnlineShoppingAPI.Models.Enum;
using OnlineShoppingAPI.Models.POCO;

namespace OnlineShoppingAPI.BL.Interface
{
    /// <summary>
    /// Interface service for <see cref="PRO01"/>.
    /// </summary>
    public interface IPRO01Service
    {
        /// <summary>
        /// Deletes the customer specified by Id.
        /// </summary>
        /// <param name="id">Customer id.</param>
        /// <param name="response"><see cref="Response"/> indicating the outcome of the operation.</param>
        void Delete(int id, out Response response);

        /// <summary>
        /// Retrieves all customer information.
        /// </summary>
        /// <param name="response"><see cref="Response"/> indicating the outcome of the operation.</param>
        void GetAll(out Response response);

        /// <summary>
        /// Initialize the object of <see cref="PRO01"/> and prepare it for create or delete operation.
        /// </summary>
        /// <param name="objPRO01DTO">DTO of product.</param>
        /// <param name="operation">Operation to perform.</param>
        void PreSave(DTOPRO01 objPRO01DTO, EnmOperation operation);

        /// <summary>
        /// Performs the create or update operation.
        /// </summary>
        /// <param name="response"><see cref="Response"/> indicating the outcome of the operation.</param>
        void Save(out Response response);

        /// <summary>
        /// Update the quantity of product.
        /// </summary>
        /// <param name="id">Product Id.</param>
        /// <param name="quantity">Quantity that user wants to add.</param>
        /// <param name="response"><see cref="Response"/> indicating the outcome of the operation.</param>
        void UpdateQuantity(int id, int quantity, out Response response);

        /// <summary>
        /// Validates the objects before saving them.
        /// </summary>
        /// <param name="response"><see cref="Response"/> indicating the outcome of the operation.</param>
        /// <returns><see langword="true"/> if validation successful, else <see langword="false"/>.</returns>
        bool Validation(out Response response);
    }
}