using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Venda.Data;
using Venda.Models;

namespace Venda.Controllers
{
    public class ItemPedidoController : Controller
    {
        private readonly VendaContext _context;

        public ItemPedidoController(VendaContext context)
        {
            _context = context;
        }


        // GET: ItemPedido
        public async Task<IActionResult> Index()
        {
            var vendaContext = _context.ItemPedidos.Include(i => i.Pedido).Include(i => i.Produto);
            return View(await vendaContext.ToListAsync());
        }

        // GET: ItemPedido/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemPedido = await _context.ItemPedidos
                .Include(i => i.Pedido)
                .Include(i => i.Produto)
                .FirstOrDefaultAsync(m => m.PedidoId == id);
            if (itemPedido == null)
            {
                return NotFound();
            }

            return View(itemPedido);
        }

        // GET: ItemPedido/Create
        public IActionResult Create()
        {
            ViewData["PedidoId"] = new SelectList(_context.Pedidos.Include(l => l.Cliente), "PedidoId", "Cliente.Nome");
            ViewData["ProdutoId"] = new SelectList(_context.Produtos, "ProdutoId", "Nome");
            return View();
        }

        // POST: ItemPedido/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PedidoId,ProdutoId,Quantidade,PrecoUnitario,TotalPedidos")] ItemPedido itemPedido)
        {
            if (ModelState.IsValid)
            {
                var existingItemPedido = await _context.ItemPedidos
                    .FirstOrDefaultAsync(ip => ip.PedidoId == itemPedido.PedidoId && ip.ProdutoId == itemPedido.ProdutoId);

                if (existingItemPedido != null)
                {
                    existingItemPedido.Quantidade += itemPedido.Quantidade;  
                    existingItemPedido.TotalPedidos = existingItemPedido.Quantidade * existingItemPedido.PrecoUnitario;                  }
                else
                {
                    _context.ItemPedidos.Add(itemPedido);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));  
            }
            if (ModelState.IsValid)
            {
                var existingItemPedido = _context.Entry(itemPedido).State == EntityState.Detached
                    ? null
                    : await _context.ItemPedidos
                        .FirstOrDefaultAsync(ip => ip.PedidoId == itemPedido.PedidoId && ip.ProdutoId == itemPedido.ProdutoId);

                if (existingItemPedido != null)
                {
                    _context.Entry(existingItemPedido).State = EntityState.Detached;
                }
                _context.ItemPedidos.Add(itemPedido);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            if (ModelState.IsValid)
            {
                var existingItemPedido = await _context.ItemPedidos.FirstOrDefaultAsync(ip => ip.PedidoId == itemPedido.PedidoId && ip.ProdutoId == itemPedido.ProdutoId);

                if (existingItemPedido != null)
                {
                    existingItemPedido.Quantidade += itemPedido.Quantidade;
                    existingItemPedido.TotalPedidos = existingItemPedido.Quantidade * existingItemPedido.PrecoUnitario;
                    _context.ItemPedidos.Update(existingItemPedido);
                }
                else
                {
                    _context.ItemPedidos.Add(itemPedido);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(itemPedido);
        }    
        

        // GET: ItemPedido/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemPedido = await _context.ItemPedidos.FindAsync(id);
            if (itemPedido == null)
            {
                return NotFound();
            }
            ViewData["PedidoId"] = new SelectList(_context.Pedidos, "PedidoId", "PedidoId", itemPedido.PedidoId);
            ViewData["ProdutoId"] = new SelectList(_context.Produtos, "ProdutoId", "ProdutoId", itemPedido.ProdutoId);
            return View(itemPedido);
        }

        // POST: ItemPedido/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PedidoId,ProdutoId,Quantidade,PrecoUnitario,TotalPedidos")] ItemPedido itemPedido)
        {
            if (id != itemPedido.PedidoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(itemPedido);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemPedidoExists(itemPedido.PedidoId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["PedidoId"] = new SelectList(_context.Pedidos, "PedidoId", "PedidoId", itemPedido.PedidoId);
            ViewData["ProdutoId"] = new SelectList(_context.Produtos, "ProdutoId", "ProdutoId", itemPedido.ProdutoId);
            return View(itemPedido);
        }

        // GET: ItemPedido/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemPedido = await _context.ItemPedidos
                .Include(i => i.Pedido)
                .Include(i => i.Produto)
                .FirstOrDefaultAsync(m => m.PedidoId == id);
            if (itemPedido == null)
            {
                return NotFound();
            }

            return View(itemPedido);
        }

        // POST: ItemPedido/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var itemPedido = await _context.ItemPedidos.FindAsync(id);
            if (itemPedido != null)
            {
                _context.ItemPedidos.Remove(itemPedido);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemPedidoExists(int id)
        {
            return _context.ItemPedidos.Any(e => e.PedidoId == id);
        }
    }
}
