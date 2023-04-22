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
    public class ComprasController : Controller
    {
        private readonly specialticketContext _context;
        private readonly UserManager<Proyecto1SpecialTicketUser> _userManager;

        public ComprasController(specialticketContext context, UserManager<Proyecto1SpecialTicketUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Compras
        public async Task<IActionResult> Index()
        {
            var specialticketContext = _context.Compras.Include(c => c.IdClienteNavigation).Include(c => c.IdEntradaNavigation);
            return View(await specialticketContext.ToListAsync());
        }

        // GET: Compras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Compras == null)
            {
                return NotFound();
            }

            //var compra = await _context.Compras
            //    .include(c => c.idclientenavigation)
            //    .include(c => c.identradanavigation)
            //    .firstordefaultasync(m => m.id == id);
            var compra = await _context.Compras.FirstOrDefaultAsync(c => c.Id == id);
            if (compra == null)
            {
                return NotFound();
            }

            return View(compra);
        }

        // GET: Compras/Create
        public IActionResult Create()
        {
            ViewData["IdCliente"] = new SelectList(_context.Usuarios, "Id", "Id");
            ViewData["IdEntrada"] = new SelectList(_context.Entradas, "Id", "Id");
            return View();
        }

        // POST: Compras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Cantidad,FechaReserva,FechaPago,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,Active,IdCliente,IdEntrada")] Compra compra)
        {
            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);
                compra.CreatedBy = userId;
                compra.UpdatedBy = userId;
                compra.IdCliente = userId;
                _context.Add(compra);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCliente"] = new SelectList(_context.Usuarios, "Id", "Id", compra.IdCliente);
            ViewData["IdEntrada"] = new SelectList(_context.Entradas, "Id", "Id", compra.IdEntrada);
            return View(compra);
        }

        // GET: Compras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Compras == null)
            {
                return NotFound();
            }

            var compra = await _context.Compras.FindAsync(id);
            if (compra == null)
            {
                return NotFound();
            }
            ViewData["IdCliente"] = new SelectList(_context.Usuarios, "Id", "Id", compra.IdCliente);
            ViewData["IdEntrada"] = new SelectList(_context.Entradas, "Id", "Id", compra.IdEntrada);
            return View(compra);
        }

        // POST: Compras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Cantidad,FechaReserva,FechaPago,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,Active,IdCliente,IdEntrada")] Compra compra)
        {
            if (id != compra.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var userId = _userManager.GetUserId(User);
                    var fechaCreacion = _context.Compras
                        .Where(te => te.Id == compra.Id)
                        .Select(te => te.CreatedAt)
                        .FirstOrDefault();
                    DateTime currentDateTime = DateTime.Now;
                    compra.CreatedAt = fechaCreacion;
                    compra.UpdatedBy = userId;
                    compra.UpdatedAt = currentDateTime;
                    _context.Update(compra);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompraExists(compra.Id))
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
            ViewData["IdCliente"] = new SelectList(_context.Usuarios, "Id", "Id", compra.IdCliente);
            ViewData["IdEntrada"] = new SelectList(_context.Entradas, "Id", "Id", compra.IdEntrada);
            return View(compra);
        }

        // GET: Compras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Compras == null)
            {
                return NotFound();
            }

            var compra = await _context.Compras
                .Include(c => c.IdClienteNavigation)
                .Include(c => c.IdEntradaNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (compra == null)
            {
                return NotFound();
            }

            return View(compra);
        }

        // POST: Compras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Compras == null)
            {
                return Problem("Entity set 'specialticketContext.Compras'  is null.");
            }
            var compra = await _context.Compras.FindAsync(id);
            if (compra != null)
            {
                _context.Compras.Remove(compra);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompraExists(int id)
        {
          return (_context.Compras?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
