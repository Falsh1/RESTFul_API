using RESTfulAPI.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace RESTfulAPI.Dtos
{
    public class UpdateTouristRoteDto : CrrateAndPutTpuristDto
    {
        [Required(ErrorMessage = "更新必备")]
        [MaxLength(1500)]
        public override string Description { get; set; }
    }
}