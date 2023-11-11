using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanHang.Context;

namespace WebsiteBanHang.Controllers
{
    public class BrandController : Controller
    {
        WebsiteBanHangOnlineEntities objWebBanHangEntities = new WebsiteBanHangOnlineEntities();
        // GET: Brand
        public ActionResult Index()
        {
            var objBrand = objWebBanHangEntities.Brands.ToList();
            return View(objBrand);
        }
    }
}