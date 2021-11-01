using AutoMapper;
using EshopWebApi.DataLayer;

namespace EshopWebApi.BusinessLayer.Interfaces.Contexts
{
    /// <summary>
    /// Product service context
    /// </summary>
    public interface IProductServiceContext
    {
        /// <summary>
        /// Database context
        /// </summary>
        IDbContext DbContext { get; }

        /// <summary>
        /// Automapper
        /// </summary>
        IMapper Mapper { get; }
    }
}
