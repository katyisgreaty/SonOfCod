using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SonOfCod.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SonOfCod.Controllers
{
    public class HomeController : Controller
    {
        private SonOfCodContext db = new SonOfCodContext();
        public IActionResult Index()
        {
            return View(db.Subscribers.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Subscriber subscriber)
        {
            db.Subscribers.Add(subscriber);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }

    
}
