using AutoMapper;
using MediatR;
using Sat.Recruitment.Applications.Common;
using Sat.Recruitment.Applications.EntitesVM.User;
using Sat.Recruitment.Common.Execptions;
using Sat.Recruitment.Commons.Models;
using Sat.Recruitment.Infrastructure.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sat.Recruitment.Application.User.Command.InsertUserCommand
{
    public class InsertUserHandler : BaseRequestHandler<InsertUserCommandRequest, UserResponse>
    {
        private readonly IUserRespository _userRespository;
        private readonly IMapper _IMapper;

        public InsertUserHandler(IUserRespository userRespository, IMapper autoMapper) : base(autoMapper)
        {
            this._userRespository = userRespository;
            this._IMapper = autoMapper;
        }

        public override async Task<UserResponse> Handle(InsertUserCommandRequest request, CancellationToken cancellationToken)
        {
            Domain.User newUser = GetNewUser(request);
            CreateNewUser(newUser);
            return await Task.FromResult(new UserResponse() { IsSuccess = true });
        }

        private void CreateNewUser(Domain.User newUser)
        {
            if(_userRespository.IsDuplicateUserValidation(newUser))
                throw new BadRequestException("User Dupilcate.");

            var user = _userRespository.CreateUser(newUser);
        }

        private Domain.User GetNewUser(InsertUserCommandRequest request)
        {
            var newUser = _IMapper.Map<Domain.User>(request.Item);
            newUser.Money = CalculeteMoneyByUserType(request.Item.UserType, request.Item.Money);
            newUser.Email = NormalizeEmial(request.Item.Email);
            return newUser;
        }

        private decimal CalculeteMoneyByUserType(string userType, string money) 
        {

                if (userType.ToLower() == UserTypesConstants.Normal.ToLower())
                {
                    if (decimal.Parse(money) > 100)
                       return CaluculateMoneyByPorcentage(money , 0.12);
                
                    if (decimal.Parse(money) < 100 && decimal.Parse(money) > 10)
                        return CaluculateMoneyByPorcentage(money, 0.8);
                
                }
                if (userType.ToLower() == UserTypesConstants.SuperUser.ToLower() && decimal.Parse(money) > 100)
                         return CaluculateMoneyByPorcentage(money, 0.2);

                if (userType.ToLower() == UserTypesConstants.Premium.ToLower() && decimal.Parse(money) > 100)
                             return CaluculateMoneyByPorcentage(money, 2);

            return decimal.Parse(money);
        }

        private static decimal CaluculateMoneyByPorcentage(string money, double percentage)
        {
            var percentageDecimal = Convert.ToDecimal(percentage);
            var gif = decimal.Parse(money) * percentageDecimal;
            return decimal.Parse(money) + gif;
        }

        private string NormalizeEmial(string email)
        {
            var aux = email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);

            var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);

            aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);

            email = string.Join("@", new string[] { aux[0], aux[1] });

            return email;
        }

    }
}
