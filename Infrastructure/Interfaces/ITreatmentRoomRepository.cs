using Domain.Entities;
using Domain.Enums;

namespace Infrastructure.Interfaces
{
    public interface ITreatmentRoomRepository
    {
        public Task<List<TreatmentRoom>> GetTreatmentRoomForConditionAndTypography(Conditions condition, Typographies? typography, CancellationToken cToken);

        public Task<List<TreatmentRoom>> GetTreatmentRoomForTypography(Typographies? typography, CancellationToken cToken);
    }
}
