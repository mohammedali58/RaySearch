using Domain.Common.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ConsultationRepository : IConsultationRepository
    {
        private readonly IApplicationDbContext _context;

        public ConsultationRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> CreateConsultation(Patient patient, Doctor doctor, TreatmentRoom treatmentRoom, DateTime registrationDate, DateTime consultationDate, CancellationToken cToken)
        {
            Consultation newConsultation = new()
            {
                Patient = patient,
                Doctor = doctor,
                TreatmentRoom = treatmentRoom,
                RegistrationDate = registrationDate,
                ConsultationDate = consultationDate,
            };

            _context.Consultations.Add(newConsultation);
            await _context.SaveChangesAsync(cToken);
            return newConsultation.Id;
        }

        public async Task<Consultation> GetConsultationById(Guid id, CancellationToken cToken)
        {
           Consultation? consultation = await _context.Consultations.FirstOrDefaultAsync(c =>  c.Id == id);
            if (consultation == null) {
                throw new HttpException(StatusCodes.Status400BadRequest, "consultation not found");
            }
            return consultation;
        }

        public async Task<List<Consultation>> GetExistingConsultations(DateTime consultationDate, CancellationToken cToken)
        {
            return await _context.Consultations
                .Include(c => c.Doctor)
                .Include(c => c.TreatmentRoom)
                .Where(c => c.ConsultationDate == consultationDate)
                .ToListAsync(cToken);
        }

    }
}
