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

    public bool Active { get; set; }

    [DisplayName("Id cliente")]
    public int IdCliente { get; set; }

    [DisplayName("Id entrada")]
    public int IdEntrada { get; set; }

    public virtual Usuario? IdClienteNavigation { get; set; }

    public virtual Entrada? IdEntradaNavigation { get; set; }
}
