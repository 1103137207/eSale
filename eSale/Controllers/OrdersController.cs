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
        public ActionResult Index(Orders selectitem)
        {
            Models.OrdersService ordersService = new Models.OrdersService();
            ViewBag.data = ordersService.GetOrdersById("OrderID");

            CodeTableService CodeTableService = new CodeTableService();
            List<CodeTable> result1 = CodeTableService.GetTitle();
            List<SelectListItem> CodeTableData = new List<SelectListItem>();
            CodeTableData.Add(new SelectListItem()
            {
                Text = "",
                Value = null
            });
            foreach (var item1 in result1)
            {
                CodeTableData.Add(new SelectListItem()
                {
                    Text = item1.CodeVal.ToString(),
                    Value = item1.CodeId.ToString()
                });
                ViewData["CodeTableData"] = CodeTableData;
            }

            return View();
        }
    }
}