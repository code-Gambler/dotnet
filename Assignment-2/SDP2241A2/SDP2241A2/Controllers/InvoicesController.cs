using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SDP2241A2.Controllers
{
    public class InvoicesController : Controller
    {
        private Manager m = new Manager();

        // GET: Invoices
        public ActionResult Index()
        {
            var i = m.InvoiceGetAll();
            return View(i);
        }

        // GET: Invoices/Details/5
        public ActionResult Details(int id)
        {
            var detailedInvoice = m.InvoiceGetByIdWithDetail(id);
            return View(detailedInvoice);
        }
    }
}
