using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Proyecto1SpecialTicket.Models;

public partial class Evento
{
    [DisplayName("Id")]
    public int Id { get; set; }

    [DisplayName("Descripción")]
    public string Descripcion { get; set; } = null!;

    [DisplayName("Fecha")]
    public DateTime Fecha { get; set; }

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

    [DisplayName("Id tipo evento")]
    public int IdTipoEvento { get; set; }

    [DisplayName("Id escenario")]
    public int IdEscenario { get; set; }

    public virtual ICollection<Entrada> Entrada { get; } = new List<Entrada>();

    [DisplayName("Id escenario")]
    public virtual Escenario IdEscenarioNavigation { get; set; } = null!;

    [DisplayName("Id tipo evento")]
    public virtual TipoEvento IdTipoEventoNavigation { get; set; } = null!;
}
