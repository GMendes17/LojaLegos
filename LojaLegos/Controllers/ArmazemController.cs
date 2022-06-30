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

namespace LojaLegos.Controllers
{
    [Authorize]
    [Authorize(Roles = "Gestor,Funcionario")]
    public class ArmazemController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ArmazemController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Armazem
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Armazem
                
                .Include(a => a.Responsavel)
                .Include(a => a.Artigos)
                
                ;

           
          
            return View(await applicationDbContext.ToListAsync());
        }


        // GET: Armazem/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Armazem == null)
            {
                return NotFound();
            }

            var armazem = await _context.Armazem
                .Include(a => a.Responsavel)
                .Include(a => a.Artigos)  
                .FirstOrDefaultAsync(m => m.Id == id);
            if (armazem == null)
            {
                return NotFound();
            }

            return View(armazem);
        }

        // GET: Armazem/Create
        public IActionResult Create()
        {
            ViewData["ResponsavelFK"] = new SelectList(_context.Funcionarios, "Id", "Id");
            return View();
        }

        // POST: Armazem/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Local,ResponsavelFK")] Armazem armazem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(armazem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ResponsavelFK"] = new SelectList(_context.Funcionarios, "Id", "Id", armazem.ResponsavelFK);
            return View(armazem);
        }

        // GET: Armazem/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Armazem == null)
            {
                return NotFound();
            }

            var armazem = await _context.Armazem.FindAsync(id);
            if (armazem == null)
            {
                return NotFound();
            }
            ViewData["ResponsavelFK"] = new SelectList(_context.Funcionarios, "Id", "Id", armazem.ResponsavelFK);
            return View(armazem);
        }

        // POST: Armazem/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Local,ResponsavelFK")] Armazem armazem)
        {
            if (id != armazem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(armazem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArmazemExists(armazem.Id))
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
            ViewData["ResponsavelFK"] = new SelectList(_context.Funcionarios, "Id", "Id", armazem.ResponsavelFK);
            return View(armazem);
        }

        // GET: Armazem/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Armazem == null)
            {
                return NotFound();
            }

            var armazem = await _context.Armazem
                .Include(a => a.Responsavel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (armazem == null)
            {
                return NotFound();
            }

            return View(armazem);
        }

        // POST: Armazem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Armazem == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Armazem'  is null.");
            }
            var armazem = await _context.Armazem.FindAsync(id);
            if (armazem != null)
            {
                _context.Armazem.Remove(armazem);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArmazemExists(int id)
        {
          return _context.Armazem.Any(e => e.Id == id);
        }
    }
}
