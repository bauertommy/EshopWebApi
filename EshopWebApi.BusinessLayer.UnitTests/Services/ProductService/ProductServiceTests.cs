using AutoMapper;
using EshopWebApi.BusinessLayer.Exceptions;
using EshopWebApi.BusinessLayer.Interfaces.Contexts;
using EshopWebApi.BusinessLayer.Models;
using EshopWebApi.DataLayer;
using EshopWebApi.DataLayer.Entities;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;

namespace EshopWebApi.BusinessLayer.UnitTests.Services.ProductService
{
    /// <summary>
    /// Test class used for test <see cref="EshopWebApi.BusinessLayer.Services.ProductService"/>
    /// </summary>
    [TestClass]
    public class ProductServiceTests
    {
        /// <summary>
        /// Product service context mock
        /// </summary>
        private static Mock<IProductServiceContext> productServiceContext;

        /// <summary>
        /// Initialization used for one time initial setting per tests run
        /// </summary>
        /// <param name="testContext">Test context</param>
        [AssemblyInitialize()]
        public static void Initialize(TestContext testContext)
        {
            var dbContext = new EshopWebApiDbContext(new DbContextOptionsBuilder<EshopWebApiDbContext>().UseInMemoryDatabase("MoqDbContext").Options);
            dbContext.Product.Add(new Product() { Id = Guid.Parse("195e0294-1a0d-4bc9-a86f-ecbf54cd0810"), Name = "Phone0", ImgUri = "/images/phone0", Description = "Nokia phone0", Price = 110 });
            dbContext.Product.Add(new Product() { Id = Guid.Parse("195e0294-1a0d-4bc9-a86f-ecbf54cd0811"), Name = "Phone1", ImgUri = "/images/phone1", Description = "Nokia phone1", Price = 111 });
            dbContext.Product.Add(new Product() { Id = Guid.Parse("195e0294-1a0d-4bc9-a86f-ecbf54cd0812"), Name = "Phone2", ImgUri = "/images/phone2", Description = "Nokia phone2", Price = 112 });
            dbContext.Product.Add(new Product() { Id = Guid.Parse("195e0294-1a0d-4bc9-a86f-ecbf54cd0813"), Name = "Phone3", ImgUri = "/images/phone3", Description = "Nokia phone3", Price = 113 });
            dbContext.Product.Add(new Product() { Id = Guid.Parse("195e0294-1a0d-4bc9-a86f-ecbf54cd0814"), Name = "Phone4", ImgUri = "/images/phone4", Description = "Nokia phone4", Price = 114 });
            dbContext.Product.Add(new Product() { Id = Guid.Parse("195e0294-1a0d-4bc9-a86f-ecbf54cd0815"), Name = "Phone5", ImgUri = "/images/phone5", Description = "Nokia phone5", Price = 115 });
            dbContext.SaveChanges();

            IMapper mapper = new MapperFactory().CreateAutoMapper();
            productServiceContext = new Mock<IProductServiceContext>();
            productServiceContext.Setup(i => i.DbContext).Returns(dbContext);
            productServiceContext.Setup(i => i.Mapper).Returns(mapper);

        }

        /// <summary>
        /// Tests whether <see cref="BusinessLayer.Services.ProductService.GetAllProductsAsync"/> returns all products from database
        /// </summary>
        [TestMethod]
        public void GetAllProducts()
        {
            var productService = new BusinessLayer.Services.ProductService(productServiceContext.Object);
            var allProducts = productService.GetAllProductsAsync().GetAwaiter().GetResult();
            Assert.AreEqual(6, allProducts.Count());
        }

        /// <summary>
        /// Tests wheter <see cref=" BusinessLayer.Services.ProductService.GetProductByIdAsync(Guid)"/> throws <see cref="NotFoundException"/> when no record was found
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public void GetProductByIdThrowsNotFound()
        {
            var productService = new BusinessLayer.Services.ProductService(productServiceContext.Object);
            productService.GetProductByIdAsync(Guid.Parse("195e0294-1a0d-4bc9-a86f-ecbf54cd0809")).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Tests wheter <see cref=" BusinessLayer.Services.ProductService.GetProductByIdAsync(Guid)"/> returns product according its id
        /// </summary>
        [TestMethod]
        public void GetProductById()
        {
            var productService = new BusinessLayer.Services.ProductService(productServiceContext.Object);
            var expectedProduct = new Product() { Id = Guid.Parse("195e0294-1a0d-4bc9-a86f-ecbf54cd0810"), Name = "Phone0", ImgUri = "/images/phone0", Description = "Nokia phone0", Price = 110 };
            var actualProduct = productService.GetProductByIdAsync(expectedProduct.Id).GetAwaiter().GetResult();

            Assert.AreEqual(expectedProduct.Id, actualProduct.Id);
            Assert.AreEqual(expectedProduct.ImgUri, actualProduct.ImgUri);
            Assert.AreEqual(expectedProduct.Name, actualProduct.Name);
            Assert.AreEqual(expectedProduct.Price, actualProduct.Price);
            Assert.AreEqual(expectedProduct.Description, actualProduct.Description);
        }

        /// <summary>
        /// Tests wheter <see cref=" BusinessLayer.Services.ProductService.UpdatePartialProductAsync(Guid, JsonPatchDocument{ProductPartialModel})"/> throws <see cref="ArgumentNullException"/> when <see cref="JsonPatchDocument{ProductPartialModel}"/> was null
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UpdatePartialProductThrowsArgumentNull()
        {
            var productService = new BusinessLayer.Services.ProductService(productServiceContext.Object);
            productService.UpdatePartialProductAsync(Guid.Parse("195e0294-1a0d-4bc9-a86f-ecbf54cd0810"), null).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Tests wheter <see cref=" BusinessLayer.Services.ProductService.UpdatePartialProductAsync(Guid, JsonPatchDocument{ProductPartialModel})"/> throws <see cref="NotFoundException"/> when record to update was not found
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public void UpdatePartialProductThrowsNotFound()
        {
            var jsonProductPatch = new JsonPatchDocument<ProductPartialModel>();
            jsonProductPatch.Replace(i => i.Description, "New product description");
            var productService = new BusinessLayer.Services.ProductService(productServiceContext.Object);
            productService.UpdatePartialProductAsync(Guid.Parse("195e0294-1a0d-4bc9-a86f-ecbf54cd0809"), jsonProductPatch).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Tests wheter <see cref=" BusinessLayer.Services.ProductService.UpdatePartialProductAsync(Guid, JsonPatchDocument{ProductPartialModel})"/> updates only product description
        /// </summary>
        [TestMethod]
        public void UpdatePartialProduct()
        {
            var dbContext = new EshopWebApiDbContext(new DbContextOptionsBuilder<EshopWebApiDbContext>().UseInMemoryDatabase("Moq1DbContext").Options);
            dbContext.Product.Add(new Product() { Id = Guid.Parse("195e0294-1a0d-4bc9-a86f-ecbf54cd0810"), Name = "Phone0", ImgUri = "/images/phone0", Description = "Nokia phone0", Price = 110 });
            dbContext.SaveChanges();

            IMapper mapper = new MapperFactory().CreateAutoMapper();
            var context = new Mock<IProductServiceContext>();
            context.Setup(i => i.DbContext).Returns(dbContext);
            context.Setup(i => i.Mapper).Returns(mapper);

            var newDescription = "New product description";
            var jsonProductPatch = new JsonPatchDocument<ProductPartialModel>();
            jsonProductPatch.Replace(i => i.Description, newDescription);
            var productService = new BusinessLayer.Services.ProductService(context.Object);
            var productId = Guid.Parse("195e0294-1a0d-4bc9-a86f-ecbf54cd0810");
            var oldProduct = context.Object.DbContext.Product.Where(i => i.Id == productId).Select(i => new { i.Id, i.ImgUri, i.Name, i.Price, i.Description }).Single();

            productService.UpdatePartialProductAsync(productId, jsonProductPatch).GetAwaiter().GetResult();

            var updatedProduct = context.Object.DbContext.Product.Single(i => i.Id == productId);

            Assert.AreEqual(oldProduct.Id, updatedProduct.Id);
            Assert.AreEqual(oldProduct.ImgUri, updatedProduct.ImgUri);
            Assert.AreEqual(oldProduct.Name, updatedProduct.Name);
            Assert.AreEqual(oldProduct.Price, updatedProduct.Price);
            Assert.AreEqual(newDescription, updatedProduct.Description);
        }
    }
}
