using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Nocturne.DAL
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string DisplayName { get; set; }

        [Required]
        [MaxLength(50)]
        public string RegCode { get; set; }

        public bool IsActive { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
    }
}
