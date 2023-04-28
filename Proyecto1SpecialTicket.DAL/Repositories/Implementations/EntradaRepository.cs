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
    /// Repository for Entrada
    /// </summary>
    public class EntradaRepository : GenericRepository<Entrada>, IEntradaRepository
    {
        /// <summary>
        /// Constructor of EntradaRepository
        /// </summary>
        /// <param name="specialTicketContext"></param>
        public EntradaRepository(specialticketContext _context) : base(_context)
        {   
        }

        public async Task<IEnumerable<Entrada>> GetAllEntradasAsync()
        {
            return await _context.Entradas
                                 .Where(x=>x.Active)
                                 .ToListAsync();
        }

        public async Task<Entrada> GetEntradaByIdAsync(int? id)
        {
            return await _context.Entradas
                                 .Where(t => t.Active)
                                 .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Entrada> GetEntradaByIdEventoAsync(int? id)
        {
            return await _context.Entradas
                                 .FirstOrDefaultAsync(m => m.IdEvento == id && m.Active);
        }
    }
}