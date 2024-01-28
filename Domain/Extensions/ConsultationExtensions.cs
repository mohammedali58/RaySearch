using Domain.Dtos;
using Domain.Entities;
using System.Runtime.CompilerServices;

namespace Domain.Extensions
{
    public static class ConsultationExtensions
    {
        public static ConsultationDto ToDto (this Consultation consultation)
        {
            return new ConsultationDto()
            {
                Id = consultation.Id,
                PatientName = consultation.Patient.Name,
                DoctorName = consultation.Doctor.Name,
                TreatmentRoom = consultation.TreatmentRoom.Name,
                RegistrationDate = consultation.RegistrationDate,
                ConsultationDate = consultation.ConsultationDate
            };
        }
    }
}
