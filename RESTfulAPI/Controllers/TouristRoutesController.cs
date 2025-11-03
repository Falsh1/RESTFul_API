using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RESTfulAPI.Services;

namespace RESTfulAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TouristRoutesController : ControllerBase
    {
        private ITouristRouteRepository _touristRouteRepository;

        public TouristRoutesController(ITouristRouteRepository touristRouteRepository)
        {
            _touristRouteRepository = touristRouteRepository;
        }

        public IActionResult GerTouristRoutes()
        {
            var routes = _touristRouteRepository.GetTouristRoutes();
            return Ok(routes);
        }
    }
}
