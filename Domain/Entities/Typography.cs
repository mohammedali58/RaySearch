
namespace Domain.Entities
{
    public class Typography : BaseEntity
    {
        public required string Name { get; set; }

        public static explicit operator int(Typography v)
        {
            throw new NotImplementedException();
        }
    }
}
