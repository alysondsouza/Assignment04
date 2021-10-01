using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Assignment4.Entities
{
    public class User
    {
        [Required]
        public int Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        public ICollection<Task> Tasks { get; set; }
    }
}
