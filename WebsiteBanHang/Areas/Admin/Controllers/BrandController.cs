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
    public class BrandController : Controller
    {
        WebsiteBanHangOnlineEntities objWebBanHangEntities = new WebsiteBanHangOnlineEntities();
        // GET: Admin/Brand
        public ActionResult Index(string SearchString)
        {
            var lstBrand = objWebBanHangEntities.Brands.Where(n => n.Name.Contains(SearchString)).ToList();
            return View(lstBrand);
        }
        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Create(Brand objBrand)
        {
            try
            {
                if (objBrand.ImageUpLoad == null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(objBrand.ImageUpLoad.FileName);
                    string extension = Path.GetExtension(objBrand.ImageUpLoad.FileName);
                    fileName = fileName + extension;
                    objBrand.Avatar = fileName;
                    objBrand.ImageUpLoad.SaveAs(Path.Combine(Server.MapPath("~/Controllers/images/"), fileName));
                }
                objWebBanHangEntities.Brands.Add(objBrand);
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
            var objBrand = objWebBanHangEntities.Brands.Where(n => n.Id == id).FirstOrDefault();
            return View(objBrand);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objBrand = objWebBanHangEntities.Brands.Where(n => n.Id == id).FirstOrDefault();
            return View(objBrand);
        }
        [HttpPost]
        public ActionResult Delete(Brand objBr)
        {
            var objBrand = objWebBanHangEntities.Brands.Where(n => n.Id == objBr.Id).FirstOrDefault();
            objWebBanHangEntities.Brands.Remove(objBrand);
            objWebBanHangEntities.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var objBrand = objWebBanHangEntities.Brands.Where(n => n.Id == id).FirstOrDefault();
            return View(objBrand);
        }
        [HttpPost]
        public ActionResult Edit(int id, Brand objBrand)
        {
            if (objBrand.ImageUpLoad != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(objBrand.ImageUpLoad.FileName);
                string extension = Path.GetExtension(objBrand.ImageUpLoad.FileName);
                objBrand.Avatar = fileName + extension;
                objBrand.ImageUpLoad.SaveAs(Path.Combine(Server.MapPath("~/Controllers/images/"), fileName + extension));
            }
            objWebBanHangEntities.Entry(objBrand).State = EntityState.Modified;
            objWebBanHangEntities.SaveChanges();
            return View(objBrand);
        }
    }
}