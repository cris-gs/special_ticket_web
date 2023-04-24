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
using Proyecto1SpecialTicket.Areas.Identity.Data;
using Proyecto1SpecialTicket.Models;

namespace Proyecto1SpecialTicket.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class TipoEscenariosController : Controller
    {
        private readonly specialticketContext _context;
        private readonly UserManager<Proyecto1SpecialTicketUser> _userManager;

        public TipoEscenariosController(specialticketContext context, UserManager<Proyecto1SpecialTicketUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: TipoEscenarios
        public async Task<IActionResult> Index()
        {
            var specialticketContext = _context.TipoEscenarios.Include(t => t.IdEscenarioNavigation);
            return View(await specialticketContext.ToListAsync());
        }

        // GET: TipoEscenarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TipoEscenarios == null)
            {
                return NotFound();
            }

            var tipoEscenario = await _context.TipoEscenarios
                .Include(t => t.IdEscenarioNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoEscenario == null)
            {
                return NotFound();
            }

            return View(tipoEscenario);
        }

        // GET: TipoEscenarios/Create
        public IActionResult Create()
        {
            ViewData["IdEscenario"] = new SelectList(_context.Escenarios, "Id", "Id");
            return View();
        }

        // POST: TipoEscenarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descripcion,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,Active,IdEscenario")] TipoEscenario tipoEscenario)
        {
            var eventoNavigation = await _context.Escenarios.FindAsync(tipoEscenario.IdEscenario);
            tipoEscenario.IdEscenarioNavigation = eventoNavigation;

            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);
                tipoEscenario.CreatedBy = userId;
                tipoEscenario.UpdatedBy = userId;
                _context.Add(tipoEscenario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdEscenario"] = new SelectList(_context.Escenarios, "Id", "Id", tipoEscenario.IdEscenario);
            return View(tipoEscenario);
        }

        // GET: TipoEscenarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TipoEscenarios == null)
            {
                return NotFound();
            }

            var tipoEscenario = await _context.TipoEscenarios.FindAsync(id);
            if (tipoEscenario == null)
            {
                return NotFound();
            }
            ViewData["IdEscenario"] = new SelectList(_context.Escenarios, "Id", "Id", tipoEscenario.IdEscenario);
            return View(tipoEscenario);
        }

        // POST: TipoEscenarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descripcion,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,Active,IdEscenario")] TipoEscenario tipoEscenario)
        {
            if (id != tipoEscenario.Id)
            {
                return NotFound();
            }

            var eventoNavigation = await _context.Escenarios.FindAsync(tipoEscenario.IdEscenario);
            tipoEscenario.IdEscenarioNavigation = eventoNavigation;

            //if (ModelState.IsValid)
            //{
            //}
            try
            {
                var userId = _userManager.GetUserId(User);
                var fechaCreacion = _context.TipoEscenarios
                    .Where(te => te.Id == tipoEscenario.Id)
                    .Select(te => te.CreatedAt)
                    .FirstOrDefault();
                DateTime currentDateTime = DateTime.Now;
                tipoEscenario.CreatedAt = fechaCreacion;
                tipoEscenario.UpdatedBy = userId;
                tipoEscenario.UpdatedAt = currentDateTime;
                _context.Update(tipoEscenario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoEscenarioExists(tipoEscenario.Id))
                {
                    return NotFound();
                }
                else
                {
                    ViewData["IdEscenario"] = new SelectList(_context.Escenarios, "Id", "Id", tipoEscenario.IdEscenario);
                    return View(tipoEscenario);
                }
            }
            
            
        }

        // GET: TipoEscenarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TipoEscenarios == null)
            {
                return NotFound();
            }

            var tipoEscenario = await _context.TipoEscenarios
                .Include(t => t.IdEscenarioNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoEscenario == null)
            {
                return NotFound();
            }

            return View(tipoEscenario);
        }

        // POST: TipoEscenarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TipoEscenarios == null)
            {
                return Problem("Entity set 'specialticketContext.TipoEscenarios'  is null.");
            }
            var tipoEscenario = await _context.TipoEscenarios.FindAsync(id);
            if (tipoEscenario != null)
            {
                _context.TipoEscenarios.Remove(tipoEscenario);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoEscenarioExists(int id)
        {
          return (_context.TipoEscenarios?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
