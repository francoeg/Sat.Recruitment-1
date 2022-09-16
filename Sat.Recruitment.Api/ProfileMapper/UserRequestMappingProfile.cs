using AutoMapper;
using Sat.Recruitment.Application.EntitesVM;

namespace Sat.Recruitment.Api.ProfileMapper
{
    public class UserRequestMappingProfile : Profile
    {
        public UserRequestMappingProfile()
        {
            CreateMap<UserRequestVM, UserRequest>();
            CreateMap<UserRequest, UserRequestVM>();
        }
    }
}
