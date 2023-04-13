using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proyecto1SpecialTicket.Models;
using Proyecto1SpecialTicket.Models.Entities;

namespace Proyecto1SpecialTicket.Controllers
{
    public class DetalleEntradasController : Controller
    {
        private readonly SpecialticketContext _context;

        public DetalleEntradasController(SpecialticketContext context)
        {
            _context = context;
        }

        // GET: DetalleEntradas
        public IActionResult Index(int? id)
        {
            var listaEntradas = _context.Entradas
                    .Where(e => e.IdEvento == id)
                    .Select(e => new DetalleEntrada {
                        Id = e.Id,
                        Disponibles = e.Disponibles,
                        TipoAsiento = e.TipoAsiento,
                        Precio = e.Precio
                    }).ToList();

            return View(listaEntradas);
        }

        public IActionResult Buy(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            return RedirectToAction("Create", "Compras", new { id = id });
        }

        // GET: DetalleEntradas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DetalleEntrada == null)
            {
                return NotFound();
            }

            var detalleEntrada = await _context.DetalleEntrada
                .FirstOrDefaultAsync(m => m.Id == id);
            if (detalleEntrada == null)
            {
                return NotFound();
            }

            return View(detalleEntrada);
        }

        // GET: DetalleEntradas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DetalleEntradas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Disponibles,TipoAsiento,Precio")] DetalleEntrada detalleEntrada)
        {
            if (ModelState.IsValid)
            {
                _context.Add(detalleEntrada);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(detalleEntrada);
        }

        // GET: DetalleEntradas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DetalleEntrada == null)
            {
                return NotFound();
            }

            var detalleEntrada = await _context.DetalleEntrada.FindAsync(id);
            if (detalleEntrada == null)
            {
                return NotFound();
            }
            return View(detalleEntrada);
        }

        // POST: DetalleEntradas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Disponibles,TipoAsiento,Precio")] DetalleEntrada detalleEntrada)
        {
            if (id != detalleEntrada.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(detalleEntrada);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetalleEntradaExists(detalleEntrada.Id))
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
            return View(detalleEntrada);
        }

        // GET: DetalleEntradas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DetalleEntrada == null)
            {
                return NotFound();
            }

            var detalleEntrada = await _context.DetalleEntrada
                .FirstOrDefaultAsync(m => m.Id == id);
            if (detalleEntrada == null)
            {
                return NotFound();
            }

            return View(detalleEntrada);
        }

        // POST: DetalleEntradas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DetalleEntrada == null)
            {
                return Problem("Entity set 'SpecialticketContext.DetalleEntrada'  is null.");
            }
            var detalleEntrada = await _context.DetalleEntrada.FindAsync(id);
            if (detalleEntrada != null)
            {
                _context.DetalleEntrada.Remove(detalleEntrada);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DetalleEntradaExists(int id)
        {
          return (_context.DetalleEntrada?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
