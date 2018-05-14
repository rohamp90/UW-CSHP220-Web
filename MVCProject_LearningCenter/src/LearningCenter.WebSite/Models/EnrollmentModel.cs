using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearningCenter.WebSite.Models
{

        public class EnrollmentModel
        {
        public int ClassId { get; set; }
        public int UserId { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }


        public EnrollmentModel( int classId, string name, decimal price, string description )
        {
            ClassId = classId;
            Name = name;
            Price = price;
            Description = description;
        }
        }

}