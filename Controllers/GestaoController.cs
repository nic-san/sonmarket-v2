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
using Microsoft.EntityFrameworkCore;

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
            var categorias = database.Categorias.Where(c => c.Status == true).ToList();
            return View(categorias);
        }
        public IActionResult NovaCategoria()
        {
            return View();
        }
        public IActionResult EditarCategoria(int id)
        {
            var categoria = database.Categorias.First(c => c.Id == id);
            CategoriaDTO categoriaView = new CategoriaDTO();
            categoriaView.Id = categoria.Id;
            categoriaView.Nome = categoria.Nome;
            return View(categoriaView);
        }
        public IActionResult AtivarCategoria()
        {
            var categoria = database.Categorias.Where(c => c.Status == false).ToList();
            return View(categoria);
        }
        #endregion Categoria

        #region Fornecedor
        public IActionResult Fornecedores()
        {
            var fornecedores = database.Fornecedores.Where(f => f.Status == true).ToList();
            return View(fornecedores);
        }
        public IActionResult NovoFornecedor()
        {
            return View();
        }
        public IActionResult EditarFornecedor(int id)
        {
            var fornecedor = database.Fornecedores.First(c => c.Id == id);
            FornecedorDTO fornecedorView = new FornecedorDTO();
            fornecedorView.Id = fornecedor.Id;
            fornecedorView.Nome = fornecedor.Nome;
            fornecedorView.Email = fornecedor.Email;
            fornecedorView.Telefone = fornecedor.Telefone;
            return View(fornecedorView);
        }
        public IActionResult AtivarFornecedor()
        {
            var fornecedores = database.Fornecedores.Where(f => f.Status == false).ToList();
            return View(fornecedores);
        }
        #endregion Fornecedor

        #region Produto
        public IActionResult Produtos()
        {
            var produtos = database.Produtos.Include(cat => cat.Categoria).Include(forn => forn.Fornecedor).Where(p => p.Status == true).ToList();
            return View(produtos);
        }
        public IActionResult EditarProduto(int id)
        {
            ViewBag.Categorias = database.Categorias.Where(c => c.Status == true).ToList();
            ViewBag.Fornecedores = database.Fornecedores.Where(f => f.Status == true).ToList();
            var produto = database.Produtos.First(c => c.Id == id);
            var fornecedor = database.Fornecedores.First(c => c.Id == produto.Categoria.Id);
            var categoria = database.Categorias.First(c => c.Id == produto.Fornecedor.Id);
            ProdutoDTO produtoView = new ProdutoDTO();
            produtoView.Id = produto.Id;
            produtoView.Nome = produto.Nome;
            produtoView.CategoriaID = categoria.Id;
            produtoView.FornecedorID = fornecedor.Id;
            produtoView.PrecoDeCusto = produto.PrecoDeCusto;
            produtoView.PrecoDeVenda = produto.PrecoDeVenda;
            produtoView.Medicao = produto.Medicao;
            return View(produtoView);
        }
        public IActionResult NovoProduto()
        {
            ViewBag.Categorias = database.Categorias.Where(c => c.Status == true).ToList();
            ViewBag.Fornecedores = database.Fornecedores.Where(f => f.Status == true).ToList();
            return View();
        }
        public IActionResult AtivarProduto()
        {
            var produtos = database.Produtos.Where(p => p.Status == false).ToList();
            return View(produtos);
        }
        #endregion Produto

        #region Promoção
        public IActionResult Promocoes()
        {
            var promocao = database.Promocoes.Include(p => p.Produto).Where(p => p.Status == true).ToList();
            return View(promocao);
        }
        public IActionResult NovaPromocao()
        {
            ViewBag.Produtos = database.Produtos.ToList();
            return View();
        }
        public IActionResult EditarPromocao(int id)
        {
            ViewBag.Produtos = database.Produtos.ToList();
            var promocao = database.Promocoes.First(p => p.Id == id);
            var produto = database.Produtos.First(c => c.Id == promocao.Produto.Id);
            PromocaoDTO promocaoView = new PromocaoDTO();
            promocaoView.Id = promocao.Id;
            promocaoView.Nome = promocao.Nome;
            promocaoView.ProdutoID = produto.Id;
            promocaoView.Porcentagem = promocao.Porcentagem;
            return View(promocaoView);
        }
        public IActionResult AtivarPromocao()
        {
            var promocao = database.Promocoes.Include(p => p.Produto).Where(p => p.Status == false).ToList();
            return View(promocao);
        }

        #endregion Promoção

    }
}