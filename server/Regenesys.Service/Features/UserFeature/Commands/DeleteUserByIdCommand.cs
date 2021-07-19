using MediatR;
using Regenesys.Service.Contract;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Regenesys.Service.Features.UserFeature.Commands
{
    public class DeleteUserByIdCommand : IRequest<bool>
    {
        public Guid UserId { get; set; }
        public class DeleteUserByIdCommandHandler : IRequestHandler<DeleteUserByIdCommand, bool>
        {
            private readonly IUserService _userService;
            public DeleteUserByIdCommandHandler(IUserService userService)
            {
                _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            }
            public async Task<bool> Handle(DeleteUserByIdCommand request, CancellationToken cancellationToken)
            {
                var results = await _userService.DeleteUserAsync(request.UserId);
                return results;
            }
        }
    }
}