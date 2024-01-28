using Domain.Enums;

namespace Domain.Entities
{
    public class TreatmentMachine : BaseEntity
    {
        public required string Name { get; set; }

        public required Capability Capability {  get; set; } 
    }
}
