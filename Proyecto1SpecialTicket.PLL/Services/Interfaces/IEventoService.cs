using Proyecto1SpecialTicket.DAL.Repositories;
using Proyecto1SpecialTicket.Models.Entities;
using Proyecto1SpecialTicket.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Proyecto1SpecialTicket.BLL.Services.Implementations
{
    /// <summary>
    /// Service Interface for Evento. 
    /// </summary>
    
    public interface IEventoService
    {
        Task<IEnumerable<Evento>> GetAllEventosAsync();

        Task<Evento> GetEventoByIdAsync(int? id);

        Task<Evento> CreateEventoAsync(Evento evento);

        Task<Evento> UpdateEventoAsync(Evento evento);

        Task<IEnumerable<DetalleEvento>> GetDetalleEventosAsync();

        Task<DetalleAsiento> GetEventoAsientosAsync(int? id);

        //Task<Evento> GetDetallesEventosAsync();
        //Task<AspnetUserDo?> StartSessionAsync(LoginRequest login);
        //Task<AspnetUserDo?> ForgotPasswordAsync(string username);
        //Task<AspnetUserDo?> UpdatePasswordAsync(UpdatePasswordRequest data);
    }
}
