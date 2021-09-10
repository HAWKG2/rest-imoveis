using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        static string urlBase = "https://613a2a361fcce10017e78dcd.mockapi.io/api/v1";
        static HttpClient client;

        // A função ListarImoveis() faz uso de um IEnumerable que irá servir como um intermediário entre as informações da API e o usuário,
        public ActionResult ListarImoveis()
        {
            IEnumerable<Imoveis> infoImoveis = null;

            using (client)
            {
                client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Realizando uma requisição (GET) que é enviada ao endereço da API na rota passada como argumento
                var resp = client.GetAsync("/imoveis");
                resp.Wait();
                var result = resp.Result;

                if (result.IsSuccessStatusCode)
                {
                    var conteudo = result.Content.ReadAsAsync<IList<Imoveis>>();
                    conteudo.Wait();
                    // Conversão de json usada na lista de imóveis para converter os dados json para o tipo especificado (no meu caso uma lista do tipo imoveis), onde a variavel conteudo
                    // carrega os dados buscados
                    infoImoveis = conteudo.Result;
                }
                else
                {
                    infoImoveis = Enumerable.Empty<Imoveis>();
                    ModelState.AddModelError(string.Empty, "Erro");
                }

                return View(infoImoveis);
            }
        }

        //Exibe imóveis cadastrados
        [HttpGet]
        public ActionResult CadastrarImovel()
        {
            return View();
        }

        // Função usada para cadastrar novos imóveis na API para serem listados subsequentemente
        [HttpPost]
        public ActionResult CadastrarImovel(Imoveis argImovel)
        {
            client = new HttpClient();
            using (client)
            {

                client.BaseAddress = new Uri(urlBase);

                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Variável post usada para postar as informações do objeto argImovel como json na rota especificada da API
                var post = client.PostAsJsonAsync<Imoveis>("/imoveis", argImovel);
                post.Wait();
                var result = post.Result;

                // Caso o post obtenha sucesso, a página irá retornar para a listagem de imóveis
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("ListarImoveis");
                }
            }
            ModelState.AddModelError(string.Empty, "Erro");

            return View(argImovel);
        }
    }
}