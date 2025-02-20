using Microsoft.AspNetCore.Mvc;
using SoundScore.Services.Abstractions;

namespace SoundScore.Controllers
{
    public class AlbumsController : Controller
    {
        private readonly IMusicBrainzService _musicBrainzService;

        public AlbumsController(IMusicBrainzService musicBrainzService)
        {
            _musicBrainzService = musicBrainzService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Search(string searchTerm)
        {
            if (!string.IsNullOrEmpty(searchTerm))
            {
                try
                {
                    var albums = await _musicBrainzService.SearchAlbums(searchTerm);
                    return View("Index", albums);
                }
                catch (Exception ex)
                {
                    // Log the exception
                    ModelState.AddModelError(string.Empty, "An error occurred while searching for albums.");
                    return View("Index");
                }
            }

            return View("Index");
        }

        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index");
            }

            try
            {
                var album = await _musicBrainzService.GetAlbumDetails(id);

                if (album == null)
                {
                    return NotFound();
                }

                return View(album);
            }
            catch (Exception ex)
            {
                // Log the exception
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public async Task<JsonResult> SearchApi(string query)
        {
            try
            {
                var albums = await _musicBrainzService.SearchAlbums(query);
                return Json(albums);
            }
            catch (Exception ex)
            {
                // Log the exception
                return Json(new { error = "An error occurred while searching for albums." });
            }
        }
    }
}