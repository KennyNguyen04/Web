using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace lab2.Controllers
{
    public class HelloWorldController : Controller
    {
        // 
        // GET: /HelloWorld/
        public IActionResult Index()
        {
            return View();
        }

        // 
        // GET: /HelloWorld/Welcome/ 
        // Requires using System.Text.Encodings.Web;
        // GET: /HelloWorld/Welcome/ 
        public IActionResult Welcome(string name, int numTimes = 1)
        {
            // Lưu dữ liệu vào từ điển ViewData
            ViewData["Message"] = "Hello " + name;
            ViewData["NumTimes"] = numTimes;

            // Trả về View (sẽ tìm file Welcome.cshtml)
            return View();
        }
    }
    
}