using AutoMapper;
using Regenesys.Domain.Dtos;
using Regenesys.Domain.Entities;

namespace Regenesys.Infrastructure.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserDto, User>().ReverseMap();
        }
    }
}
