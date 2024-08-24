using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace VCardOnAbp.Masters
{
    public class Supplier : Entity<Guid>
    {
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        public DateTime? ValidTo { get; set; }

        private Supplier() { }
        public Supplier(string name, string description, DateTime? validTo = null)
        {
            Name = name;
            Description = description;
            ValidTo = validTo;
        }
    }
}
