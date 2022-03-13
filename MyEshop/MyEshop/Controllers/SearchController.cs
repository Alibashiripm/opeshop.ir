using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyEshop.Controllers
{
    public class SearchController : Controller
    {
        DataLayer.MyEshop_DBEntities db = new DataLayer.MyEshop_DBEntities();
        public ActionResult Index(string q)
        {
            List<DataLayer.Products> list = new List<DataLayer.Products>();

            list.AddRange(db.Product_Tags.Where(t => t.Tag == q).Select(t => t.Products).ToList());
            list.AddRange(db.Products.Where(p => p.Title.Contains(q) || p.ShortDescription.Contains(q) || p.Text.Contains(q)).ToList());

            ViewBag.search = q;
            return View(list.Distinct());
        }
    }
}