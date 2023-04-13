using System.ComponentModel;

namespace Proyecto1SpecialTicket.Models.Entities
{
    public class DetalleEntrada
    {
        public int Id { get; set; }

        public int Disponibles { get; set; }

        [DisplayName("Tipo de asiento")]
        public string TipoAsiento { get; set; } = null!;

        public decimal Precio { get; set; }
    }
}