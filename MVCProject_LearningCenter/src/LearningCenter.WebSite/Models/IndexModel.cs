using System.Collections.Generic;

namespace LearningCenter.WebSite.Models
{
    public class IndexModel
    {
        public UserModel user { get; set; }
        public ClassModel[] classlist { get; set; }

        public string classToAdd { get; set; }
        public List<EnrollmentModel> registeredClasses { get; set; }
    }
}