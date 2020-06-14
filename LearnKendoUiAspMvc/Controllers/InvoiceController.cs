using System;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using LearnKendoUiAspMvc.Models;

namespace LearnKendoUiAspMvc.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly NorthwindDbContext _db = new NorthwindDbContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult InvoicesRead([DataSourceRequest] DataSourceRequest request,
            string salesPerson,
            DateTime statsFrom,
            DateTime statsTo)
        {
            var invoices = _db.Invoices.Where(inv => inv.Salesperson == salesPerson)
                .Where(inv => inv.OrderDate >= statsFrom && inv.OrderDate <= statsTo);

            DataSourceResult result = invoices.ToDataSourceResult(request, invoice => new
            {
                OrderID = invoice.OrderID,
                CustomerName = invoice.CustomerName,
                OrderDate = invoice.OrderDate,
                ProductName = invoice.ProductName,
                UnitPrice = invoice.UnitPrice,
                Quantity = invoice.Quantity,
                Salesperson = invoice.Salesperson
            });

            return Json(result);
        }

        public ActionResult UpdateInvoice([DataSourceRequest] DataSourceRequest request, Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                var invoiceEnt = _db.Invoices.FirstOrDefault(inv => inv.OrderID == invoice.OrderID);

                if (invoiceEnt != null)
                {
                    invoiceEnt.CustomerName = invoice.CustomerName;
                    invoiceEnt.OrderDate = invoice.OrderDate;
                    invoiceEnt.ProductName = invoice.ProductName;
                    invoiceEnt.UnitPrice = invoice.UnitPrice;
                    invoiceEnt.Quantity = invoice.Quantity;
                    invoiceEnt.Salesperson = invoice.Salesperson;

                    _db.SaveChanges();
                }
            }

            return Json(new[] { invoice }.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public ActionResult Excel_Export_Save(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);

            return File(fileContents, contentType, fileName);
        }

        [HttpPost]
        public ActionResult Pdf_Export_Save(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);

            return File(fileContents, contentType, fileName);
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
    }
}
