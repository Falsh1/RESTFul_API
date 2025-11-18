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
            if (!_touristRouteRepository.ExitTouristRoute(touristRouteId))
            {
                return NotFound("没有找到对应的旅游路线");
            }
            else
            {
                var touristroutepicture = _touristRouteRepository.GetTouristRoutePicturesByTouristRouteId(touristRouteId);
                if (touristroutepicture != null && touristroutepicture.Count() >= 0)
                {
                    return Ok(_mapper.Map<IEnumerable<TouristRoutePictureDto>>(touristroutepicture));
                }
                else
                {
                    return NotFound("没有找到该旅游路线的图片");
                }
            }
        }
        [HttpGet("{pictureId}" ,Name = "GetPictureById")]
        public IActionResult GetPictureById(Guid touristRouteId,int pictureId)
        {
            if (!_touristRouteRepository.ExitTouristRoute(touristRouteId))
            {
                return NotFound("没有找到对应的旅游路线");
            }
            var touristroutepicture = _touristRouteRepository.GetTouristRoutePicturesByTouristRouteId(touristRouteId);

            var picture = touristroutepicture.Where(p => p.Id == pictureId).FirstOrDefault();
            if(picture == null)
            {
                return NotFound("该旅游路线没有此图片");
            }
            return Ok(_mapper.Map<TouristRoutePictureDto>(picture));
        }
        [HttpPost]
        public IActionResult CreateTouristRoutePicture(
            [FromRoute] Guid touristRouteId, 
            [FromBody] CreateTouristRoutePictureDto createTouristRoutePictureDto)
        {
            if (!_touristRouteRepository.ExitTouristRoute(touristRouteId))
            {
                return NotFound("没有找到对应的旅游路线");
            }
            else{
                var TouristRoutePicture = _mapper.Map<TouristRoutePicture>(createTouristRoutePictureDto);
                bool result = _touristRouteRepository.CreateTouristRoutePicture(touristRouteId,TouristRoutePicture);
                if (result)
                {
                    var TouristRoutePictureToReturn = _mapper.Map<TouristRoutePictureDto>(TouristRoutePicture);
                    return CreatedAtRoute("GetPictureById"
                        , new {
                            touristRouteId = TouristRoutePictureToReturn.TouristRouteId ,
                            pictureId = TouristRoutePictureToReturn.Id }
                        , TouristRoutePictureToReturn);
                }
                else
                {
                    return BadRequest();
                }
            }
        }
    }
}
