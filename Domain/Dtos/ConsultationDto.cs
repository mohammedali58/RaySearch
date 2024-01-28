namespace Domain.Dtos
{
    public class ConsultationDto
    {
        public required Guid Id { get; set; }
        public required string PatientName { get; set; }

        public required string DoctorName { get; set; }

        public required string TreatmentRoom {  get; set; }

        public required DateTime RegistrationDate { get; set; }

        public required DateTime ConsultationDate { get; set; } 
    }
}
