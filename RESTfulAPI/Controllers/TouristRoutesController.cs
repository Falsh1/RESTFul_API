using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RESTfulAPI.Services;
using AutoMapper;
using RESTfulAPI.Dtos;
using System.Text.RegularExpressions;
using RESTfulAPI.ResourceParameters;
using RESTfulAPI.Models;
using Azure;
using Microsoft.AspNetCore.JsonPatch;

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
        public IActionResult GerTouristRoutesByKeyword(
            //[FromQuery] string keyword,
            //string? rating //按评分筛选  lessThan3,equalTo2,greaterThan4

            //使用ResourceParameters类封装查询参数
            [FromBody] TouristRouteResourceParamaters parameters
            )
        {
            //var routes = _touristRouteRepository.GetTouristRoutesByKeyword(parameters.Keyword,parameters.operatorType,parameters.ratingValue);
            var routes = _touristRouteRepository.GetTouristRoutesByKeyword(parameters);

            if (routes == null || routes.Count() <= 0)
            {
                return NotFound("没有旅游路线");
            }
            var routesdto = _mapper.Map<IEnumerable<TouristRouteDto>>(routes);
            return Ok(routesdto);
        }

        [HttpGet("{touristRouteId}", Name = "GetTouristRoutesbyId")]
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

        [HttpPost]
        public IActionResult CreateTouristRoute([FromBody] CreateTouristRouteDto createTouristRouteDto)
        {
            TouristRoute TouristRoute = _mapper.Map<TouristRoute>(createTouristRouteDto);
            bool result = _touristRouteRepository.CreateTouristRoute(TouristRoute);
            if (result)
            {
                var TouristRouteToReturn = _mapper.Map<TouristRouteDto>(TouristRoute);
                return CreatedAtRoute("GetTouristRoutesbyId"
                    , new { touristRouteId = TouristRouteToReturn.Id }
                    , TouristRouteToReturn);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("{touristRouteId}")]
        public IActionResult UpdateTouristRoute(
            [FromRoute] Guid touristRouteId,
            [FromBody] UpdateTouristRoteDto updateTouristRoteDto)
        {
            if (!_touristRouteRepository.ExitTouristRoute(touristRouteId))
            {
                return NotFound("没有旅游路线");
            }
            else
            {
                var UpdateTouristRoute = _touristRouteRepository.GetTouristRouteByID(touristRouteId);
                _mapper.Map(updateTouristRoteDto, UpdateTouristRoute);
                _touristRouteRepository.Save();
                return NoContent();
            }
        }

        [HttpPatch("{touristRouteId}")]
        public IActionResult PartiallyUpdateTouristRoute(
             [FromRoute] Guid touristRouteId,
             [FromBody] JsonPatchDocument<UpdateTouristRoteDto> jsonPatchDocument)
        {
            if (!_touristRouteRepository.ExitTouristRoute(touristRouteId))
            {
                return NotFound("没有旅游路线");
            }
            else
            {
                var UpdateTouristRoute = _touristRouteRepository.GetTouristRouteByID(touristRouteId);
                var TouristRouteToPatch = _mapper.Map<UpdateTouristRoteDto>(UpdateTouristRoute);
                jsonPatchDocument.ApplyTo(TouristRouteToPatch);
                _mapper.Map(TouristRouteToPatch, UpdateTouristRoute);
                _touristRouteRepository.Save();
                return NoContent();
            }
        }
    }
}
