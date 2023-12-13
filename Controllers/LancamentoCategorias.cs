using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CashFlowMvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Newtonsoft.Json;

namespace CashFlowMvc.Controllers
{
    public class LancamentoCategoriasController : Controller
    {
        public string uriBase = "http://cashflow.somee.com/CashFlowApi/LancamentoCategorias/";

        [HttpGet("LancamentoCategorias/{id}")]
        public async Task<ActionResult> IndexAsync(int id)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                string token = HttpContext.Session.GetString("SessionTokenUsuario");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = await httpClient.GetAsync(uriBase + id.ToString());
                string serialized = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    List<LancamentoCategoriaViewModel> lista = await Task.Run(() =>
                        JsonConvert.DeserializeObject<List<LancamentoCategoriaViewModel>>(serialized));

                    return View(lista);
                }
                else
                    throw new System.Exception(serialized);
            }
            catch (System.Exception ex)
            {
                TempData["MensagemErro"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpGet("Delete/{categoriaId}/{lancamentoId}")]
        public async Task<ActionResult> DeleteAsync(int categoriaId, int lancamentoId)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                string uriComplementar = "DeleteLancamentoCategoria";
                string token = HttpContext.Session.GetString("SessionTokenUsuario");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                LancamentoCategoriaViewModel lc = new LancamentoCategoriaViewModel();
                lc.CategoriaId = categoriaId;
                lc.LancamentoId = lancamentoId;

                var content = new StringContent(JsonConvert.SerializeObject(lc));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = await httpClient.PostAsync(uriBase + uriComplementar, content);
                string serialized = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    TempData["Mensagem"] = "Categoria removida com sucesso";
                else
                    throw new System.Exception(serialized);


            }
            catch (System.Exception ex)
            {
                TempData["Mensagemrro"] = ex.Message;
            }
            return RedirectToAction("Index", new { Id = lancamentoId });
        }

        [HttpGet]
        public async Task<ActionResult> CreateAsync(int id, string descricao)
        {
            try
            {
                string uriComplementar = "GetHabilidades";
                HttpClient httpClient = new HttpClient();
                string token = HttpContext.Session.GetString("SessionTokenUsuario");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage response = await httpClient.GetAsync(uriBase + uriComplementar);

                string serialized = await response.Content.ReadAsStringAsync();
                List<CategoriaViewModel> categorias = await Task.Run(() =>
                    JsonConvert.DeserializeObject<List<CategoriaViewModel>>(serialized));
                ViewBag.ListaCategorias = categorias;

                LancamentoCategoriaViewModel lc = new LancamentoCategoriaViewModel();
                lc.Lancamento = new LancamentoViewModel();
                lc.Categoria = new CategoriaViewModel();
                lc.LancamentoId = id;
                lc.Lancamento.Descricao = descricao;

                return View(lc);
            }
            catch (System.Exception ex)
            {
                TempData["MensagemErro"] = ex.Message;
                return RedirectToAction("Create", new { id, descricao });
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync(LancamentoCategoriaViewModel lc)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                string token = HttpContext.Session.GetString("SessionTokenUsuario");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var content = new StringContent(JsonConvert.SerializeObject(lc));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = await httpClient.PostAsync(uriBase, content);
                string serialized = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    TempData["Mensagem"] = "Categoria cadastrada com sucesso";
                else
                    throw new System.Exception(serialized);
            }
            catch (System.Exception ex)
            {
                TempData["MensagemErro"] = ex.Message;
            }
            return RedirectToAction("Index", new { id = lc.LancamentoId });
        }
    }
}