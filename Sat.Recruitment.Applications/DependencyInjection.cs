
using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Sat.Recruitment.Application.User.Command.InsertUserCommand;
using Sat.Recruitment.Infrastructure.Implementation.UserRepository;
using Sat.Recruitment.Infrastructure.Interfaces;

namespace Sat.Recruitment.Applications
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSatRecruitmentApplications(this IServiceCollection services)
        {
            services.AddMediatR(typeof(InsertUserHandler));
            services.AddTransient<IUserRespository, UserTxtRespository>();
            return services;
        }
    }
}
