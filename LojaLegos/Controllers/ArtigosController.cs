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
using System.Data;
using Microsoft.AspNetCore.Hosting;

namespace LojaLegos.Controllers
{
    public class ArtigosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ArtigosController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }


        [HttpPost]
        public async Task CoockieCarrinhoCompras(string num)
        {
            if (HttpContext.Request.Cookies["carrinho"] is null)
            {
                HttpContext.Response.Cookies.Append("carrinho", num + "-", new CookieOptions { Path = "/" });
            }
            else
            {
                if (!HttpContext.Request.Cookies["carrinho"].Split("-").Contains(num))
                {
                    HttpContext.Response.Cookies.Append("carrinho", HttpContext.Request.Cookies["carrinho"] + num + "-", new CookieOptions { Path = "/" });
                }
            }
        }

        [HttpPost]
        public async Task RemoverArtigoCarrinho(int artigoId)
        {
            var cookieValue = HttpContext.Request.Cookies["carrinho"];
            var numeros = cookieValue?.Split('-')
                .Where(s => !string.IsNullOrEmpty(s)) // Remover strings vazias
                .Select(s =>
                {
                    int.TryParse(s, out int num);
                    return num;
                })
                .ToList();

            if (numeros != null && numeros.Contains(artigoId))
            {
                numeros.Remove(artigoId); // Remove o artigoId da lista

                // Cria um novo valor de cookie com os artigos restantes
                var novoCookieValue = string.Join("-", numeros);

                // Define o novo cookie com o caminho "/"
                HttpContext.Response.Cookies.Append("carrinho", novoCookieValue, new CookieOptions { Path = "/" });
            }
        }

        public async Task<JsonResult> ObterDadosCookie()
        {
            var cookie = HttpContext.Request.Cookies["carrinho"];
            return Json(new { cookie });
        }

        private async Task<List<Artigo>> ObterArtigosPorIds(List<int> artigoIds)
        {
            var artigos = await _context.Artigos.Where(a => artigoIds.Contains(a.Id)).ToListAsync();
            return artigos;
        }

        public async Task<IActionResult> CarrinhoCompras()
        {
            var cookieValue = Request.Cookies["carrinho"];
            var numeros = cookieValue?.Split('-')
            .Where(s => !string.IsNullOrEmpty(s)) // Remover strings vazias
            .Select(s =>
            {
             int.TryParse(s, out int num);
             return num;
             })
            .ToList();

            if (numeros != null && numeros.Any())
            {
                var artigos = await ObterArtigosPorIds(numeros);
                return View(artigos);
            }

            return View(new List<Artigo>());
        }

        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;

            var artigos = from a in _context.Artigos.Include(a => a.Armazem)
                          select a;

            if (!String.IsNullOrEmpty(searchString))
            {
                artigos = artigos.Where(a => a.Tipo.Contains(searchString));
            }

            return View(await artigos.AsNoTracking().ToListAsync());
        }

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

        public IActionResult Create()
        {
            ViewData["ArmazemFK"] = new SelectList(_context.Armazem, "Id", "Id");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nr,Tipo,Nome,Preco,Foto,NrPecas,Detalhes,Stock,ArmazemFK")] Artigo artigo, IFormFile foto)
        {

            /**
             recebe os valores da view , o valor da foto que irá ser guardado na bd passa a ser o numero de artigo mais a extensão ".jpg"
             
             */

            if (ModelState.IsValid)
            {
                artigo.Foto = artigo.Nr + ".jpg";
                _context.Add(artigo);

                string nomeLocalizacaoFicheiro = Path.Combine(_webHostEnvironment.WebRootPath, "Imagens");
                Directory.CreateDirectory(nomeLocalizacaoFicheiro); 

                string nomeDaFoto = Path.Combine(nomeLocalizacaoFicheiro, artigo.Foto);
                using var stream = new FileStream(nomeDaFoto, FileMode.Create);
                await foto.CopyToAsync(stream);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["ArmazemFK"] = new SelectList(_context.Armazem, "Id", "Id", artigo.ArmazemFK);
            return View(artigo);
        }

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

        private bool ArtigoExists(int id)
        {
            throw new NotImplementedException();
        }

        [Authorize(Roles = "Gestor , Funcionario")]
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

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Artigos == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Artigos' is null.");
            }
            var artigo = await _context.Artigos.FindAsync(id);
            if (artigo != null)
            {
                _context.Artigos.Remove(artigo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
