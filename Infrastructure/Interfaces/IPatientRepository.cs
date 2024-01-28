using Domain.Entities;
using Domain.Enums;

namespace Infrastructure.Interfaces
{
    public interface IPatientRepository
    {
        public Task<Patient> GetPatientById(int id, CancellationToken cToken);

        public Task<Patient> CreatePatient(string name, Condition condition, Typography? typography, CancellationToken cToken);

    }
}
