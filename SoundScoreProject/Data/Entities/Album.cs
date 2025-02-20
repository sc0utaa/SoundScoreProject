using System.ComponentModel.DataAnnotations;

namespace SoundScore.Data.Entities
{
    public class Album
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [Required]
        public int ArtistId { get; set; }
        public Artist Artist { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [MaxLength(200)]
        public string CoverImageUrl { get; set; }

        public List<Track> Tracks { get; set; }
    }
}
