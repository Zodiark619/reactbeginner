using Microsoft.AspNetCore.Mvc;

namespace exerccc.Controllers
{
    public class CheckoutResultController : Controller
    {
        [Route("success")]
        public IActionResult Success(string session_id)
        {
            return View();
        }
        [Route("cancel")]
        public IActionResult Cancel()
        {
            return View();
        }
    }
  
}
