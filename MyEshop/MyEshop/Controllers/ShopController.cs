using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace MyEshop.Controllers
{
    public class ShopController : ApiController
    {
        // GET: api/Shop
        public int Get()
        {
            List<DataLayer.ViewModels.ShopCartItem> list = new List<DataLayer.ViewModels.ShopCartItem>();
            var sessions = HttpContext.Current.Session;
            if (sessions["ShopCart"] != null)
            {
                list = sessions["ShopCart"] as List<DataLayer.ViewModels.ShopCartItem>;
            }

            return list.Sum(l => l.Count);
        }

        // GET: api/Shop/5
        public int Get(int id)
        {
            List<DataLayer.ViewModels.ShopCartItem> list = new List<DataLayer.ViewModels.ShopCartItem>();
            var sessions = HttpContext.Current.Session;
            if(sessions["ShopCart"]!=null)
            {
                list = sessions["ShopCart"] as List<DataLayer.ViewModels.ShopCartItem>;
            }
            if(list.Any(p=>p.ProductID==id))
            {
                int index = list.FindIndex(p => p.ProductID == id);
                list[index].Count += 1;
            }
            else
            {
                list.Add(new DataLayer.ViewModels.ShopCartItem()
                {
                    ProductID=id,
                    Count=1
                });
            }

            sessions["ShopCart"] = list;
            return Get();
        }


    }
}
