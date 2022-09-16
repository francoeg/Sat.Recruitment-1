using Sat.Recruitment.Application.EntitesVM;
using Sat.Recruitment.Applications.EntitesVM.User;
using Sat.Recruitment.Common.Models;

namespace Sat.Recruitment.Application.User.Command.InsertUserCommand
{
    public class InsertUserCommandRequest : CommandRequest<UserRequest, UserResponse>
    {
        public InsertUserCommandRequest(UserRequest item) : base(item)
        {
        }
    }
}
