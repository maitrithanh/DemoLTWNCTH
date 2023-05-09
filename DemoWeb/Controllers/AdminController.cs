using DemoWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace DemoWeb.Controllers
{
    public class AdminController : Controller
    {
        private DBSportStoreEntities database = new DBSportStoreEntities();
        // GET: Admin
        public ActionResult Index(int? page)
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            var products = database.Products.ToList();
            int pageSize = 8;
            int pageNum = (page ?? 1);
            return View(products.OrderBy(sp => sp.ProductID).ToPagedList(pageNum, pageSize));
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(AdminUser ad)
        {
            if(ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(ad.NameUser))
                    ModelState.AddModelError(string.Empty, "Tên tài khoản không được trống");
                if (string.IsNullOrEmpty(ad.PasswordUser))
                    ModelState.AddModelError(string.Empty, "Mật khẩu không được trống");
                if(ModelState.IsValid)
                {
                    var admin = database.AdminUsers.FirstOrDefault(quantri => quantri.NameUser == ad.NameUser && quantri.PasswordUser == ad.PasswordUser);
                    if(admin != null)
                    {
                        ViewBag.ThongBao = "Chúc mừng bạn đã đăng nhập thành công";
                        Session["Admin"] = admin;
                        return RedirectToAction("Index", "Admin");
                    }
                    else
                    {
                        ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng";
                    }
                }
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "Admin");
        }
    }
}