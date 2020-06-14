using System.Collections.Generic;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using LearnKendoUiAspMvc.Models;

namespace LearnKendoUiAspMvc.Controllers
{
    public class TreeViewController : Controller
    {
        private readonly NorthwindDbContext _dbContext;

        public TreeViewController()
        {
            _dbContext = new NorthwindDbContext();
        }

        // GET: TreeView
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TreeListRead([DataSourceRequest] DataSourceRequest request, int? parentId)
        {
            var result = (_dbContext.GetAllRemote().ToTreeDataSourceResult(request,
                e => e.EmployeeId,
                e => e.ReportsTo,
                e => id.HasValue ? e.ReportsTo == parentId : e.ReportsTo == null,
                e => e
            );
            
        }

        public JsonResult Index([DataSourceRequest] DataSourceRequest request, int? id)
        {
            var result = ((EmployeeDirectoryService)employeeDirectory).GetAllRemote().ToTreeDataSourceResult(request,
                e => e.EmployeeId,
                e => e.ReportsTo,
                e => id.HasValue ? e.ReportsTo == id : e.ReportsTo == null,
                e => e
            );

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult All([DataSourceRequest] DataSourceRequest request)
        {
            var result = GetDirectory().ToTreeDataSourceResult(request,
                e => e.EmployeeId,
                e => e.ReportsTo,
                e => e
            );

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Destroy([DataSourceRequest] DataSourceRequest request, EmployeeDirectoryModel employee)
        {
            if (ModelState.IsValid)
            {
                employeeDirectory.Delete(employee, ModelState);
            }

            return Json(new[] { employee }.ToTreeDataSourceResult(request, ModelState));
        }

        public JsonResult Create([DataSourceRequest] DataSourceRequest request, EmployeeDirectoryModel employee)
        {
            if (ModelState.IsValid)
            {
                employeeDirectory.Insert(employee, ModelState);
            }

            return Json(new[] { employee }.ToTreeDataSourceResult(request, ModelState));
        }

        public JsonResult Update([DataSourceRequest] DataSourceRequest request, EmployeeDirectoryModel employee)
        {
            if (ModelState.IsValid)
            {
                employeeDirectory.Update(employee, ModelState);
            }

            return Json(new[] { employee }.ToTreeDataSourceResult(request, ModelState));
        }

        private IEnumerable<EmployeeDirectoryModel> GetDirectory()
        {
            return _dbContext.Employees.;
        }

        protected override void Dispose(bool disposing)
        {
            _dbContext.Dispose();

            base.Dispose(disposing);
        }
    }
}