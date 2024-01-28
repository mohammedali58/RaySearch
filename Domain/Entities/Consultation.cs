namespace Domain.Entities
{
    public  class Consultation : BaseEntity
    {
        public new Guid Id { get; set; }

        public required Doctor Doctor { get; set; }

        public required Patient Patient { get; set; }

        public required TreatmentRoom TreatmentRoom { get; set; }

        public DateTime RegistrationDate { get; set; } = DateTime.Now;

        public required DateTime ConsultationDate { get; set; }

    }
}
