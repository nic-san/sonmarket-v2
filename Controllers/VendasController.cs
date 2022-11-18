using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using sonmarket.Data;
using sonmarket.Models;
using sonmarket.DTO;
using Microsoft.EntityFrameworkCore;
public class VendasController : Controller
{
    private readonly ApplicationDbContext database;
    public VendasController(ApplicationDbContext database)
    {
        this.database = database;
    }

    [HttpPost]
    public IActionResult GerarVenda([FromBody] VendaDTO dados)
    {
        //gerar venda
        Venda venda = new Venda();
        venda.Total = dados.total;
        venda.Troco = dados.troco;
        venda.ValorPago = dados.troco <= 0.01f ? dados.total : dados.total + dados.troco;
        venda.Data = DateTime.Now;
        database.Vendas.Add(venda);
        database.SaveChanges();

        //gerar saida
        List<Saida> saidas = new List<Saida>();
        foreach (var saida in dados.produtos)
        {
            Saida s = new Saida();
            s.Quantidade = saida.quantidade;
            s.ValorDaVenda = saida.subtotal;
            s.Venda = venda;
            s.Produto = database.Produtos.First(p => p.Id == saida.produto);
            s.Data = DateTime.Now;
            saidas.Add(s);


        }
        database.Saidas.AddRange(saidas);
        database.SaveChanges();
        foreach (var produto in saidas)
        {
            var saida = database.Saidas.First(s => s.Venda.Id == venda.Id && s.Produto.Id == produto.Produto.Id).Produto.Id;
            var estoque = database.Estoques.First(e => e.Produto.Id == saida);
            var qtd = database.Saidas.First(s => s.Venda.Id == venda.Id && s.Produto.Id == produto.Produto.Id);
            estoque.Quantidade = estoque.Quantidade - qtd.Quantidade;
        }
        database.SaveChanges();
        return Ok(new { msg = "Venda processada com sucesso" });
    }
}