using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        private DateTime? _createdAt { get; set; }
        public DateTime? CreatedAt
        {
            get { return _createdAt; }
            set { _createdAt = value == null ? DateTime.UtcNow : value; }
        }
        public DateTime? Updated { get; set; }
    }
}