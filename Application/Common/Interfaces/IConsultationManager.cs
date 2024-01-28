using Domain.Dtos;
using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface IConsultationManager
    {
        public Task<ConsultationDto> BookConsultation(Patient patient,List<Doctor> doctors, List<TreatmentRoom> rooms, int offDays, CancellationToken cToken);
        public Task<ConsultationDto> PerformBooking(Patient patient, List<Doctor> doctors, List<TreatmentRoom> rooms, DateTime consultationDate, CancellationToken cToken);

    }
}
