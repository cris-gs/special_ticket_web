﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proyecto1SpecialTicket.Models;

namespace Proyecto1SpecialTicket.Controllers
{
    public class TipoEventosController : Controller
    {
        private readonly SpecialticketContext _context;

        public TipoEventosController(SpecialticketContext context)
        {
            _context = context;
        }

        // GET: TipoEventos
        public async Task<IActionResult> Index()
        {
              return View(await _context.TipoEventos.ToListAsync());
        }

        // GET: TipoEventos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TipoEventos == null)
            {
                return NotFound();
            }

            var tipoEvento = await _context.TipoEventos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoEvento == null)
            {
                return NotFound();
            }

            return View(tipoEvento);
        }

        // GET: TipoEventos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoEventos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descripcion,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,Active")] TipoEvento tipoEvento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoEvento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoEvento);
        }

        // GET: TipoEventos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TipoEventos == null)
            {
                return NotFound();
            }

            var tipoEvento = await _context.TipoEventos.FindAsync(id);
            if (tipoEvento == null)
            {
                return NotFound();
            }
            return View(tipoEvento);
        }

        // POST: TipoEventos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descripcion,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,Active")] TipoEvento tipoEvento)
        {
            if (id != tipoEvento.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoEvento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoEventoExists(tipoEvento.Id))
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
            return View(tipoEvento);
        }

        // GET: TipoEventos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TipoEventos == null)
            {
                return NotFound();
            }

            var tipoEvento = await _context.TipoEventos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoEvento == null)
            {
                return NotFound();
            }

            return View(tipoEvento);
        }

        // POST: TipoEventos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TipoEventos == null)
            {
                return Problem("Entity set 'SpecialticketContext.TipoEventos'  is null.");
            }
            var tipoEvento = await _context.TipoEventos.FindAsync(id);
            if (tipoEvento != null)
            {
                _context.TipoEventos.Remove(tipoEvento);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoEventoExists(int id)
        {
          return _context.TipoEventos.Any(e => e.Id == id);
        }
    }
}
