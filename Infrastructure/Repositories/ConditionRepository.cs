using Domain.Common.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ConditionRepository : IConditionRepository
    {
        private readonly IApplicationDbContext _context;

        public ConditionRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Condition> GetConditionById(Conditions condition, CancellationToken cToken)
        {
            Condition PatientCondition = await _context.Conditions.FirstOrDefaultAsync(c => c.Id == (int)condition, cToken);

            if(PatientCondition is null)
            {
                throw new HttpException(StatusCodes.Status404NotFound, "Patient Condition Not Found");
            }

            return PatientCondition;
        }
    }
}
