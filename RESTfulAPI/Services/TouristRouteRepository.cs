using RESTfulAPI.Models;
using RESTfulAPI.DataBase;
using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace RESTfulAPI.Services
{
    public class TouristRouteRepository : ITouristRouteRepository
    {
        private readonly AppDbContext _appDbContext;
        public TouristRouteRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<TouristRoute> GetTouristRoutes()
        {
            return _appDbContext.TouristRoutes.Include(p => p.TouristRoutePictures).ToList();
        }
        public TouristRoute GetTouristRouteByID(Guid TouristRouteId)
        {
            return _appDbContext.TouristRoutes.Include(p => p.TouristRoutePictures).FirstOrDefault(n => n.Id == TouristRouteId);
        }
        public IEnumerable<TouristRoute> GetTouristRoutesByKeyword(string keyword)
        {
            //IQueryable延迟加载，处理sql,数据库只执行最终sql，减少IO操作
            IQueryable<TouristRoute> result = _appDbContext.TouristRoutes.Include(p => p.TouristRoutePictures);

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                keyword = keyword.Trim();
                result = result.Where(r => r.Title.Contains(keyword));
            }
            return result.ToList();
        }

        public bool ExitPictureForTouristRoute(Guid TouristRouteId)
        {
            return _appDbContext.TouristRoutePictures.Any(t => t.TouristRouteId == TouristRouteId);
        }

        public IEnumerable<TouristRoutePicture> GetTouristRoutePicturesByTouristRouteId(Guid TouristRouteId)
        {
            return _appDbContext.TouristRoutePictures.Where(t => t.TouristRouteId == TouristRouteId).ToList();  
        }

        TouristRoutePicture ITouristRouteRepository.GetTouristRoutePictureById(int Id)
        {
            return _appDbContext.TouristRoutePictures.Where(p => p.Id == Id).FirstOrDefault();
        }
    }
}
