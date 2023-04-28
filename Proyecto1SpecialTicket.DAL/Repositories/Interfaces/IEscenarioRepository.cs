using Proyecto1SpecialTicket.DAL.Repositories.Implementations;
using Proyecto1SpecialTicket.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Proyecto1SpecialTicket.DAL.Repositories.Interfaces
{
    /// <summary>
    /// Repository interface for Escenario
    /// </summary>
    public interface IEscenarioRepository : IGenericRepository<Escenario>
    {
        Task<IEnumerable<Escenario>> GetAllEscenariosAsync();

        Task<Escenario> GetEscenarioByIdAsync(int? id);

    }

    
}

