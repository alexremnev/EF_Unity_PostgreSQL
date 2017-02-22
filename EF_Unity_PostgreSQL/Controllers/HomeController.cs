using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using EF_Unity_PostgreSQL.Models;
using Microsoft.Practices.Unity;

namespace EF_Unity_PostgreSQL.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(ITaxRateContext context)
        {
            _context = context;
        }

        [Dependency]
        public ITaxRateContext _context { get; set; }
        
        public ActionResult Index()
        {
            var list = _context.TaxRates.ToList();
            var str = new StringBuilder();
            foreach (var taxRate in list)
            {
                str.Append($"Id ={taxRate.Id}, countrySubDivision = {taxRate.CountrySubDivision}, tax ={taxRate.Tax} ");
            }
            return Content(str.ToString());
        }
    }
}

