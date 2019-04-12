using System;

namespace _19GRPADS01BNT401_Assessment.Domain.Entities
{
    public abstract class EntityBase
    {
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}