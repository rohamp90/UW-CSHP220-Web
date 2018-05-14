using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningCenter.Repository
{
    public interface IEnrollmentRepository
    {
        void Add(int userId, int classId);
         List<EnrollmentModel> RegisteredClasses(int userId);

    }

    public class EnrollmentModel
        {
        public int classId { get; set; }
        public int userId { get; set; }
        public decimal classPrice { get; set; }
        public string description { get; set; }
        public string name { get; set; }
    }



    class EnrollmentRepository : IEnrollmentRepository
    {
        List<EnrollmentModel> classlist;

        // Adding a class for a user
        public void Add(int userId, int classId)
        {
            var User = DatabaseAccessor.Instance.Users
                                                   .Where(t => t.UserId == userId)
                                                   .First();

            var Class = DatabaseAccessor.Instance.Classes
                                                   .Where(t => t.ClassId == classId)
                                                   .First();

            User.Classes.Add(Class);

            DatabaseAccessor.Instance.SaveChanges();
        }

        // Finding all classes a user is registered for
        public List<EnrollmentModel> RegisteredClasses(int userId)
        {
            classlist = new List<EnrollmentModel>();

            var User = DatabaseAccessor.Instance.Users
                                                   .Where(t => t.UserId == userId)
                                                   .First();

          foreach ( var classToFind in User.Classes)
            {

                classlist.Add(new EnrollmentModel { userId = User.UserId, classId = classToFind.ClassId, classPrice = classToFind.ClassPrice, description = classToFind.ClassDescription, name = classToFind.ClassName });
            }

            return classlist;
        }
    }
}
