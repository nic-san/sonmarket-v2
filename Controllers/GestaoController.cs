using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using sonmarket.Data;
using sonmarket.DTO;
using sonmarket.Models;

namespace sonmarket.Controllers
{
    public class GestaoController : Controller
    {
        private readonly ApplicationDbContext database;
        public GestaoController(ApplicationDbContext database)
        {
            this.database = database;
        }
        public IActionResult Index()
        {
            return View();
        }

        #region Categoria
        public IActionResult Categorias()
        {
            var categorias = database.Categorias.Where(c => c.Status == true);
            return View();
        }
        public IActionResult NovaCategoria()
        {
            return View();
        }
        public IActionResult AtivarCategoria()
        {
            return View();
        }
        #endregion Categoria

        #region Fornecedor
        public IActionResult Fornecedores()
        {
            var fornecedores = database.Fornecedores.Where(f => f.Status == true);
            return View();
        }
        public IActionResult NovoFornecedor()
        {
            return View();
        }
        public IActionResult AtivarFornecedor()
        {
            return View();
        }
        #endregion Fornecedor

        #region Produto
        public IActionResult Produtos()
        {
            var produtos = database.Produtos.Where(p => p.Status == true);
            return View();
        }
        public IActionResult NovoProduto()
        {
            ViewBag.Categorias = database.Categorias.ToList();
            ViewBag.Fornecedores = database.Fornecedores.ToList();
            return View();
        }
        public IActionResult AtivarProduto()
        {
            return View();
        }
        #endregion Produto
    }
}