using eSale.Models;
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
        public ActionResult Index(Models.Orders selectitem)
        {
            Models.OrdersService ordersService = new Models.OrdersService();
            ViewBag.data = ordersService.GetOrdersById(selectitem);
            
            List<SelectListItem> empData = new List<SelectListItem>();
            var result1 = ordersService.GetEmpname();
            empData.Add(new SelectListItem()
            {
                Text = "",
                Value = null
            });
            foreach (var item in result1)
            empData.Add(new SelectListItem()
            {
                Text =item.Empname.ToString(),
                Value = item.EmployeeID.ToString()
            });
            ViewBag.empData= empData;

            List<SelectListItem> shipData = new List<SelectListItem>();
            var result2 = ordersService.GetShipperName();
            shipData.Add(new SelectListItem()
            {
                Text = "",
                Value = null
            });
            foreach (var item in result2)
                shipData.Add(new SelectListItem()
                {
                    Text = item.ShipperName.ToString(),
                    Value = item.ShipperID.ToString()
                });
            ViewBag.shipData = shipData;

            return View();
        }
     
    }
}