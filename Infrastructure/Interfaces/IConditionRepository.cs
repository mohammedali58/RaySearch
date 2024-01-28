using Domain.Entities;
using Domain.Enums;

namespace Infrastructure.Interfaces
{
    public interface IConditionRepository
    {
        public Task<Condition> GetConditionById(Conditions condition, CancellationToken cToken);
    }
}
