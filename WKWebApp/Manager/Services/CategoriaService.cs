﻿using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WKDomain.Models;
using WKDomain.ModelViews;
using WKWebApp.Manager.Repositories;

namespace WKWebApp.AppService
{
    public class CategoriaService : ICategoriaRepository
    {
        public async Task<Categoria> ObterCategoriaAsync(int id)
        {
            var categoria = new Categoria();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(string.Format("https://localhost:44369/api/categorias/obter/{0}", id)))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    categoria = JsonConvert.DeserializeObject<Categoria>(apiResponse);
                }
            }

            return categoria;
        }

        public async Task<IEnumerable<Categoria>> ObterCategoriasAsync()
        {
            var categorias = new List<Categoria>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44369/api/categorias/listar"))
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        categorias = JsonConvert.DeserializeObject<List<Categoria>>(apiResponse);
                    }
                }
            }

            return categorias;
        }

        public async Task<Categoria> InserirCategoriaAsync(NovaCategoria categoria)
        {
            var categoriaInserida = new Categoria();

            using (var httpClient = new HttpClient())
            {
                var content = JsonConvert.SerializeObject(categoria);

                var buffer = System.Text.Encoding.UTF8.GetBytes(content);
                var byteContent = new ByteArrayContent(buffer);

                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                using (var response = await httpClient.PostAsync("https://localhost:44369/api/categorias/inserir", byteContent))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    categoriaInserida = JsonConvert.DeserializeObject<Categoria>(apiResponse);
                }
            }

            return categoriaInserida;
        }

        public async Task<Categoria> AtualizarCategoriaAsync(AtualizaCategoria categoria)
        {
            var categoriaAtualizada = new Categoria();

            using (var httpClient = new HttpClient())
            {
                var content = JsonConvert.SerializeObject(categoria);

                var buffer = System.Text.Encoding.UTF8.GetBytes(content);
                var byteContent = new ByteArrayContent(buffer);

                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                using (var response = await httpClient.PostAsync("https://localhost:44369/api/categorias/atualizar", byteContent))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    categoriaAtualizada = JsonConvert.DeserializeObject<Categoria>(apiResponse);
                }
            }

            return categoriaAtualizada;
        }

        public async void DeletarCategoriaAsync(int id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync(string.Format("https://localhost:44369/api/categorias/deletar/{0}", id)))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }
        }
    }
}