using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Nocturne.DAL
{
    public class Role
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
    }
}
