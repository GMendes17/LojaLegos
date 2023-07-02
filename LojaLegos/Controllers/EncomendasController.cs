using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LojaLegos.Data;
using LojaLegos.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.CodeAnalysis.Differencing;
using System.Security.Cryptography;

namespace LojaLegos.Controllers
{
    public class EncomendasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EncomendasController(ApplicationDbContext context)
        {
            _context = context;
        }




        [HttpPost]
        public ActionResult SetArtigosQuantidades(string artigosQuantidades)
        {
            TempData["ArtigosQuantidades"] = artigosQuantidades;
            
            Console.WriteLine(artigosQuantidades);
            return Json(new { success = true });
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
                .Include(e => e.ArtigoEncomendas)
                    .ThenInclude(ae => ae.Artigo)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (encomenda == null)
            {
                return NotFound();
            }

            return View(encomenda);
        }

        //public IActionResult Create()
        //{
        //    ViewData["ClienteFK"] = new SelectList(_context.Clientes, "Id", "Id", "ClienteFK");

        //    return View();
        //}

        // GET: Encomendas/Create

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,Total,Data,ClienteFK")] Encomenda encomenda,string ArtQuant)
        {
            var utilizador = await _context.Clientes.FirstOrDefaultAsync(u => u.Email == User.Identity.Name);
            if (utilizador == null)
            {
                // Lidar com o caso em que o utilizador não foi encontrado
                return NotFound();
            }

            ViewData["apelido"] = utilizador.Apelido;
            ViewData["CodPostal"] = utilizador.CodPostal;
            ViewData["Nrtelemovel"] = utilizador.NrTelemovel;
            ViewData["nome"] = utilizador.PrimeiroNome;
            ViewData["NrContri"] = utilizador.NrContribuinte;
            ViewData["morada"] = utilizador.Morada;
            ViewData["email"] = utilizador.Email;

            encomenda.ClienteFK = utilizador.Id;
            encomenda.Data = DateTime.Now;
            Console.WriteLine("teste: {0}", ArtQuant);


            var artigosQuantidades = ArtQuant;

            if (ModelState.IsValid)
            {
                _context.Add(encomenda);
                await _context.SaveChangesAsync();

                // Obter o ID da encomenda recém-criada
                var encomendaId = encomenda.Id;

                // Processar a string artigosQuantidades e criar as entradas na tabela EncomendasArtigos
                
                

                if (!string.IsNullOrEmpty(artigosQuantidades))
                {
                    string[] artigosQuantidadesArray = artigosQuantidades.Split('|');

                    foreach (var item in artigosQuantidadesArray)
                    {
                        var artigoQuantidade = item.Split(';');
                        var artigoId = artigoQuantidade[0];
                        var quantidade = int.Parse(artigoQuantidade[1]);

                        var encomendaArtigo = new ArtigoEncomenda
                        {
                            EncomendaId = encomendaId,
                            ArtigoId = int.Parse(artigoId),
                            Quantidade = quantidade
                        };

                        _context.ArtigoEncomendas.Add(encomendaArtigo);
                    }

                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Artigos");
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["ClienteFK"] = new SelectList(_context.Clientes, "Id", "Id", encomenda.ClienteFK);
            return View(encomenda);
        }


        // GET: Encomendas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var encomenda = await _context.Encomendas
                .Include(e => e.Cliente)
                .Include(e => e.ArtigoEncomendas)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (encomenda == null)
            {
                return NotFound();
            }

            ViewData["ClienteFK"] = new SelectList(_context.Clientes, "Id", "Id", encomenda.ClienteFK);
            return View(encomenda);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Encomenda encomenda)
        {
            if (id != encomenda.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingEncomenda = await _context.Encomendas
                        .Include(e => e.Cliente)
                        .Include(e => e.ArtigoEncomendas)
                        .FirstOrDefaultAsync(m => m.Id == id);

                    if (existingEncomenda == null)
                    {
                        return NotFound();
                    }

                    existingEncomenda.Total = encomenda.Total;
                    existingEncomenda.Data = encomenda.Data;
                    existingEncomenda.ClienteFK = encomenda.ClienteFK;

                    // Limpar a lista existente de ArtigoEncomendas
                    existingEncomenda.ArtigoEncomendas.Clear();

                    // Adicionar os itens de encomenda atualizados
                    foreach (var updatedArtigoEncomenda in encomenda.ArtigoEncomendas)
                    {
                        var artigoEncomenda = new ArtigoEncomenda
                        {
                            Quantidade = updatedArtigoEncomenda.Quantidade,
                            ArtigoId = updatedArtigoEncomenda.ArtigoId
                        };
                        existingEncomenda.ArtigoEncomendas.Add(artigoEncomenda);
                    }

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


        private bool EncomendaExists(int id)
        {
          return _context.Encomendas.Any(e => e.Id == id);
        }
    }
}
