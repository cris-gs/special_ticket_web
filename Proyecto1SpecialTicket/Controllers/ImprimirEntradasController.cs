using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto1SpecialTicket.IdentityData;
using Proyecto1SpecialTicket.BLL.Services.Interfaces;
using Proyecto1SpecialTicket.Models;
using Proyecto1SpecialTicket.Models.Entities;
using Proyecto1SpecialTicket.DAL.DataContext;


namespace Proyecto1SpecialTicket.Controllers
{
    [Authorize(Roles = "Administrador, Cajero")]
    public class ImprimirEntradasController : Controller
    {
        private readonly specialticketContext _context;
        private readonly ICompraService _compraService;
        private readonly UserManager<Proyecto1SpecialTicketUser> _userManager;

        public ImprimirEntradasController(specialticketContext context, ICompraService compraService, UserManager<Proyecto1SpecialTicketUser> userManager)
        {
            _context = context;
            _compraService = compraService;
            _userManager = userManager;
        }

        // GET: ImprimirEntradas
        public IActionResult Index()
        {
            var listaClientes = _context.Users
                                    .Select(u => new ImprimirEntrada
                                    {
                                        IdCliente = u.Id,
                                        UserName = u.UserName,
                                    }).ToList();

            return View(listaClientes);
        }


        public async Task<IActionResult> Imprimir(string? idCliente)
        {
            if (idCliente == null) return NotFound();

            //Todo: Agregar al modelo una propiedad de pagado que se muestra en true si ya tiene fecha de pago
            var listaEntradaComprada = await _compraService.GetCompraByClienteAsync(idCliente);

            return View(listaEntradaComprada);
        }

        public async Task<IActionResult> ImprimirDoc(int? id)
        {

            if (id == null) return NotFound();

            var fechaReserva = _compraService.GetFechaReserva(id);
            DateTime currentDateTime = DateTime.Now;

            var listaEntradaComprada = await _compraService.GetEntradaCompradaByIdAsync(id);

            Compra compra = new()
            {
                Id = listaEntradaComprada[0].Id,
                Cantidad = listaEntradaComprada[0].Cantidad,
                FechaReserva = listaEntradaComprada[0].FechaReserva,
                FechaPago = listaEntradaComprada[0].FechaPago,
                CreatedAt = listaEntradaComprada[0].CreatedAt,
                CreatedBy = listaEntradaComprada[0].CreatedBy,
                UpdatedAt = listaEntradaComprada[0].UpdatedAt,
                UpdatedBy = listaEntradaComprada[0].UpdatedBy,
                Active = listaEntradaComprada[0].Active,
                IdCliente = listaEntradaComprada[0].IdCliente,
                IdEntrada = listaEntradaComprada[0].IdEntrada
            };

            compra.FechaReserva = fechaReserva;
            compra.FechaPago = currentDateTime;

            try
            {
                await _compraService.UpdateCompraAsync(compra);

                var entradaComprada = (from c in _context.Compras
                                       join en in _context.Entradas on c.IdEntrada equals en.Id
                                       join ev in _context.Eventos on en.IdEvento equals ev.Id
                                       where c.Id == id
                                       select new
                                       {
                                           en.TipoAsiento,
                                           en.Precio,
                                           ev.IdTipoEvento,
                                           ev.IdEscenario,
                                           ev.Descripcion
                                       }).ToList();

                var eventoDescipcion = (from c in _context.TipoEventos
                                       where c.Id == entradaComprada[0].IdTipoEvento
                                        select new
                                       {
                                           c.Descripcion
                                       }).ToList();

                var eventoEscenario = (from c in _context.Escenarios
                                        where c.Id == entradaComprada[0].IdEscenario
                                        select new
                                        {
                                            c.Nombre
                                        }).ToList();

                // Configuración de la página
                Document doc = new Document(PageSize.A4, 50, 50, 50, 50);

                // Crear el archivo PDF en memoria
                MemoryStream ms = new MemoryStream();

                // Crear un escritor PDF
                PdfWriter writer = PdfWriter.GetInstance(doc, ms);

                // Abrir el documento
                doc.Open();

                // Configuración de la fuente
                BaseFont font = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                Font boldFont = new Font(font, 12, Font.BOLD);
                Font normalFont = new Font(font, 10, Font.NORMAL);

                doc.AddTitle("Factura de pago por compra de entrada");

                // Agregar el título
                Paragraph title = new Paragraph("Factura de pago por compra de entrada", boldFont);
                title.Alignment = Element.ALIGN_CENTER;
                title.SpacingAfter = 20f;
                doc.Add(title);

                // Agregar los detalles de la compra
                Paragraph compraId = new Paragraph("Id de la compra: " + listaEntradaComprada[0].Id, normalFont);
                compraId.Alignment = Element.ALIGN_LEFT;
                doc.Add(compraId);

                Paragraph cantidad = new Paragraph("Cantidad de entradas: " + listaEntradaComprada[0].Cantidad, normalFont);
                cantidad.Alignment = Element.ALIGN_LEFT;
                doc.Add(cantidad);

                Paragraph fechaReserva2 = new Paragraph("Fecha de reserva: " + fechaReserva, normalFont);
                fechaReserva2.Alignment = Element.ALIGN_LEFT;
                doc.Add(fechaReserva2);

                Paragraph fechaCompra = new Paragraph("Fecha de la compra: " + currentDateTime, normalFont);
                fechaCompra.Alignment = Element.ALIGN_LEFT;
                doc.Add(fechaCompra);

                doc.Add(new Paragraph("\n"));

                // Agregar los detalles del evento
                Paragraph evento = new Paragraph("Detalles del evento", boldFont);
                evento.Alignment = Element.ALIGN_LEFT;
                evento.SpacingAfter = 10f;
                doc.Add(evento);

                Paragraph eventoNombre = new Paragraph("Nombre del evento: " + entradaComprada[0].Descripcion, normalFont);
                eventoNombre.Alignment = Element.ALIGN_LEFT;
                doc.Add(eventoNombre);

                Paragraph eventoDescripcion = new Paragraph("Descripción del evento: " + eventoDescipcion[0].Descripcion, normalFont);
                eventoDescripcion.Alignment = Element.ALIGN_LEFT;
                doc.Add(eventoDescripcion);

                Paragraph eventoEscenario2 = new Paragraph("Escenario: " + eventoEscenario[0].Nombre, normalFont);
                eventoEscenario2.Alignment = Element.ALIGN_LEFT;
                doc.Add(eventoEscenario2);

                doc.Add(new Paragraph("\n"));

                // Agregar los detalles de la entrada
                Paragraph entrada = new Paragraph("Detalles de la entrada", boldFont);
                entrada.Alignment = Element.ALIGN_LEFT;
                entrada.SpacingAfter = 10f;
                doc.Add(entrada);

                Paragraph tipoAsiento = new Paragraph("Tipo de asiento: " + entradaComprada[0].TipoAsiento, normalFont);
                tipoAsiento.Alignment = Element.ALIGN_LEFT;
                doc.Add(tipoAsiento);

                Paragraph precio = new Paragraph("Precio por entrada: " + entradaComprada[0].Precio, normalFont);
                precio.Alignment = Element.ALIGN_LEFT;
                doc.Add(precio);

                doc.Add(new Paragraph("\n"));

                // Agregar el total
                Paragraph total = new Paragraph("Total: " + entradaComprada[0].Precio * listaEntradaComprada[0].Cantidad, boldFont);
                total.Alignment = Element.ALIGN_RIGHT;
                total.SpacingAfter = 20f;
                doc.Add(total);

                // Cerramos el documento
                writer.Close();
                doc.Close();


                // Enviamos el PDF como respuesta con el tipo de contenido "application/pdf"
                ms.Seek(0, SeekOrigin.Begin);
                return File(ms, "application/pdf");


            }
            catch (DbUpdateConcurrencyException)
            {
                {
                    if (!CompraExists(compra.Id))
                        return NotFound();
                    else throw;
                }
            }
        }

        private bool CompraExists(int id)
        {
            return _compraService.GetCompraByIdAsync(id) == null ? true : false;
        }
    }
}
