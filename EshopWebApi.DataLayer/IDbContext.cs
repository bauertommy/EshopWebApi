using EshopWebApi.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace EshopWebApi.DataLayer
{
    /// <summary>
    /// Database context used for access to the database
    /// </summary>
    public interface IDbContext
    {
        /// <summary>
        /// Set for <see cref="Product"/> database entity
        /// </summary>
        DbSet<Product> Product { get; set; }

        /// <summary>
        /// Saves all changes made in this context to the database.
        /// </summary>
        /// <param name="cancellationToken">A <see cref="System.Threading.CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous save operation. The task result contains the number of state entries written to the database.</returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Saves all changes made in this context to the database
        /// </summary>
        /// <returns>The number of entries written to the database</returns>
        int SaveChanges();
    }
}