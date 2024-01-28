using Domain.Entities;
using Domain.Enums;

namespace Infrastructure.Interfaces
{
    public interface IDoctorRepository
    {
        public Task<List<Doctor>> GetDoctorForCondition(Conditions condition, CancellationToken cToken);
    }
}
