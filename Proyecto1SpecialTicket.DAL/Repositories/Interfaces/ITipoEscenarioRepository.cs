using Proyecto1SpecialTicket.DAL.Repositories.Implementations;
using Proyecto1SpecialTicket.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Proyecto1SpecialTicket.DAL.Repositories.Interfaces
{
    /// <summary>
    /// Repository interface for Evento
    /// </summary>
    public interface ITipoEscenarioRepository : IGenericRepository<TipoEscenario>
    {
        Task<IEnumerable<TipoEscenario>> GetAllTipoEscenariosAsync();

        Task<TipoEscenario> GetTipoEscenarioByIdAsync(int id);
    }

}

