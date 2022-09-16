

using Sat.Recruitment.Common.Execptions;
using Sat.Recruitment.Domain;
using Sat.Recruitment.Infrastructure.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Sat.Recruitment.Infrastructure.Implementation.UserRepository
{
    public  class UserTxtRespository : IUserRespository
    {

        public List<User> UserList { get; set; } = new List<User>();
        public UserTxtRespository()
        {
            UserList = GetUsers();
        }

        public User CreateUser(User user)
        {  
            AddUser(user);  
            return user;
        }

        public bool IsDuplicateUserValidation(User user) 
        {
            var userlist = GetUsers();

            bool Predicate(User x) =>
                 ( (x.Name.ToLower() == user.Name.ToLower()
                    && x.Address.ToLower() == user.Address.ToLower())
                    ||
                     (x.Email == user.Email
                           && x.Phone == user.Phone)
                    );
            var userListFilter = userlist.Where(Predicate).ToList();
            
            if (userListFilter.Count() > 0 ) return true;

            return false;
        }


        private bool AddUser(User newUser) {
            try
            {
                if (UserList?.Count == 0)  UserList = GetUsers();
                UserList.Add(newUser);
            }
            catch (System.Exception ex)
            {
                throw new NotFoundException("user",ex.Message);
            }
        return true;
        } 

        private List<User> GetUsers() 
        {
            if (UserList.Count > 0) return UserList ;

            var reader = ReadUsersFromFile();

            var userlist = new List<User>();

            while (reader.Peek() >= 0)
            {
                var line = reader.ReadLineAsync().Result;
                var userArray = new User
                {
                    Name = line.Split(',')[0].ToString(),
                    Email = line.Split(',')[1].ToString(),
                    Phone = line.Split(',')[2].ToString(),
                    Address = line.Split(',')[3].ToString(),
                    UserType = line.Split(',')[4].ToString(),
                    Money = decimal.Parse(line.Split(',')[5].ToString()),
                };
                userlist.Add(userArray);
            }

            reader.Close();
             return userlist.Count==0 ? new List<User>() : userlist;   
        }


        private StreamReader ReadUsersFromFile()
        {
            var path = Directory.GetCurrentDirectory() + "/Files/Users.txt";

            FileStream fileStream = new FileStream(path, FileMode.Open);

            StreamReader reader = new StreamReader(fileStream);
            return reader;
        }
    }
}
