using System.ComponentModel.DataAnnotations;

namespace Nocturne.DAL
{
    public class Client
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string Surname { get; set; }

        [Required]
        [MaxLength(50)]
        public string IdCode { get; set; }       
    }
}
