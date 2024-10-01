using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VCardOnAbp.Cards;

namespace VCardOnAbp.BackgroundJobs.Dtos
{
    public class CreateCardJobArgs
    {
        public string? CardName { get; set; }
        public Supplier Supplier { get; set; }
        public Guid UserId { get; set; }
        public decimal Amount { get; set; }
    }
}
