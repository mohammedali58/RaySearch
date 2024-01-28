using Domain.Dtos;
using Moq;
using UnitTests.AutoFixure;
using AutoFixture.NUnit3;
using FluentAssertions;
using Infrastructure.Interfaces;
using Domain.Entities;
using Application.Consultations.Commands.BookConsultation;
using Application.Common.Interfaces;
using Domain.Enums;

namespace UnitTesting.CoursesTests.Commands
{
    public class BookConsultationCommandTests
    {

        [Theory]
        [AutoMockData]
        public async Task BookConsultation_Flu_NoTypography_Handle_Success(BookConsultationCommand request,
         [Frozen] Mock<IPatientRepository> patientRepository,
         [Frozen] Mock<IDoctorRepository> doctorRepository,
         [Frozen] Mock<ITreatmentRoomRepository> treatmentRoomRepository,
         [Frozen] Mock<IConsultationManager> consultationManager,
         [Frozen] Mock<IConditionRepository> conditionRepository,
         [Frozen] Mock<ITypographyRepository> typographyRepository,
         ConsultationDto consultation,
         string typographyName,
         string conditionName,
         string patientName,
       [Greedy] BookConsultationCommandHandler handler)
        {
            Typography typography = new Typography() { Name = typographyName };
            Condition condition = new Condition() { Name = conditionName };
            List<Doctor> doctors = new List<Doctor>();
            List<TreatmentRoom> rooms = new List<TreatmentRoom>();
            Patient patient = new Patient() { Condition = condition, Name = patientName };

            //Arrange
            request.Condition = Conditions.Flu;
            request.Typography = null;
            conditionRepository.Setup(pa => pa.GetConditionById(It.IsAny<Conditions>(), new CancellationToken())).ReturnsAsync(condition);
            typographyRepository.Setup(pa => pa.GetTypographyById(It.IsAny<Typographies>(), new CancellationToken())).ReturnsAsync(typography);
            doctorRepository.Setup(pa => pa.GetDoctorForCondition(It.IsAny<Conditions>(), new CancellationToken())).ReturnsAsync(doctors);
            treatmentRoomRepository.Setup(pa => pa.GetTreatmentRoomForConditionAndTypography(It.IsAny<Conditions>(), null, new CancellationToken())).ReturnsAsync(rooms);
            patientRepository.Setup(pa => pa.CreatePatient(It.IsAny<string>(), It.IsAny<Condition>(), It.IsAny<Typography>(), new CancellationToken())).ReturnsAsync(patient);
            consultationManager.Setup(pa => pa.BookConsultation(
                It.IsAny<Patient>(),
                It.IsAny<List<Doctor>>(),
                It.IsAny<List<TreatmentRoom>>(),
                It.IsAny<int>(),
                new CancellationToken()
                )).ReturnsAsync(consultation);

            //Act
            var response = await handler.Handle(request, CancellationToken.None);

            //Assert
            response.Should().BeEquivalentTo(consultation);


        }

        [Theory]
        [AutoMockData]
        public async Task BookConsultation_Cancer_WithTypography_Handle_Success(BookConsultationCommand request,
            [Frozen] Mock<IPatientRepository> patientRepository,
            [Frozen] Mock<IDoctorRepository> doctorRepository,
            [Frozen] Mock<ITreatmentRoomRepository> treatmentRoomRepository,
            [Frozen] Mock<IConsultationManager> consultationManager,
            [Frozen] Mock<IConditionRepository> conditionRepository,
            [Frozen] Mock<ITypographyRepository> typographyRepository,
            ConsultationDto consultation,
            string typographyName,
            string conditionName,
            string patientName,
       [Greedy] BookConsultationCommandHandler handler)
        {
            Typography typography = new Typography() { Name = typographyName };
            Condition condition = new Condition() { Name = conditionName };
            List<Doctor> doctors = new List<Doctor>();
            List<TreatmentRoom> rooms = new List<TreatmentRoom>();
            Patient patient = new Patient() { Condition = condition, Name = patientName };

            //Arrange
            request.Condition = Conditions.Cancer;
            conditionRepository.Setup(pa => pa.GetConditionById(It.IsAny<Conditions>(), new CancellationToken())).ReturnsAsync(condition);
            typographyRepository.Setup(pa => pa.GetTypographyById(It.IsAny<Typographies>(), new CancellationToken())).ReturnsAsync(typography);
            doctorRepository.Setup(pa => pa.GetDoctorForCondition(It.IsAny<Conditions>(), new CancellationToken())).ReturnsAsync(doctors);
            treatmentRoomRepository.Setup(pa => pa.GetTreatmentRoomForConditionAndTypography(It.IsAny<Conditions>(), It.IsAny<Typographies>(), new CancellationToken())).ReturnsAsync(rooms);
            patientRepository.Setup(pa => pa.CreatePatient(It.IsAny<string>(), It.IsAny<Condition>(), It.IsAny<Typography>(), new CancellationToken())).ReturnsAsync(patient);
            consultationManager.Setup(pa => pa.BookConsultation(
                It.IsAny<Patient>(),
                It.IsAny<List<Doctor>>(),
                It.IsAny<List<TreatmentRoom>>(),
                It.IsAny<int>(),
                new CancellationToken()
                )).ReturnsAsync(consultation);

            //Act
            var response = await handler.Handle(request, CancellationToken.None);

            //Assert
            response.Should().BeEquivalentTo(consultation);


        }


        [Theory]
        [AutoMockData]
        public async Task BookConsultation_Flu_NoTypography_Handle_Exception(BookConsultationCommand request,
            [Frozen] Mock<IPatientRepository> patientRepository,
            [Frozen] Mock<IDoctorRepository> doctorRepository,
            [Frozen] Mock<ITreatmentRoomRepository> treatmentRoomRepository,
            [Frozen] Mock<IConsultationManager> consultationManager,
            [Frozen] Mock<IConditionRepository> conditionRepository,
            [Frozen] Mock<ITypographyRepository> typographyRepository,
            ConsultationDto consultation,
            string typographyName,
            string conditionName,
            string patientName,
       [Greedy] BookConsultationCommandHandler handler)
        {
            Typography typography = new Typography() { Name = typographyName };
            Condition condition = new Condition() { Name = conditionName };
            List<Doctor> doctors = new List<Doctor>();
            List<TreatmentRoom> rooms = new List<TreatmentRoom>();
            Patient patient = new Patient() { Condition = condition, Name = patientName };

            //Arrange
            request.Condition = Conditions.Cancer;
            request.Typography = null;
            conditionRepository.Setup(pa => pa.GetConditionById(It.IsAny<Conditions>(), new CancellationToken())).ReturnsAsync(condition);
            typographyRepository.Setup(pa => pa.GetTypographyById(It.IsAny<Typographies>(), new CancellationToken())).ReturnsAsync(typography);
            doctorRepository.Setup(pa => pa.GetDoctorForCondition(It.IsAny<Conditions>(), new CancellationToken())).ReturnsAsync(doctors);
            treatmentRoomRepository.Setup(pa => pa.GetTreatmentRoomForConditionAndTypography(It.IsAny<Conditions>(), null, new CancellationToken())).ReturnsAsync(rooms);
            patientRepository.Setup(pa => pa.CreatePatient(It.IsAny<string>(), It.IsAny<Condition>(), It.IsAny<Typography>(), new CancellationToken())).ReturnsAsync(patient);
            consultationManager.Setup(pa => pa.BookConsultation(
                  It.IsAny<Patient>(),
                It.IsAny<List<Doctor>>(),
                It.IsAny<List<TreatmentRoom>>(),
                It.IsAny<int>(),
                new CancellationToken()
                )).ReturnsAsync(consultation);

            //Act
            Func<Task> func = async () => await handler.Handle(request, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<Exception>();


        }

        [Theory]
        [AutoMockData]
        public async Task BookConsultation_Cancer_WithTypography_Handle_Exception(BookConsultationCommand request,
            [Frozen] Mock<IPatientRepository> patientRepository,
            [Frozen] Mock<IDoctorRepository> doctorRepository,
            [Frozen] Mock<ITreatmentRoomRepository> treatmentRoomRepository,
            [Frozen] Mock<IConsultationManager> consultationManager,
            [Frozen] Mock<IConditionRepository> conditionRepository,
            [Frozen] Mock<ITypographyRepository> typographyRepository,

         ConsultationDto consultation,
       string typographyName,
       string conditionName,
       string patientName,
       [Greedy] BookConsultationCommandHandler handler)
        {
            Typography typography = new Typography() { Name = typographyName };
            Condition condition = new Condition() { Name = conditionName };
            List<Doctor> doctors = new List<Doctor>();
            List<TreatmentRoom> rooms = new List<TreatmentRoom>();
            Patient patient = new Patient() { Condition = condition, Name = patientName };

            //Arrange
            request.Condition = Conditions.Flu;
            conditionRepository.Setup(pa => pa.GetConditionById(It.IsAny<Conditions>(), new CancellationToken())).ReturnsAsync(condition);
            typographyRepository.Setup(pa => pa.GetTypographyById(It.IsAny<Typographies>(), new CancellationToken())).ReturnsAsync(typography);
            doctorRepository.Setup(pa => pa.GetDoctorForCondition(It.IsAny<Conditions>(), new CancellationToken())).ReturnsAsync(doctors);
            treatmentRoomRepository.Setup(pa => pa.GetTreatmentRoomForConditionAndTypography(It.IsAny<Conditions>(), It.IsAny<Typographies>(), new CancellationToken())).ReturnsAsync(rooms);
            patientRepository.Setup(pa => pa.CreatePatient(It.IsAny<string>(), It.IsAny<Condition>(), It.IsAny<Typography>(), new CancellationToken())).ReturnsAsync(patient);
            consultationManager.Setup(pa => pa.BookConsultation(
                  It.IsAny<Patient>(),
                It.IsAny<List<Doctor>>(),
                It.IsAny<List<TreatmentRoom>>(),
                It.IsAny<int>(),
                new CancellationToken()
                )).ReturnsAsync(consultation);

            //Act
            Func<Task> func = async () => await handler.Handle(request, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<Exception>();


        }

    }
}
