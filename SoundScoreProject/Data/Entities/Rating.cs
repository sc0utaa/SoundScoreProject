namespace SoundScore.Data.Entities
{
    public class Rating
    {
        public int Id { get; set; }
        public int AlbumId { get; set; }
        public int UserId { get; set; }
        public int RatingValue { get; set; }
    }
}
