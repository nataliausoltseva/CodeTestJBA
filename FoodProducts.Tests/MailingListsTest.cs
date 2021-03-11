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
    class MailingListsTest
    {
        [Test]
        public async Task TestGetMailingList()
        {
            DbContextOptions<FoodProductsContext> options = new DbContextOptionsBuilder<FoodProductsContext>().UseInMemoryDatabase(databaseName: "GetMailingList").Options;
            using (var context = new FoodProductsContext(options))
            {
                context.Add(new MailingList()
                {
                    Email = "email123@gmail.com"
                });
                context.SaveChanges();
                MailingList mailingList1 = context.MailingList.First();
                MailingListsController mailingListsController = new MailingListsController(context);
                IActionResult result = await mailingListsController.GetMailingList(mailingList1.Id) as IActionResult;

                var okObjectResult = result as OkObjectResult;
                Assert.IsNotNull(okObjectResult);

                var model = okObjectResult.Value as MailingList;
                Assert.IsNotNull(model);
                Assert.AreEqual(mailingList1.Email, model.Email);
            }
        }

        [Test]
        public async Task TestGetMailingLists()
        {
            DbContextOptions<FoodProductsContext> options = new DbContextOptionsBuilder<FoodProductsContext>().UseInMemoryDatabase(databaseName: "GetMailingLists").Options;
            using(var context = new FoodProductsContext(options))
            {
                context.MailingList.Add(new MailingList()
                {
                    Email = "email123@gmail.com"
                });
                context.MailingList.Add(new MailingList() {
                    Email = "email112233@gmail.com"
                });

                context.SaveChanges();
                MailingListsController mailingListsController = new MailingListsController(context);
                IActionResult result = await mailingListsController.GetMailingLists() as IActionResult;

                var okObjectResult = result as OkObjectResult;
                Assert.IsNotNull(okObjectResult);

                var model = okObjectResult.Value as IEnumerable<MailingList>;
                Assert.IsNotNull(model);
                Assert.AreEqual(2, model.Count());

            }
        }

        [Test]
        public async Task TestPostMailingList()
        {
            DbContextOptions<FoodProductsContext> options = new DbContextOptionsBuilder<FoodProductsContext>().UseInMemoryDatabase(databaseName: "PostMailingList").Options;
            using(var context = new FoodProductsContext(options))
            {
                MailingList mailingList1 = new MailingList { Email = "email1234@gmail.com" };

                MailingListsController mailingListsController = new MailingListsController(context);
                IActionResult result = await mailingListsController.PostMailingList(mailingList1) as IActionResult;

                var okObjectResult = result as OkObjectResult;
                Assert.IsNotNull(okObjectResult);

                var model = okObjectResult.Value as MailingList;
                Assert.IsNotNull(model);
                Assert.AreNotEqual(0, model.Id);
                Assert.AreEqual(mailingList1.Email, model.Email);

                Assert.AreEqual(true, context.MailingList.Any(x => x.Id == model.Id));
            }
        }

        [Test]
        public async Task TestDeleteMailingList()
        {
            DbContextOptions<FoodProductsContext> options = new DbContextOptionsBuilder<FoodProductsContext>().UseInMemoryDatabase(databaseName: "DeleteMailingList").Options;
            using(var context = new FoodProductsContext(options))
            {
                context.MailingList.Add(new MailingList()
                {
                    Email = "email123@gmail.com"
                });
                context.SaveChanges();

                MailingList mailingList1 = context.MailingList.First();

                MailingListsController mailingListsController = new MailingListsController(context);
                IActionResult result = await mailingListsController.DeleteMailingList(mailingList1.Id) as IActionResult;

                var okObjectResult = result as OkObjectResult;
                Assert.IsNotNull(okObjectResult);

                var model = okObjectResult.Value as MailingList;
                Assert.IsNotNull(model);
                Assert.AreNotEqual(0, model.Id);
                Assert.AreEqual(mailingList1.Email, model.Email);

                Assert.AreEqual(false, context.MailingList.Any(x => x.Id == mailingList1.Id));
            }
        }
    }
}
