using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RESTfulAPI.Models
{
    public class TouristRoute
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        [Required]
        [MaxLength(1500)]
        public string Description { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal OriginalPrice { get; set; }
        [Range(0.0, 1.0)]
        public double? DiscountPresent {  get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public DateTime? DepartureTime { get; set; }//出发时间
        [MaxLength]
        public string Features {  get; set; }
        [MaxLength]
        public string FeeDescription {  get; set; }
        [MaxLength]
        public string Note { get; set; }
        public ICollection<TouristRoutePicture> TouristRoutePictures {  get; set; } 
            =new List<TouristRoutePicture>();   
        public TouristRoute() { }
    }
}
