using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Regenesys.Domain.Dtos;
using Regenesys.Service.Features.UserFeature.Commands;
using Regenesys.Service.Features.UserFeature.Queries;
using System;
using System.Threading.Tasks;

namespace Regenesys.Controller
{
    [ApiController]
    [Route("api/v{version:apiVersion}/users")]
    [ApiVersion("1.0")]
    public class UsersController : ControllerBase
    {
        private IMediator _mediator;
        public IMediator Mediator
        {
            get { return _mediator ??= HttpContext.RequestServices.GetService<IMediator>(); }
            set
            {
                if (_mediator != null) throw new InvalidOperationException("Mediator is already set");
                _mediator = value;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserDto userDto)
        {
            CreateUserCommand command = new()
            {
                UserDto = userDto
            };
            return Ok(await Mediator.Send(command));
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetAllUsersQuery()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await Mediator.Send(new GetUserByIdQuery { UserId = id }));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UserDto userDto)
        {
            UpdateUserCommand command = new()
            {
                UserDto = userDto
            };
            if (command.UserDto.UserId == Guid.Empty)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await Mediator.Send(new DeleteUserByIdCommand { UserId = id }));
        }
    }
}
