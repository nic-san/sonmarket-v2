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
            var produto = database.Produtos.Include(cat => cat.Categoria).Include(forn => forn.Fornecedor).First(c => c.Id == id);
            ProdutoDTO produtoView = new ProdutoDTO();
            produtoView.Id = produto.Id;
            produtoView.Nome = produto.Nome;
            produtoView.CategoriaID = produto.Categoria.Id;
            produtoView.FornecedorID = produto.Fornecedor.Id;
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
            var produtos = database.Produtos.Include(p => p.Categoria).Include(p => p.Fornecedor).Where(p => p.Status == false).ToList();
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
            ViewBag.Produtos = database.Produtos.Where(p => p.Status == true).ToList();
            return View();
        }
        public IActionResult EditarPromocao(int id)
        {
            ViewBag.Produtos = database.Produtos.Where(p => p.Status == true).ToList();
            var promocao = database.Promocoes.Include(p => p.Produto).First(p => p.Id == id);

            PromocaoDTO promocaoView = new PromocaoDTO();
            promocaoView.Id = promocao.Id;
            promocaoView.Nome = promocao.Nome;
            promocaoView.ProdutoID = promocao.Produto.Id;
            promocaoView.Porcentagem = promocao.Porcentagem;
            return View(promocaoView);
        }
        public IActionResult AtivarPromocao()
        {
            var promocao = database.Promocoes.Include(p => p.Produto).Where(p => p.Status == false).ToList();
            return View(promocao);
        }

        #endregion Promoção

        #region Estoque
        public IActionResult Estoques()
        {
            var estoques = database.Estoques.Include(e => e.Produto).Where(e => e.Status == true).ToList();
            return View(estoques);
        }

        public IActionResult NovoEstoque()
        {
            ViewBag.Produtos = database.Produtos.Where(p => p.Status == true).ToList();
            return View();
        }

        public IActionResult EditarEstoque(int id)
        {
            ViewBag.Produtos = database.Produtos.Where(p => p.Status == true).ToList();
            var estoque = database.Estoques.Include(e => e.Produto).First(e => e.Id == id);
            EstoqueDTO estoqueView = new EstoqueDTO();
            estoqueView.Id = estoque.Id;
            estoqueView.Nome = estoque.Nome;
            estoqueView.ProdutoID = estoque.Produto.Id;
            estoqueView.Quantidade = estoque.Quantidade;
            return View(estoqueView);
        }
        public IActionResult AtivarEstoque()
        {
            var estoques = database.Estoques.Include(e => e.Produto).Where(e => e.Status == false).ToList();
            return View(estoques);
        }

        #endregion Estoque
    }
}