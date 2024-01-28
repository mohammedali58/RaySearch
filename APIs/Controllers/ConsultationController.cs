using Application.Consultations.Commands.BookConsultation;
using Domain.Dtos;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ConsultationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ConsultationDto> BookConsultation(BookConsultationCommand command)
        {
            return await _mediator.Send(command);
        }
        
    }
}
