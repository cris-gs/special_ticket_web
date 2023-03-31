using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Proyecto1SpecialTicket.Models;

public partial class Usuario
{
    [DisplayName("Id")]
    public int Id { get; set; }

    [DisplayName("Nombre")]
    public string Nombre { get; set; } = null!;

    [DisplayName("Correo")]
    public string Correo { get; set; } = null!;

    [DisplayName("Teléfono")]
    public int Telefono { get; set; }

    [DisplayName("Rol")]
    public int Rol { get; set; }

    [DisplayName("Fecha de creación")]
    public DateTime CreatedAt { get; set; }

    [DisplayName("Creado por")]
    public int CreatedBy { get; set; }

    [DisplayName("Fecha de actualización")]
    public DateTime UpdatedAt { get; set; }

    [DisplayName("Actualizado por")]
    public int UpdatedBy { get; set; }

    [DisplayName("Activo")]
    public bool Active { get; set; }

    //public virtual ICollection<Compra> Compras { get; } = new List<Compra>();
}
