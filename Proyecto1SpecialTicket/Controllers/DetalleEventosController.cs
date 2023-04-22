using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proyecto1SpecialTicket.Models;
using Proyecto1SpecialTicket.Models.Entities;

namespace Proyecto1SpecialTicket.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class DetalleEventosController : Controller
    {
        private readonly specialticketContext _context;

        public DetalleEventosController(specialticketContext context)
        {
            _context = context;
        }

        // GET: DetalleEventos
        public IActionResult Index()
        {
              var listaEventos = _context.Eventos
                                .Join(_context.TipoEventos, evnt => evnt.IdTipoEvento, tev => tev.Id, (evnt, tev) => new { evnt, tev })
                                .Join(_context.Escenarios, x => x.evnt.IdTipoEvento, esc => esc.Id, (ev, esc) => new { ev, esc })
                                .Join(_context.TipoEscenarios, y => y.esc.Id, te => te.IdEscenario, (esc, te) => new { esc, te })
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

        // Asientos
        public IActionResult Seats(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            return RedirectToAction("Index", "DetalleAsientos", new { id = id });
        }

    }
}
