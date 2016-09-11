using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using gerenciamentoCarrinhoService.Model;

namespace gerenciamentoCarrinhoService.Controllers
{
    [Route("api/[controller]")]
    public class CarrinhosController : Controller
    {
        private static string BASE_AUTHENTICATION_URL = "http://localhost:9001/";
        private static IDictionary<int, Carrinho> carrinhos = new Dictionary<int, Carrinho>();

        static CarrinhosController() {
            Console.WriteLine("Inicializando Mocks");
            carrinhos.Add(1, new Carrinho(1, new List<Livro>()));
            Carrinho c = new Carrinho(2, new List<Livro>());
            carrinhos.Add(2, c);
        }

        // PUT api/carrinhos/{id}/livro}
        [HttpPut("{id}/livro/{idLivro}")]
        public IActionResult adicionarLivro(int id, int idLivro)
        {
            if (!isAuthenticated().Result) {
                // return Forbid(); TODO: Verificar: retorna erro no servidor
                return Unauthorized();
            }

            Carrinho c = null;
            carrinhos.TryGetValue(id, out c);
            if (c == null) {
                c = new Carrinho(id, new List<Livro>());
                carrinhos.Add(id, c);
            }
            Livro livro = findLivroById(idLivro).Result;
            if (!c.Livros.Contains(livro)) {
                c.Livros.Add(livro);
            }
            return Ok(livro);
        }

        // DELETE api/carrinhos/{id}
        [HttpDelete("{id}")]
        public IActionResult EsvaziarCarrinho(int id)
        {
            if (!isAuthenticated().Result) {
                // return Forbid(); TODO: Verificar: retorna erro no servidor
                return Unauthorized();
            }

            Carrinho c = null;
            carrinhos.TryGetValue(id, out c);
            if (c != null) {
                c.Livros = new List<Livro>();
                return Ok();
            }
            return NotFound("NotFound");
        }
        
        // GET api/carrinhos/{id}
        [HttpGet("{id}")]
        public IActionResult ConsultarLivrosCarrinho(int id)
        {
            if (!isAuthenticated().Result) {
                // return Forbid(); TODO: Verificar: retorna erro no servidor
                return Unauthorized();
            }
            
            Carrinho c = null;
            carrinhos.TryGetValue(id, out c);
            if (c != null) {
                return Ok(c);
            }
            return NotFound("NotFound");
        }

        private async Task<bool> isAuthenticated() {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BASE_AUTHENTICATION_URL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response =  client.GetAsync("api/isauthenticated").Result;
                string result = await response.Content.ReadAsStringAsync();

                return result.Equals("true");
            }
        }

        async Task<Livro> findLivroById(int id) {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5000/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response =  client.GetAsync("api/livros/"+id).Result;

                if(response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Livro>(result);
                }   
                return null;
                
            }
        }
    }
}
