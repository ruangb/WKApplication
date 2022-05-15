using Newtonsoft.Json;
using System;
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
        public async Task<Produto> GetAsync(int id)
        {
            var produto = new Produto();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(string.Format("https://localhost:44369/api/produto/get/{0}", id)))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    produto = JsonConvert.DeserializeObject<Produto>(apiResponse);
                }
            }

            return produto;
        }

        public async Task<IEnumerable<Produto>> GetAsync()
        {
            var produtos = new List<Produto>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44369/api/produto/list"))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        produtos = JsonConvert.DeserializeObject<List<Produto>>(apiResponse);
                    }
                }
            }

            return produtos;
        }

        public async Task<Produto> InsertAsync(NovoProduto produto)
        {
            var produtoInserido = new Produto();

            using (var httpClient = new HttpClient())
            {
                var content = JsonConvert.SerializeObject(produto);

                var buffer = System.Text.Encoding.UTF8.GetBytes(content);
                var byteContent = new ByteArrayContent(buffer);

                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                using (var response = await httpClient.PostAsync("https://localhost:44369/api/produto/insert", byteContent))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    produtoInserido = JsonConvert.DeserializeObject<Produto>(apiResponse);
                }
            }

            return produtoInserido;
        }

        public async Task<Produto> UpdateAsync(Produto produto)
        {
            var produtoAtualizado = new Produto();

            using (var httpClient = new HttpClient())
            {
                var content = JsonConvert.SerializeObject(produto);

                var buffer = System.Text.Encoding.UTF8.GetBytes(content);
                var byteContent = new ByteArrayContent(buffer);

                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                try
                {
                    using (var response = await httpClient.PutAsync("https://localhost:44369/api/produto/update", byteContent))
                    {
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            produtoAtualizado = JsonConvert.DeserializeObject<Produto>(apiResponse);
                        }
                        else
                            throw new Exception(response.StatusCode.ToString());
                    }
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }

            return produtoAtualizado;
        }

        public void Delete(int id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = httpClient.DeleteAsync(string.Format("https://localhost:44369/api/produto/delete/{0}", id)))
                {
                    if (response.Result.StatusCode != System.Net.HttpStatusCode.OK)
                        throw new Exception(response.Result.StatusCode.ToString());
                }
            }
        }
    }
}
