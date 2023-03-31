using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Proyecto1SpecialTicket.Models;

public partial class Compra
{
    public int Id { get; set; }

    public int Cantidad { get; set; }

    [DisplayName("Fecha de reserva")]
    public DateTime FechaReserva { get; set; }

    [DisplayName("Fecha de pago")]
    public DateTime FechaPago { get; set; }

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

    [DisplayName("Id cliente")]
    public string IdCliente { get; set; } = null!;

    [DisplayName("Id entrada")]
    public int IdEntrada { get; set; }

    [DisplayName("Id cliente")]
    public virtual Users? IdClienteNavigation { get; set; }

    [DisplayName("Id entrada")]
    public virtual Entrada? IdEntradaNavigation { get; set; }
}
