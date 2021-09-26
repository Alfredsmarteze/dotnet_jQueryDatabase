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
            if (id >0 )
            {
                _appDBContext.users.Update(user);
                _appDBContext.SaveChanges();
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
        public IActionResult Index()
        {
            //var asdf = _appDBContext.users.SingleOrDefault(q => q.ID == id);
            //User user1 = new User();
            //if(id>0)
            
            //{            
            //user1.FirstName = asdf.FirstName;
            //user1.LastName = asdf.LastName;
            //user1.Contact = asdf.Contact;
            //user1.MiddleName = asdf.MiddleName;

               
            //}
            //return View();
        }

        public JsonResult getAllUsers()
        {

            List<User> users = new List<User>();
            users = _appDBContext.users.Select(c => c).ToList();

            return Json(new { data = users });
        }

        [HttpGet]
        public async Task<IActionResult> UserEdit(int id)
        {
            if (id == null)
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
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var ret = _appDBContext.users.Find(id);
            return View(ret);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(int id, User userd)
        {
            if (id==null)
            {
                return NotFound();
            }
            var qw = _appDBContext.users.Find(id);
             _appDBContext.users.Remove(qw);
            _appDBContext.SaveChanges();
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
