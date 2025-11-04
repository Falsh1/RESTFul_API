using RESTfulAPI.Models;
using RESTfulAPI.DataBase;
using System.Reflection.Metadata.Ecma335;

namespace RESTfulAPI.Services
{
    public class TouristRouteRepository : ITouristRouteRepository
    {
        private readonly AppDbContext _appDbContext;
        public TouristRouteRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public TouristRoute GetTouristRouteByID(Guid TouristRouteId)
        {
            return _appDbContext.TouristRoutes.FirstOrDefault(n => n.Id == TouristRouteId);
        }

        public IEnumerable<TouristRoute> GetTouristRoutes()
        {
            return _appDbContext.TouristRoutes.ToList();
        }

        public bool ExitPictureForTouristRoute(Guid TouristRouteId)
        {
            return _appDbContext.TouristRoutePictures.Any(t => t.TouristRouteId == TouristRouteId);
        }

        public IEnumerable<TouristRoutePicture> GetTouristRoutePicturesByTouristRouteId(Guid TouristRouteId)
        {
            return _appDbContext.TouristRoutePictures.Where(t => t.TouristRouteId == TouristRouteId).ToList();  
        }
    }
}
