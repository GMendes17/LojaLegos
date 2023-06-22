using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LojaLegos.Data;
using LojaLegos.Models;

namespace LojaLegos.Controllers
{
    public class EncomendasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EncomendasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Encomendas
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Encomendas.Include(e => e.Cliente);
            return View(await applicationDbContext.ToListAsync());
        }

        

        // GET: Encomendas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Encomendas == null)
            {
                return NotFound();
            }

            var encomenda = await _context.Encomendas
                .Include(e => e.Cliente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (encomenda == null)
            {
                return NotFound();
            }

            return View(encomenda);
        }

        // GET: Encomendas/Create
        public IActionResult Create()
        {
            ViewData["ClienteFK"] = new SelectList(_context.Clientes, "Id", "Id");
            return View();
        }

        // POST: Encomendas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Total,Data,ClienteFK")] Encomenda encomenda)
        {
            var utilizador = _context.Clientes.FirstOrDefault(u => u.Email == User.Identity.Name);
            encomenda.ClienteFK = utilizador.Id;
            
            encomenda.Data = DateTime.Now;
            System.Diagnostics.Debug.WriteLine(encomenda.ClienteFK);
            
            System.Diagnostics.Debug.WriteLine(encomenda.Total);
            System.Diagnostics.Debug.WriteLine(encomenda.Data);
            if (ModelState.IsValid)
            {
                _context.Add(encomenda);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("tou fdd");
            }
            ViewData["ClienteFK"] = new SelectList(_context.Clientes, "Id", "Id", encomenda.ClienteFK);
            return View(encomenda);
        }

        // GET: Encomendas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Encomendas == null)
            {
                return NotFound();
            }

            var encomenda = await _context.Encomendas.FindAsync(id);
            if (encomenda == null)
            {
                return NotFound();
            }
            ViewData["ClienteFK"] = new SelectList(_context.Clientes, "Id", "Id", encomenda.ClienteFK);
            return View(encomenda);
        }

        // POST: Encomendas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Total,Data,ClienteFK")] Encomenda encomenda)
        {
            if (id != encomenda.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(encomenda);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EncomendaExists(encomenda.Id))
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
            ViewData["ClienteFK"] = new SelectList(_context.Clientes, "Id", "Id", encomenda.ClienteFK);
            return View(encomenda);
        }

        // GET: Encomendas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Encomendas == null)
            {
                return NotFound();
            }

            var encomenda = await _context.Encomendas
                .Include(e => e.Cliente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (encomenda == null)
            {
                return NotFound();
            }

            return View(encomenda);
        }

        // POST: Encomendas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Encomendas == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Encomendas'  is null.");
            }
            var encomenda = await _context.Encomendas.FindAsync(id);
            if (encomenda != null)
            {
                _context.Encomendas.Remove(encomenda);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Pagar()
        {
            var carrinho = HttpContext.Request.Cookies["Carrinho"];

            if (!string.IsNullOrEmpty(carrinho))
            {
                var encomenda = new Encomenda
                {
                    Total = ObterValorTotalEncomenda().ToString(),
                    Data = DateTime.Now
                };

                var clienteId = ((System.Security.Claims.ClaimsIdentity)User.Identity).FindFirst("Id")?.Value;
                var cliente = await _context.Clientes.SingleOrDefaultAsync(c => c.Id == int.Parse(clienteId));

                encomenda.ClienteFK = cliente.Id;
                _context.Encomendas.Add(encomenda);
                await _context.SaveChangesAsync();

                var itens = carrinho.Split('-');
                foreach (var item in itens)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        var artigoEncomenda = new ArtigoEncomenda
                        {
                            ArtigoId = int.Parse(item),
                            EncomendaId = encomenda.Id
                        };
                        _context.ArtigoEncomendas.Add(artigoEncomenda);
                    }
                }

                await _context.SaveChangesAsync();

                HttpContext.Response.Cookies.Delete("Carrinho");

                // Adicione a mensagem de pagamento realizado com sucesso na ViewBag
                ViewBag.MensagemPagamento = "O pagamento foi realizado com sucesso!";
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("CarrinhoVazio", "Artigos");
        }

        public IActionResult Pagamento()
        {
            // Lógica para a página de pagamento
            return View();
        }
        private decimal ObterValorTotalEncomenda()
        {
            // Lógica para calcular o valor total da encomenda

            return 0; // Valor total fictício para exemplo
        }

        private bool EncomendaExists(int id)
        {
          return _context.Encomendas.Any(e => e.Id == id);
        }
    }
}
