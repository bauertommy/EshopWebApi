using EshopWebApi.DataLayer;
using EshopWebApi.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace EshopWebApi
{
    /// <summary>
    /// Factory that creates database context instance
    /// </summary>
    public class DbContextFactory
    {
        /// <summary>
        /// Create auto mapper instance
        /// </summary>
        /// <returns>Auto mapper instance</returns>
        public IDbContext CreateDbContext()
        {
            var dbContext = new EshopWebApiDbContext(new DbContextOptionsBuilder<EshopWebApiDbContext>().UseInMemoryDatabase("EshopWebApi").Options);
            SetInitialDb(dbContext);
            return dbContext;
        }

        /// <summary>
        /// Initial setup for database
        /// </summary>
        private void SetInitialDb(IDbContext dbContext)
        {
            dbContext.Product.Add(new Product() { Id = Guid.Parse("195e0294-1a0d-4bc9-a86f-ecbf54cd0810"), Name = "Nokia 3310", ImgUri = "/images/nokia3310.png", Description = "Super Nokia 3310", Price = 110 });
            dbContext.Product.Add(new Product() { Id = Guid.Parse("195e0294-1a0d-4bc9-a86f-ecbf54cd0811"), Name = "IPhone 5", ImgUri = "/images/iphone5.png", Description = "Starý IPhone 5", Price = 111 });
            dbContext.Product.Add(new Product() { Id = Guid.Parse("195e0294-1a0d-4bc9-a86f-ecbf54cd0812"), Name = "Samsung S2", ImgUri = "/images/samsungs2.png", Description = "Pomalý Samsung S2", Price = 112 });
            dbContext.Product.Add(new Product() { Id = Guid.Parse("195e0294-1a0d-4bc9-a86f-ecbf54cd0813"), Name = "Xiaomi Redmi 10", ImgUri = "/images/redmi10.png", Description = "Čínský Redmi 10", Price = 113 });
            dbContext.Product.Add(new Product() { Id = Guid.Parse("195e0294-1a0d-4bc9-a86f-ecbf54cd0814"), Name = "POCO X3", ImgUri = "/images/pocox3.png", Description = "Super Xiaomi mobil", Price = 114 });
            dbContext.Product.Add(new Product() { Id = Guid.Parse("195e0294-1a0d-4bc9-a86f-ecbf54cd0815"), Name = "LG 560", ImgUri = "/images/lg560.png", Description = "Velmi starý mobil", Price = 115 });
            dbContext.Product.Add(new Product() { Id = Guid.Parse("195e0294-1a0d-4bc9-a86f-ecbf54cd0816"), Name = "ZOPO G300", ImgUri = "/images/zopog300.png", Description = "Neznámá značka", Price = 116 });
            dbContext.Product.Add(new Product() { Id = Guid.Parse("195e0294-1a0d-4bc9-a86f-ecbf54cd0817"), Name = "IPhone 6", ImgUri = "/images/iphone6.png", Description = "Kultovní kus", Price = 117 });
            dbContext.Product.Add(new Product() { Id = Guid.Parse("195e0294-1a0d-4bc9-a86f-ecbf54cd0818"), Name = "Samsung S20", ImgUri = "/images/samsungs20.png", Description = "Top model", Price = 118 });
            dbContext.SaveChanges();
        }
    }
}
