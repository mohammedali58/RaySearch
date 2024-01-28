using Application.Common.Interfaces;
using Domain.Common.Interfaces;
using Domain.Dtos;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Extensions;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class ConsultationManager : IConsultationManager
    {
        private readonly IApplicationDbContext _context;
        private readonly IConsultationRepository _consultationRepository;

        public ConsultationManager(IApplicationDbContext context, IConsultationRepository consultationRepository)
        {
            _consultationRepository = consultationRepository;
            _context = context;
        }
        public async Task<ConsultationDto> BookConsultation(Patient patient, List<Doctor> doctors, List<TreatmentRoom> rooms, int offDays, CancellationToken cToken)
        {
            Consultation consultation = null!;

            DateTime consultationStartDate = DateTime.Today.AddDays(offDays);

            List<Consultation>? existingConsultations = await _consultationRepository.GetExistingConsultations(consultationStartDate, cToken);

            while (consultation == null)
            {
                var doctorsToCheck = doctors;
                var roomsToCheck = rooms;

                if (existingConsultations.Count == 0)
                {
                    return await PerformBooking(patient, doctorsToCheck, rooms, consultationStartDate, cToken);
                }

                foreach (var existingConsultation in existingConsultations)
                {
                    doctorsToCheck = doctorsToCheck.Where(d => d.Id != existingConsultation.Doctor.Id).ToList();
                    roomsToCheck = roomsToCheck.Where(r => r.Id != existingConsultation.TreatmentRoom.Id).ToList();

                }

                if(doctorsToCheck.Count != 0 &&  roomsToCheck.Count != 0)
                {
                    return await PerformBooking(patient, doctorsToCheck, rooms, consultationStartDate, cToken);
                }

                consultationStartDate = consultationStartDate.AddDays(1);
                existingConsultations = await _consultationRepository.GetExistingConsultations(consultationStartDate, cToken);
            }

            throw new HttpException(StatusCodes.Status400BadRequest, "Can not Book Consultation");
        }

        public async Task<ConsultationDto> PerformBooking(Patient patient, List<Doctor> doctors, List<TreatmentRoom> rooms, DateTime consultationDate, CancellationToken cToken)
        {
            Consultation consultation = new()
            {
                Patient = patient,
                Doctor = doctors.First(),
                TreatmentRoom = rooms.First(),
                RegistrationDate = DateTime.Now,
                ConsultationDate = consultationDate,
            };

            _context.Consultations.Add(consultation);
            await _context.SaveChangesAsync(cToken);
            return consultation.ToDto();
        }

    }
}
