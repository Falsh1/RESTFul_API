using RESTfulAPI.Models;
using RESTfulAPI.DataBase;
using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;
using RESTfulAPI.ResourceParameters;

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
        public IEnumerable<TouristRoute> GetTouristRoutesByKeyword(string keyword, string? operatorType, int? ratingValue)
        {
            //IQueryable延迟加载，处理sql,数据库只执行最终sql，减少IO操作
            IQueryable<TouristRoute> result = _appDbContext.TouristRoutes.Include(p => p.TouristRoutePictures);

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                keyword = keyword.Trim();
                result = result.Where(r => r.Title.Contains(keyword));
            }
            if(ratingValue >= 0)
            {
                result = operatorType switch
                {
                    "lessThan" => result.Where(r => r.Rating < ratingValue),
                    "greaterThan" => result.Where(r => r.Rating > ratingValue),
                    _ => result.Where(r => r.Rating == ratingValue)
                };
                //switch (operatorType)
                //{
                //    case "lessThan":
                //        result = result.Where(r => r.Rating < ratingValue);
                //        break;
                //    case "greaterThan":
                //        result = result.Where(r => r.Rating > ratingValue);
                //        break;
                //    default:
                //        result = result.Where(r => r.Rating == ratingValue);
                //        break;
                //}
            }

            return result.ToList();
        }
        
        public IEnumerable<TouristRoute> GetTouristRoutesByKeyword(TouristRouteResourceParamaters paramaters)
        {
            string keyword = "";
            string? operatorType = "";
            int? ratingValue = -1;
            if (paramaters != null)
            {
                keyword = paramaters.Keyword;
                operatorType = paramaters.operatorType;
                ratingValue = paramaters.ratingValue;
            }

            IQueryable<TouristRoute> result = _appDbContext.TouristRoutes.Include(p => p.TouristRoutePictures);

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                keyword = keyword.Trim();
                result = result.Where(r => r.Title.Contains(keyword));
            }
            if (ratingValue >= 0)
            {
                result = operatorType switch
                {
                    "lessThan" => result.Where(r => r.Rating < ratingValue),
                    "greaterThan" => result.Where(r => r.Rating > ratingValue),
                    _ => result.Where(r => r.Rating == ratingValue)
                };
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
