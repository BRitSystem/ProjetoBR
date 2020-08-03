using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using RestSharp.Authenticators;

namespace WebAdmin.Controllers
{
    public class CadastroMembrosController : Controller
    {
        public IActionResult Index()
        {
            var client = new RestClient("https://api.twitter.com/1.1");
            //client.Authenticator = new HttpBasicAuthenticator("username", "password");
            var request = new RestRequest("resource", Method.GET);
            var response = client.Get(request);

            return View();
        }

        [HttpPost]
        public IActionResult SalvarMembro(string nome)
        {

            return View();
        }

    }
}
