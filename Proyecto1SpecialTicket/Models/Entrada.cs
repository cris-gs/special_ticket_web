﻿using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Proyecto1SpecialTicket.Models;

public partial class Entrada
{
    public int Id { get; set; }

    public int Disponibles { get; set; }

    [DisplayName("Tipo de asiento")]
    public string TipoAsiento { get; set; } = null!;

    public decimal Precio { get; set; }

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

    [DisplayName("Id evento")]
    public int IdEvento { get; set; }

    public virtual ICollection<Compra> Compras { get; } = new List<Compra>();

    [DisplayName("Id evento")]
    public virtual Evento? IdEventoNavigation { get; set; }
}
