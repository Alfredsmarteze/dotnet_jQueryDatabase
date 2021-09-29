using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using dotnet_webgrid.Models;


namespace dotnet_webgrid.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDBContext _appDBContext;

        public HomeController(ILogger<HomeController> logger, AppDBContext appDBContext)
        {
            _logger = logger;
            _appDBContext = appDBContext;
        }

        [HttpPost]
        public IActionResult Index(User user, int id)
        {
            int ds = user.ID;
            
                if (ds > 0)
                {
                    _appDBContext.users.Update(user);
                    _appDBContext.SaveChanges();
                    ModelState.Clear();
            }
            else
            {
                _appDBContext.users.Add(user);
                _appDBContext.SaveChanges();
                ModelState.Clear();
            }
          
          return View();
        }

        [HttpGet]
        public IActionResult Index(int id)
        {
            var asdf = _appDBContext.users.SingleOrDefault(q => q.ID == id);
            User user1 = new User();
            if (id > 0)

            {
                user1.FirstName = asdf.FirstName;
                user1.LastName = asdf.LastName;
                user1.Contact = asdf.Contact;
                user1.MiddleName = asdf.MiddleName;


            }
            return View(user1);
        }

        public JsonResult getAllUsers()
        {

            List<User> users = new List<User>();
            users = _appDBContext.users.Select(c => c).ToList();

            return Json(new { data = users });
        }

        [HttpGet]
        public IActionResult UserEdit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var ret = _appDBContext.users.FirstOrDefault(z=>z.ID==id);
            return View(ret);
        }

        [HttpPost]
        public IActionResult UserEdit(User user)
        {
            _appDBContext.users.Update(user);
            _appDBContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult DeleteUser(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var ret = _appDBContext.users.Find(id);
            return View(ret);
        }

        [HttpPost]
        public IActionResult DeleteUser(int id, User userd)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var qw = _appDBContext.users.Where(w=>w.ID==id).FirstOrDefault();
            _appDBContext.users.Remove(qw);
            _appDBContext.SaveChanges();
            //return Json(new {success = true, message="Successfully Deleted" }, System.Web.Mvc.JsonRequestBehavior.AllowGet);
            return RedirectToAction("Index");
            
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
