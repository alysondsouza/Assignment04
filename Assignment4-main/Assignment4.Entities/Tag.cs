namespace Assignment4.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;
    public class Tag
    {
        [Required]
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; }

        public ICollection<Task> Tasks { get; set; }
    }

}
