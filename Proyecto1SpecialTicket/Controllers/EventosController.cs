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
using Proyecto1SpecialTicket.IdentityData;
using Proyecto1SpecialTicket.BLL.Services.Interfaces;
using Proyecto1SpecialTicket.Models;

namespace Proyecto1SpecialTicket.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class EventosController : Controller
    {
        //private readonly specialticketContext _context;
        private readonly IEventoService _eventoService;
        private readonly IEscenarioService _escenarioService;
        private readonly ITipoEventoService _tipoEventoService;
        private readonly UserManager<Proyecto1SpecialTicketUser> _userManager;

        public EventosController(IEventoService eventoService, IEscenarioService escenarioService, ITipoEventoService tipoEventoService, UserManager<Proyecto1SpecialTicketUser> userManager)
        {
            //_context = context;
            _eventoService = eventoService;
            _escenarioService = escenarioService;
            _tipoEventoService = tipoEventoService;
            _userManager = userManager;
        }

        // GET: Eventos
        public async Task<IActionResult> Index()
        {
            return View(await _eventoService.GetAllEventosAsync());
        }

        // GET: Eventos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var evento = await _eventoService.GetEventoByIdAsync(id);

            if (evento == null) return NotFound();

            return View(evento);
        }

        // GET: Eventos/Create
        public async Task<IActionResult> Create()
        {
            var listaEscenarios = await _escenarioService.GetAllEscenariosAsync();
            ViewData["IdEscenario"] = new SelectList(listaEscenarios, "Id", "Nombre");
            var listaTipoEventos = await _tipoEventoService.GetAllTipoEventosAsync();
            ViewData["IdTipoEvento"] = new SelectList(listaTipoEventos, "Id", "Descripcion");
            return View();
        }

        // POST: Eventos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descripcion,Fecha,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,Active,IdTipoEvento,IdEscenario")] Evento evento)
        {
            var escenarioNavigation = await _escenarioService.GetEscenariosByIdAsync(evento.IdEscenario);
            evento.IdEscenarioNavigation = escenarioNavigation;

            var tipoEventoNavigation = await _tipoEventoService.GetTipoEventoByIdAsync(evento.IdTipoEvento);
            evento.IdTipoEventoNavigation = tipoEventoNavigation;

            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);
                evento.CreatedBy = userId;
                evento.UpdatedBy = userId;
                //_context.Add(evento);
                //await _context.SaveChangesAsync();
                await _eventoService.CreateEventoAsync(evento);
                return RedirectToAction(nameof(Index));
            }
            var listaEscenarios = await _escenarioService.GetAllEscenariosAsync();
            ViewData["IdEscenario"] = new SelectList(listaEscenarios, "Id", "Nombre", evento.IdEscenario);
            var listaTipoEventos = await _tipoEventoService.GetAllTipoEventosAsync();
            ViewData["IdTipoEvento"] = new SelectList(listaTipoEventos, "Id", "Descripcion", evento.IdTipoEvento);
            return View(evento);
        }

        // GET: Eventos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var evento = await _eventoService.GetEventoByIdAsync(id);
            if (evento == null) return NotFound();

            var listaEscenarios = await _escenarioService.GetAllEscenariosAsync();
            ViewData["IdEscenario"] = new SelectList(listaEscenarios, "Id", "Nombre", evento.IdEscenario);
            var listaTipoEventos = await _tipoEventoService.GetAllTipoEventosAsync();
            ViewData["IdTipoEvento"] = new SelectList(listaTipoEventos, "Id", "Descripcion", evento.IdTipoEvento);
            return View(evento);
        }

        // POST: Eventos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descripcion,Fecha,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,Active,IdTipoEvento,IdEscenario")] Evento evento)
        {
            if (id != evento.Id) return NotFound();

            var escenarioNavigation = await _escenarioService.GetEscenariosByIdAsync(evento.IdEscenario);
            evento.IdEscenarioNavigation = escenarioNavigation;

            var tipoEventoNavigation = await _tipoEventoService.GetTipoEventoByIdAsync(evento.IdTipoEvento);
            evento.IdTipoEventoNavigation = tipoEventoNavigation;

            //if (ModelState.IsValid)
            //{
            //}
            try
            {
                //var fechaCreacion = _context.Eventos
                //    .Where(te => te.Id == evento.Id)
                //    .Select(te => te.CreatedAt)
                //    .FirstOrDefault();
                //evento.CreatedAt = fechaCreacion;
                var userId = _userManager.GetUserId(User);
                evento.UpdatedBy = userId;
                await _eventoService.UpdateEventoAsync(evento);
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
                    var listaEscenarios = await _escenarioService.GetAllEscenariosAsync();
                    ViewData["IdEscenario"] = new SelectList(listaEscenarios, "Id", "Nombre", evento.IdEscenario);
                    var listaTipoEventos = await _tipoEventoService.GetAllTipoEventosAsync();
                    ViewData["IdTipoEvento"] = new SelectList(listaTipoEventos, "Id", "Descripcion", evento.IdTipoEvento);
                    return View(evento);
                }
            }
        }

        // GET: Eventos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var evento = await _eventoService.GetEventoByIdAsync(id);

            if (evento == null) return NotFound();

            return View(evento);
        }

        // POST: Eventos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var evento = await _eventoService.GetEventoByIdAsync(id);
            evento.Active = false;

            try
            {
                await _eventoService.UpdateEventoAsync(evento);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventoExists(evento.Id))
                    return NotFound();
                else throw;
            }

            return RedirectToAction(nameof(Index));
        }

        private bool EventoExists(int id)
        {
          return _eventoService.GetEventoByIdAsync(id) == null ? true : false;
        }
    }
}
