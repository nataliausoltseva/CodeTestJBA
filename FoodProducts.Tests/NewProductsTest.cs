using FoodProducts.Controllers;
using FoodProducts.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodProducts.Tests
{
    class NewProductsTest
    {
        [Test]
        public async Task TestGetNewProduct()
        {
            DbContextOptions<FoodProductsContext> options = new DbContextOptionsBuilder<FoodProductsContext>().UseInMemoryDatabase(databaseName: "GetNewProduct").Options;
            using (var context = new FoodProductsContext(options))
            {
                context.Add(new NewProducts()
                {
                    Product = "Milo"
                });
                context.SaveChanges();
                NewProducts product1 = context.NewProducts.First();
                NewProductsController newProductsController = new NewProductsController(context);
                IActionResult result = await newProductsController.GetNewProducts(product1.Id) as IActionResult;

                var okObjectResult = result as OkObjectResult;
                Assert.IsNotNull(okObjectResult);

                var model = okObjectResult.Value as NewProducts;
                Assert.IsNotNull(model);
                Assert.AreEqual(product1.Product, model.Product);
            }
        }

        [Test]
        public async Task TestGetNewProducts()
        {
            DbContextOptions<FoodProductsContext> options = new DbContextOptionsBuilder<FoodProductsContext>().UseInMemoryDatabase(databaseName: "GetNewProducts").Options;
            using (var context = new FoodProductsContext(options))
            {
                context.NewProducts.Add(new NewProducts()
                {
                    Product = "Nespresso"
                });
                context.NewProducts.Add(new NewProducts()
                {
                    Product = "Nesquik"
                });

                context.SaveChanges();
                NewProductsController newProductsController = new NewProductsController(context);
                IActionResult result = await newProductsController.GetNewProducts() as IActionResult;

                var okObjectResult = result as OkObjectResult;
                Assert.IsNotNull(okObjectResult);

                var model = okObjectResult.Value as IEnumerable<NewProducts>;
                Assert.IsNotNull(model);
                Assert.AreEqual(2, model.Count());

            }
        }

        [Test]
        public async Task TestPostNewProduct()
        {
            DbContextOptions<FoodProductsContext> options = new DbContextOptionsBuilder<FoodProductsContext>().UseInMemoryDatabase(databaseName: "PostNewProduct").Options;
            using (var context = new FoodProductsContext(options))
            {
                NewProducts currentProducts1 = new NewProducts
                {
                    Product = "Maggi"
                };

                NewProductsController newProductsController = new NewProductsController(context);
                IActionResult result = await newProductsController.PostNewProducts(currentProducts1) as IActionResult;

                var okObjectResult = result as OkObjectResult;
                Assert.IsNotNull(okObjectResult);

                var model = okObjectResult.Value as NewProducts;
                Console.WriteLine(model);
                Assert.IsNotNull(model);
                Assert.AreNotEqual(0, model.Id);
                Assert.AreEqual(currentProducts1.Product, model.Product);

                Assert.AreEqual(true, context.NewProducts.Any(x => x.Id == model.Id));
            }
        }

        [Test]
        public async Task TestDeleteNewProduct()
        {
            DbContextOptions<FoodProductsContext> options = new DbContextOptionsBuilder<FoodProductsContext>().UseInMemoryDatabase(databaseName: "DeleteNewProduct").Options;
            using (var context = new FoodProductsContext(options))
            {
                context.NewProducts.Add(new NewProducts()
                {
                    Product = "Milo"
                });
                context.SaveChanges();

                NewProducts product1 = context.NewProducts.First();

                NewProductsController newProductsController = new NewProductsController(context);
                IActionResult result = await newProductsController.DeleteNewProducts(product1.Id) as IActionResult;

                var okObjectResult = result as OkObjectResult;
                Assert.IsNotNull(okObjectResult);

                var model = okObjectResult.Value as NewProducts;
                Assert.IsNotNull(model);
                Assert.AreNotEqual(0, model.Id);
                Assert.AreEqual(product1.Product, model.Product);

                Assert.AreEqual(false, context.MailingList.Any(x => x.Id == product1.Id));
            }
        }
    }
}
