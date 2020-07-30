using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebLoja.Libraries.Security;
using WebLoja.Models;

namespace WebLoja.Libraries.Cookie
{
    public class Cookie 
    {
        private IHttpContextAccessor _context;
        private IConfiguration _configuration;

        public Cookie(IHttpContextAccessor context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public void CriaCookie(string key, string value, int? expireTime)
        {
            CookieOptions option = new CookieOptions();

            if (expireTime.HasValue)
                option.Expires = DateTime.Now.AddMinutes(expireTime.Value);
            else
                option.Expires = DateTime.Now.AddDays(7);

            _context.HttpContext.Response.Cookies.Append(key, value, option);
        }


        public void Remover(string key)
        {
            _context.HttpContext.Response.Cookies.Delete(key);
        }


        public List<ProdutoCarrinho> Consultar(string key)
        {
            if (Existe(key))
            {
                string valor = DecriptarCookie(key);
                return JsonConvert.DeserializeObject<List<ProdutoCarrinho>>(valor);
            }
            else
            {
                return new List<ProdutoCarrinho>();
            }
        }

        public string DecriptarCookie(string Key, bool Cript = true)
        {
            var valor = _context.HttpContext.Request.Cookies[Key];

            if (Cript)
            {
                valor = StringCipher.Decrypt(valor, _configuration.GetValue<string>("KeyCrypt"));
            }
            return valor;
        }

        public void Salvar(string key, List<ProdutoCarrinho> Lista)
        {
            string Valor = JsonConvert.SerializeObject(Lista);
            Cadastrar(key, Valor);
        }

        public void Atualizar(ProdutoCarrinho item, string key)
        {
            var Lista = Consultar(key);
            var ItemLocalizado = Lista.SingleOrDefault(a => a.Id == item.Id);

            if (ItemLocalizado != null)
            {
                ItemLocalizado.Quantidade = item.Quantidade;
                Salvar(key, Lista);
            }
        }

        public void Cadastrar(string Key, string Valor)
        {
            CookieOptions Options = new CookieOptions();
            Options.Expires = DateTime.Now.AddDays(7);
            Options.IsEssential = true;

            var ValorCrypt = StringCipher.Encrypt(Valor, _configuration.GetValue<string>("KeyCrypt"));

            _context.HttpContext.Response.Cookies.Append(Key, ValorCrypt, Options);
        }

        public bool Existe(string Key)
        {
            if (_context.HttpContext.Request.Cookies[Key] == null)
            {
                return false;
            }
            return true;
        }

        public void RemoverTodos(string key)
        {
            Remover(key);
        }
    }
}
