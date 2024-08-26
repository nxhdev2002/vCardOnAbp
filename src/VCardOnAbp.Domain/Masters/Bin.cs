using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace VCardOnAbp.Masters
{
    public class Bin : Entity<Guid>
    {
        [MaxLength(100)]
        public string Name { get; private set; }
        [MaxLength(500)]
        public string Description { get; private set; }
        public Guid SupplierId { get; private set; }
        public bool IsActive { get; private set; }

        private Bin() { }
        public Bin(string name, Guid supplierId, bool isActive = true)
        {
            Name = name;
            SupplierId = supplierId;
            IsActive = isActive;
        }
    }
}
