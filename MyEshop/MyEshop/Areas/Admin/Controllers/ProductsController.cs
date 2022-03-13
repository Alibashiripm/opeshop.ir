using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using InsertShowImage;
using KooyWebApp_MVC.Classes;

namespace MyEshop.Areas.Admin.Controllers
{
    public class ProductsController : Controller
    {
        private MyEshop_DBEntities db = new MyEshop_DBEntities();

        // GET: Admin/Products
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }

        // GET: Admin/Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = db.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        // GET: Admin/Products/Create
        public ActionResult Create()
        {
            ViewBag.Groups = db.Product_Groups.ToList();
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,Title,ShortDescription,Text,Price,ImageName,CreateDate,ACUserName,ACPassword")] Products products,List<int> selectedGroups,HttpPostedFileBase imageProduct,string tags)
        {
            if (ModelState.IsValid)
            {
                if (selectedGroups == null)
                {
                    ViewBag.ErrorSelectedGroup = true;
                    ViewBag.Groups = db.Product_Groups.ToList();
                    return View(products);
                }
     
                products.ImageName = "images.jpg";
                if (imageProduct != null&&imageProduct.IsImage())
                {
                    products.ImageName = Guid.NewGuid().ToString() + Path.GetExtension(imageProduct.FileName);
                    imageProduct.SaveAs(Server.MapPath("/Images/ProductImages/"+products.ImageName));
                    ImageResizer img=new ImageResizer();
                    img.Resize(Server.MapPath("/Images/ProductImages/" + products.ImageName),
                        Server.MapPath("/Images/ProductImages/Thumb/" + products.ImageName));
                }
                products.CreateDate=DateTime.Now;
                db.Products.Add(products);

                foreach (int selectedGroup in selectedGroups)
                {
                    db.Prodct_Selected_Groups.Add(new Prodct_Selected_Groups()
                    {
                        ProductID = products.ProductID,
                        GroupID = selectedGroup
                    });
                }

                if (!string.IsNullOrEmpty(tags))
                {
                    string[] tag = tags.Split(',');
                    foreach (string t in tag)
                    {
                        db.Product_Tags.Add(new Product_Tags()
                        {
                            ProductID = products.ProductID,
                            Tag = t.Trim()
                        });
                    }
                }
                 
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Groups = db.Product_Groups.ToList();
            return View(products);
        }

        // GET: Admin/Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = db.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }

            ViewBag.SelectedGroups = products.Prodct_Selected_Groups.ToList();
            ViewBag.Groups = db.Product_Groups.ToList();
            ViewBag.Tags = string.Join(",", products.Product_Tags.Select(t => t.Tag).ToList());
            return View(products);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,Title,ShortDescription,Text,Price,ImageName,CreateDate")] Products products, List<int> selectedGroups, HttpPostedFileBase imageProduct, string tags)
        {
            if (ModelState.IsValid)
            {
                if (imageProduct != null && imageProduct.IsImage())
                {
                    if (products.ImageName != "images.jpg")
                    {
                        System.IO.File.Delete(Server.MapPath("/Images/ProductImages/"+products.ImageName));
                        System.IO.File.Delete(Server.MapPath("/Images/ProductImages/Thumb/"+products.ImageName));
                    }

                    products.ImageName = Guid.NewGuid().ToString() + Path.GetExtension(imageProduct.FileName);
                    imageProduct.SaveAs(Server.MapPath("/Images/ProductImages/" + products.ImageName));
                    ImageResizer img = new ImageResizer();
                    img.Resize(Server.MapPath("/Images/ProductImages/" + products.ImageName),
                        Server.MapPath("/Images/ProductImages/Thumb/" + products.ImageName));
                }
                db.Entry(products).State = EntityState.Modified;


                db.Product_Tags.Where(t=>t.ProductID==products.ProductID).ToList().ForEach(t=> db.Product_Tags.Remove(t));
                if (!string.IsNullOrEmpty(tags))
                {
                    string[] tag = tags.Split(',');
                    foreach (string t in tag)
                    {
                        db.Product_Tags.Add(new Product_Tags()
                        {
                            ProductID = products.ProductID,
                            Tag = t.Trim()
                        });
                    }
                }


                db.Prodct_Selected_Groups.Where(g=>g.ProductID==products.ProductID).ToList().ForEach(g=>db.Prodct_Selected_Groups.Remove(g));
                if (selectedGroups != null && selectedGroups.Any())
                {
                    foreach (int selectedGroup in selectedGroups)
                    {
                        db.Prodct_Selected_Groups.Add(new Prodct_Selected_Groups()
                        {
                            ProductID = products.ProductID,
                            GroupID = selectedGroup
                        });
                    }
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SelectedGroups = selectedGroups;
            ViewBag.Groups = db.Product_Groups.ToList();
            ViewBag.Tags = tags;
            return View(products);
        }

        // GET: Admin/Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = db.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Products products = db.Products.Find(id);
            db.Products.Remove(products);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        #region Gallery

        public ActionResult Gallery(int id)
        {
            ViewBag.Galleries = db.Product_Galleries.Where(p => p.ProductID == id).ToList();
            return View(new Product_Galleries()
            {
                ProductID = id
            });
        }

        [HttpPost]
        public ActionResult Gallery(Product_Galleries galleries, HttpPostedFileBase imgUp)
        {
            if (ModelState.IsValid)
            {
                if (imgUp != null && imgUp.IsImage())
                {
                    galleries.ImageName = Guid.NewGuid().ToString() + Path.GetExtension(imgUp.FileName);
                    imgUp.SaveAs(Server.MapPath("/Images/ProductImages/" + galleries.ImageName));
                    ImageResizer img = new ImageResizer();
                    img.Resize(Server.MapPath("/Images/ProductImages/" + galleries.ImageName),
                        Server.MapPath("/Images/ProductImages/Thumb/" + galleries.ImageName));
                    db.Product_Galleries.Add(galleries);
                    db.SaveChanges();
                }
            }

            return RedirectToAction("Gallery", new { id = galleries.ProductID });
        }

        public ActionResult DeleteGallery(int id)
        {
            var gallery = db.Product_Galleries.Find(id);

            System.IO.File.Delete(Server.MapPath("/Images/ProductImages/" + gallery.ImageName));
            System.IO.File.Delete(Server.MapPath("/Images/ProductImages/Thumb/" + gallery.ImageName));

            db.Product_Galleries.Remove(gallery);
            db.SaveChanges();
            return RedirectToAction("Gallery", new { id = gallery.ProductID });
        }

        #endregion


        #region  Featurs

        public ActionResult ProductFeaturs(int id)
        {
            
            ViewBag.Features = db.Product_Features.Where(f => f.ProductID == id).ToList();
            ViewBag.FeatureID = new SelectList(db.Features, "FeatureID", "FeatureTitle");
            return View(new Product_Features()
            {
                ProductID = id
            });
        }

        [HttpPost]
        public ActionResult ProductFeaturs(Product_Features feature)
        {
            if (ModelState.IsValid)
            {
                db.Product_Features.Add(feature);
                db.SaveChanges();
            }

            return RedirectToAction("ProductFeaturs", new {id = feature.ProductID});
        }

        public void DeleteFeature(int id)
        {
            var feature = db.Product_Features.Find(id);
            db.Product_Features.Remove(feature);
            db.SaveChanges();
        }
        #endregion
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
