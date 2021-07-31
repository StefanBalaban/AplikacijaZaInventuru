using System;

namespace ApplicationCore.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string name) : base($"Entity {name} not found")
        {
        }
    }
}