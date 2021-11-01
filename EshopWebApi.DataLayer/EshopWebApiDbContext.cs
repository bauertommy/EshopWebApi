using EshopWebApi.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace EshopWebApi.DataLayer
{
    ///<inheritdoc/>
    public class EshopWebApiDbContext : DbContext, IDbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EshopWebApiDbContext"/> class.
        /// </summary>
        /// <param name="options"><see cref="DbContextOptions"/> parameter for setting database options</param>
        public EshopWebApiDbContext(DbContextOptions<EshopWebApiDbContext> options) : base(options)
        { }

        ///<inheritdoc/>
        public DbSet<Product> Product { get; set; }

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            Entities.Product.ConfigureEntity(modelBuilder);
        }
    }
}
