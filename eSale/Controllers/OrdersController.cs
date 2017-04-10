using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eSale.Controllers
{
    public class OrdersController : Controller
    {
        // GET: Orders
        public ActionResult Index()
        {
            Models.OrdersService orderService = new Models.OrdersService();
            var aa=orderService.GetOrdersById("10250");
            ViewBag.aa = aa.CustomerID;
            return View();
        }
    }
}