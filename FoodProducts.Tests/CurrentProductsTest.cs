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
    class CurrentProductsTest
    {
        [Test]
        public async Task TestGetCurrentProduct()
        {
            DbContextOptions<FoodProductsContext> options = new DbContextOptionsBuilder<FoodProductsContext>().UseInMemoryDatabase(databaseName: "GetCurrentProduct").Options;
            using (var context = new FoodProductsContext(options))
            {
                context.Add(new CurrentProducts()
                {
                    Product = "Milo"
                });
                context.SaveChanges();
                CurrentProducts currentProducts1 = context.CurrentProducts.First();
                CurrentProductsController mailingListsController = new CurrentProductsController(context);
                IActionResult result = await mailingListsController.GetCurrentProducts(currentProducts1.Id) as IActionResult;

                var okObjectResult = result as OkObjectResult;
                Assert.IsNotNull(okObjectResult);

                var model = okObjectResult.Value as CurrentProducts;
                Assert.IsNotNull(model);
                Assert.AreEqual(currentProducts1.Product, model.Product);
            }
        }

        [Test]
        public async Task TestGetCurrentProducts()
        {
            DbContextOptions<FoodProductsContext> options = new DbContextOptionsBuilder<FoodProductsContext>().UseInMemoryDatabase(databaseName: "GetCurrentProducts").Options;
            using (var context = new FoodProductsContext(options))
            {
                context.CurrentProducts.Add(new CurrentProducts()
                {
                    Product = "Nespresso"
                });
                context.CurrentProducts.Add(new CurrentProducts()
                {
                    Product = "Nesquik"
                });

                context.SaveChanges();
                CurrentProductsController currentProductsController = new CurrentProductsController(context);
                IActionResult result = await currentProductsController.GetCurrentProducts() as IActionResult;

                var okObjectResult = result as OkObjectResult;
                Assert.IsNotNull(okObjectResult);

                var model = okObjectResult.Value as IEnumerable<CurrentProducts>;
                Assert.IsNotNull(model);
                Assert.AreEqual(2, model.Count());

            }
        }

        [Test]
        public async Task TestPostCurrentProduct()
        {
            DbContextOptions<FoodProductsContext> options = new DbContextOptionsBuilder<FoodProductsContext>().UseInMemoryDatabase(databaseName: "PostCurrentProduct").Options;
            using (var context = new FoodProductsContext(options))
            {
                CurrentProducts currentProducts1 = new CurrentProducts {
                    Product = "Maggi"
                };

                CurrentProductsController currentProductsController = new CurrentProductsController(context);
                IActionResult result = await currentProductsController.PostCurrentProducts(currentProducts1) as IActionResult;

                var okObjectResult = result as OkObjectResult;
                Assert.IsNotNull(okObjectResult);

                var model = okObjectResult.Value as CurrentProducts;
                Console.WriteLine(model);
                Assert.IsNotNull(model);
                Assert.AreNotEqual(0, model.Id);
                Assert.AreEqual(currentProducts1.Product, model.Product);

                Assert.AreEqual(true, context.CurrentProducts.Any(x => x.Id == model.Id));
            }
        }

        [Test]
        public async Task TestDeleteCurrentProduct()
        {
            DbContextOptions<FoodProductsContext> options = new DbContextOptionsBuilder<FoodProductsContext>().UseInMemoryDatabase(databaseName: "DeleteCurrentProduct").Options;
            using (var context = new FoodProductsContext(options))
            {
                context.CurrentProducts.Add(new CurrentProducts()
                {
                    Product = "Milo"
                });
                context.SaveChanges();

                CurrentProducts currentProducts1 = context.CurrentProducts.First();

                CurrentProductsController currentProductsController = new CurrentProductsController(context);
                IActionResult result = await currentProductsController.DeleteCurrentProducts(currentProducts1.Id) as IActionResult;

                var okObjectResult = result as OkObjectResult;
                Assert.IsNotNull(okObjectResult);

                var model = okObjectResult.Value as CurrentProducts;
                Assert.IsNotNull(model);
                Assert.AreNotEqual(0, model.Id);
                Assert.AreEqual(currentProducts1.Product, model.Product);

                Assert.AreEqual(false, context.MailingList.Any(x => x.Id == currentProducts1.Id));
            }
        }
    }
}
