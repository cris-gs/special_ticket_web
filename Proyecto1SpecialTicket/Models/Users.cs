using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto1SpecialTicket.Models
{
    [Table("SpecialTicketUsers")]
    public class Users : IdentityUser
    {
        public virtual ICollection<Compra> Compras { get; } = new List<Compra>();
    }
}





