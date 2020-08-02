using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PagarMe;
using System.Configuration;
using WebLoja.Models;
using WebLoja.Libraries.Helper;
using WebLoja.Libraries.Pagamento;
using Microsoft.Extensions.Configuration;
using ViaCep;

namespace WebLoja.Controllers
{
    public class PagamentoController : Controller
    {
        private readonly IConfiguration _configuration;

        public PagamentoController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult BuscaCep()
        {
            var result = new ViaCepClient().Search("01001000");
            
            return View();
        }

        public IActionResult BoletoBancario()
        {
            GerenciadorPagarMe pagarMe = new GerenciadorPagarMe(_configuration);

            PagarMeService.DefaultApiKey = "ak_live_ZhBo2jlFzXFGHz7qKbNEvWcyw6W2nm";
            PagarMeService.DefaultEncryptionKey = "ek_live_mQAmnLoyo2KmTR9JV9z59P1rQXEXHJ";
            int DaysExpire = 2;

            Transaction transaction = new Transaction();

            transaction.Amount = 151000;
            transaction.PaymentMethod = PaymentMethod.Boleto;
            transaction.BoletoExpirationDate = DateTime.Now.AddDays(DaysExpire);

            transaction.Customer = new Customer
            {
                ExternalId = "#123456789",
                Name = "João das Neves",
                Type = CustomerType.Individual,
                Country = "br",
                Email = "joaoneves@norte.com",
                Documents = new[] {
                        new Document{
                            Type = DocumentType.Cpf,
                            Number = Mascara.Remover("306.211.430-49")
                        }
                    },
                PhoneNumbers = new string[]
                {
                        "+55" + Mascara.Remover( "11999999999" )
                },
                Birthday = "1985-01-01"
            };

            var Today = DateTime.Now;
            var fee = Convert.ToDecimal(1000);

            transaction.Shipping = new Shipping
            {
                Name = "João das Neves",
                Fee = Mascara.ConverterValorPagarMe(fee),
                DeliveryDate = "2017-12-25",
                Expedited = false,
                Address = new Address()
                {
                    Country = "br",
                    State = "SP",
                    City = "São Paulo",
                    Neighborhood = "Vila Carrao",
                    Street = "Rua Lobo",
                    StreetNumber = "999",
                    Zipcode = Mascara.Remover("03424-030")
                }
            };

            //Item[] itens = new Item[1];

            var itemA = new Item()
            {
                Id = "a123",
                Title = "Trono de Ferro",
                Quantity = 1,
                Tangible = true,
                UnitPrice = 120000
            };

            var itemB = new Item()
            {
                Id = "b123",
                Title = "Capa Negra de Inverno",
                Quantity = 1,
                Tangible = true,
                UnitPrice = 30000
            };

            Item[] itens = new Item[2];
            itens[0] = itemA;
            itens[1] = itemB;

            transaction.Item = itens;

            transaction.Save();

            Boleto boleto = new Boleto();
            boleto.BoletoUrl = transaction.BoletoUrl;
            return new RedirectToActionResult("Index", "Finalizacao", boleto);
        }
    }
}
