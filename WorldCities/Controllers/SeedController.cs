using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorldCities.Data;
using OfficeOpenXml;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using WorldCities.Data.Models;
using System.Text.Json;

namespace WorldCities.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeedController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SeedController(ApplicationDbContext context, IWebHostEnvironment env) {
            _context = context;
            _env = env;
        }

        [HttpGet]
        public async Task<ActionResult> Import()
        {
            var path = Path.Combine(_env.ContentRootPath, String.Format("Data/Source/worldcities.xlsx"));

            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                using (var ep = new ExcelPackage(stream))
                {
                    //get the first worksheet
                    var ws = ep.Workbook.Worksheets[0];
                    //initialize the record counters
                    var nCountries = 0;
                    var nCities = 0;

                    #region import all countries
                    //create a list containing all countries  already existing into db
                    var firstCountries = _context.Countries.ToList();
                    for (int nRow = 2; nRow <= ws.Dimension.End.Row; nRow++)
                    {
                        var row = ws.Cells[nRow, 1, nRow, ws.Dimension.End.Column];
                        var name = row[nRow, 5].GetValue<string>();

                        //check if country is already created
                        if (firstCountries.Where(c => c.Name == name).Count() == 0)
                        {
                            var country = new Country();
                            country.Name = name;

                            _context.Countries.Add(country);
                            await _context.SaveChangesAsync();

                            firstCountries.Add(country);

                            nCountries++;
                        }
                    }
                    #endregion

                    #region import all cities
                    for (int nRow = 2; nRow <= ws.Dimension.End.Row; nRow++)
                    {
                        var row = ws.Cells[nRow, 1, nRow, ws.Dimension.End.Column];

                        var city = new City();
                        city.Name = row[nRow, 1].GetValue<string>();
                        city.Lat = row[nRow, 3].GetValue<decimal>();
                        city.Lon = row[nRow, 4].GetValue<decimal>();

                        var countryName = row[nRow, 5].GetValue<string>();
                        var country = firstCountries.Where(c => c.Name == countryName).FirstOrDefault();

                        city.CountryId = country.Id;

                        _context.Cities.Add(city);
                        await _context.SaveChangesAsync();

                        nCities++;
                    }
                    #endregion

                    return new JsonResult(new
                    {
                        Cities = nCities,
                        Countries = nCountries
                    });
                }
            }
        }
    }
}
