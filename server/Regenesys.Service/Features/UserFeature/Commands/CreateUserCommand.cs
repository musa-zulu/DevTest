using AutoMapper;
using MediatR;
using Regenesys.Domain.Dtos;
using Regenesys.Domain.Entities;
using Regenesys.Service.Contract;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Regenesys.Service.Features.UserFeature.Commands
{
    public class CreateUserCommand : IRequest<bool>
    {
        public UserDto UserDto { get; set; }

        public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, bool>
        {
            private readonly IUserService _userService;
            private readonly IMapper _mapper;
            public CreateUserCommandHandler(IUserService userService, IMapper mapper)
            {
                _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
                _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            }
            public async Task<bool> Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {
                var user = _mapper.Map<User>(request.UserDto);
                user.UserId = Guid.NewGuid();
                var userSaved = false;
                if (user != null)
                {
                    userSaved = await _userService.CreateUserAsync(user);
                }
                return userSaved;
            }
        }
    }
}