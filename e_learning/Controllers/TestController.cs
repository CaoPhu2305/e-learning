using System.Web.Mvc;

namespace e_learning.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public JsonResult Index()
        {
            return Json(new { success = true, message = "Test successful!" }, JsonRequestBehavior.AllowGet);
        }
    }
}
