using Domain.Dtos;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Consultations.Commands.BookConsultation
{
    public class BookConsultationCommand : IRequest<ConsultationDto>
    {
        public required string PatientName { get; set; }

        public required Conditions Condition { get; set; }

        public Typographies? Typography { get; set; } = null;

        public int offDays { get; set; }
    }
}
