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
    public class UpdateUserCommand : IRequest<bool>
    {
        public UserDto UserDto { get; set; }

        public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
        {
            private readonly IUserService _userService;
            private readonly IMapper _mapper;
            public UpdateUserCommandHandler(IUserService userService, IMapper mapper)
            {
                _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
                _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            }
            public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
            {
                var user = _mapper.Map<User>(request.UserDto);
                var isSaved = false;
                if (user != null)
                {
                    isSaved = await _userService.UpdateUserAsync(user);
                }
                return isSaved;
            }
        }
    }
}