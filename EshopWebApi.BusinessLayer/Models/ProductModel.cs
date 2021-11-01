using System;

namespace EshopWebApi.BusinessLayer.Models
{
    /// <summary>
    /// Product DTO model
    /// </summary>
    public class ProductModel
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
    }
}
