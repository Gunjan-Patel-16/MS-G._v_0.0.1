using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPIVersionControl.Context;
using WebAPIVersionControl.Models;

namespace WebAPIVersionControl.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiVersion("0.3")]
    public class CityController : ControllerBase
    {
        private readonly ApiVersionControlDbContext _context;
        public CityController(ApiVersionControlDbContext apiVersionControlDbContext)
        {
            this._context = apiVersionControlDbContext;
        }

        [HttpGet]
        public List<City> GetCities()
        {
            var cities = new List<City>();
            try
            {
                cities = _context.city.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return cities;
        }
    }

}
