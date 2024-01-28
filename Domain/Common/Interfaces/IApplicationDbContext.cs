using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domain.Common.Interfaces
{
    public interface IApplicationDbContext
    {

        DbSet<Role> Roles { get; } 

        DbSet<Doctor> Doctors { get; }

        DbSet<Capability> Capabilities { get; }

        DbSet<TreatmentMachine> TreatmentMachines { get; }

        DbSet<TreatmentRoom> TreatmentRooms { get; }

        DbSet<Condition> Conditions { get; }


        DbSet<Typography> Typography { get; }

        DbSet<Patient> Patients { get; }

        DbSet<Consultation > Consultations { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
