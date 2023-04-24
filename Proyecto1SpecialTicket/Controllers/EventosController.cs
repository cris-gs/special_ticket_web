﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proyecto1SpecialTicket.Areas.Identity.Data;
using Proyecto1SpecialTicket.Models;

namespace Proyecto1SpecialTicket.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class EventosController : Controller
    {
        private readonly specialticketContext _context;
        private readonly UserManager<Proyecto1SpecialTicketUser> _userManager;

        public EventosController(specialticketContext context, UserManager<Proyecto1SpecialTicketUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Eventos
        public async Task<IActionResult> Index()
        {
            var specialticketContext = _context.Eventos.Include(e => e.IdEscenarioNavigation).Include(e => e.IdTipoEventoNavigation);
            return View(await specialticketContext.ToListAsync());
        }

        // GET: Eventos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Eventos == null)
            {
                return NotFound();
            }

            var evento = await _context.Eventos
                .Include(e => e.IdEscenarioNavigation)
                .Include(e => e.IdTipoEventoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (evento == null)
            {
                return NotFound();
            }

            return View(evento);
        }

        // GET: Eventos/Create
        public IActionResult Create()
        {
            ViewData["IdEscenario"] = new SelectList(_context.Escenarios, "Id", "Id");
            ViewData["IdTipoEvento"] = new SelectList(_context.TipoEventos, "Id", "Id");
            return View();
        }

        // POST: Eventos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descripcion,Fecha,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,Active,IdTipoEvento,IdEscenario")] Evento evento)
        {
            var tipoEventoNavigation = await _context.TipoEventos.FindAsync(evento.IdTipoEvento);
            evento.IdTipoEventoNavigation = tipoEventoNavigation;

            var escenarioNavigation = await _context.Escenarios.FindAsync(evento.IdEscenario);
            evento.IdEscenarioNavigation = escenarioNavigation;

            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);
                evento.CreatedBy = userId;
                evento.UpdatedBy = userId;
                _context.Add(evento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdEscenario"] = new SelectList(_context.Escenarios, "Id", "Id", evento.IdEscenario);
            ViewData["IdTipoEvento"] = new SelectList(_context.TipoEventos, "Id", "Id", evento.IdTipoEvento);
            return View(evento);
        }

        // GET: Eventos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Eventos == null)
            {
                return NotFound();
            }

            var evento = await _context.Eventos.FindAsync(id);
            if (evento == null)
            {
                return NotFound();
            }
            ViewData["IdEscenario"] = new SelectList(_context.Escenarios, "Id", "Id", evento.IdEscenario);
            ViewData["IdTipoEvento"] = new SelectList(_context.TipoEventos, "Id", "Id", evento.IdTipoEvento);
            return View(evento);
        }

        // POST: Eventos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descripcion,Fecha,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,Active,IdTipoEvento,IdEscenario")] Evento evento)
        {
            if (id != evento.Id)
            {
                return NotFound();
            }

            var tipoEventoNavigation = await _context.TipoEventos.FindAsync(evento.IdTipoEvento);
            evento.IdTipoEventoNavigation = tipoEventoNavigation;

            var escenarioNavigation = await _context.Escenarios.FindAsync(evento.IdEscenario);
            evento.IdEscenarioNavigation = escenarioNavigation;

            //if (ModelState.IsValid)
            //{
            //}
            try
            {
                var userId = _userManager.GetUserId(User);
                var fechaCreacion = _context.Eventos
                    .Where(te => te.Id == evento.Id)
                    .Select(te => te.CreatedAt)
                    .FirstOrDefault();
                DateTime currentDateTime = DateTime.Now;
                evento.CreatedAt = fechaCreacion;
                evento.UpdatedBy = userId;
                evento.UpdatedAt = currentDateTime;
                _context.Update(evento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventoExists(evento.Id))
                {
                    return NotFound();
                }
                else
                {
                    ViewData["IdEscenario"] = new SelectList(_context.Escenarios, "Id", "Id", evento.IdEscenario);
                    ViewData["IdTipoEvento"] = new SelectList(_context.TipoEventos, "Id", "Id", evento.IdTipoEvento);
                    return View(evento);
                }
            }
        }

        // GET: Eventos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Eventos == null)
            {
                return NotFound();
            }

            var evento = await _context.Eventos
                .Include(e => e.IdEscenarioNavigation)
                .Include(e => e.IdTipoEventoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (evento == null)
            {
                return NotFound();
            }

            return View(evento);
        }

        // POST: Eventos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Eventos == null)
            {
                return Problem("Entity set 'specialticketContext.Eventos'  is null.");
            }
            var evento = await _context.Eventos.FindAsync(id);
            if (evento != null)
            {
                _context.Eventos.Remove(evento);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventoExists(int id)
        {
          return (_context.Eventos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
