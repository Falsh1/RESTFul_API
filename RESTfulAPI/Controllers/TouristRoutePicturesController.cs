using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RESTfulAPI.Models;
using RESTfulAPI.Services;
using System.Reflection.Metadata.Ecma335;
using RESTfulAPI.Dtos;

namespace RESTfulAPI.Controllers
{
    [Route("api/touristroutes/{touristRouteId}/pictures")]
    [ApiController]
    public class TouristRoutePicturesController : ControllerBase
    {
        private ITouristRouteRepository _touristRouteRepository;
        private readonly IMapper _mapper;
        public TouristRoutePicturesController(ITouristRouteRepository touristRouteRepository, IMapper mapper)
        {
            _touristRouteRepository = touristRouteRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetPictureListByTouristRoute(Guid touristRouteId)
        {
            if (!_touristRouteRepository.ExitPictureForTouristRoute(touristRouteId))
            {
               return NotFound("没有找到对应的旅游路线");
            }
            else
            {
                var touristroutepicture = _touristRouteRepository.GetTouristRoutePicturesByTouristRouteId(touristRouteId);
                if(touristroutepicture != null && touristroutepicture.Count() >= 0)
                {
                    return Ok(_mapper.Map<IEnumerable<TouristRoutePictureDto>>(touristroutepicture));
                }
                else
                {
                    return NotFound("没有找到该旅游路线的图片");
                }
            }
        }
    }
}
