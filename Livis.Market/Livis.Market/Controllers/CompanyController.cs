using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Livis.Market.Models.ViewModel;

namespace Livis.Market.Controllers
{
    public class CompanyController : BaseController
    { 
        public IActionResult Index()
        {
            var model = new CompanyViewModel()
            {
                Title = "Company"
            };
            return View(model);
        }

        public IActionResult Map()
        {
            var model = new MapViewModel()
            {
                Title = "Map"
            };
            return View(model);
        }

        public IActionResult ContactUs()
        {
            var model = new ContactUsViewModel()
            {
                Title = "Contact Us"
            };
            return View(model);
        }
    }
}