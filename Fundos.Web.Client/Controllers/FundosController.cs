using Fundos.Web.Client.Models;
using Fundos.Web.Client.Services;
using Fundos.Web.Client.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Fundos.Web.Client.Controllers
{
    public class FundosController : Controller
    {
        private readonly ILogger<FundosController> _logger;

        private readonly IAPIService _service;
        public FundosController(ILogger<FundosController> logger, IAPIService service)
        {
            _logger = logger;
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                return View(await _service.GetFundosAsync());
            }
            catch (Exception ex)
            {
                return View("Error", ex.Message);
            }
        }

        public async Task<IActionResult> Create()
        {
            var tiposFundos = await _service.GetTipoFundosAsync();
            ViewBag.TiposFundo = new SelectList(tiposFundos, "Codigo", "Nome", tiposFundos.Select(x => x.Codigo));
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Fundo fundo)
        {
            try
            {
                var tiposFundos = await _service.GetTipoFundosAsync();
                ViewBag.TiposFundo = new SelectList(tiposFundos, "Codigo", "Nome", tiposFundos.Select(x => x.Codigo));

                if (!ModelState.IsValid)
                {
                    return View(fundo);
                }

                string message = await _service.CreateFundo(fundo);

                TempData["Mensagem"] = message;
                return View(fundo);
            }
            catch (Exception ex)
            {
                TempData["Mensagem"] = ex.Message;
                return View(fundo);
            }
        }

        public async Task<IActionResult> Edit(string codigo)
        {
            try
            {

                var fundo = await _service.GetFundoAsync(codigo);

                var tiposFundos = await _service.GetTipoFundosAsync();

                ViewBag.TiposFundo = new SelectList(tiposFundos, "Codigo", "Nome", fundo.Codigo_Tipo);

                if (fundo == null)
                {
                    return NotFound();
                }

                return View(fundo);
            }
            catch (Exception ex)
            {
                return View("Error", ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Fundo fundo)
        {
            try
            {

                var tiposFundos = await _service.GetTipoFundosAsync();
                ViewBag.TiposFundo = new SelectList(tiposFundos, "Codigo", "Nome", fundo.Codigo_Tipo);

                if (!ModelState.IsValid)
                {
                    return View(fundo);
                }

                string message = await _service.UpdateFundoAsync(fundo);

                TempData["Mensagem"] = message;
                return View(fundo);
            }
            catch (Exception ex)
            {
                return View("Error", ex.Message);
            }
        }

        public async Task<IActionResult> Delete(string codigo)
        {
            string message = await _service.DeleteFundoAsync(codigo);
            TempData["Mensagem"] = message;
            return RedirectToAction("Index");
        }

        public async Task<decimal> AlterarPatrimonio(string codigo, decimal valor, int acao)
        {
            var movimentacao = new Movimentacao() { Valor = valor, Acao = acao };
            return await _service.MovimentarPatrimonioAsync(codigo, movimentacao);
        }





    }
}
