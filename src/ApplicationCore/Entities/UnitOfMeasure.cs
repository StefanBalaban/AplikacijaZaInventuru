namespace ApplicationCore.Entities
{
    public class UnitOfMeasure : BaseEntity
    {
        public UnitOfMeasure(int id)
        {
            Id = id;
        }

        public string Measure { get; private set; }
    }
}