using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemoWeb.Models;
using System.Net;

namespace DemoWeb.Controllers
{
    public class CustomerProductsController : Controller
    {
        private DBSportStoreEntities db = new DBSportStoreEntities();
        // GET: CustomerProducts
        public ActionResult Index()
        {
            var products = db.Products;
            return View(products.ToList());
        }

        public ActionResult Details(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            } 
            var product = db.Products.Find(id);
            if(product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        public ActionResult GetProductsByCategory()
        {
            var categories = db.Categories.ToList();
            return PartialView("CategoriesPartialView", categories);
        }

        public ActionResult GetProductsByCateId(int id)
        {
            var products = db.Products.Where(p => p.Category1.Id == id).ToList();
            return View("Index", products);
        }

    }
}