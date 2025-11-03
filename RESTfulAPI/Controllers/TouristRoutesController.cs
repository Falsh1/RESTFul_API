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
        [HttpGet]
        public IActionResult GerTouristRoutes()
        {
            var routes = _touristRouteRepository.GetTouristRoutes();
            if(routes == null || routes.Count() <= 0)
            {
                return NotFound("没有旅游路线");
            }
            return Ok(routes);
        }
        [HttpGet("{touristRouteId}")]
        public IActionResult GetTouristRoutesbyId(Guid touristRouteId)
        {
            var routes = _touristRouteRepository.GetTouristRouteByID(touristRouteId);
            if (routes == null)
            {
                return NotFound("没有旅游路线");
            }
            return Ok(routes);
        }
    }
}
