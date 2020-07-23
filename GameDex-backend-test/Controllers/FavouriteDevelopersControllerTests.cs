using NUnit.Framework;
using GameDex_backend.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using GameDex_backend.Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace GameDex_backend.Controllers.Tests
{
    [TestFixture()]
    public class FavouriteDevelopersControllerTests
    {
        UserContext context;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<UserContext>()
               .UseInMemoryDatabase("fakeDb")
               .Options;

            context = new UserContext(options);
        }

        [Test()]
        public void GetFavouriteDeveloperTest()
        {
            context.Database.EnsureDeleted();
            context.SaveChanges();
            var fav = new FavouriteDevelopersController(context);
            var item = new Models.FavouriteDeveloper { FavId = 1, UserId = 1 };
            context.Users.Add(new User { Id = 1, Username = "asd" });
            context.SaveChanges();
            fav.PostFavouriteDeveloper(item);
            var res = fav.GetFavouriteDeveloper();

            TestContext.Out.WriteLine(context.Users.FirstOrDefault());
            Assert.AreEqual(item, res.Result.Value.First());
        }

        [Test()]
        public void GetFavouriteDeveloperTest1()
        {
            context.Database.EnsureDeleted();

            var fav = new FavouriteDevelopersController(context);
            var item = new Models.FavouriteDeveloper { FavId = 1, UserId = 1 };
            context.Users.Add(new User { Id = 1, Username = "lajos" });
            context.SaveChanges();
            fav.PostFavouriteDeveloper(item);
            var res = fav.GetFavouriteDeveloper();

            TestContext.Out.WriteLine(context.Users.FirstOrDefault());
            Assert.AreEqual(item, res.Result.Value.First());
        }

        [Test()]
        public void GetFavouriteDeveloperTest2()
        {
            context.Database.EnsureDeleted();
            context.SaveChanges();
            var fav = new FavouriteDevelopersController(context);

            context.Users.Add(new User { Id = 1, Username = "asd" });
            context.SaveChanges();

            var item = new Models.FavouriteDeveloper { FavId = 1, UserId = 1 };
            var item2 = new Models.FavouriteDeveloper { FavId = 2, UserId = 1 };
            fav.PostFavouriteDeveloper(item);
            fav.PostFavouriteDeveloper(item2);

            Assert.AreEqual(new List<FavouriteDeveloper>() { item, item2 }, fav.GetFavouriteDeveloper().Result.Value.ToList());
        }


        [Test()]
        public void PostFavouriteDeveloperTest()
        {
            context.Users.RemoveRange(context.Users);

            context.Users.RemoveRange(context.Users);
            context.SaveChanges();


            var fav = new FavouriteDevelopersController(context);

            var user = new User { Id = 1, Username = "asd" };
            context.Users.Add(user);
            context.SaveChanges();

            var item = new FavouriteDeveloper { FavId = 1, UserId = 1 };
            //user.FavGames.Add(item);
            var res = fav.PostFavouriteDeveloper(item).Result;
            //var expected = new JsonResult(user);
            var expected = new ActionResult<FavouriteDeveloper>(item);
            Assert.AreEqual(expected.Result, res.Result);
        }

    }
}