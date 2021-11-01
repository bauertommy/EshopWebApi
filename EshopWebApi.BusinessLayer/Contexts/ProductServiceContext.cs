using AutoMapper;
using EshopWebApi.BusinessLayer.Interfaces.Contexts;
using EshopWebApi.DataLayer;
using System;

namespace EshopWebApi.BusinessLayer.Contexts
{
    /// <summary>
    /// Product service context
    /// </summary>
    public class ProductServiceContext : IProductServiceContext
    {
        ///<inheritdoc/>
        public IDbContext DbContext { get; private set; }

        ///<inheritdoc/>
        public IMapper Mapper { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductServiceContext"/> class.
        /// </summary>
        /// <param name="dbContext">Database context</param>
        /// <param name="mapper">Automapper</param>
        public ProductServiceContext(IDbContext dbContext, IMapper mapper)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
    }
}
