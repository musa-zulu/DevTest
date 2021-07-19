using MediatR;
using Regenesys.Domain.Entities;
using Regenesys.Service.Contract;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Regenesys.Service.Features.UserFeature.Queries
{
    public class GetAllUsersQuery : IRequest<IEnumerable<User>>
    {
        public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<User>>
        {
            private readonly IUserService _userService;
            public GetAllUsersQueryHandler(IUserService userService)
            {
                _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            }
            public async Task<IEnumerable<User>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
            {
                var users = await _userService.GetUsersAsync();
                if (users == null)
                {
                    return null;
                }
                return users.AsReadOnly();
            }
        }
    }
}
