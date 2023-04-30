using Proyecto1SpecialTicket.DAL.Repositories;
using Proyecto1SpecialTicket.Models;
using Proyecto1SpecialTicket.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Proyecto1SpecialTicket.BLL.Services.Interfaces
{
    /// <summary>
    /// Service Interface for Escenario. 
    /// </summary>
    
    public interface IEscenarioService
    {
        Task<IEnumerable<Escenario>> GetAllEscenariosAsync();

        Task<Escenario> GetEscenariosByIdAsync(int? id);

        Task<Escenario> CreateEscenariosAsync(Escenario escenario);

        Task<Escenario> UpdateEscenariosAsync(Escenario escenario);


    }
}
