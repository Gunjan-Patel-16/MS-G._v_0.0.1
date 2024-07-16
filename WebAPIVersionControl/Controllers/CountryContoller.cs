using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPIVersionControl.Context;
using WebAPIVersionControl.Models;

namespace WebAPIVersionControl.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiVersion("2")]
    public class CountryContoller : ControllerBase
    {
        private readonly ApiVersionControlDbContext _context;
        public CountryContoller(ApiVersionControlDbContext apiVersionControlDbContext)
        {
            this._context = apiVersionControlDbContext;
        }

        [HttpGet]
        public List<Country> GetCountries()
        {
            var countries = new List<Country>();
            try
            {
               countries = _context.country.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return countries;
        }
    }
}
