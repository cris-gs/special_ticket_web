using Proyecto1SpecialTicket.DAL.Repositories.Interfaces;
using Proyecto1SpecialTicket.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using Proyecto1SpecialTicket.DAL.DataContext;
using System.Linq;
using Proyecto1SpecialTicket.Models.Entities;

namespace Proyecto1SpecialTicket.DAL.Repositories.Implementations
{
    /// <summary>
    /// Repository for Evento
    /// </summary>
    public class EventoRepository : GenericRepository<Evento>, IEventoRepository
    {
        /// <summary>
        /// Constructor of EventoRepository
        /// </summary>
        /// <param name="specialticketContext"></param>
        public EventoRepository(specialticketContext _context) : base(_context)
        {   
        }

        public async Task<IEnumerable<Evento>> GetAllEventosAsync()
        {
            var listaEventos = await _context.Eventos
                                             .Include(e => e.IdEscenarioNavigation)
                                             .Include(e => e.IdTipoEventoNavigation)
                                             .Where(e => e.Active)
                                             .ToListAsync();
            return listaEventos;
        }

        public async Task<Evento> GetEventoByIdAsync(int? id)
        {
            var eventos = await _context.Eventos
                                       .Include(e => e.IdEscenarioNavigation)
                                       .Include(e => e.IdTipoEventoNavigation)
                                       .Where(e => e.Active)
                                       .FirstOrDefaultAsync(m => m.Id == id);
            return eventos;
        }

        public async Task<IEnumerable<DetalleEvento>> GetDetalleEventosAsync()
        {
            var now = DateTime.Now;
            var listaEventos = await (from ev in _context.Eventos
                         join tev in _context.TipoEventos on ev.IdTipoEvento equals tev.Id
                         join esc in _context.Escenarios on ev.IdEscenario equals esc.Id
                         join te in _context.TipoEscenarios on esc.Id equals te.IdEscenario
                         where ev.Fecha > now && ev.Active == true
                         orderby ev.Id ascending
                         select new DetalleEvento
                         {
                             Id = ev.Id,
                             Descripcion = ev.Descripcion,
                             TipoEvento = tev.Descripcion,
                             Fecha = ev.Fecha,
                             TipoEscenario = te.Descripcion,
                             Escenario = esc.Nombre,
                             Localizacion = esc.Localizacion
                         }).Distinct().ToListAsync();

            return listaEventos;
        }

        public async Task<DetalleEvento> GetDetalleEventosByIdAsync(int? id)
        {
            var evento = await (from ev in _context.Eventos
                          join tev in _context.TipoEventos on ev.IdTipoEvento equals tev.Id
                          join esc in _context.Escenarios on ev.IdEscenario equals esc.Id
                          join te in _context.TipoEscenarios on esc.Id equals te.IdEscenario
                          where ev.Id == id && ev.Active == true
                          select new DetalleEvento
                          {
                              Id = ev.Id,
                              Descripcion = ev.Descripcion,
                              TipoEvento = tev.Descripcion,
                              Fecha = ev.Fecha,
                              TipoEscenario = te.Descripcion,
                              Escenario = esc.Nombre,
                              Localizacion = esc.Localizacion
                          }).FirstOrDefaultAsync();

            return evento;
        }

        public async Task<IEnumerable<DetalleAsiento>> GetDetalleAsientosAsync(int? id)
        {
            var listaAsientos = await _context.Eventos
                              .Join(_context.TipoEventos, e => e.IdTipoEvento, te => te.Id, (e, te) => new { Evento = e, TipoEvento = te })
                              .Join(_context.Escenarios, ev => ev.Evento.IdEscenario, esc => esc.Id, (ev, esc) => new { ev, Escenario = esc })
                              .Join(_context.Asientos, es => es.Escenario.Id, a => a.IdEscenario, (es, a) => new { es, Asiento = a })
                              .Where(x => x.Asiento.Active && x.es.ev.Evento.Id == id)
                              .Select(x => new DetalleAsiento
                              {
                                  Id = x.Asiento.Id,
                                  IdEvento = x.es.ev.Evento.Id,
                                  TipoAsiento = x.Asiento.Descripcion,
                                  Cantidad = x.Asiento.Cantidad
                              }).ToListAsync();

            return listaAsientos;
        }

        //public async Task<Evento> GetEventByIdAsync(int id)
        //{
        //    var evento = await _context.Eventos
        //        .Include(e => e.IdEscenarioNavigation)
        //        .Include(e => e.IdTipoEventoNavigation)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    return evento;
        //}

        //public async Task<IEnumerable<Evento>> GetDetallesEventosAsync()
        //{
        //   var listaEventos = (from E in _context.Eventos
        //                         join TE in _context.TipoEventos on E.IdTipoEvento equals TE.Id
        //                         join ESC in _context.Escenarios on E.IdEscenario equals ESC.Id
        //                         join TESC in _context.TipoEscenarios on ESC.Id equals TESC.IdEscenario
        //                         where E.Active
        //                         orderby E.Id ascending
        //                         select new {
        //                                Id = E.Id,
        //                                Descripcion = E.Descripcion,
        //                                TipoEvento = TE.Descripcion,
        //                                Fecha = E.Fecha,
        //                                TipoEscenario = TESC.Descripcion,
        //                                Escenario = ESC.Nombre,
        //                                Localizacion = ESC.Localizacion
        //                         }).ToListAsync();


        //    return (IEnumerable<Evento>)await listaEventos; 
        //}
    }
}