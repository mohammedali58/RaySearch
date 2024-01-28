namespace Domain.Entities
{
    public class RoleInput : BaseEntity
    {
        public required string Name { get; set; }
        
        public required int ConditionId { get; set; }
    }
}
