using EshopWebApi.BusinessLayer.Interfaces.Services;
using EshopWebApi.BusinessLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EshopWebApi.Versions.v2.Controllers
{
    /// <summary>
    /// Product service
    /// </summary>
    [ApiController]
    [ApiVersion("2")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductController"/> class.
        /// </summary>
        /// <param name="productService">Product service</param>
        public ProductController(IProductService productService)
        {
            this.productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }

        /// <summary>
        /// Gets all products
        /// </summary>
        /// <returns>Product´s collection</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProductModel>), StatusCodes.Status200OK)]
        [Route("GetAllProducts")]
        public async Task<IActionResult> GetAllProductsAsync()
        {
            return Ok(await productService.GetAllProductsAsync());
        }

        /// <summary>
        /// Gets product by it´s id
        /// </summary>
        /// <param name="id">Product´s id</param>
        /// <returns>Product detail</returns>
        [HttpGet]
        [ProducesResponseType(typeof(ProductModel), StatusCodes.Status200OK)]
        [Route("GetProductById/{id}")]
        public async Task<IActionResult> GetProductByIdAsync(Guid id)
        {
            return Ok(await productService.GetProductByIdAsync(id));
        }

        /// <summary>
        /// Updates product partially
        /// </summary>
        /// <param name="id">Product´s id</param>
        /// <param name="jsonProductPartial">Partial product´s update represented by <see cref="JsonPatchDocument"/> object</param>
        [HttpPatch]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [Route("UpdatePartialProduct/{id}")]
        public async Task<IActionResult> UpdatePartialProductAsync(Guid id, [FromBody] JsonPatchDocument<ProductPartialModel> jsonProductPartial)
        {
            await productService.UpdatePartialProductAsync(id, jsonProductPartial);
            return Ok();
        }
    }
}
