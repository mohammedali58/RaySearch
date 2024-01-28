using Domain.Entities;

namespace Infrastructure.Interfaces
{
    public interface IConsultationRepository
    {
        public Task<Consultation> GetConsultationById(Guid id, CancellationToken cToken);

        public Task<Guid> CreateConsultation(Patient patient, Doctor doctor, TreatmentRoom treatmentRoom, DateTime registrationDate, DateTime consultationDate, CancellationToken cToken);

        public Task<List<Consultation>> GetExistingConsultations(DateTime consultationDate, CancellationToken cToken);
    }
}
