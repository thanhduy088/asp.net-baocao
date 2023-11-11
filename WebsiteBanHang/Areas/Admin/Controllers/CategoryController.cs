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
    public class CategoryController : Controller
    {
        WebsiteBanHangOnlineEntities objWebBanHangEntities = new WebsiteBanHangOnlineEntities();
        // GET: Admin/Category
        public ActionResult Index(string SearchString)
        {
            var lstCategory = objWebBanHangEntities.Categories.Where(n => n.Name.Contains(SearchString)).ToList();
            return View(lstCategory);
        }
        public ActionResult Details(int Id)
        {
            var objCategory = objWebBanHangEntities.Categories.Where(n => n.Id == Id).FirstOrDefault();
            return View(objCategory);
        }
        [HttpGet]
        public ActionResult Create()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult Create(Category objCategory)
        {
            try
            {
                if (objCategory.ImageUpLoad != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(objCategory.ImageUpLoad.FileName);
                    string extension = Path.GetExtension(objCategory.ImageUpLoad.FileName);
                    fileName = fileName + extension;
                    objCategory.Avatar = fileName;
                    objCategory.ImageUpLoad.SaveAs(Path.Combine(Server.MapPath("~/Controllers/images/"), fileName));
                }
                objWebBanHangEntities.Categories.Add(objCategory);
                objWebBanHangEntities.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objCategory = objWebBanHangEntities.Categories.Where(n => n.Id == id).FirstOrDefault();
            return View(objCategory);
        }
        [HttpPost]
        public ActionResult Delete(Product objCat)
        {
            var objCategory = objWebBanHangEntities.Categories.Where(n => n.Id == objCat.Id).FirstOrDefault();
            objWebBanHangEntities.Categories.Remove(objCategory);
            objWebBanHangEntities.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var objCategory = objWebBanHangEntities.Categories.Where(n => n.Id == id).FirstOrDefault();
            return View(objCategory);
        }
        [HttpPost]
        public ActionResult Edit(int id, Category objCategory)
        {
            if (objCategory.ImageUpLoad != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(objCategory.ImageUpLoad.FileName);
                string extension = Path.GetExtension(objCategory.ImageUpLoad.FileName);
                objCategory.Avatar = fileName + extension;
                objCategory.ImageUpLoad.SaveAs(Path.Combine(Server.MapPath("~/Controllers/images/"), fileName + extension));
            }
            objWebBanHangEntities.Entry(objCategory).State = EntityState.Modified;
            objWebBanHangEntities.SaveChanges();
            return View(objCategory);
        }
    }
}