using RESTfulAPI.Models;
using RESTfulAPI.DataBase;

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
    }
}
