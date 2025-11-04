using RESTfulAPI.Models;

namespace RESTfulAPI.Services
{
    public interface ITouristRouteRepository
    {
        IEnumerable<TouristRoute> GetTouristRoutes();
        TouristRoute GetTouristRouteByID(Guid TouristRouteId);
        bool ExitPictureForTouristRoute(Guid TouristRouteId);
        IEnumerable<TouristRoutePicture> GetTouristRoutePicturesByTouristRouteId(Guid TouristRouteId);
    }
}
