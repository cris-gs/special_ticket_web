using Proyecto1SpecialTicket.DAL.Repositories.Implementations;
using Proyecto1SpecialTicket.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Proyecto1SpecialTicket.DAL.Repositories.Interfaces
{
    /// <summary>
    /// Repository interface for TipoEvento
    /// </summary>
    public interface ITipoEventoRepository : IGenericRepository<TipoEvento>
    {
        Task<IEnumerable<TipoEvento>> GetAllTipoEventosAsync();

        Task<TipoEvento> GetTipoEventoByIdAsync(int id);
        
    }

    
}

