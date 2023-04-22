using System.ComponentModel;

namespace Proyecto1SpecialTicket.Models.Entities;

public class DetalleEvento
{
    [DisplayName("Id del Evento")]
    public int Id { get; set; }

    [DisplayName("Descripción")]
    public string Descripcion { get; set; }

    [DisplayName("Tipo de Evento")]
    public string TipoEvento { get; set; }

    [DisplayName("Fecha y Hora")]
    public DateTime Fecha { get; set; }

    [DisplayName("Tipo de Escenario")]
    public string TipoEscenario { get; set; }

    [DisplayName("Escenario")]
    public string Escenario { get; set; }

    [DisplayName("Localización")]
    public string Localizacion { get; set; }
}
