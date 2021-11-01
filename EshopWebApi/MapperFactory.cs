using AutoMapper;
using EshopWebApi.BusinessLayer.Models;
using EshopWebApi.DataLayer.Entities;

namespace EshopWebApi
{
    /// <summary>
    /// Factory that creates mapper instance
    /// </summary>
    public class MapperFactory
    {
        /// <summary>
        /// Create auto mapper instance
        /// </summary>
        /// <returns>Auto mapper instance</returns>
        public IMapper CreateAutoMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Product, ProductModel>();
                cfg.CreateMap<Product, ProductPartialModel>();
                cfg.CreateMap<ProductPartialModel, Product>();
            });

            return config.CreateMapper();
        }
    }
}
