using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Proyecto1SpecialTicket.Models;

public partial class TipoEvento
{
    [DisplayName("Id")]
    public int Id { get; set; }

    [DisplayName("Descripción")]
    public string Descripcion { get; set; } = null!;

    [DisplayName("Fecha de creación")]
    public DateTime CreatedAt { get; set; }

    [DisplayName("Creado por")]
    public int CreatedBy { get; set; }

    [DisplayName("Fecha de actualización")]
    public DateTime UpdatedAt { get; set; }

    [DisplayName("Actualizado por")]
    public int UpdatedBy { get; set; }

    [DisplayName("Activo")]
    public int Active { get; set; }

    public virtual ICollection<Evento> Eventos { get; } = new List<Evento>();
}
