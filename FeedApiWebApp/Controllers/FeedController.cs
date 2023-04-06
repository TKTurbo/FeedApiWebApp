using FeedApiWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace FeedApiWebApp.Controllers
{
    public class FeedController : Controller
    {
        public IActionResult Index()
        {
            // Create a new feed object and get the FeedViewModel from it.
            Feed feed = new Feed();
            return View(feed.get());
        }
    }
}
