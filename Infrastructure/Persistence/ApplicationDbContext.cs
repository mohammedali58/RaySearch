using Domain.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext: DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {}


        public DbSet<Doctor> Doctors {  get; set; }

        public DbSet<TreatmentMachine> TreatmentMachines { get; set; }

        public DbSet<TreatmentRoom> TreatmentRooms {  get; set; }

        public DbSet<Patient> Patients {  get; set; }

        public DbSet<Consultation> Consultations {  get; set; }

        public DbSet<Role> Roles {  get; set; }

        public DbSet<Capability> Capabilities {  get; set; }

        public DbSet<Condition> Conditions {  get; set; }

        public DbSet<Typography> Typography {  get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
