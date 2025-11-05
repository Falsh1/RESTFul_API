using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RESTfulAPI.Services;
using AutoMapper;
using RESTfulAPI.Dtos;

namespace RESTfulAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TouristRoutesController : ControllerBase
    {
        private ITouristRouteRepository _touristRouteRepository;
        private readonly IMapper _mapper;
        public TouristRoutesController(ITouristRouteRepository touristRouteRepository, IMapper mapper)
        {
            _touristRouteRepository = touristRouteRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GerTouristRoutes()
        {
            var routes = _touristRouteRepository.GetTouristRoutes();
            if (routes == null || routes.Count() <= 0)
            {
                return NotFound("没有旅游路线");
            }
            var routesdto = _mapper.Map<IEnumerable<TouristRouteDto>>(routes);
            return Ok(routesdto);
        }

        //关键词查询 http://localhost:5001/api/TouristRoutes/search?keyword=越南
        [HttpGet("search")]
        public IActionResult GerTouristRoutesByKeyword([FromQuery] string keyword)
        {
            var routes = _touristRouteRepository.GetTouristRoutesByKeyword(keyword);
            if (routes == null || routes.Count() <= 0)
            {
                return NotFound("没有旅游路线");
            }
            var routesdto = _mapper.Map<IEnumerable<TouristRouteDto>>(routes);
            return Ok(routesdto);
        }

        [HttpGet("{touristRouteId}")]
        public IActionResult GetTouristRoutesbyId(Guid touristRouteId)
        {
            var routes = _touristRouteRepository.GetTouristRouteByID(touristRouteId);
            if (routes == null)
            {
                return NotFound("没有旅游路线");
            }
            var routedto = _mapper.Map<TouristRouteDto>(routes);
            return Ok(routedto);
        }
    }
}
