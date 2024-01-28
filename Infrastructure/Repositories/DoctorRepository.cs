using Domain.Common.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly IApplicationDbContext _context;


        public DoctorRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Doctor>> GetDoctorForCondition(Conditions condition, CancellationToken cToken)
        {


            return await _context.Doctors.Include(d => d.Roles)
                                        .ThenInclude(r => r.Condition)
                                        .Where(d => d.Roles.Any(r => r.Condition.Id == (int)condition))
                                        .ToListAsync();
        }
    }
}
