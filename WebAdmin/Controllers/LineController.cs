using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebAdmin.Controllers
{
    public class LineController : Controller
    {
        public IActionResult Index()
        {
            var url = "https://app.powerbi.com/home?redirectedFromSignup=1&noSignUpCheck=1&response=AlreadyAssignedLicense";
            ViewBag.url = url;

            return View();
        }

        public class LineChartData
        {
            public DateTime xValue;
            public double yValue;
            public double yValue1;
        }
    }
}
