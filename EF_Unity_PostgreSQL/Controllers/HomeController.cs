using System.Web.Mvc;
using EF_Unity_PostgreSQL.Models.Repository;

namespace EF_Unity_PostgreSQL.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(ITaxRateRepository taxRateRepository, IOAuthRepository oAuthRepository)
        {
            _taxRateRepository = taxRateRepository;
            _oAuthRepository = oAuthRepository;
        }

        private readonly ITaxRateRepository _taxRateRepository;
        private readonly IOAuthRepository _oAuthRepository;

        public ActionResult Index()
        {
            _oAuthRepository.Delete();
            var countrySubDivisionCode = _taxRateRepository.Get(3.ToString());
            return Content(countrySubDivisionCode.CountrySubDivisionCode);
        }
    }
}

