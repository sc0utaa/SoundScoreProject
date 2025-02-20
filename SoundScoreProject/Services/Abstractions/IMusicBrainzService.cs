using SoundScore.Data.Entities;

namespace SoundScore.Services.Abstractions
{
    public interface IMusicBrainzService
    {
        Task<List<Album>> SearchAlbums(string query);
        Task<Album> GetAlbumDetails(string mbid);
    }
}
