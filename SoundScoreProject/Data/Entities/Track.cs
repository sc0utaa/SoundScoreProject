using System.ComponentModel.DataAnnotations;

namespace SoundScore.Data.Entities
{
    public class Track
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        public int AlbumId { get; set; }
        public Album Album { get; set; }

        public int TrackNumber { get; set; }

        public TimeSpan Duration { get; set; }
    }
}
