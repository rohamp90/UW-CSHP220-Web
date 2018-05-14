using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearningCenter.Repository;

namespace LearningCenter.Business
{
    public interface IEnrollmentManager
    {
        void Add(int userId, int classId);
        List<EnrollmentModel> GetAll(int userId);

    }

    public class EnrollmentModel
    {
        public int classId { get; set; }
        public int userId { get; set; }
        public decimal classPrice { get; set; }
        public string description { get; set; }
        public string name { get; set; }
    }


    class EnrollmentManager : IEnrollmentManager
    {

        private readonly IEnrollmentRepository enrollmentRepository;

        public EnrollmentManager(IEnrollmentRepository enrollmentRepository)
        {
            this.enrollmentRepository = enrollmentRepository;
        }


        public void Add(int userId, int classId)
        {
            enrollmentRepository.Add(userId, classId);
        }


        // Finding all classes a user is registered for
        public List<EnrollmentModel> GetAll(int userId)
        {
            var all = enrollmentRepository.RegisteredClasses(userId)
                .Select(t => new EnrollmentModel { classId = t.classId, userId = t.userId, classPrice = t.classPrice, description = t.description, name = t.name}).ToList();
            return all;
        }
    }

}

