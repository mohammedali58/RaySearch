using Domain.Common.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly IApplicationDbContext _context;

        public PatientRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Patient> CreatePatient(string name, Condition condition, Typography? typography, CancellationToken cToken)
        {
            Patient newPatient = new() { Name = name, Condition = condition, Typography = typography };
            _context.Patients.Add(newPatient);
            await _context.SaveChangesAsync(cToken);
            return newPatient;
        }

        public async Task<Patient> GetPatientById(int id, CancellationToken cToken)
        {
            Patient patient = await _context.Patients.FirstOrDefaultAsync(p => p.Id == id);
            if (patient is null)
            {
                throw new HttpException(StatusCodes.Status404NotFound, "course not found");
            }
            return patient;
        }
    }
}
