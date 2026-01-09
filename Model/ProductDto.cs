using System.ComponentModel.DataAnnotations;

namespace FMS.API.Model
{
    public class ProductDto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public string Code { get; set; }
        public string Barcode { get; set; }
    }
}
