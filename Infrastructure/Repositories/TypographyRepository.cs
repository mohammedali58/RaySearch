using Domain.Common.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class TypographyRepository : ITypographyRepository
    {

        private readonly IApplicationDbContext _context;

        public TypographyRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Typography> GetTypographyById(Typographies? typography, CancellationToken cToken)
        {
            Typography PatientTypography =  await _context.Typography.FirstOrDefaultAsync(t => t.Id == (int)typography!, cToken);

            if (PatientTypography is null)
            {
                throw new HttpException(StatusCodes.Status404NotFound, "Patient Typography Not Found");
            }

            return PatientTypography;
        }
    }
}
