using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proyecto1SpecialTicket.Models;

namespace Proyecto1SpecialTicket.Controllers
{
    [Authorize]
    public class EscenariosController : Controller
    {
        private readonly SpecialticketContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public EscenariosController(SpecialticketContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Escenarios
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);

            var query = from ur in _context.UserRoles
                        join r in _context.Roles
                        on ur.RoleId equals r.Id
                        select new
                        {
                            Id = ur.UserId,
                            NameRole = r.Name,
                        };
            bool tienePermiso = false;
            foreach (var resultado in query)
            {
                if (userId == resultado.Id && resultado.NameRole == "Administrador")
                {
                    tienePermiso = true;
                }
            }
            if (!tienePermiso)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(await _context.Escenarios.ToListAsync());
        }

        // GET: Escenarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Escenarios == null)
            {
                return NotFound();
            }

            var escenario = await _context.Escenarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (escenario == null)
            {
                return NotFound();
            }

            return View(escenario);
        }

        // GET: Escenarios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Escenarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Localizacion,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,Active")] Escenario escenario)
        {
            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);
                escenario.CreatedBy = userId;
                escenario.UpdatedBy = userId;
                _context.Add(escenario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(escenario);
        }

        // GET: Escenarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Escenarios == null)
            {
                return NotFound();
            }

            var escenario = await _context.Escenarios.FindAsync(id);
            if (escenario == null)
            {
                return NotFound();
            }
            return View(escenario);
        }

        // POST: Escenarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Localizacion,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,Active")] Escenario escenario)
        {
            if (id != escenario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var userId = _userManager.GetUserId(User);
                    var fechaCreacion = _context.Escenarios
                        .Where(te => te.Id == escenario.Id)
                        .Select(te => te.CreatedAt)
                        .FirstOrDefault();
                    DateTime currentDateTime = DateTime.Now;
                    escenario.CreatedAt = fechaCreacion;
                    escenario.UpdatedBy = userId;
                    escenario.UpdatedAt = currentDateTime;
                    _context.Update(escenario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EscenarioExists(escenario.Id))
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
            return View(escenario);
        }

        // GET: Escenarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Escenarios == null)
            {
                return NotFound();
            }

            var escenario = await _context.Escenarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (escenario == null)
            {
                return NotFound();
            }

            return View(escenario);
        }

        // POST: Escenarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Escenarios == null)
            {
                return Problem("Entity set 'SpecialticketContext.Escenarios'  is null.");
            }
            var escenario = await _context.Escenarios.FindAsync(id);
            if (escenario != null)
            {
                _context.Escenarios.Remove(escenario);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EscenarioExists(int id)
        {
          return _context.Escenarios.Any(e => e.Id == id);
        }
    }
}
