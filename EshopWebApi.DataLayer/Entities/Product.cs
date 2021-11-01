using Microsoft.EntityFrameworkCore;
using System;

namespace EshopWebApi.DataLayer.Entities
{
    /// <summary>
    /// Product entity
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Product´s id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Product´s name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Product´s image URI
        /// </summary>
        public string ImgUri { get; set; }

        /// <summary>
        /// Product´s price
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Product´s description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Configure entity metadata
        /// </summary>
        /// <param name="modelBuilder">Model builder used for configuration entity metadata</param>
        internal static void ConfigureEntity(ModelBuilder modelBuilder)
        {
            var entityBuilder = modelBuilder.Entity<Product>();

            entityBuilder
                .HasKey(k => k.Id);

            entityBuilder
                .Property(p => p.Id)
                .IsRequired();

            entityBuilder
                .Property(p => p.Name)
                .IsRequired();

            entityBuilder
                .Property(p => p.ImgUri)
                .IsRequired();

            entityBuilder
                .Property(p => p.Price)
                .IsRequired();
        }
    }
}
