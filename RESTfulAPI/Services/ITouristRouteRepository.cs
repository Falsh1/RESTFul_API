using RESTfulAPI.Dtos;
using RESTfulAPI.Models;
using RESTfulAPI.ResourceParameters;

namespace RESTfulAPI.Services
{
    public interface ITouristRouteRepository
    {
        IEnumerable<TouristRoute> GetTouristRoutes();
        IEnumerable<TouristRoute> GetTouristRoutesByKeyword(string keyWord,string? operatorType,int? ratingValue);
        IEnumerable<TouristRoute> GetTouristRoutesByKeyword(TouristRouteResourceParamaters paramaters);
        TouristRoute GetTouristRouteByID(Guid TouristRouteId);
        bool ExitTouristRoute(Guid TouristRouteId);
        IEnumerable<TouristRoutePicture> GetTouristRoutePicturesByTouristRouteId(Guid TouristRouteId);
        TouristRoutePicture GetTouristRoutePictureById(int Id);
        bool CreateTouristRoute(TouristRoute TouristRoute);
        bool CreateTouristRoutePicture(Guid touristRouteId, TouristRoutePicture TouristRoutePicture);
        bool Save();
    }
}
