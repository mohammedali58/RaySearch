using Domain.Entities;
using Domain.Enums;

namespace Infrastructure.Interfaces
{
    public interface ITypographyRepository
    {
        public Task<Typography> GetTypographyById(Typographies? typography, CancellationToken cToken);
    }
}