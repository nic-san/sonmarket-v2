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

    public class EstoquesController : Controller
    {
        private readonly ApplicationDbContext database;
        public EstoquesController(ApplicationDbContext database)
        {
            this.database = database;
        }
        [HttpPost]
        public IActionResult Salvar(EstoqueDTO estoqueTemporario)
        {
            if (ModelState.IsValid)
            {
                Estoque estoque = new Estoque();
                estoque.Nome = estoqueTemporario.Nome;
                estoque.Produto = database.Produtos.First(p => p.Id == estoqueTemporario.ProdutoID);
                estoque.Quantidade = estoqueTemporario.Quantidade;
                estoque.Status = true;
                database.Estoques.Add(estoque);
                database.SaveChanges();
                return RedirectToAction("Estoques", "Gestao");
            }
            else
            {
                ViewBag.Produtos = database.Produtos.Where(p => p.Status == true).ToList();
                return View("../Gestao/NovoEstoque");
            }
        }
        [HttpPost]
        public IActionResult Atualizar(EstoqueDTO estoqueTemporario)
        {
            if (ModelState.IsValid)
            {
                var estoque = database.Estoques.First(e => e.Id == estoqueTemporario.Id);
                estoque.Nome = estoqueTemporario.Nome;
                estoque.Produto = database.Produtos.First(p => p.Id == estoqueTemporario.ProdutoID);
                estoque.Quantidade = estoqueTemporario.Quantidade;
                database.SaveChanges();
                return RedirectToAction("Estoques", "Gestao");
            }
            else
            {
                return View("../Gestao/EditarEstoque");
            }
        }

        [HttpPost]
        public IActionResult Deletar(int id)
        {
            if (id > 0)
            {
                var estoque = database.Estoques.First(c => c.Id == id);
                estoque.Status = false;
                database.SaveChanges();
            }
            return RedirectToAction("Estoques", "Gestao");
        }
        [HttpPost]
        public IActionResult Ativar(int id)
        {
            if (id > 0)
            {
                var estoque = database.Estoques.First(c => c.Id == id);
                estoque.Status = true;
                database.SaveChanges();
            }
            return RedirectToAction("Estoques", "Gestao");
        }
    }
}