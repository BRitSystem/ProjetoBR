﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebAdmin.Controllers
{
    public class LojaCadastroController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
