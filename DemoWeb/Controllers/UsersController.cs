using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using DemoWeb.Models;

namespace DemoWeb.Controllers
{
    public class UsersController : Controller
    {
        private DBSportStoreEntities database = new DBSportStoreEntities();
        // GET: Users
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(Customer cust)
        {
            if(ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(cust.NameCus))
                    ModelState.AddModelError(string.Empty, "Tên đăng nhập không được trống");
                if (string.IsNullOrEmpty(cust.PassCus))
                    ModelState.AddModelError(string.Empty, "Mật khẩu không được trống");
                if (string.IsNullOrEmpty(cust.EmailCus))
                    ModelState.AddModelError(string.Empty, "Email không được trống");
                if (string.IsNullOrEmpty(cust.PhoneCus))
                    ModelState.AddModelError(string.Empty, "Số điện thoại không được trống");

                var khachhang = database.Customers.FirstOrDefault(kh => kh.NameCus == cust.NameCus);
                if(khachhang != null)
                {
                    ModelState.AddModelError(string.Empty, "Đã có người đăng ký tên này");
                }

                if(ModelState.IsValid)
                {
                    database.Customers.Add(cust);
                    database.SaveChanges();
                } else
                {
                    return View(cust);
                }
            }
            return RedirectToAction("Login");
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Customer cust)
        {
            if(ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(cust.NameCus))
                    ModelState.AddModelError(string.Empty, "Tên đăng nhập không được trống");
                if (string.IsNullOrEmpty(cust.PassCus))
                    ModelState.AddModelError(string.Empty, "Mật khẩu không được trống");
                if (ModelState.IsValid)
                {
                    var khachhang = database.Customers.FirstOrDefault(kh => kh.NameCus == cust.NameCus && kh.PassCus == cust.PassCus);
                    if(khachhang != null)
                    {
                        ViewBag.ThongBao = "Chúc mừng bạn đã đăng nhập thành công";
                        Session["TaiKhoan"] = khachhang;
                        return RedirectToAction("Index", "CustomerProducts");
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
            return RedirectToAction("Login");
        }
    }
}