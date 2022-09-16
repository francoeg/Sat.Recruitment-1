using AutoMapper;
using Sat.Recruitment.Application.EntitesVM;

namespace Sat.Recruitment.Applications.User.Command.InsertUserCommand
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<Domain.User, UserRequest>();
            CreateMap<UserRequest, Domain.User>();
        }
    }
}
