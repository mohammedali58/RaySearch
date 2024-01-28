using Domain.Common.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repositories
{
    public class TreatmentRoomRepository : ITreatmentRoomRepository
    {
        private readonly IApplicationDbContext _context;

        public TreatmentRoomRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<TreatmentRoom>> GetTreatmentRoomForConditionAndTypography(Conditions condition, Typographies? typography, CancellationToken cToken)
        {
            switch (condition)
            {
                case Conditions.Flu:
                    return await _context.TreatmentRooms.ToListAsync(cToken);
                case Conditions.Cancer:
                    return await GetTreatmentRoomForTypography(typography, cToken);
                default:
                    throw new HttpException(StatusCodes.Status400BadRequest, "No Condition in the hospital match this condition");
            }
        }

        public async Task<List<TreatmentRoom>> GetTreatmentRoomForTypography(Typographies? typography, CancellationToken cToken)
        {
            switch(typography)
            {
                case Typographies.Breast:
                    return await _context.TreatmentRooms.Include(r => r.TreatmentMachine)
                        .Where(r => r.TreatmentMachine != null).ToListAsync(cToken);
                case Typographies.Heads:
                case Typographies.Neck:
                    return await _context.TreatmentRooms.Include(r => r.TreatmentMachine)
                        .Where(r => (r.TreatmentMachine !=null && r.TreatmentMachine.Capability.Id == (int)Capabilities.Advanced)).ToListAsync(cToken);
                default:
                    throw new HttpException(StatusCodes.Status400BadRequest, "No Typography in the hospital match this typography");
            }
        }

    }
}
