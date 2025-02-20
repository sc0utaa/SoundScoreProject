using System.ComponentModel.DataAnnotations;

namespace SoundScore.Data.Entities
{
    public class Artist
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [MaxLength(1000)]
        public string Biography { get; set; }

        [MaxLength(200)]
        public string ImageUrl { get; set; }

        public List<Album> Albums { get; set; }
    }
}
