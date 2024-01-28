using Domain.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContextInitializer
    {
        private readonly ILogger<ApplicationDbContextInitializer> _logger;
        private readonly ApplicationDbContext _context;

        public ApplicationDbContextInitializer(ILogger<ApplicationDbContextInitializer> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task InitializeAsync()
        {
            try
            {
                if (_context.Database.IsSqlite())
                {
                   await _context.Database.MigrateAsync();
                }

            }catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while initializing the database.");
                throw;
            }
        }

        public async Task SeedDataAsync()
        {
            try
            {
                List<Condition>? conditions = default;

                List<RoleInput>? rolesInput = default;
                List<DoctorInput>? doctorsInput = default;

                List<Capability>? capabilities = default;
                List<TreatmentMachineInput>? treatmentMachinesInput = default;
                List<TreatmentRoomInput>? treatmentRoomInputs = default;

                List<Typography>? typographies = default;


                if (!_context.Conditions.Any())
                {
                    var conditionsTxt = File.ReadAllText("../Infrastructure/SeedingData/Conditions.json");
                    conditions = JsonSerializer.Deserialize<List<Condition>>(conditionsTxt);
                    if (conditions is null)
                    {
                        throw new Exception("conditions data seeding is null");
                    }

                    _context.AddRange(conditions);
                    _context.SaveChanges();
                }

                if (!_context.Roles.Any())
                {
                    var rolesTxt = File.ReadAllText("../Infrastructure/SeedingData/Roles.json");
                    rolesInput = JsonSerializer.Deserialize<List<RoleInput>>(rolesTxt);
                    if (rolesInput is null)
                    {
                        throw new Exception("roles data seeding is null");
                    }

                    List<Role> roles = new();

                    foreach (var role in rolesInput)
                    {
                        Condition condition = _context.Conditions.FirstOrDefault(c => c.Id == role.ConditionId)!;
                        Role newRole = new()
                        {
                            Name = role.Name,
                            Condition = condition,
                        };
                        roles.Add(newRole);
                    }

                    _context.AddRange(roles);
                    _context.SaveChanges();
                }

              if(!_context.Doctors.Any())
                {
                    var doctorsTxt = File.ReadAllText("../Infrastructure/SeedingData/Doctors.json");
                    doctorsInput = JsonSerializer.Deserialize<List<DoctorInput>>(doctorsTxt);
                    if (doctorsInput is null)
                    {
                        throw new Exception("doctors data seeding is null");
                    }

                    List<Doctor> doctors = new();

                    foreach(var doctor in doctorsInput)
                    {

                        List<Role> doctorRoles = new();
                        foreach(var roleId in doctor.RolesId)
                        {
                            Role? newRole = _context.Roles.FirstOrDefault(r => r.Id == roleId);
                            doctorRoles.Add(newRole!);
                        }

                        Doctor newDoctor = new() { Name = doctor.Name, Roles = doctorRoles };
                        doctors.Add(newDoctor);
                    };

                    _context.AddRange(doctors); 
                    _context.SaveChanges();
                }


              if(!_context.Capabilities.Any())
                {
                    var capabilitesTxt = File.ReadAllText("../Infrastructure/SeedingData/Capabilites.json");
                    capabilities = JsonSerializer.Deserialize<List<Capability>>(capabilitesTxt);
                    if (capabilities is null)
                    {
                        throw new Exception("capabilities data seeding is null");
                    }
                    _context.AddRange(capabilities);
                    _context.SaveChanges();
                }

              if(!_context.TreatmentMachines.Any())
                {
                    var treatmentMachinesTxt = File.ReadAllText("../Infrastructure/SeedingData/TreatmentMachines.json");
                    treatmentMachinesInput = JsonSerializer.Deserialize<List<TreatmentMachineInput>>(treatmentMachinesTxt);
                    if (treatmentMachinesInput is null)
                    {
                        throw new Exception("treatmentMachines data seeding is null");
                    }
                    List<TreatmentMachine> machines = new();
                    foreach(var machine in treatmentMachinesInput)
                    {
                        Capability? capability = _context.Capabilities.FirstOrDefault(m => m.Id == machine.CapabilityId);
                        TreatmentMachine newMachine = new() {Name=machine.Name , Capability = capability! };
                        machines.Add(newMachine);
                    }

                    _context.AddRange(machines);
                    _context.SaveChanges();
                }

                if (!_context.TreatmentRooms.Any())
                {
                    var treatmentRoomInputTxt = File.ReadAllText("../Infrastructure/SeedingData/Rooms.json");
                    treatmentRoomInputs = JsonSerializer.Deserialize<List<TreatmentRoomInput>>(treatmentRoomInputTxt);
                    if (treatmentRoomInputs is null)
                    {
                        throw new Exception("treatmentRoomInputs data seeding is null");
                    }
                    List<TreatmentRoom> rooms = new();
                    foreach (var room in treatmentRoomInputs)
                    {
                        TreatmentMachine? machine = _context.TreatmentMachines.FirstOrDefault(m => m.Id == room.TreatmentMachineId);
                        TreatmentRoom newRoom = new() { Name = room.Name , TreatmentMachine = machine! };
                        rooms.Add(newRoom);
                    }
                    _context.AddRange(rooms);
                    _context.SaveChanges();
                }



                if (!_context.Typography.Any())
                {
                    var typographiesTxt = File.ReadAllText("../Infrastructure/SeedingData/Typographies.json");
                    typographies = JsonSerializer.Deserialize<List<Typography>>(typographiesTxt);
                    if (typographies is null)
                    {
                        throw new Exception("typographies data seeding is null");
                    }

                    _context.AddRange(typographies);
                    _context.SaveChanges();
                }
          
                    if(_context.ChangeTracker.HasChanges())
                    {
                        await _context.SaveChangesAsync();
                    }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while seeding data.");
            }
        }
    }
}
