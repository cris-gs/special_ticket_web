using Proyecto1SpecialTicket.DAL.Repositories;
using Proyecto1SpecialTicket.Models;
using Proyecto1SpecialTicket.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Proyecto1SpecialTicket.BLL.Services.Implementations
{
    /// <summary>
    /// Service Interface for Escenario. 
    /// </summary>
    
    public interface ITipoEscenarioService
    {
        Task<IEnumerable<TipoEscenario>> GetAllTipoEscenariosAsync();

        Task<TipoEscenario> GetTipoEscenarioByIdAsync(int id);

        Task<TipoEscenario> CreateTipoEscenariosAsync(TipoEscenario tipoEscenario);

        Task<TipoEscenario> UpdateTipoEscenariosAsync(TipoEscenario tipoEscenario);


    }
}
