using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using Kendo.Mvc.Extensions; // You need this to be able to use the ToDataSourceResult() method for processing the request.
using Kendo.Mvc.UI; // You need this to be able to use the DataSourceRequest class and attribute to parse the request.

using LearnKendoUiAspMvc.Models.ViewModels;


namespace LearnKendoUiAspMvc.Controllers
{
    public class GridController : Controller
    {
        // The example will add some dummy data but you can use a data base Select() if you like.
        public static List<OrderViewModel> orders = Enumerable.Range(1, 10).Select(i => new OrderViewModel
        {
            OrderID = i,
            ShipCountry = i % 2 == 0 ? "ShipCountry 1" : "ShipCountry 2",
            Freight = i * 10
        }).ToList();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ReadOrders([DataSourceRequest] DataSourceRequest request)
        {
            return Json(orders.ToDataSourceResult(request));
        }

        public ActionResult CreateOrders([DataSourceRequest] DataSourceRequest request, OrderViewModel order)
        {
            if (ModelState.IsValid)
            {
                // Add a model Id so that the dataSource will treat this item as existing next time.
                order.OrderID = orders.Count + 1;

                // Save the item in the data base.
                orders.Add(order);
            }

            // Return a collection which contains only the newly created item and any validation errors.
            return Json(new[] { order }.ToDataSourceResult(request, ModelState));
        }

        public ActionResult UpdateOrders([DataSourceRequest] DataSourceRequest request, OrderViewModel order)
        {
            if (ModelState.IsValid)
            {
                // Save the item in the data base or follow with the dummy data.
                for (int i = 0; i < orders.Count; i++)
                {
                    // The example uses the model Id to identify the model that needs to be updated.
                    if (orders[i].OrderID == order.OrderID)
                    {
                        orders[i] = order;
                        break;
                    }
                }
            }

            // Return a collection which contains only the updated item and any validation errors.
            return Json(new[] { order }.ToDataSourceResult(request, ModelState));
        }

        public ActionResult DestroyOrders([DataSourceRequest] DataSourceRequest request, OrderViewModel order)
        {
            // Delete the item in the data base or follow with the dummy data.
            orders.Remove(order);

            // Return a collection which contains only the destroyed item.
            return Json(new[] { order }.ToDataSourceResult(request));
        }
    }
}