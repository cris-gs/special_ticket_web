using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Proyecto1SpecialTicket.Models;

public partial class TipoEscenario
{
    [DisplayName("Id")]
    public int Id { get; set; }

    [DisplayName("Descripción")]
    public string Descripcion { get; set; } = null!;

    [DisplayName("Fecha de creación")]
    public DateTime CreatedAt { get; set; }

    [DisplayName("Creado por")]
    public string CreatedBy { get; set; } = null!;

    [DisplayName("Fecha de actualización")]
    public DateTime UpdatedAt { get; set; }

    [DisplayName("Actualizado por")]
    public string UpdatedBy { get; set; } = null!;

    [DisplayName("Activo")]
    public bool Active { get; set; }

    [DisplayName("Id escenario")]
    public int IdEscenario { get; set; }

    [DisplayName("Id escenario")]
    public virtual Escenario? IdEscenarioNavigation { get; set; }
}
