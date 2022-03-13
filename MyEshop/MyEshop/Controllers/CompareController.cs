using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyEshop.Controllers
{
    public class CompareController : Controller
    {
        DataLayer.MyEshop_DBEntities db = new DataLayer.MyEshop_DBEntities();
        // GET: Compare
        public ActionResult Index()
        {
            List<DataLayer.ViewModels.CompareItem> list = new List<DataLayer.ViewModels.CompareItem>();

            if (Session["Compare"] != null)
            {
                list = Session["Compare"] as List<DataLayer.ViewModels.CompareItem>;
            }
            List<DataLayer.Features> features = new List<DataLayer.Features>();
            List<DataLayer.Product_Features> productFeatures = new List<DataLayer.Product_Features>();
            foreach(var item in list)
            {
                features.AddRange(db.Product_Features.Where(p => p.ProductID == item.ProductID).Select(f => f.Features).ToList());
                productFeatures.AddRange(db.Product_Features.Where(p => p.ProductID == item.ProductID).ToList());
            }
            ViewBag.features = features.Distinct().ToList();
            ViewBag.productFeatures = productFeatures;
            return View(list);
        }

        public ActionResult AddToCompare(int id)
        {
            List<DataLayer.ViewModels.CompareItem> list = new List<DataLayer.ViewModels.CompareItem>();

            if(Session["Compare"]!=null)
            {
                list = Session["Compare"] as List<DataLayer.ViewModels.CompareItem>;
            }

            if(!list.Any(p=>p.ProductID==id))
            {
                var product = db.Products.Where(p => p.ProductID == id).Select(p => new { p.Title,p.ImageName}).Single();
                list.Add(new DataLayer.ViewModels.CompareItem()
                {
                    ProductID=id,
                    Title=product.Title,
                    ImageName=product.ImageName
                });
            }
            Session["Compare"] = list;

            return PartialView("ListCompare",list);
        }

        public ActionResult ListCompare()
        {
            List<DataLayer.ViewModels.CompareItem> list = new List<DataLayer.ViewModels.CompareItem>();

            if (Session["Compare"] != null)
            {
                list = Session["Compare"] as List<DataLayer.ViewModels.CompareItem>;
            }
            return PartialView(list);
        }

        public ActionResult DeleteFromCompare(int id)
        {
            List<DataLayer.ViewModels.CompareItem> list = new List<DataLayer.ViewModels.CompareItem>();

            if (Session["Compare"] != null)
            {
                list = Session["Compare"] as List<DataLayer.ViewModels.CompareItem>;
                int index = list.FindIndex(p => p.ProductID == id);
                list.RemoveAt(index);
                Session["Compare"] = list;
            }
            return PartialView("ListCompare", list);

        }
    }
}