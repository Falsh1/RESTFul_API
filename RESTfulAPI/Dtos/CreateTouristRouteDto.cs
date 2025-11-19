namespace RESTfulAPI.Dtos
{
    public class CreateTouristRouteDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        //Price = OriginalPrice * DiscountPresent
        public decimal Price { get; set; }
        //public decimal OriginalPrice { get; set; }//原价
        //public double? DiscountPresent { get; set; }//折扣
        public DateTime CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public DateTime? DepartureTime { get; set; }//出发时间
        public string Features { get; set; }
        public string? FeeDescription { get; set; }
        public string? Note { get; set; }
        public double? Rating { get; set; }//评分
        public string TravelDays { get; set; }
        public string TripType { get; set; }
        public string DepartureCity { get; set; }
        //与图片类名一致，Automapper自动映射
        public ICollection<CreateTouristRoutePictureDto> TouristRoutePictures { get; set; }
            =new List<CreateTouristRoutePictureDto>();
    }
}
