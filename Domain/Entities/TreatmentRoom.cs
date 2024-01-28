namespace Domain.Entities
{
    public class TreatmentRoom : BaseEntity
    {
        public required string Name { get; set; }

        public TreatmentMachine? TreatmentMachine { get; set; } = null; 
    }
}
