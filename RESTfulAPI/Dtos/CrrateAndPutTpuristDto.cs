using RESTfulAPI.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace RESTfulAPI.Dtos
{
    [TR_TitleAndDescriptionMustBeDiff]
    /**
     * CrrateAndPutTpuristDto 新建&全量更新TouristDto,分别被CreateTouristRouteDto，UpdateTouristRoteDto继承
     * 目的是使新建，更新使用相同的类级别校验
     */
    public abstract class CrrateAndPutTpuristDto : IValidatableObject
    {
        [Required(ErrorMessage = "Title不可为空！")]
        [MaxLength(100)]
        public string Title { get; set; }
        [Required]
        [MaxLength(1500)]
        public virtual string Description { get; set; }
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
            = new List<CreateTouristRoutePictureDto>();


        //yield return
        /*
         * 框架拿错误的方式foreach (var error in instance.Validate(ctx)) 
         * 如果Validate直接return一个List,需要new，还要提前计算所有的错误填充List
         * 使用yield return，真正被 foreach 时，才会跑进 if 判断，算一条返回一条，避免new也减少计算
         */
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Title == Description)
            {
                yield return new ValidationResult(
                    "Title与Description必须不同(属性校验)",
                    new[] { "CrrateAndPutTpuristDto" });
            }
        }
        //等价于
        /*
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var list = new List<ValidationResult>();   // 1. 先 new 盒子

            if (Title == Description)                  // 2. 判断
                list.Add(new ValidationResult("Title与Description必须不同",
                                              new[] { "CreateTouristRouteDto" }));

            return list;                               // 3. 把盒子返回
        }
        */
    }
}
