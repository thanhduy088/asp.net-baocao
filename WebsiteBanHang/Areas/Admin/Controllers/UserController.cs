using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanHang.Context;

namespace WebsiteBanHang.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        WebsiteBanHangOnlineEntities objWebBanHangEntities = new WebsiteBanHangOnlineEntities();
        // GET: Admin/User
        public ActionResult Index(string SearchString)
        {
            var lstUser = objWebBanHangEntities.Users.Where(n => n.FirstName.Contains(SearchString)).ToList();
            return View(lstUser);
        }
        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Create(User objUser)
        {
            try
            {
                objWebBanHangEntities.Users.Add(objUser);
                objWebBanHangEntities.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            var objUser = objWebBanHangEntities.Users.Where(n => n.Id == id).FirstOrDefault();
            return View(objUser);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objUser = objWebBanHangEntities.Users.Where(n => n.Id == id).FirstOrDefault();
            return View(objUser);
        }
        [HttpPost]
        public ActionResult Delete(User objUsers)
        {
            var objUser = objWebBanHangEntities.Users.Where(n => n.Id == objUsers.Id).FirstOrDefault();
            objWebBanHangEntities.Users.Remove(objUser);
            objWebBanHangEntities.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var objUser = objWebBanHangEntities.Users.Where(n => n.Id == id).FirstOrDefault();
            return View(objUser);
        }
        [HttpPost]
        public ActionResult Edit(int id, User objUser)
        {
            objWebBanHangEntities.Entry(objUser).State = EntityState.Modified;
            objWebBanHangEntities.SaveChanges();
            return View(objUser);
        }
    }
}