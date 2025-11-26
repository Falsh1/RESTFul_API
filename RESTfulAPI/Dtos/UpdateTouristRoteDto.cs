using RESTfulAPI.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace RESTfulAPI.Dtos
{
    [TR_TitleAndDescriptionMustBeDiff]
    public class UpdateTouristRoteDto
    {
        [Required(ErrorMessage = "Title不可为空！")]
        [MaxLength(100)]
        public string Title { get; set; }
        [Required]
        [MaxLength(1500)]
        public string Description { get; set; }
        public decimal Price { get; set; }

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

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Title == Description)
            {
                yield return new ValidationResult(
                    "Titley与Description必须不同(属性校验)",
                    new[] { "CreateTouristRouteDto" });
            }
        }
    }
}