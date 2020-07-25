using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebLoja.Libraries.Cookie
{
    public class Cookie 
    { 
        private readonly IHttpContextAccessor _context;
        public Cookie(IHttpContextAccessor context) 
        {         
            _context = context;
        }


        public void Criar(string key, string value)
        {
            var options = new CookieOptions()
            {
                Expires = DateTime.Now.AddDays(14),
                Secure = true
            };
            _context.HttpContext.Response.Cookies.Append(key, value, options);
        }

        public void Atualizar(string key, string value)
        {
            Remover(key);
            Criar(key, value);
        }

        public void Remover(string key)
        {
            if (Existe(key))
            {
                _context.HttpContext.Response.Cookies.Delete(key);
            }
        }

        public void RemoverTodas()
        {
            var cookies = _context.HttpContext.Request.Cookies.ToList();
            foreach (var cookie in cookies)
            {
                Remover(cookie.Key);
            }
        }

        public string Consultar(string key)
        {
            return _context.HttpContext.Request.Cookies[key];
        }

        public bool Existe(string key)
        {
            return _context.HttpContext.Request.Cookies[key] != null;
        }
    }
}
