using System.ComponentModel;

namespace Proyecto1SpecialTicket.Models
{
    public class Users
    {
        [DisplayName("Id")]
        public int Id { get; set; }

        [DisplayName("Nombre")]
        public string? UserName { get; set; }

        public string? NormalizedUserName { get; set; }

        [DisplayName("Correo")]
        public string? Email { get; set; }

        public string? NormalizedEmail { get; set; }

        public bool EmailConfirmed { get; set; }

        public string? PasswordHash { get; set; }
        
        public string? SecurityStamp { get; set; }

        public string? ConcurrencyStamp { get; set; }

        [DisplayName("Teléfono")]
        public string? PhoneNumber { get; set; }

        public bool PhoneNumberConfirmed { get; set; }

        public bool TwoFactorEnabled { get; set; }

        [DisplayName("Fin de inicio de sesión")]
        public DateTime? LockoutEnd { get; set; }

        public bool LockoutEnable { get; set; }

        public int AccessFailedCount { get; set; }

        public virtual ICollection<Compra> Compras { get; } = new List<Compra>();
    }

}

