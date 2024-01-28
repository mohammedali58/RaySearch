using APIs.Controllers;
using Application.Consultations.Commands.BookConsultation;
using AutoFixture.NUnit3;
using Domain.Dtos;
using FluentAssertions;
using MediatR;
using Moq;
using UnitTests.AutoFixure;

namespace UnitTesting.ConsultationTests
{
    public class ConsultationControllerTests
    {
        [Theory]
        [AutoMockData]
        public async Task BookConsultation_ValidData_Success(BookConsultationCommand command, ConsultationDto result,
        [Frozen] Mock<IMediator> mediator,
          [Greedy] ConsultationController ConsultationController)
        {
            
            //Arrange
            mediator.Setup(pa => pa.Send(It.IsAny<BookConsultationCommand>(), new CancellationToken())).ReturnsAsync(result);

            //Act
            var response = await ConsultationController.BookConsultation(command);

            //Assert
            response.Should().BeEquivalentTo(result);

        }

        [Theory]
        [AutoMockData]
        public async Task BookConsultation_ValidData_Exception(BookConsultationCommand command, List<ConsultationDto> result,
         [Frozen] Mock<IMediator> mediator,
         [Greedy] ConsultationController ConsultationController)
        {

            //Arrange
            mediator.Setup(pa => pa.Send(It.IsAny<BookConsultationCommand>(), new CancellationToken())).ThrowsAsync(new Exception());


            //Act
            Func<Task> func = async () =>  await ConsultationController.BookConsultation(command);


            //Assert
            await func.Should().ThrowAsync<Exception>();

        }

    }
}
