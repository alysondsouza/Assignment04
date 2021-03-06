namespace Assignment4.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;
    public class Tag
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public IEnumerable<Task> Tasks { get; set; }
    }

}
