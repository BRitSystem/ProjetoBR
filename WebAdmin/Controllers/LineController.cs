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
