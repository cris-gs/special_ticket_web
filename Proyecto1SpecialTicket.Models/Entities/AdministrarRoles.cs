using System.ComponentModel;

namespace Proyecto1SpecialTicket.Models.Entities
{
    public class AdministrarRoles
    {

        [DisplayName("Id del Usuario")]
        public string? Id { get; set; }

        [DisplayName("Nombre de usuario")]
        public string? UserName { get; set; }

        [DisplayName("Id del Rol")]
        public string? RoleId { get; set; }

        [DisplayName("Nombre del Rol")]
        public string? RolName { get; set; }

        [DisplayName("Nuevo Rol")]
        public string? Roles { get; set; }
    }
}
