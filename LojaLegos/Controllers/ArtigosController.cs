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
    public class ArtigosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private object cookieValue;

        public ArtigosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task CoockieCarrinhoCompras(string num)
        {
            //HttpContext.Response.Cookies.Append(key, value);
            //HttpContext.Request.Cookies[key];
            //Response.Cookies.Delete(key)


            if (HttpContext.Request.Cookies["carrinho"] == null)
            {
                HttpContext.Response.Cookies.Append("carrinho", num + "-");
            }
            else
            {
               if(!HttpContext.Request.Cookies["carrinho"].Split("-").Contains(num))
               {
                 HttpContext.Response.Cookies.Append("carrinho", HttpContext.Request.Cookies["carrinho"] + num + "-");
               }

            }
            
        }

        public async Task<JsonResult> ObterDadosCookie()
        {
            var cookie = HttpContext.Request.Cookies["carrinho"];
            return Json(new { cookieValue, cookie });
        }

        public async Task<IActionResult> CarrinhoCompras()
        {

           
            return View();
        }

        //esta função recebe um valor da view e transmite apenas os artigos onde o tipo é igual ao valor recebido
        public async Task<IActionResult> Index(string searchString)
        {
            
            ViewData["CurrentFilter"] = searchString;

            var artigos = from a in _context.Artigos
                          .Include(a => a.Armazem)
                          select a;
                          
            if (!String.IsNullOrEmpty(searchString))
            {
                artigos = artigos.Where(a => a.Tipo.Contains(searchString));
            }

            return View(await artigos.AsNoTracking().ToListAsync());
        }


        




        // GET: Artigos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Artigos == null)
            {
                return NotFound();
            }

            var artigo = await _context.Artigos
                .Include(a => a.Armazem)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (artigo == null)
            {
                return NotFound();
            }

            return View(artigo);
        }

        // GET: Artigos/Create
        public IActionResult Create()
        {
            ViewData["ArmazemFK"] = new SelectList(_context.Armazem, "Id", "Id");
            return View();
        }

        // POST: Artigos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nr,Tipo,Nome,Preco,Foto,NrPecas,Detalhes,Stock,ArmazemFK")] Artigo artigo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(artigo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArmazemFK"] = new SelectList(_context.Armazem, "Id", "Id", artigo.ArmazemFK);
            return View(artigo);
        }

        // GET: Artigos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Artigos == null)
            {
                return NotFound();
            }

            var artigo = await _context.Artigos.FindAsync(id);
            if (artigo == null)
            {
                return NotFound();
            }
            ViewData["ArmazemFK"] = new SelectList(_context.Armazem, "Id", "Id", artigo.ArmazemFK);
            return View(artigo);
        }

        // POST: Artigos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nr,Tipo,Nome,Preco,Foto,NrPecas,Detalhes,Stock,ArmazemFK")] Artigo artigo)
        {
            if (id != artigo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(artigo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArtigoExists(artigo.Id))
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
            ViewData["ArmazemFK"] = new SelectList(_context.Armazem, "Id", "Id", artigo.ArmazemFK);
            return View(artigo);
        }

        // GET: Artigos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Artigos == null)
            {
                return NotFound();
            }

            var artigo = await _context.Artigos
                .Include(a => a.Armazem)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (artigo == null)
            {
                return NotFound();
            }

            return View(artigo);
        }

        // POST: Artigos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Artigos == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Artigos'  is null.");
            }
            var artigo = await _context.Artigos.FindAsync(id);
            if (artigo != null)
            {
                _context.Artigos.Remove(artigo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArtigoExists(int id)
        {
          return _context.Artigos.Any(e => e.Id == id);
        }
    }
}
