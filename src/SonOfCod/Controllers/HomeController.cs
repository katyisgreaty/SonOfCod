using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SonOfCod.Models;
using Microsoft.EntityFrameworkCore;

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

        public IActionResult Marketing()
        {
            return View(db.Markets.ToList());
        }

        public IActionResult Edit(int id)
        {
            var thisItem = db.Markets.FirstOrDefault(items => items.MarketId == id);
            return View(thisItem);
        }

        [HttpPost]
        public IActionResult Edit(Market market)
        {
            db.Entry(market).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Marketing");
        }

        public IActionResult Delete(int id)
        {
            var thisMarket = db.Markets.FirstOrDefault(markets => markets.MarketId == id);
            return View(thisMarket);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var thisMarket = db.Markets.FirstOrDefault(markets => markets.MarketId == id);
            db.Markets.Remove(thisMarket);
            db.SaveChanges();
            return RedirectToAction("Marketing");
        }
    }

    
}
