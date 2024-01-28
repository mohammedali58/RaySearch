namespace Domain.Entities
{
    public class TreatmentRoomInput : BaseEntity
    {
        public required string Name { get; set; }

        public int? TreatmentMachineId { get; set; } = null;

    }
}
