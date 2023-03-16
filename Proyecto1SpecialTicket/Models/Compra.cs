﻿using System;
using System.Collections.Generic;

namespace Proyecto1SpecialTicket.Models;

public partial class Compra
{
    public int Id { get; set; }

    public int Cantidad { get; set; }

    public DateTime FechaReserva { get; set; }

    public DateTime FechaPago { get; set; }

    public DateTime CreatedAt { get; set; }

    public int CreatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public int UpdatedBy { get; set; }

    public bool Active { get; set; }

    public int IdCliente { get; set; }

    public int IdEntrada { get; set; }

    public virtual Usuario? IdClienteNavigation { get; set; }

    public virtual Entrada? IdEntradaNavigation { get; set; }
}
