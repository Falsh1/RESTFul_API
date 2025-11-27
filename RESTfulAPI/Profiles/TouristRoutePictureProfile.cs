using AutoMapper;
using RESTfulAPI.Models;
using RESTfulAPI.Dtos;

namespace RESTfulAPI.Profiles
{
    public class TouristRoutePictureProfile: Profile
    {
        public TouristRoutePictureProfile()
        {
            CreateMap<TouristRoutePicture, TouristRoutePictureDto>();
            CreateMap<CreateTouristRoutePictureDto, TouristRoutePicture>();
            CreateMap<TouristRoutePicture, CreateTouristRoutePictureDto>();
        }
    }
}
