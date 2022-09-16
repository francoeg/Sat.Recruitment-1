
using Sat.Recruitment.Domain;
using System;

namespace Sat.Recruitment.Infrastructure.Interfaces
{
    public interface IUserRespository
    {
        User CreateUser(User user);
        bool IsDuplicateUserValidation(User user);
    }
}
