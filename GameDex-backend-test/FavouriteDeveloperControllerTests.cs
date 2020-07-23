using NUnit.Framework;
using GameDex_backend.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using GameDex_backend_test;
using Microsoft.EntityFrameworkCore;
using GameDex_backend.Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using GameDex_backend;

namespace GameDex_backend_test
{
    [TestFixture()]
    class FavouriteDeveloperControllerTests
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
        public void GetFavourite_OneElement_ReturnAllFavourites()
        {
            context.Database.EnsureDeleted();
            context.SaveChanges();
            FavouriteDevelopersController favDev = new FavouriteDevelopersController(context);
            var item = new GameDex_backend.Models.FavouriteDeveloper { FavId = 1, UserId = 1 };
            context.Users.Add(new User { Id = 1, Username = "user" });
            context.SaveChanges();

            favDev.PostFavouriteDeveloper(item);
            var result = favDev.GetFavouriteDeveloper();

            Assert.AreEqual(item, result.Result.Value.First());
        }

        [Test()]
        public void GetFavourite_MultipleElements_ReturnAllFavourites()
        {
            context.Database.EnsureDeleted();
            context.SaveChanges();
            FavouriteDevelopersController favDev = new FavouriteDevelopersController(context);
            var item = new GameDex_backend.Models.FavouriteDeveloper { FavId = 1, UserId = 1 };
            var item2 = new GameDex_backend.Models.FavouriteDeveloper { FavId = 2, UserId = 1 };
            context.Users.Add(new User { Id = 1, Username = "user" });
            context.SaveChanges();

            favDev.PostFavouriteDeveloper(item);
            favDev.PostFavouriteDeveloper(item2);

            Assert.AreEqual(new List<FavouriteDeveloper>() { item, item2 }, favDev.GetFavouriteDeveloper().Result.Value.ToList());
        }

        [Test()]
        public void GetFavouriteById_ValidId_ReturnFavourite()
        {
            context.Database.EnsureDeleted();
            context.SaveChanges();
            FavouriteDevelopersController favDev = new FavouriteDevelopersController(context);
            var item = new GameDex_backend.Models.FavouriteDeveloper { FavId = 1, UserId = 1 };
            var item2 = new GameDex_backend.Models.FavouriteDeveloper { FavId = 2, UserId = 1 };
            context.Users.Add(new User { Id = 1, Username = "user" });
            context.SaveChanges();

            favDev.PostFavouriteDeveloper(item);
            favDev.PostFavouriteDeveloper(item2);

            Assert.AreEqual(item, favDev.GetFavouriteDeveloper(1));
        }
    }
}
