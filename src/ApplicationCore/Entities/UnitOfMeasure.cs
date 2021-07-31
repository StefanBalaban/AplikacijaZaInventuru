namespace ApplicationCore.Entities
{
    public class UnitOfMeasure : BaseEntity
    {
        public string Measure { get; private set; }

        public UnitOfMeasure(int id)
        {
            Id = id;
        }
    }
}