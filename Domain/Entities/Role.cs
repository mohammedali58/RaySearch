namespace Domain.Entities
{
    public class Role : BaseEntity
    {
        public required string Name { get; set; }

        public ICollection<Doctor> Doctors { get; set; } = new HashSet<Doctor>();

        public required Condition Condition { get; set; }
    }
}