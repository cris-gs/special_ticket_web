using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using Proyecto1SpecialTicket.Areas.Identity.Data;
using Proyecto1SpecialTicket.Models;

namespace Proyecto1SpecialTicket.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class EntradasController : Controller
    {
        private readonly specialticketContext _context;
        private readonly UserManager<Proyecto1SpecialTicketUser> _userManager;

        public EntradasController(specialticketContext context, UserManager<Proyecto1SpecialTicketUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Entradas
        public async Task<IActionResult> Index()
        {
            var specialticketContext = _context.Entradas.Include(e => e.IdEventoNavigation);
            return View(await specialticketContext.ToListAsync());
        }

        // GET: Entradas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Entradas == null)
            {
                return NotFound();
            }

            var entrada = await _context.Entradas
                .Include(e => e.IdEventoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (entrada == null)
            {
                return NotFound();
            }

            return View(entrada);
        }

        // GET: Eventos
        public IActionResult Events()
        {
            return RedirectToAction("Index", "DetalleEventos");
        }

        // GET: Entradas/Create
        public IActionResult Create(int? id, int? idE)
        {
            var entrada = _context.Eventos
                              .Join(_context.TipoEventos, e => e.IdTipoEvento, te => te.Id, (e, te) => new { Evento = e, TipoEvento = te })
                              .Join(_context.Escenarios, ev => ev.Evento.IdEscenario, esc => esc.Id, (ev, esc) => new { ev, Escenario = esc })
                              .Join(_context.Asientos, es => es.Escenario.Id, a => a.IdEscenario, (es, a) => new { es, Asiento = a })
                              .Where(x => x.es.ev.Evento.Active && x.Asiento.Id == id && x.es.ev.Evento.Id == idE)
                              .Select(x => new Entrada
                              {
                                  IdEvento = x.es.ev.Evento.Id,
                                  TipoAsiento = x.Asiento.Descripcion,
                                  Disponibles = x.Asiento.Cantidad
                              })
                              .FirstOrDefault();

            if (entrada == null)
            {
                return NotFound();
            }

            return View(entrada);
        }

        // POST: Entradas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Disponibles,TipoAsiento,Precio,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,Active,IdEvento")] Entrada entrada)
        {
            var eventoNavigation = await _context.Eventos.FindAsync(entrada.IdEvento);
            entrada.IdEventoNavigation = eventoNavigation;

            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);
                entrada.CreatedBy = userId;
                entrada.UpdatedBy = userId;
                _context.Add(entrada);
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    if (ex.InnerException is MySqlException mySqlEx && mySqlEx.Number == 1062) // 1062 es el número de error de MySQL para entradas duplicadas
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe una entrada con este tipo de asiento para este evento.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Ocurrió un error al guardar la entrada.");
                    }
                }
            }
            return View(entrada);
        }

        // GET: Entradas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Entradas == null)
            {
                return NotFound();
            }

            var entrada = await _context.Entradas.FindAsync(id);
            if (entrada == null)
            {
                return NotFound();
            }
            ViewData["IdEvento"] = new SelectList(_context.Eventos, "Id", "Id", entrada.IdEventoNavigation);
            return View(entrada);
        }

        // POST: Entradas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Disponibles,TipoAsiento,Precio,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,Active,IdEvento")] Entrada entrada)
        {
            if (id != entrada.Id)
            {
                return NotFound();
            }

            var eventoNavigation = await _context.Eventos.FindAsync(entrada.IdEvento);
            entrada.IdEventoNavigation = eventoNavigation;

            //if (ModelState.IsValid)
            //{
            //}
            try
            {
                var userId = _userManager.GetUserId(User);
                var fechaCreacion = _context.Entradas
                    .Where(te => te.Id == entrada.Id)
                    .Select(te => te.CreatedAt)
                    .FirstOrDefault();
                DateTime currentDateTime = DateTime.Now;
                entrada.CreatedAt = fechaCreacion;
                entrada.UpdatedBy = userId;
                entrada.UpdatedAt = currentDateTime;
                _context.Update(entrada);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntradaExists(entrada.Id))
                {
                    return NotFound();
                }
                else
                {
                    ViewData["IdEvento"] = new SelectList(_context.Eventos, "Id", "Id", entrada.IdEventoNavigation);
                    return View(entrada);
                }
            }
        }

        // GET: Entradas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Entradas == null)
            {
                return NotFound();
            }

            var entrada = await _context.Entradas
                .Include(e => e.IdEventoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (entrada == null)
            {
                return NotFound();
            }

            return View(entrada);
        }

        // POST: Entradas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Entradas == null)
            {
                return Problem("Entity set 'specialticketContext.Entradas'  is null.");
            }
            var entrada = await _context.Entradas.FindAsync(id);
            if (entrada != null)
            {
                _context.Entradas.Remove(entrada);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EntradaExists(int id)
        {
            return (_context.Entradas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
