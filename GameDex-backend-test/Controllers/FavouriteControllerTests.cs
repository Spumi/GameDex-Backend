using NUnit.Framework;
using GameDex_backend.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using GameDex_backend_test;
using Microsoft.EntityFrameworkCore;
using GameDex_backend.Models;
using System.Linq;

namespace GameDex_backend.Controllers.Tests
{
    [TestFixture()]
    public class FavouriteControllerTests
    {
        [Test()]
        public void FavouriteControllerTest()
        {
            
            Assert.Fail();
        }

        [Test()]
        public void GetFavouriteTest()
        {
            var options = new DbContextOptionsBuilder<UserContext>()
                .UseInMemoryDatabase("fakeDb")
                .Options;

            var context = new UserContext(options);
            FavouriteController fav = new FavouriteController(context);
            var item = new Models.Favourite { FavId = 1, UserId = 1 };
            context.Users.Add(new User { Id = 1, Username = "asd" });
            context.SaveChanges();
            fav.PostFavourite(item);
            var res = fav.GetFavourite();
            
            TestContext.Out.WriteLine(fav.GetFavourite(1).Result.Value);
            Assert.AreEqual(item, res.Result.Value.First());
        }

        [Test()]
        public void GetFavouriteTest1()
        {
            Assert.Fail();
        }

        [Test()]
        public void PutFavouriteTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void PostFavouriteTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void DeleteFavouriteTest()
        {
            Assert.Fail();
        }
    }
}