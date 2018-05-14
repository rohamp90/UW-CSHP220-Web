using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearningCenter.Repository;

namespace LearningCenter.Repository
{
    public interface IUserRepository
    {
        UserModel LogIn(string email, string password);
        UserModel Register(string email, string password);

    }

    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class UserRepository : IUserRepository
    {

         private readonly IClassRepository classRepository;

        public UserRepository(IClassRepository classRepository)
        {
            this.classRepository = classRepository;
        }

        public UserModel LogIn(string email, string password)
        {
            var user = DatabaseAccessor.Instance.Users
                .FirstOrDefault(t => t.UserEmail.ToLower() == email.ToLower()
                                      && t.UserPassword == password);

            if (user == null)
            {
                return null;
            }

            return new UserModel { Id = user.UserId, Name = user.UserEmail };
        }


        public UserModel Register(string email, string password)
        {
            if (DatabaseAccessor.Instance.Users.FirstOrDefault(t => t.UserEmail.ToLower() == email.ToLower()) != null)
            {
                return null;
            }
            else
            {
                var user = DatabaseAccessor.Instance.Users
                        .Add(new LearningCenter.ClassDatabase.User { UserEmail = email, UserPassword = password });

                DatabaseAccessor.Instance.SaveChanges();

                return new UserModel { Id = user.UserId, Name = user.UserEmail };
            }
        }

        


    }
}
