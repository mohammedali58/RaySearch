using Application.Common.Interfaces;
using Domain.Dtos;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Infrastructure.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using SQLitePCL;

namespace Application.Consultations.Commands.BookConsultation
{
    public class BookConsultationCommandHandler : IRequestHandler<BookConsultationCommand, ConsultationDto>
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly ITreatmentRoomRepository _treatmentRoomRepository;
        private readonly IConsultationManager _consultationManager;
        private readonly IConditionRepository _conditionRepository;
        private readonly ITypographyRepository _typographyRepository;



        public BookConsultationCommandHandler(IPatientRepository patientRepository,
            IConsultationRepository consultationRepository,
            IDoctorRepository doctorRepository,
            ITreatmentRoomRepository treatmentRoomRepository,
            IConsultationManager consultationManager,
            IConditionRepository conditionRepository,
            ITypographyRepository typographyRepository
            )
        {
            _patientRepository = patientRepository;
            _doctorRepository = doctorRepository;
            _treatmentRoomRepository = treatmentRoomRepository;
            _consultationManager = consultationManager;
            _conditionRepository = conditionRepository;
            _typographyRepository = typographyRepository;
        }

        public async Task<ConsultationDto> Handle(BookConsultationCommand request, CancellationToken cancellationToken)
        {
           ArgumentNullException.ThrowIfNull(nameof(request));

            if(request.Condition == Conditions.Flu && request.Typography != null)
            {
                throw new HttpException(StatusCodes.Status400BadRequest, "typography must be null in case of Flu condition");
            }

            if (request.Condition == Conditions.Cancer && request.Typography == null)
            {
                throw new HttpException(StatusCodes.Status400BadRequest, "typography must not be null in case of Cancer condition");
            }

            Condition condition = await _conditionRepository.GetConditionById(request.Condition, cancellationToken);

            Typography? typography = request.Typography == null ? null : await _typographyRepository.GetTypographyById(request.Typography, cancellationToken);

            List<Doctor> doctors = await _doctorRepository.GetDoctorForCondition(request.Condition, cancellationToken);

            List<TreatmentRoom> rooms = await _treatmentRoomRepository.GetTreatmentRoomForConditionAndTypography(request.Condition,
                request.Typography, cancellationToken);


            Patient patient = await _patientRepository.CreatePatient(request.PatientName, condition, typography, cancellationToken);

            return await _consultationManager.BookConsultation(patient, doctors, rooms, request.offDays, cancellationToken);
           
        }
    }
}
