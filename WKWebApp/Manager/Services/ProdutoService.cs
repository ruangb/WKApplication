using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WKDomain.Models;
using WKDomain.ModelViews;
using WKWebApp.Manager.Repositories;

namespace WKWebApp.AppService
{
    public class ProdutoService : IProdutoRepository
    {
        public async void DeleteProdutoAsync(int id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync(string.Format("https://localhost:44369/api/produtos/deletar/{0}", id)))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }
        }

        public async Task<Produto> GetProdutoAsync(int id)
        {
            var produto = new Produto();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(string.Format("https://localhost:44369/api/produtos/obter/{0}", id)))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    produto = JsonConvert.DeserializeObject<Produto>(apiResponse);
                }
            }

            return produto;
        }

        public async Task<IEnumerable<Produto>> GetProdutosAsync()
        {
            var produtos = new List<Produto>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44369/api/produtos/obter"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    produtos = JsonConvert.DeserializeObject<List<Produto>>(apiResponse);
                }
            }

            return produtos;
        }

        public async Task<Produto> InsertProdutoAsync(NovoProduto produto)
        {
            var produtoInserido = new Produto();

            using (var httpClient = new HttpClient())
            {
                var content = JsonConvert.SerializeObject(produto);

                var buffer = System.Text.Encoding.UTF8.GetBytes(content);
                var byteContent = new ByteArrayContent(buffer);

                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                using (var response = await httpClient.PostAsync("https://localhost:44369/api/produtos/inserir", byteContent))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    produtoInserido = JsonConvert.DeserializeObject<Produto>(apiResponse);
                }
            }

            return produtoInserido;
        }

        public async Task<Produto> UpdateProdutoAsync(AtualizaProduto produto)
        {
            var produtoAtualizado = new Produto();

            using (var httpClient = new HttpClient())
            {
                var content = JsonConvert.SerializeObject(produto);

                var buffer = System.Text.Encoding.UTF8.GetBytes(content);
                var byteContent = new ByteArrayContent(buffer);

                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                using (var response = await httpClient.PostAsync("https://localhost:44369/api/produtos/atualizar", byteContent))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    produtoAtualizado = JsonConvert.DeserializeObject<Produto>(apiResponse);
                }
            }

            return produtoAtualizado;
        }
    }
}
