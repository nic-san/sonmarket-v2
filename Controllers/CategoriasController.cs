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
    public class CategoriasController : Controller
    {
        private readonly ApplicationDbContext database;
        public CategoriasController(ApplicationDbContext database)
        {
            this.database = database;
        }

        [HttpPost]
        public IActionResult Salvar(CategoriaDTO categoriaTemporaria)
        {
            if (ModelState.IsValid)
            {
                Categoria categoria = new Categoria();
                categoria.Nome = categoriaTemporaria.Nome;
                categoria.Status = true;
                database.Categorias.Add(categoria);
                database.SaveChanges();
                return RedirectToAction("Categorias", "Gestao");
            }
            else
            {
                return View("../Gestao/NovaCategoria");
            }
        }
        // [HttpPost]
        // public IActionResult Deletar(int id)
        // {
        //     if (id > 0)
        //     {
        //         Categoria categoria = new Categoria();
        //         database.Categorias.Where(c => c.Id == id);
        //         categoria.Status = false;
        //     }

        // }
    }
}