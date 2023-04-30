using Microsoft.Extensions.Configuration;
using Proyecto1SpecialTicket.BLL.Services.Interfaces;
using Proyecto1SpecialTicket.DAL.Repositories.Implementations;
using Proyecto1SpecialTicket.DAL.Repositories.Interfaces;
using Proyecto1SpecialTicket.Models;
using Proyecto1SpecialTicket.Models.Entities;
using System.Collections.Generic;
using System.Globalization;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Proyecto1SpecialTicket.BLL.Services.Implementations
{
    /// <summary>
    /// Service for Evento
    /// </summary>
    public class EventoService : IEventoService
    {
        private readonly IEventoRepository _eventoRepository;
        private readonly IConfiguration _config;

        /// <summary>
        /// Constructor of EventoService
        /// </summary>
        /// <param name="eventoRepository"></param>
        public EventoService(IEventoRepository eventoRepository, IConfiguration config)
        {
            _eventoRepository = eventoRepository;
            _config = config;   
        }

        public async Task<IEnumerable<Evento>> GetAllEventosAsync()
        {
             var lista = await _eventoRepository.GetAllEventosAsync();
             return lista;
        }

        public async Task<Evento> GetEventoByIdAsync(int? id)
        {
             var lista = await _eventoRepository.GetEventoByIdAsync(id);
             return lista;
        }

        public async Task<Evento> CreateEventoAsync(Evento evento)
        {
            evento.Active = true;
            var lista = await _eventoRepository.AddAsync(evento);
            return lista;
        }

        public async Task<Evento> UpdateEventoAsync(Evento evento)
        {
            DateTime currentDateTime = DateTime.Now;
            evento.UpdatedAt = currentDateTime;
            var lista = await _eventoRepository.UpdateAsync(evento);
            return lista;
        }

        public async Task<IEnumerable<DetalleEvento>> GetDetalleEventosAsync()
        {
            var lista = await _eventoRepository.GetDetalleEventosAsync();
            return lista;
        }

        public async Task<DetalleEvento> GetDetalleEventosByIdAsync(int? id)
        {
            var lista = await _eventoRepository.GetDetalleEventosByIdAsync(id);
            return lista;
        }

        public async Task<IEnumerable<DetalleAsiento>> GetDetalleAsientosAsync(int? id)
        {
            var eventoAsientos = await _eventoRepository.GetDetalleAsientosAsync(id);
            return eventoAsientos;
        }
        
        //public async Task<Evento> GetDetallesEventosAsync()
        //{
        //    return (Evento)await _eventoRepository.GetAllEventosAsync();
        //}

        //public async Task<AspnetUserDo?> StartSessionAsync(LoginRequest login)
        //{
        //    var user = await _aspnetUserRepository.GetUserByUserNameAsync(login.Username);
        //    if (user == null) return null;
        //    var encrypt = SecurityHelper.EncodePassword(login.Password, 1, user.AspnetMembership.PasswordSalt);
        //    if (encrypt != user.AspnetMembership.Password)
        //        return null;
        //    var userDo = _mapper.Map<AspnetUserDo>(user); 
        //    userDo.Membership = _mapper.Map<AspnetMembershipDo>(user.AspnetMembership);
        //    return userDo;
        //}

        //public async Task<AspnetUserDo?> ForgotPasswordAsync(string username)
        //{
        //    //set culture
        //    var culture = Thread.CurrentThread.CurrentCulture.Name;
        //    var lang = culture.Substring(0, 2);
        //    Thread.CurrentThread.CurrentCulture = new CultureInfo(lang);
        //    Thread.CurrentThread.CurrentUICulture = new CultureInfo(lang);
        //    //Process
        //    var user = await _aspnetUserRepository.GetUserByUserNameAsync(username);
        //    if (user == null) return null;
        //    var userDo = _mapper.Map<AspnetUserDo>(user);
        //    userDo.Membership = _mapper.Map<AspnetMembershipDo>(user.AspnetMembership);
        //    var response = new ForgotPasswordResponse { UserId = userDo.UserId.ToString(), UserName = userDo.UserName, Hour = DateTime.Now };
        //    var link = _config["Hosts:FrontEndURL"] + "/RecoverPassword?token=" + SecurityHelper.Encript(JsonSerializer.Serialize(response));
        //    var subject = _localizer["Subject"];
        //    var body = _localizer["Body1"] + "\r\n\n" + link + "\r\n\n" + _localizer["Body2"];
        //    MailHelper.RecoverPasswordSendMail(userDo.Membership.Email, subject, body);
        //    return userDo;
        //}

        //public async Task<AspnetUserDo?> UpdatePasswordAsync(UpdatePasswordRequest data)
        //{   
        //    //set culture
        //    var culture = Thread.CurrentThread.CurrentCulture.Name;
        //    var lang = culture.Substring(0, 2);
        //    Thread.CurrentThread.CurrentCulture = new CultureInfo(lang);
        //    Thread.CurrentThread.CurrentUICulture = new CultureInfo(lang);
        //    //Process
        //    Regex regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,16}$");
        //    var user = await _aspnetUserRepository.GetUserByUserNameAsync(data.Username);
        //    if (user == null) return null;
        //    var s = data.Password.Trim();
        //    var password = ((s.Length % 4 == 0) && Regex.IsMatch(s, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None)) ? SecurityHelper.Decript(data.Password):data.Password;
        //    Match match = regex.Match(password);
        //    if (!match.Success) throw new Exception(_localizer["InvalidPassword"]);
        //    var minutes = (Clock.Now - data.Hour).TotalMinutes;
        //    if (minutes > 30) throw new Exception(_localizer["ExpiredLink"]);
        //    user.AspnetMembership.Password = SecurityHelper.EncodePassword(password, 1, user.AspnetMembership.PasswordSalt);
        //    user.AspnetMembership.LastPasswordChangedDate = Clock.Now;   
        //    var result = await _aspnetUserRepository.UpdateAsync(user);
        //    var userDo = _mapper.Map<AspnetUserDo>(result);
        //    userDo.Membership = _mapper.Map<AspnetMembershipDo>(result.AspnetMembership);
        //    return userDo;
        //}
    }
}