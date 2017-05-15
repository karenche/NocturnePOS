using System.ComponentModel.DataAnnotations;

namespace Nocturne.DAL
{
    public class DiscountType
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public bool IsActive { get; set; }   
    }
}
