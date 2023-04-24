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
                return RedirectToAction("Events", "DetalleEventos");
            }

            var specialticketContext = _context.Compras;
            return View(await specialticketContext.ToListAsync());
        }

        // GET: Compras/Details/5
        [Authorize(Roles = "Administrador")]
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
        [Authorize(Roles = "Administrador, Cliente")]
        public IActionResult Create(int id)
        {
            //ViewData["IdCliente"] = new SelectList(_context.Usuarios, "Id", "Id");
            var compra = new Compra { IdEntrada = id };
            return View(compra);
        }

        // POST: Compras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Cliente")]
        public async Task<IActionResult> Create([Bind("Cantidad,FechaReserva,FechaPago,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,Active,IdCliente,IdEntrada")] Compra compra)
        {
            var error = false;
            var userId = _userManager.GetUserId(User);

            var entradaNavigation = await _context.Entradas.FindAsync(compra.IdEntrada);
            compra.IdEntradaNavigation = entradaNavigation;

            // Validación 1: Cantidad de entradas a comprar
            var entrada = await _context.Entradas.FindAsync(compra.IdEntrada);
            if (entrada == null)
            {
                ModelState.AddModelError("IdEntrada", "La entrada no existe.");
                error = true;
            }
            else if (compra.Cantidad > entrada.Disponibles)
            {
                ModelState.AddModelError("Cantidad", "No hay suficientes entradas disponibles para realizar la compra.");
                error = true;
            }

            // Validación 2: Fecha
            //var evento = await _context.Eventos.FindAsync(entrada?.IdEvento);
            //if (compra.FechaPago < DateTime.Now.Date)
            //{
            //    ModelState.AddModelError("FechaPago", "La fecha de pago debe ser una fecha actual o futura.");
            //    error = true;
            //}
            //if (evento != null && DateTime.Now > evento.Fecha)
            //{
            //    ModelState.AddModelError("FechaPago", "El evento ya finalizó.");
            //    error = true;
            //}

            if (error == false)
            {
                compra.CreatedBy = userId;
                compra.UpdatedBy = userId;
                compra.IdCliente = userId;
                _context.Add(compra);
                await _context.SaveChangesAsync();

                if (entrada != null)
                {
                    entrada.Disponibles -= compra.Cantidad;
                    _context.SaveChanges();
                }

                return RedirectToAction(nameof(Index));
            }

            return View(compra);
        }

        // GET: Compras/Edit/5
        [Authorize(Roles = "Administrador")]
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
            ViewData["IdCliente"] = new SelectList(_context.Users, "Id", "Id", compra.IdCliente);
            ViewData["IdEntrada"] = new SelectList(_context.Entradas, "Id", "Id", compra.IdEntradaNavigation);
            return View(compra);
        }

        // POST: Compras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Cantidad,FechaReserva,FechaPago,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,Active,IdCliente,IdEntrada")] Compra compra)
        {
            if (id != compra.Id)
            {
                return NotFound();
            }

            //var idCliente = _context.Compras
            //    .Where(c => c.Id == id)
            //    .Select(c => c.IdCliente)
            //    .FirstOrDefault();

            //var compraPrueba = await _context.Compras
            //.Include(a => a.IdEntradaNavigation)
            //.FirstOrDefaultAsync(a => a.Id == id);

            var entradaNavigation = await _context.Entradas.FindAsync(compra.IdEntrada);
            compra.IdEntradaNavigation = entradaNavigation;

            //compra.IdCliente = idCliente;

            //if (ModelState.IsValid)
            //{
            //}
            try
            {
                var compraAnterior = await _context.Compras.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
                var entrada = await _context.Entradas.FindAsync(compra.IdEntrada);
                var userId = _userManager.GetUserId(User);
                var fechaCreacion = _context.Compras
                    .Where(te => te.Id == compra.Id)
                    .Select(te => te.CreatedAt)
                    .FirstOrDefault();
                DateTime currentDateTime = DateTime.Now;
                compra.CreatedAt = fechaCreacion;
                compra.UpdatedBy = userId;
                compra.UpdatedAt = currentDateTime;
                if (compraAnterior != null && entrada != null)
                {
                    var diferencia = compraAnterior.Cantidad - compra.Cantidad;
                    if (diferencia < entrada.Disponibles)
                    {
                        entrada.Disponibles += diferencia;
                        _context.SaveChanges();
                    }
                    else
                    {
                        ModelState.AddModelError("Cantidad", "Ya no quedan suficientes entradas");
                        return View(compra);
                    }
                }
                else
                {
                    ModelState.AddModelError("Cantidad", "No se pudo editar la entrada");
                    return View(compra);
                }
                _context.Update(compra);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompraExists(compra.Id))
                {
                    return NotFound();
                }
                else
                {
                    ViewData["IdCliente"] = new SelectList(_context.Users, "Id", "Id", compra.IdCliente);
                    ViewData["IdEntrada"] = new SelectList(_context.Entradas, "Id", "Id", compra.IdEntradaNavigation);
                    return View(compra);
                }
            }
        }

        // GET: Compras/Delete/5
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Compras == null)
            {
                return NotFound();
            }

            var compra = await _context.Compras.FirstOrDefaultAsync(c => c.Id == id);
            if (compra == null)
            {
                return NotFound();
            }

            return View(compra);
        }

        // POST: Compras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Compras == null)
            {
                return Problem("Entity set 'SpecialticketContext.Compras'  is null.");
            }
            var compra = await _context.Compras.FindAsync(id);
            if (compra != null)
            {
                var entrada = await _context.Entradas.FindAsync(compra.IdEntrada);
                if (entrada != null)
                {
                    entrada.Disponibles += compra.Cantidad;
                    _context.SaveChanges();
                    _context.Compras.Remove(compra);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

            }

            return Problem("No se pudo borrar la entrada");
        }

        [Authorize(Roles = "Administrador")]
        private bool CompraExists(int id)
        {
          return (_context.Compras?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
