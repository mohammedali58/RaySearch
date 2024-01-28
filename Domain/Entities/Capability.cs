namespace Domain.Entities
{
    public class Capability : BaseEntity
    {
        public required string Name { get; set; }

        public ICollection<TreatmentMachine> TreatmentMachines { get; set; } = new HashSet<TreatmentMachine>();
    }
}
