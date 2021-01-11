using danjoworksCore.Models;
using danjoworksCoreLibrary.IPAddress;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace danjoworksCore.Controllers.CodeSamples
{
    public class WeatherController : Controller
    {
        private readonly IMemoryCache _cache;
        private readonly IIPAddressService iIPAddressService;
        private const string IPinfoCacheKey = "IPinfo-cache-key";

        public WeatherController(IMemoryCache memoryCache, IIPAddressService iIPAddressService)
        {
            _cache = memoryCache;
            this.iIPAddressService = iIPAddressService;
        }

        // GET: WeatherController
        public ActionResult Index()
        {
            return View();
        }

        // GET: WeatherController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: WeatherController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WeatherController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: WeatherController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: WeatherController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: WeatherController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: WeatherController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [AcceptVerbs("Get")]
        public async Task<JsonResult> GetIPinfo()
        {
            // Look for cache key.
            if (!_cache.TryGetValue(IPinfoCacheKey, out IPinfo ipInfo))
            {
                // Key not in cache, so get data.
                var pinfo = await iIPAddressService.GetIPAddressLocation();

                if (pinfo.owner == null)
                {
                    pinfo.owner = "no value";
                }

                ipInfo = new IPinfo()
                {
                    ip_address = pinfo.ip_address,
                    country = pinfo.country,
                    country_code = pinfo.country_code,
                    continent = pinfo.continent,
                    continent_code = pinfo.continent_code,
                    city = pinfo.city,
                    county = pinfo.county,
                    region = pinfo.region,
                    region_code = pinfo.region_code,
                    timezone = pinfo.timezone,
                    owner = pinfo.owner,
                    longitude = pinfo.longitude,
                    latitude = pinfo.latitude,
                    currency = pinfo.currency,
                    languages = pinfo.languages
                };

                // Set cache options.
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    // Keep in cache for this time, reset time if accessed.
                    .SetSlidingExpiration(TimeSpan.FromMinutes(60));

                // Save data in cache.
                _cache.Set(IPinfoCacheKey, ipInfo, cacheEntryOptions);
            }

            return Json(new { data = ipInfo });
        }

        [AcceptVerbs("Post")]
        public IActionResult GetWeatherByLocation(string location)
        {
            var client = new RestClient("https://community-open-weather-map.p.rapidapi.com/find?cnt=1&type=link%252C%20accurate&units=metric&q=" + location);
            var request = new RestRequest(Method.GET);
            request.AddHeader("x-rapidapi-host", "community-open-weather-map.p.rapidapi.com");
            request.AddHeader("x-rapidapi-key", "daedcb03bemsh6ce87fd7ecb826dp1f8140jsn7e498fd09cec");
            request.RequestFormat = DataFormat.Json;
            IRestResponse response = client.Execute(request);

            if (response.IsSuccessful)
            {
                return Ok(response.Content);
                //return Json(new { success = response.IsSuccessful });
            }
            else
            {
                return NotFound(response.ErrorMessage);
            }
        }
    }
}
