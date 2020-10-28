using System;

namespace Todo.Domain.Entities
{
    public abstract class Entity : IEquatable<Entity>//PErmite que eu compare entidades
    {
        public Entity()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }

        public bool Equals(Entity other)
        {
            return Id == other.Id;
        }
    }
}