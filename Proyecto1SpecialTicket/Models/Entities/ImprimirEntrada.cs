using System.ComponentModel;

namespace Proyecto1SpecialTicket.Models.Entities
{
    public class ImprimirEntrada
    {
        [DisplayName("Id de la compra")]
        public int Id { get; set; }

        [DisplayName("Cantidad de entradas")]
        public int Cantidad { get; set; }

        [DisplayName("Fecha de reserva")]
        public DateTime FechaReserva { get; set; }

        [DisplayName("Fecha de pago")]
        public DateTime FechaPago { get; set; }

        [DisplayName("Tipo de asiento")]
        public string? TipoAsiento { get; set; }

        public decimal Precio { get; set; }

        public decimal Total { get; set; }

        public string? Evento { get; set; }

        public string? IdCliente { get; set; }

        [DisplayName("Nombre de usuario")]
        public string? UserName { get; set; }

    }

}

