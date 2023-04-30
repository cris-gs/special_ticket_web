using Proyecto1SpecialTicket.DAL.Repositories.Interfaces;
using Proyecto1SpecialTicket.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using Proyecto1SpecialTicket.DAL.DataContext;
using System.Linq;

namespace Proyecto1SpecialTicket.DAL.Repositories.Implementations
{
    /// <summary>
    /// Repository for TipoEvento
    /// </summary>
    public class TipoEventoRepository : GenericRepository<TipoEvento>, ITipoEventoRepository
    {
        /// <summary>
        /// Constructor of TipoEventoRepository
        /// </summary>
        /// <param name="specialTicketContext"></param>
        public TipoEventoRepository(specialticketContext _context) : base(_context)
        {   
        }

        public async Task<IEnumerable<TipoEvento>> GetAllTipoEventosAsync()
        {
            return await _context.TipoEventos
                                 .Where(t => t.Active)
                                 .ToListAsync();
        }

        public async Task<TipoEvento> GetTipoEventoByIdAsync(int? id)
        {
            return await _context.TipoEventos
                                 .Where(t => t.Active)
                                 .FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}