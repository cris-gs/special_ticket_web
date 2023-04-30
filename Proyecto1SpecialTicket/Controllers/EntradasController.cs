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
using Proyecto1SpecialTicket.IdentityData;
using Proyecto1SpecialTicket.BLL.Services.Interfaces;
using Proyecto1SpecialTicket.Models;

namespace Proyecto1SpecialTicket.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class EntradasController : Controller
    {
        //private readonly specialticketContext _context;
        private readonly IEntradaService _entradaService;
        private readonly IEventoService _eventoService;
        private readonly UserManager<Proyecto1SpecialTicketUser> _userManager;

        public EntradasController(IEntradaService entradaService, IEventoService eventoService, UserManager<Proyecto1SpecialTicketUser> userManager)
        {
            //_context = context;
            _entradaService = entradaService;
            _eventoService = eventoService;
            _userManager = userManager;
        }

        // GET: Entradas
        public async Task<IActionResult> Index()
        {
            return View(await _entradaService.GetAllEntradasAsync());
        }

        // GET: Entradas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var entrada = await _entradaService.GetEntradaByIdAsync(id);

            if (entrada == null) return NotFound();

            return View(entrada);
        }

        // GET: Eventos
        public IActionResult Events()
        {
            return RedirectToAction("Index", "DetalleEventos");
        }

        // GET: Entradas/Create
        public async Task<IActionResult> Create(int? idAsiento, int? idEvento)
        {
            var entrada = await _entradaService.GetEntradaByEventoAndAsientoAsync(idAsiento, idEvento);

            if (entrada == null) return NotFound();

            return View(entrada);
        }

        // POST: Entradas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Disponibles,TipoAsiento,Precio,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,Active,IdEvento")] Entrada entrada)
        {
            var eventoNavigation = await _eventoService.GetEventoByIdAsync(entrada.IdEvento);
            entrada.IdEventoNavigation = eventoNavigation;

            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);
                entrada.CreatedBy = userId;
                entrada.UpdatedBy = userId;
                try
                {
                    await _entradaService.CreateEntradaAsync(entrada);
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
            if (id == null) return NotFound();

            var entrada = await _entradaService.GetEntradaByIdAsync(id);
            if (entrada == null) return NotFound();

            var listaEventos = await _eventoService.GetAllEventosAsync();
            ViewData["IdEvento"] = new SelectList(listaEventos, "Id", "Descripcion", entrada.IdEvento);
            return View(entrada);
        }

        // POST: Entradas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Disponibles,TipoAsiento,Precio,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,Active,IdEvento")] Entrada entrada)
        {
            if (id != entrada.Id) return NotFound();

            var eventoNavigation = await _eventoService.GetEventoByIdAsync(entrada.IdEvento);
            entrada.IdEventoNavigation = eventoNavigation;

            //if (ModelState.IsValid)
            //{
            //}
            try
            {
                //var fechaCreacion = _context.Entradas
                //    .Where(te => te.Id == entrada.Id)
                //    .Select(te => te.CreatedAt)
                //    .FirstOrDefault();
                //entrada.CreatedAt = fechaCreacion;
                var userId = _userManager.GetUserId(User);
                entrada.UpdatedBy = userId;
                await _entradaService.UpdateEntradaAsync(entrada);
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
                    var listaEventos = await _eventoService.GetAllEventosAsync();
                    ViewData["IdEvento"] = new SelectList(listaEventos, "Id", "Descripcion", entrada.IdEvento);
                    return View(entrada);
                }
            }
        }

        // GET: Entradas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var entrada = await _entradaService.GetEntradaByIdAsync(id);

            if (entrada == null) return NotFound();

            return View(entrada);
        }

        // POST: Entradas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var entrada = await _entradaService.GetEntradaByIdAsync(id);
            entrada.Active = false;

            try
            {
                await _entradaService.UpdateEntradaAsync(entrada);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntradaExists(entrada.Id))
                    return NotFound();
                else throw;
            }

            return RedirectToAction(nameof(Index));
        }

        private bool EntradaExists(int id)
        {
            return _entradaService.GetEntradaByIdAsync(id) == null ? true : false;
        }
    }
}
