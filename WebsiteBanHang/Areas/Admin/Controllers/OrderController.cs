using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanHang.Context;

namespace WebsiteBanHang.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        WebsiteBanHangOnlineEntities objWebBanHangEntities = new WebsiteBanHangOnlineEntities();
        // GET: Admin/Order
        public ActionResult Index(string SearchString)
        {
            var lstOrder = objWebBanHangEntities.Orders.Where(n => n.Name.Contains(SearchString)).ToList();
            return View(lstOrder);
        }
        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Create(Order objOrder)
        {
            try
            {
                objWebBanHangEntities.Orders.Add(objOrder);
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
            var objOrder = objWebBanHangEntities.Orders.Where(n => n.Id == id).FirstOrDefault();
            return View(objOrder);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objOrder = objWebBanHangEntities.Orders.Where(n => n.Id == id).FirstOrDefault();
            return View(objOrder);
        }
        [HttpPost]
        public ActionResult Delete(Order objOrd)
        {
            var objOrder = objWebBanHangEntities.Orders.Where(n => n.Id == objOrd.Id).FirstOrDefault();
            objWebBanHangEntities.Orders.Remove(objOrder);
            objWebBanHangEntities.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var objOrder = objWebBanHangEntities.Orders.Where(n => n.Id == id).FirstOrDefault();
            return View(objOrder);
        }
        [HttpPost]
        public ActionResult Edit(int id, Order objOrder)
        {
            objWebBanHangEntities.Entry(objOrder).State = EntityState.Modified;
            objWebBanHangEntities.SaveChanges();
            return View(objOrder);
        }
    }
}