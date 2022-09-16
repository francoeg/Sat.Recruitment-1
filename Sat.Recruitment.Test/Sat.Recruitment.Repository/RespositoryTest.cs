using Sat.Recruitment.Infrastructure.Implementation.UserRepository;
using Xunit;

namespace Sat.Recruitment.Test.Sat.Recruitment.Repository
{
    public class RespositoryTest
    {
        [Fact]
        public  void Should_IsDuplicateUserValidation_True()
        {
            var userTxtRepository = new UserTxtRespository();

            var user = new Domain.User()
            {
                Address = "Av. Juan G",
                Email = "mike@gmail.com",
                Money = 124,
                Name = "Mike",
                Phone = "+349 1122354215",
                UserType = "Normal",
            };

            var newUser = userTxtRepository.CreateUser(user);
            var response = userTxtRepository.IsDuplicateUserValidation(user);

            Assert.True(response);  
        }

        [Fact]
        public void Should_IsDuplicateUserValidation_False()
        {
            var userTxtRepository = new UserTxtRespository();

            var user1 = new Domain.User()
            {
                Address = "Av. Juan G",
                Email = "mike@gmail.com",
                Money = 124,
                Name = "Mike",
                Phone = "+349 1122354215",
                UserType = "Normal",
            };

            var newUser = userTxtRepository.CreateUser(user1);
            var user2 = new Domain.User()
            {
                Address = "Av. Juan Grant",
                Email = "mike2@gmail.com",
                Money = 124,
                Name = "Mikes",
                Phone = "+349 1122354215",
                UserType = "Normal",
            };
            var response = userTxtRepository.IsDuplicateUserValidation(user2);

            Assert.False(response);
        }
    }
}
