using SoundScore.Data.Entities;
using SoundScore.Services.Abstractions;
using MetaBrainz.MusicBrainz;
using MetaBrainz.MusicBrainz.Interfaces.Entities;
using SoundScoreProject.Models;


namespace SoundScore.Services
{
    public class MusicBrainzService : IMusicBrainzService
    {
        private readonly Query _query;
        private const string ApplicationName = "SoundScore";
        private const string Version = "1.0.0";
        private const string ContactInfo = "your@email.com"; // Replace with your contact info

        public MusicBrainzService()
        {
            _query = new Query(ApplicationName, Version, ContactInfo);
        }

        public async Task<List<Album>> SearchAlbums(string query)
        {
            var results = await _query.FindReleasesAsync(query);
            var albums = new List<Album>();

            foreach (var release in results.Results.Take(10)) // Limiting to 10 results for performance
            {
                albums.Add(await ConvertToAlbum(release.Item));
            }

            return albums;
        }

        public async Task<Album> GetAlbumDetails(string mbid)
        {
            var release = await _query.LookupReleaseAsync(
                Guid.Parse(mbid),
                Include.Artists | Include.Recordings | Include.ArtistCredits | Include.Genres);

            return await ConvertToAlbum(release, includeDetails: true);
        }

        private async Task<Album> ConvertToAlbum(IRelease release, bool includeDetails = false)
        {
            // Parse the release date
            DateTime releaseDate = DateTime.MinValue;
            if (release.Date != null)
            {
                // Try to parse the date based on available information
                if (int.TryParse(release.Date.Year?.ToString(), out int year))
                {
                    int month = release.Date.Month ?? 1;
                    int day = release.Date.Day ?? 1;
                    releaseDate = new DateTime(year, month, day);
                }
            }

            var album = new Album
            {
                Title = release.Title,
                ReleaseDate = releaseDate,
                Description = release.Disambiguation,
                CoverImageUrl = $"https://coverartarchive.org/release/{release.Id}/front",
                Artist = new Artist
                {
                    Name = release.ArtistCredit?.FirstOrDefault()?.Name ?? "Unknown Artist"
                }
            };

            if (includeDetails)
            {
                // Add tracks if available
                album.Tracks = new List<Track>();

                foreach (var medium in release.Media)
                {
                    foreach (var track in medium.Tracks)
                    {
                        // Parse track number safely
                        int trackNumber = 0;
                        if (int.TryParse(track.Number, out int parsedNumber))
                        {
                            trackNumber = parsedNumber;
                        }

                        var duration = TimeSpan.Zero;
                        if (track.Recording?.Length.HasValue == true)
                        {
                            duration = track.Recording.Length.Value;
                        }

                        album.Tracks.Add(new Track
                        {
                            Title = track.Recording?.Title ?? track.Title,
                            TrackNumber = trackNumber,
                            Duration = duration
                        });
                    }
                }
            }

            return album;
        }
    }
}
