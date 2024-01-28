namespace Domain.Entities
{
    public class DoctorInput : BaseEntity
    {
        public required string Name { get; set; }

        public int[] RolesId { get; set; } = new int[0];
    }
}
