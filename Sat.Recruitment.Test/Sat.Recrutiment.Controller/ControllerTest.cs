using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Application.EntitesVM;
using Sat.Recruitment.Application.User.Command.InsertUserCommand;
using Sat.Recruitment.Applications.EntitesVM.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xunit;

namespace Sat.Recruitment.Test.Sat.Recrutiment.Controller
{
    public class ControllerTest
    {
        [Fact]
        public void Should_Return_200Ok_Status_Code()
        {
            var userResponse = new UserResponse() { IsSuccess = true };
            var fakeMediator = new Mock<IMediator>();
            fakeMediator.Setup(m => m.Send(It.IsAny<InsertUserCommandRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(userResponse);

            var userRequestVM = new UserRequestVM()
            {
                Address = "mike@gmail.com",
                Email = "Av. Juan G",
                Money = "124",
                Name = "Mike",
                Phone = "+349 1122354215",
                UserType = "Normal",
            };
            var fakeMapper = new Mock<IMapper>();
            var userController = new UsersController(fakeMediator.Object, fakeMapper.Object);

            var response = userController.CreateUser(userRequestVM);

            OkObjectResult objectResult = Assert.IsType<OkObjectResult>(response.Result);
            var userResponsePost = Assert.IsType<UserResponse>(objectResult.Value);

            Assert.Equal(200, objectResult.StatusCode);
            Assert.NotNull(userResponsePost);
            Assert.True(userResponsePost.IsSuccess);
        }
    }
}
