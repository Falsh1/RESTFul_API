using RESTfulAPI.Models;

namespace RESTfulAPI.Services
{
    public interface ITouristRouteRepository
    {
        IEnumerable<TouristRoute> GetTouristRoutes();
        IEnumerable<TouristRoute> GetTouristRoutesByKeyword(string keyword);
        TouristRoute GetTouristRouteByID(Guid TouristRouteId);
        bool ExitPictureForTouristRoute(Guid TouristRouteId);
        IEnumerable<TouristRoutePicture> GetTouristRoutePicturesByTouristRouteId(Guid TouristRouteId);
        TouristRoutePicture GetTouristRoutePictureById(int Id);
    }
}
