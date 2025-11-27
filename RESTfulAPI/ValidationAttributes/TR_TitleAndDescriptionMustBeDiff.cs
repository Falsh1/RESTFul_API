using RESTfulAPI.Dtos;
using System.ComponentModel.DataAnnotations;

namespace RESTfulAPI.ValidationAttributes
{
    public class TR_TitleAndDescriptionMustBeDiff : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var touristRouteDto = (CrrateAndPutTpuristDto)validationContext.ObjectInstance;
            if (touristRouteDto.Title == touristRouteDto.Description)
            {
                return new ValidationResult(
                    "Title与Description必须不同(类级别校验)",
                    new[] { "CrrateAndPutTpuristDto" });
            }
            return ValidationResult.Success;
        }
    }
}
