using Domain.Enums;

namespace Domain.Entities
{
    public class Doctor : BaseEntity
    { 
        public required string Name {  get; set; } 
        
        public ICollection<Role> Roles { get; set; } = new HashSet<Role>();
    }
}