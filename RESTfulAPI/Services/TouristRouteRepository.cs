using RESTfulAPI.Models;
using RESTfulAPI.DataBase;
using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;
using RESTfulAPI.ResourceParameters;
using RESTfulAPI.Dtos;

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

        public bool ExitTouristRoute(Guid TouristRouteId)
        {
            return _appDbContext.TouristRoutes.Any(t => t.Id == TouristRouteId);
        }

        public IEnumerable<TouristRoutePicture> GetTouristRoutePicturesByTouristRouteId(Guid TouristRouteId)
        {
            return _appDbContext.TouristRoutePictures.Where(t => t.TouristRouteId == TouristRouteId).ToList();  
        }

        TouristRoutePicture ITouristRouteRepository.GetTouristRoutePictureById(int Id)
        {
            return _appDbContext.TouristRoutePictures.Where(p => p.Id == Id).FirstOrDefault();
        }
        public bool CreateTouristRoute(TouristRoute TouristRoute)
        {
            if (TouristRoute == null) throw new ArgumentNullException(nameof(TouristRoute));
            _appDbContext.TouristRoutes.Add(TouristRoute);
            return _appDbContext.SaveChanges() >= 0 ? true:false;
        }
        public bool CreateTouristRoutePicture(Guid touristRouteId, TouristRoutePicture TouristRoutePicture)
        {
            if (TouristRoutePicture == null) throw new ArgumentNullException(nameof(TouristRoutePicture));
            //外键关系字段TouristRouteId，必须赋值，否则无法新增
            TouristRoutePicture.TouristRouteId = touristRouteId;
            _appDbContext.TouristRoutePictures.Add(TouristRoutePicture);
            return _appDbContext.SaveChanges() >= 0 ? true : false;
        }
        public bool Save()
        {
            return (_appDbContext.SaveChanges() >= 0);
        }
        public void DeleteTouristRoute(TouristRoute TouristRoute) 
        { 
            _appDbContext.Remove(TouristRoute);   
        }
        public void DeleteTouristRoutePicture(TouristRoutePicture TouristRoutePicture)
        {
            _appDbContext.Remove(TouristRoutePicture);
        }
    }
}
