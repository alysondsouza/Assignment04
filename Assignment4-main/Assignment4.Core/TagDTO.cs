using System.ComponentModel.DataAnnotations;

namespace Assignment4.Core
{
    public record TagDTO(int Id, string Name);

    public record TagCreateDTO
    {
        //TagRepository.Create was meant to return an Id using a TagCreateDTO, so added an Id field
        [Required]
        public int Id;

        [Required]
        [StringLength(50)]
        public string Name { get; init; }

    }

    public record TagUpdateDTO : TagCreateDTO
    {
        public int Id { get; init; }
    }
}