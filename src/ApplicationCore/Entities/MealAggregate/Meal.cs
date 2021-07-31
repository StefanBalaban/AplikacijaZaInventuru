using ApplicationCore.Interfaces;
using Ardalis.GuardClauses;
using System.Collections.Generic;
using System.Linq;

namespace ApplicationCore.Entities.MealAggregate
{
    public class Meal : BaseEntity, IAggregateRoot
    {
        private readonly List<MealItem> _items = new List<MealItem>();
        public IReadOnlyList<MealItem> Items => _items.AsReadOnly();
        public string Name { get; private set; }

        public Meal()
        {
        }
    }
}