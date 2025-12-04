using RESTfulAPI.Dtos;
using RESTfulAPI.Models;
using RESTfulAPI.ResourceParameters;

namespace RESTfulAPI.Services
{
    public interface ITouristRouteRepository
    {
        Task<IEnumerable<TouristRoute>> GetTouristRoutesAsync();
        Task<IEnumerable<TouristRoute>> GetTouristRoutesByKeywordAsync(string keyWord,string? operatorType,int? ratingValue);
        Task<IEnumerable<TouristRoute>> GetTouristRoutesByKeywordAsync(TouristRouteResourceParamaters paramaters);
        Task<TouristRoute> GetTouristRouteByIDAsync(Guid TouristRouteId);
        Task<bool> ExitTouristRouteAsync(Guid TouristRouteId);
        Task<IEnumerable<TouristRoutePicture>> GetTouristRoutePicturesByTouristRouteIdAsync(Guid TouristRouteId);
        Task<TouristRoutePicture> GetTouristRoutePictureByIdAsync(int Id);
        bool CreateTouristRoute(TouristRoute TouristRoute);
        bool CreateTouristRoutePicture(Guid touristRouteId, TouristRoutePicture TouristRoutePicture);
        Task<bool> SaveAsync();
        void DeleteTouristRoute(TouristRoute TouristRoute);
        void DeleteTouristRoutePicture(TouristRoutePicture TouristRoutePicture);
    }
}
