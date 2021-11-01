using EshopWebApi.BusinessLayer.Models;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EshopWebApi.BusinessLayer.Interfaces.Services
{
    /// <summary>
    /// Handles business logic for Products
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// Gets all products
        /// </summary>
        /// <returns>A task that represents the asynchronous get operation. The task result contains <see cref="ProductModel"/> collection</returns>
        Task<IEnumerable<ProductModel>> GetAllProductsAsync();

        /// <summary>
        /// Gets product by id
        /// </summary>
        /// <param name="id">Product´s id</param>
        /// <returns>A task that represents the asynchronous get operation. The task result contains <see cref="ProductModel"/></returns>
        Task<ProductModel> GetProductByIdAsync(Guid id);

        /// <summary>
        /// Partial product´s update
        /// </summary>
        /// <param name="id">Product´s id</param>
        /// <param name="jsonProductPatch">Json product patch object</param>
        /// <returns>A task that represents the asynchronous update operation</returns>
        Task UpdatePartialProductAsync(Guid id, JsonPatchDocument<ProductPartialModel> jsonProductPatch);
    }
}
