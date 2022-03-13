using DataLayer;
using DataLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyEshop.Controllers
{
    public class ShopCartController : Controller
    {
        DataLayer.MyEshop_DBEntities db = new DataLayer.MyEshop_DBEntities();
        // GET: ShopCart
        public ActionResult ShowCart()
        {
            List<DataLayer.ViewModels.ShopCartItemViewModel> list = new List<DataLayer.ViewModels.ShopCartItemViewModel>();

            if (Session["ShopCart"] != null)
            {
                List<DataLayer.ViewModels.ShopCartItem> listShop = (List<DataLayer.ViewModels.ShopCartItem>)Session["ShopCart"];

                foreach (var item in listShop)
                {
                    var product = db.Products.Where(p => p.ProductID == item.ProductID).Select(p => new
                    {
                        p.ImageName,
                        p.Title
                    }).Single();
                    list.Add(new DataLayer.ViewModels.ShopCartItemViewModel()
                    {
                        Count = item.Count,
                        ProductID = item.ProductID,
                        Title = product.Title,
                        ImageName = product.ImageName

                    });
                }
            }

            return PartialView(list);
        }

        public ActionResult Index()
        {
            return View();
        }

        List<ShowOrderViewModel> getListOrder()
        {
            List<DataLayer.ViewModels.ShowOrderViewModel> list = new List<DataLayer.ViewModels.ShowOrderViewModel>();

            if (Session["ShopCart"] != null)
            {
                List<DataLayer.ViewModels.ShopCartItem> listShop = Session["ShopCart"] as List<DataLayer.ViewModels.ShopCartItem>;

                foreach (var item in listShop)
                {
                    var product = db.Products.Where(p => p.ProductID == item.ProductID).Select(p => new
                    {
                        p.ImageName,
                        p.Title,
                        p.Price
                    }).Single();
                    list.Add(new DataLayer.ViewModels.ShowOrderViewModel()
                    {
                        Count = item.Count,
                        ProductID = item.ProductID,
                        Price = product.Price,
                        ImageName = product.ImageName,
                        Title = product.Title,
                        Sum = item.Count * product.Price
                    });
                }
            }
            return list;
        }

        public ActionResult Order()
        {
            return PartialView(getListOrder());
        }

        public ActionResult CommandOrder(int id, int count)
        {
            List<DataLayer.ViewModels.ShopCartItem> listShop = Session["ShopCart"] as List<DataLayer.ViewModels.ShopCartItem>;
            int index = listShop.FindIndex(p => p.ProductID == id);
            if (count == 0)
            {
                listShop.RemoveAt(index);
            }
            else
            {
                listShop[index].Count = count;
            }
            Session["ShopCart"] = listShop;

            return PartialView("Order", getListOrder());
        }

        [Authorize]
        public ActionResult Payment()
        {
            int userId = db.Users.Single(u => u.UserName == User.Identity.Name).UserID;
            Orders order = new DataLayer.Orders()
            {
                UserID = userId,
                Date = DateTime.Now,
                IsFinaly = false,
            };
            db.Orders.Add(order);

            var listDetails = getListOrder();

            foreach (var item in listDetails)
            {
                db.OrderDetails.Add(new DataLayer.OrderDetails()
                {
                    Count = item.Count,
                    OrderID = order.OrderID,
                    Price = item.Price,
                    ProductID = item.ProductID,
                });
            }
            db.SaveChanges();

            //TODO : Online Payment
            var price = db.OrderDetails.SingleOrDefault(s => s.OrderID == order.OrderID).Price;
            System.Net.ServicePointManager.Expect100Continue = false;
            ServiceReference1.PaymentGatewayImplementationServicePortTypeClient zp = new ServiceReference1.PaymentGatewayImplementationServicePortTypeClient();
            string Authority;

            int Status = zp.PaymentRequest("9ad3c2fe-1482-4015-a7c5-6d406610fae4", price, "درگاه زرین پال", "bashiriali2002@gmail.com", "09376924833", "https://opeshop.ir/ShopCart/Verify/" + order.OrderID, out Authority);

            if (Status == 100)
            {
                Response.Redirect("https://www.zarinpal.com/pg/StartPay/" + Authority);
    

            }
            else
            {
                ViewBag.Error = "Error : " + Status;
            }
            return View();
        }

        public ActionResult Verify(int id)
        {
            var order = db.Orders.Find(id);

            var price = db.OrderDetails.SingleOrDefault(s => s.OrderID == order.OrderID).Price;
            var product = db.Products.SingleOrDefault(p => p.Price == price);


            db.SaveChanges();

            if (Request.QueryString["Status"] != "" && Request.QueryString["Status"] != null && Request.QueryString["Authority"] != "" && Request.QueryString["Authority"] != null)
            {
                if (Request.QueryString["Status"].ToString().Equals("OK"))
                {
    
                    int Amount = price;
                    long RefID;
                    System.Net.ServicePointManager.Expect100Continue = false;
                    ServiceReference1.PaymentGatewayImplementationServicePortTypeClient zp = new ServiceReference1.PaymentGatewayImplementationServicePortTypeClient();

                    int Status = zp.PaymentVerification("9ad3c2fe-1482-4015-a7c5-6d406610fae4", Request.QueryString["Authority"].ToString(), Amount, out RefID);

                    if (Status == 100)
                    {
                        ViewBag.p = product.ACPassword;
                        ViewBag.z = product.ACUserName;
                        order.IsFinaly = true;
                        db.SaveChanges();
                        ViewBag.IsSuccess = true;
                        ViewBag.RefId = RefID;
                      
                        return View();
                        // Response.Write("Success!! RefId: " + RefID);
                    }
                    else
                    {
                        ViewBag.Status = Status;
                    }

                }
                else
                {
                    Response.Write("Error! Authority: " + Request.QueryString["Authority"].ToString() + " Status: " + Request.QueryString["Status"].ToString());
                }
            }
            else
            {
                Response.Write("Invalid Input");
            }
            return View();
        }
    }
}

