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
    /// Repository for Escenario
    /// </summary>
    public class EscenarioRepository : GenericRepository<Escenario>, IEscenarioRepository
    {
        /// <summary>
        /// Constructor of EscenarioRepository
        /// </summary>
        /// <param name="specialTicketContext"></param>
        public EscenarioRepository(specialticketContext _context) : base(_context)
        {   
        }

        public async Task<IEnumerable<Escenario>> GetAllEscenariosAsync()
        {
            return await _context.Escenarios.ToListAsync();
        }

        public async Task<Escenario> GetEscenarioByIdAsync(int? id)
        {
            return await _context.Escenarios
                                 .Where(t => t.Active)
                                 .FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}