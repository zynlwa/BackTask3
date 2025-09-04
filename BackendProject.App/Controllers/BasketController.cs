using Microsoft.AspNetCore.Mvc;

namespace BackendProject.App.Controllers

{
    public class BasketController : Controller
    {
        public IActionResult Index()
        {

            return Content("");
        }
        public void SetCookie()
        {
            string value = "PB305";
            CookieOptions cookieOptions = new CookieOptions();
            cookieOptions.Expires=DateTime.Now.AddDays(1);
            Response.Cookies.Append("name", value,cookieOptions);
            
        }
        public string GetCookie()
        {
            string value = Request.Cookies["name"];

            return value;
        }
        public IActionResult DeleteCookie()
        {
            Response.Cookies.Delete("name");
            return RedirectToAction("Index");
        }
        public IActionResult SessionSet()
        {
            HttpContext.Session.SetString("name", "PB305");
            return RedirectToAction("Index");
        }
        public IActionResult SessionGet()
        {
            string value = HttpContext.Session.GetString("name");
            return Content(value);
        }
    }

}
