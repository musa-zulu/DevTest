using MediatR;
using Regenesys.Domain.Entities;
using Regenesys.Service.Contract;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Regenesys.Service.Features.UserFeature.Queries
{
    public class GetUserByIdQuery : IRequest<User>
    {
        public Guid UserId { get; set; }
        public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, User>
        {
            private readonly IUserService _userService;
            public GetUserByIdQueryHandler(IUserService userService)
            {
                _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            }
            public async Task<User> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
            {
                var user = _userService.GetUserByIdAsync(request.UserId);
                if (user == null) return null;
                return await user;
            }
        }
    }
}
