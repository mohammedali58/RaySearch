namespace Domain.Entities
{
    public class TreatmentMachineInput : BaseEntity
    {
        public required string Name { get; set; }

        public required int CapabilityId { get; set; }
    }
}
