using AutoMapper;
using Moq;
using Sat.Recruitment.Application.EntitesVM;
using Sat.Recruitment.Application.User.Command.InsertUserCommand;
using Sat.Recruitment.Domain;
using Sat.Recruitment.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sat.Recruitment.Test.Command
{
    public class CommandTest
    {
        [Fact]
        public async void Should_create_User()
        {
            var userRequest = new UserRequest() 
            {
                Address = "Av. Juan G",
                Email = "mike@gmail.com",
                Money = "124",
                Name = "Mike",
                Phone = "+349 1122354215",
                UserType = "Normal",
            };
            var command = new InsertUserCommandRequest(userRequest);

            var user = new User()
            {
                Address = "Av. Juan G",
                Email = "mike@gmail.com",
                Money = 124,
                Name = "Mike",
                Phone = "+349 1122354215",
                UserType = "Normal",
            };
            var fakeMapper = new Mock<IMapper>();
            fakeMapper.Setup(m =>
                m.Map<User>(It.IsAny<UserRequest>()))
                .Returns(user);

            var newUser = new User()
            {
                Address = "Av. Juan G",
                Email = "mike@gmail.com",
                Money = 138.88M,
                Name = "Mike",
                Phone = "+349 1122354215",
                UserType = "Normal",
            };

            var fakeRepo = new Mock<IUserRespository>();
            fakeRepo.Setup(m => m.CreateUser(It.IsAny<User>()))
                .Returns(newUser);

            var res = await new InsertUserHandler(fakeRepo.Object,
                                 fakeMapper.Object).Handle(command,default);

            Assert.True( res.IsSuccess);
        }
    }
}
