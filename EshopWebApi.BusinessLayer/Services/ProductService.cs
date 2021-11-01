using EshopWebApi.BusinessLayer.Exceptions;
using EshopWebApi.BusinessLayer.Interfaces.Contexts;
using EshopWebApi.BusinessLayer.Interfaces.Services;
using EshopWebApi.BusinessLayer.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EshopWebApi.BusinessLayer.Services
{
    /// <summary>
    /// Handles business logic for Products
    /// </summary>
    public class ProductService : IProductService
    {
        /// <summary>
        /// Product service context
        /// </summary>
        private readonly IProductServiceContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductService"/> class.
        /// </summary>
        /// <param name="context">Product service context</param>
        public ProductService(IProductServiceContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        ///<inheritdoc/>
        public async Task<IEnumerable<ProductModel>> GetAllProductsAsync()
        {
            var allProducts = await context.DbContext.Product.ToListAsync();
            return context.Mapper.Map<IEnumerable<ProductModel>>(allProducts);
        }

        ///<inheritdoc/>
        public async Task<ProductModel> GetProductByIdAsync(Guid id)
        {
            var product = await context.DbContext.Product.FirstOrDefaultAsync(i => i.Id == id);

            if (product == null)
            {
                throw new NotFoundException();
            }

            return context.Mapper.Map<ProductModel>(product);
        }

        ///<inheritdoc/>
        public async Task UpdatePartialProductAsync(Guid id, JsonPatchDocument<ProductPartialModel> jsonProductPatch)
        {
            if (jsonProductPatch is null)
            {
                throw new ArgumentNullException(nameof(jsonProductPatch));
            }

            var authorFromDB = await context.DbContext.Product.FirstOrDefaultAsync(x => x.Id == id);

            if (authorFromDB == null)
            {
                throw new NotFoundException();
            }

            var authorDTO = context.Mapper.Map<ProductPartialModel>(authorFromDB);

            jsonProductPatch.ApplyTo(authorDTO);

            context.Mapper.Map(authorDTO, authorFromDB);

            await context.DbContext.SaveChangesAsync();
        }
    }
}
