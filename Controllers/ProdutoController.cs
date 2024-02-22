using Br.Com.Merlot.Aplicacao.Interfaces;
using Br.Com.Merlot.Dominio.Entidades;
using Br.Com.Merlot.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Br.Com.Merlot.Web.Controllers
{
    public class ProdutoController(ITipoProdutoService tipoProdutoService, ILogger<ProdutoController> logger) : Controller
    {
        private readonly ITipoProdutoService _tipoProdutoService = tipoProdutoService;
        private readonly ILogger<ProdutoController> _logger = logger;

        public async Task<IActionResult> Index()
        {
            try
            {
                var produtos = await _tipoProdutoService.Obter();
                var produtosViewModel = produtos
                                        .Select(p => new TipoProdutoViewModel { Id = p.Id, Nome = p.Nome })
                                        .ToList();

                return View(produtosViewModel);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        public IActionResult Adicionar()
        {
            try
            {
                return PartialView("/Views/Produto/PartialViews/Adicionar.cshtml");
            }
            catch (Exception exception)
            {
                return View("Error", new ErrorViewModel { RequestId = exception.Message });
            }

        }

        [HttpPost]
        public async Task<IActionResult> Adicionar(TipoProdutoViewModel produtoViewModel)
        {
            try
            {
                var produto = new TipoProduto { Nome = produtoViewModel.Nome };

                await _tipoProdutoService.Adicionar(produto);

                return RedirectToAction("Index");

            }
            catch (Exception exception)
            {
                return View("Error", new ErrorViewModel { RequestId = exception.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Atualizar(string id)
        {
            try
            {
                var tipoProduto = await _tipoProdutoService.ObterPorId(Convert.ToUInt64(id));
                var tipoProdutoViewModel = new TipoProdutoViewModel { Id = tipoProduto.Id, Nome = tipoProduto.Nome };

                return PartialView("/Views/Produto/PartialViews/Editar.cshtml", tipoProdutoViewModel);

            }
            catch (Exception exception)
            {
                return View("Error", new ErrorViewModel { RequestId = exception.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Atualizar(TipoProdutoViewModel novoTipoProdutoViewModel)
        {
            try
            {
                var novoTipoProduto = new TipoProduto { Id = novoTipoProdutoViewModel.Id, Nome = novoTipoProdutoViewModel.Nome };
                await _tipoProdutoService.Atualizar(novoTipoProduto);

                return RedirectToAction("Index");

            }
            catch (Exception exception)
            {
                return View("Error", new ErrorViewModel { RequestId = exception.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Excluir(string id)
        {
            try
            {
                var tipoProduto = await _tipoProdutoService.ObterPorId(Convert.ToUInt64(id));
                var tipoProdutoViewModel = new TipoProdutoViewModel { Id = tipoProduto.Id, Nome = tipoProduto.Nome };

                return PartialView("/Views/Produto/PartialViews/Excluir.cshtml", tipoProdutoViewModel);

            }
            catch (Exception exception)
            {
                return View("Error", new ErrorViewModel { RequestId = exception.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Excluir(TipoProdutoViewModel tipoProdutoViewModel)
        {
            try
            {
                await _tipoProdutoService.Excluir(tipoProdutoViewModel.Id);
                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                return View("Error", new ErrorViewModel { RequestId = exception.Message });
            }
        }
    }
}
