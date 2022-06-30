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
    [Authorize(Roles = "Gestor,Gestor")]
    public class FuncionariosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FuncionariosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Funcionarios
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Funcionarios.Include(f => f.Gestor);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Funcionarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Funcionarios == null)
            {
                return NotFound();
            }

            var funcs = await _context.Funcionarios
                .Include(f => f.Gestor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (funcs == null)
            {
                return NotFound();
            }

            return View(funcs);
        }

        // GET: Funcionarios/Create
        public IActionResult Create()
        {
            ViewData["ChefeFK"] = new SelectList(_context.Gestor, "Id", "Id");
            return View();
        }

        // POST: Funcionarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,NrTelemovel,Email,Cargo,ChefeFK")] Funcs funcs)
        {
            if (ModelState.IsValid)
            {
                _context.Add(funcs);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ChefeFK"] = new SelectList(_context.Gestor, "Id", "Id", funcs.ChefeFK);
            return View(funcs);
        }

        // GET: Funcionarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Funcionarios == null)
            {
                return NotFound();
            }

            var funcs = await _context.Funcionarios.FindAsync(id);
            if (funcs == null)
            {
                return NotFound();
            }
            ViewData["ChefeFK"] = new SelectList(_context.Gestor, "Id", "Id", funcs.ChefeFK);
            return View(funcs);
        }

        // POST: Funcionarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,NrTelemovel,Email,Cargo,ChefeFK")] Funcs funcs)
        {
            if (id != funcs.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(funcs);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FuncsExists(funcs.Id))
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
            ViewData["ChefeFK"] = new SelectList(_context.Gestor, "Id", "Id", funcs.ChefeFK);
            return View(funcs);
        }

        // GET: Funcionarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Funcionarios == null)
            {
                return NotFound();
            }

            var funcs = await _context.Funcionarios
                .Include(f => f.Gestor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (funcs == null)
            {
                return NotFound();
            }

            return View(funcs);
        }

        // POST: Funcionarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Funcionarios == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Funcionarios'  is null.");
            }
            var funcs = await _context.Funcionarios.FindAsync(id);
            if (funcs != null)
            {
                _context.Funcionarios.Remove(funcs);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FuncsExists(int id)
        {
          return _context.Funcionarios.Any(e => e.Id == id);
        }
    }
}
