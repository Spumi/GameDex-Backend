using NUnit.Framework;
using GameDex_backend.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using GameDex_backend.Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace GameDex_backend.Controllers.Tests
{
    [TestFixture()]
    public class FavouriteControllerTests
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
        public void GetFavouriteTest()
        {
            context.Database.EnsureDeleted();
            context.SaveChanges();
            FavouriteController fav = new FavouriteController(context);
            var item = new Models.Favourite { FavId = 1, UserId = 1 };
            context.Users.Add(new User { Id = 1, Username = "asd" });
            context.SaveChanges();
            fav.PostFavourite(item);
            var res = fav.GetFavourite();

            TestContext.Out.WriteLine(context.Users.FirstOrDefault());
            Assert.AreEqual(item, res.Result.Value.First());
        }

        [Test()]
        public void GetFavouriteTest2()
        {
            context.Database.EnsureDeleted();

            FavouriteController fav = new FavouriteController(context);
            var item = new Models.Favourite { FavId = 1, UserId = 1 };
            context.Users.Add(new User { Id = 1, Username = "lajos" });
            context.SaveChanges();
            fav.PostFavourite(item);
            var res = fav.GetFavourite();

            TestContext.Out.WriteLine(context.Users.FirstOrDefault());
            Assert.AreEqual(item, res.Result.Value.First());
        }


        [Test()]
        public void GetFavouriteTest1()
        {
            context.Database.EnsureDeleted();
            context.SaveChanges();
            FavouriteController fav = new FavouriteController(context);

            context.Users.Add(new User { Id = 1, Username = "asd" });
            context.SaveChanges();

            var item = new Models.Favourite { FavId = 1, UserId = 1 };
            var item2 = new Models.Favourite { FavId = 2, UserId = 1 };
            fav.PostFavourite(item);
            fav.PostFavourite(item2);

            Assert.AreEqual(new List<Favourite>(){ item, item2}, fav.GetFavourite().Result.Value.ToList());
        }


        [Test()]
        public void PostFavouriteTest()
        {
            context.Users.RemoveRange(context.Users);

            context.Users.RemoveRange(context.Users);
            context.SaveChanges();


            FavouriteController fav = new FavouriteController(context);

            var user = new User { Id = 1, Username = "asd" };
            context.Users.Add(user);
            context.SaveChanges();

            var item = new Favourite { FavId = 1, UserId = 1 };
            //user.FavGames.Add(item);
            var res = fav.PostFavourite(item);
            var asd = (JsonResult)fav.PostFavourite(item).Result.Result;
            var valami = asd.Value;
            var expected = new JsonResult(user);

            TestContext.Out.WriteLine(expected.Value);
            Assert.AreEqual(expected,  res);
        }


    }
}