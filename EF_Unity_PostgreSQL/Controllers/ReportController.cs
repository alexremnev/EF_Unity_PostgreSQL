using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Common.Logging;
using EF_Unity_PostgreSQL.Models.Business;

namespace EF_Unity_PostgreSQL.Controllers
{
    public class ReportController : Controller
    {
        public ReportController(IInvoiceService invoiceService, ICreditMemoService creditMemoService, 
            ISalesReceiptService salesReceiptService, IEstimateService estimateService)
        {
            _services.Add(invoiceService);
            _services.Add(creditMemoService);
            _services.Add(salesReceiptService);
            _services.Add(estimateService);
        }
        private static readonly ILog Log = LogManager.GetLogger<ReportController>();
        private readonly IList<IPersistable> _services = new List<IPersistable>();

        public ActionResult Save()
        {
            try
            {
                SaveData();
                return RedirectToActionPermanent("Index", "Home");
            }
            catch (Exception e)
            {
                Log.Error("Exception occured when you tried to pull entity", e);
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }

        public ActionResult Recalculate()
        {
            try
            {
                CalculateDocuments();
                return RedirectToActionPermanent("Index", "Home");
            }
            catch (Exception e)
            {
                Log.Error("Exception occured when you tried to recalculate sales tax", e);
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }

        private void SaveData()
        {
            foreach (var service in _services)
            {
                service.Save();
            }
        }

        private void CalculateDocuments()
        {
            var tasks = new Task[_services.Count];
            for (var i = 0; i < _services.Count; i++)
            {
                var i1 = i;
                tasks[i] = Task.Factory.StartNew(() => _services[i1].Calculate());
            }
            foreach (var task in tasks)
            {
                task.Wait();
                task.Dispose();
            }
        }
    }
}