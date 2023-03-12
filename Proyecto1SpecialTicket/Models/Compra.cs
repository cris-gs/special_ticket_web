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
    public int CreatedBy { get; set; }

    [DisplayName("Fecha de actualización")]
    public DateTime UpdatedAt { get; set; }

    [DisplayName("Actualizado por")]
    public int UpdatedBy { get; set; }

    [DisplayName("Activo")]
    public int Active { get; set; }

    [DisplayName("Id Cliente")]
    public int IdCliente { get; set; }

    [DisplayName("Id Entrada")]
    public int IdEntrada { get; set; }

    [DisplayName("Id Cliente")]
    public virtual Usuario IdClienteNavigation { get; set; } = null!;

    [DisplayName("Id Cliente")]
    public virtual Entrada IdEntradaNavigation { get; set; } = null!;
}
