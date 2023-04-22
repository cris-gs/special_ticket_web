using System;
using System.Collections.Generic;

namespace Proyecto1SpecialTicket.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            Compras = new HashSet<Compra>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public int Telefono { get; set; }
        public int Rol { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; } = null!;
        public bool Active { get; set; }

        public virtual ICollection<Compra> Compras { get; set; }
    }
}
