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
    public class DetalleEventosController : Controller
    {
        private readonly SpecialticketContext _context;

        public DetalleEventosController(SpecialticketContext context)
        {
            _context = context;
        }

        public List<DetalleEvento> GetEvents()
        {
            var listaEventos = _context.Eventos
                              .Join(_context.TipoEventos, evnt => evnt.IdTipoEvento, tev => tev.Id, (evnt, tev) => new { evnt, tev })
                              .Join(_context.Escenarios, x => x.evnt.IdTipoEvento, esc => esc.Id, (ev, esc) => new { ev, esc })
                              .Join(_context.TipoEscenarios, y => y.esc.Id, te => te.IdEscenario, (esc, te) => new { esc, te })
                              .Where(e => e.esc.ev.evnt.Fecha > DateTime.Now)
                              .OrderBy(e => e.esc.ev.evnt.Id)
                              .Select(m => new DetalleEvento 
                              {
                                  Id = m.esc.ev.evnt.Id,
                                  Descripcion = m.esc.ev.evnt.Descripcion,
                                  TipoEvento = m.esc.ev.tev.Descripcion,
                                  Fecha = m.esc.ev.evnt.Fecha,
                                  TipoEscenario = m.te.Descripcion,
                                  Escenario = m.esc.esc.Nombre,
                                  Localizacion = m.esc.esc.Localizacion,
                              }).ToList();
            return listaEventos;
        }

        // GET: DetalleEventos
        public IActionResult Index()
        {
            var listaEventos = GetEvents();

            return View(listaEventos);
        }

        public IActionResult Events()
        {
            var listaEventos = GetEvents();

            return View(listaEventos);
        }

        // GET: DetalleEventos/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null || _context.DetalleEvento == null)
            {
                return NotFound();
            }

            var detalleEvento = _context.Eventos
                                .Join(_context.TipoEventos, evnt => evnt.IdTipoEvento, tev => tev.Id, (evnt, tev) => new { evnt, tev })
                                .Join(_context.Escenarios, x => x.evnt.IdTipoEvento, esc => esc.Id, (ev, esc) => new { ev, esc })
                                .Join(_context.TipoEscenarios, y => y.esc.Id, te => te.IdEscenario, (esc, te) => new { esc, te })
                                .Where(e => e.esc.ev.evnt.Id == id)
                                .Select(m => new DetalleEvento
                                {
                                    Id = m.esc.ev.evnt.Id,
                                    Descripcion = m.esc.ev.evnt.Descripcion,
                                    TipoEvento = m.esc.ev.tev.Descripcion,
                                    Fecha = m.esc.ev.evnt.Fecha,
                                    TipoEscenario = m.te.Descripcion,
                                    Escenario = m.esc.esc.Nombre,
                                    Localizacion = m.esc.esc.Localizacion,
                                })
                                .SingleOrDefault();

            if (detalleEvento == null)
            {
                return NotFound();
            }

            return View(detalleEvento);
        }

        public IActionResult DetailsEvents(int? id)
        {
            if (id == null || _context.DetalleEvento == null)
            {
                return NotFound();
            }

            var detalleEvento = _context.Eventos
                                .Join(_context.TipoEventos, evnt => evnt.IdTipoEvento, tev => tev.Id, (evnt, tev) => new { evnt, tev })
                                .Join(_context.Escenarios, x => x.evnt.IdTipoEvento, esc => esc.Id, (ev, esc) => new { ev, esc })
                                .Join(_context.TipoEscenarios, y => y.esc.Id, te => te.IdEscenario, (esc, te) => new { esc, te })
                                .Where(e => e.esc.ev.evnt.Id == id)
                                .Select(m => new DetalleEvento
                                {
                                    Id = m.esc.ev.evnt.Id,
                                    Descripcion = m.esc.ev.evnt.Descripcion,
                                    TipoEvento = m.esc.ev.tev.Descripcion,
                                    Fecha = m.esc.ev.evnt.Fecha,
                                    TipoEscenario = m.te.Descripcion,
                                    Escenario = m.esc.esc.Nombre,
                                    Localizacion = m.esc.esc.Localizacion,
                                })
                                .SingleOrDefault();

            if (detalleEvento == null)
            {
                return NotFound();
            }

            return View(detalleEvento);
        }

        // Asientos
        public IActionResult Seats(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            return RedirectToAction("Index", "DetalleAsientos", new { id = id });
        }

        public IActionResult Tickets(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            return RedirectToAction("Index", "DetalleEntradas", new { id = id });
        }

    }
}
