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
        Models.OrdersService ordersService = new Models.OrdersService();
        Models.CodeService codeService = new Models.CodeService();
        // GET: Orders
        public ActionResult Index(Models.Orders selectitem)
        {
            
            ViewBag.data = ordersService.GetOrdersById(selectitem);

            ViewBag.CompanyName = this.codeService.GetCustomer();
            ViewBag.empData = this.codeService.GetEmpname();
            ViewBag.shipData = this.codeService.GetShipperName();
            ViewBag.ProductCodeData = this.codeService.GetProduct();
            return View(new Models.Orders());
        }
        [HttpPost()]
        public JsonResult DeleteOrders(string OrderID)
        {

            try
            {

                Models.OrdersService OrdersService = new Models.OrdersService();
                OrdersService.DeleteOrdersById(OrderID);

                return this.Json(true);
            }
            catch (Exception)
            {
                return this.Json(false);
            }
        }
        public ActionResult InsertIndex(Models.Orders selectitem)
        {
            Models.OrdersService ordersService = new Models.OrdersService();
            Models.CodeService codeService = new Models.CodeService();

            ViewBag.CompanyName = this.codeService.GetCustomer();
            ViewBag.empData = this.codeService.GetEmpname();
            ViewBag.shipData = this.codeService.GetShipperName();
            ViewBag.ProductCodeData = this.codeService.GetProduct();
            return View(new Models.Orders());

        }
    }
}