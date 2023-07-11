﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LojaLegos.Data;
using LojaLegos.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;

namespace LojaLegos.Controllers
{

    [Authorize]
    [Authorize(Roles = "Gestor")]

    public class GestoresController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public GestoresController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Gestores
        public async Task<IActionResult> Index()
        {
              return View(await _context.Gestor.ToListAsync());
        }

        // GET: Gestores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Gestor == null)
            {
                return NotFound();
            }

            var gestor = await _context.Gestor
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gestor == null)
            {
                return NotFound();
            }

            return View(gestor);
        }

        // GET: Gestores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Gestores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,NrTelemovel,Foto,Email,Cargo")] Gestor gestor, IFormFile foto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gestor);
                gestor.Foto = gestor.Nome + ".jpg";

                string nomeLocalizacaoFicheiro = Path.Combine(_webHostEnvironment.WebRootPath, "Imagens");
                Directory.CreateDirectory(nomeLocalizacaoFicheiro);

                string nomeDaFoto = Path.Combine(nomeLocalizacaoFicheiro, gestor.Foto);
                using var stream = new FileStream(nomeDaFoto, FileMode.Create);
                await foto.CopyToAsync(stream);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gestor);
        }

        // GET: Gestores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Gestor == null)
            {
                return NotFound();
            }

            var gestor = await _context.Gestor.FindAsync(id);
            if (gestor == null)
            {
                return NotFound();
            }
            return View(gestor);
        }

        // POST: Gestores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,NrTelemovel,Foto,Email,Cargo")] Gestor gestor,IFormFile foto)
        {
            if (id != gestor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    gestor.Foto = gestor.Nome + ".jpg";

                    string nomeLocalizacaoFicheiro = Path.Combine(_webHostEnvironment.WebRootPath, "Imagens");
                    Directory.CreateDirectory(nomeLocalizacaoFicheiro);

                    string nomeDaFoto = Path.Combine(nomeLocalizacaoFicheiro, gestor.Foto);
                    using var stream = new FileStream(nomeDaFoto, FileMode.Create);
                    await foto.CopyToAsync(stream);
                    _context.Update(gestor);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GestorExists(gestor.Id))
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
            return View(gestor);
        }

        // GET: Gestores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Gestor == null)
            {
                return NotFound();
            }

            var gestor = await _context.Gestor
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gestor == null)
            {
                return NotFound();
            }

            return View(gestor);
        }

        // POST: Gestores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Gestor == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Gestor'  is null.");
            }
            var gestor = await _context.Gestor.FindAsync(id);
            if (gestor != null)
            {
                _context.Gestor.Remove(gestor);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GestorExists(int id)
        {
          return _context.Gestor.Any(e => e.Id == id);
        }
    }
}
