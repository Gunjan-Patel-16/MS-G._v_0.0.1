using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPIVersionControl.Context;
using WebAPIVersionControl.Models;

namespace WebAPIVersionControl.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")] //,Order =1)]
    //[Route("api/v{Api-version:apiVersion}/[controller]/[action]",Order =2)]
    [ApiVersion("0.2")]
    public class CountryController : ControllerBase
    {
        private readonly ApiVersionControlDbContext _context;
        public CountryController(ApiVersionControlDbContext apiVersionControlDbContext)
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

    [ApiController]
    [Route("api/[controller]/[action]")] //,Order =1)]
    //[Route("api/v{Api-version:apiVersion}/[controller]/[action]",Order =2)]
    [ApiVersion("0.3")]

    public class CountryV3Controller : ControllerBase
    {
        private readonly ApiVersionControlDbContext _context;
        public CountryV3Controller(ApiVersionControlDbContext apiVersionControlDbContext)
        {
            this._context = apiVersionControlDbContext;
        }

        [HttpGet]
        public List<Country> GetCountries()
        {
            var countries = new List<Country>();
            try
            {
                countries = _context.country.OrderBy(x => x.Name).Take(10).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return countries;
        }
    }
}
