using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Proyecto1SpecialTicket.Models;

public partial class Escenario
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    [DisplayName("Localización")]
    public string Localizacion { get; set; } = null!;

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

    public virtual ICollection<Asiento> Asientos { get; } = new List<Asiento>();

    public virtual ICollection<Evento> Eventos { get; } = new List<Evento>();

    [DisplayName("Tipo escenario")]
    public virtual ICollection<TipoEscenario> TipoEscenarios { get; } = new List<TipoEscenario>();
}
