using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Livis.Market.Controllers
{
    [Authorize(Roles = "Admin")]
    public class LivisServerController : BaseLivisServerController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}