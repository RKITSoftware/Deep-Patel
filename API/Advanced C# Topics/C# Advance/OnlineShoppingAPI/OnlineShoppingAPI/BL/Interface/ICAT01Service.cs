using OnlineShoppingAPI.Models;
using OnlineShoppingAPI.Models.DTO;
using OnlineShoppingAPI.Models.Enum;

namespace OnlineShoppingAPI.BL.Interface
{
    /// <summary>
    /// Interface for service handling category-related operations.
    /// </summary>
    public interface ICAT01Service
    {
        /// <summary>
        /// Deletes a category by its ID.
        /// </summary>
        /// <param name="id">The ID of the category to be deleted.</param>
        /// <param name="response">Out parameter containing the response status after deletion.</param>
        void Delete(int id, out Response response);

        /// <summary>
        /// Retrieves all categories.
        /// </summary>
        /// <param name="response">Out parameter containing the response with all categories.</param>
        void GetAll(out Response response);

        /// <summary>
        /// Retrieves a category by its ID.
        /// </summary>
        /// <param name="id">The ID of the category to retrieve.</param>
        /// <param name="response">Out parameter containing the response with the requested category.</param>
        void GetById(int id, out Response response);

        /// <summary>
        /// Prepares for saving a category.
        /// </summary>
        /// <param name="objDTOCAT01">Data Transfer Object representing the category.</param>
        /// <param name="operation">Operation type for the save action.</param>
        void PreSave(DTOCAT01 objDTOCAT01, EnmOperation operation);

        /// <summary>
        /// Saves changes made to a category.
        /// </summary>
        /// <param name="operation">Operation type for the save action.</param>
        /// <param name="response">Out parameter containing the response status after saving.</param>
        void Save(EnmOperation operation, out Response response);

        /// <summary>
        /// Validates category information.
        /// </summary>
        /// <param name="response">Out parameter containing the validation result.</param>
        /// <returns>True if the category information is valid, otherwise false.</returns>
        bool Validation(out Response response);
    }
}
