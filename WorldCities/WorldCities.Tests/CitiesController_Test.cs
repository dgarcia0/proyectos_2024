using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorldCities.Controllers;
using WorldCities.Data;
using WorldCities.Data.Models;
using Xunit;

namespace WorldCities.Tests
{
    public class CitiesController_Test
    {
        [Fact]
        public async void GetCity()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "WorldCities").Options;

            using (var context = new ApplicationDbContext(options)) {
                context.Add(new City()
                {
                    Id = 1,
                    CountryId = 1,
                    Lat = 1,
                    Lon = 1,
                    Name = "TestCity1"
                });
                context.SaveChanges();
            }
            City city_existing = null;
            City city_notExisting = null;

            using (var context = new ApplicationDbContext(options)) { 
                var controller = new CitiesController(context);
                city_existing = (await controller.GetCity(1)).Value;
                city_notExisting = (await controller.GetCity(2)).Value;
            }

            Assert.True(city_existing != null &&  city_notExisting == null);
        }
    }
}
