using Domain.Enums;

namespace Domain.Entities
{
    public  class Patient : BaseEntity
    {
        public required string Name { get; set; }

        public required Condition Condition { get; set; }

        public Typography? Typography { get; set; } = null;
    }
}
