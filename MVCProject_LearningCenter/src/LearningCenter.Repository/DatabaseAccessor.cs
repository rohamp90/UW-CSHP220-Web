using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearningCenter.ClassDatabase;

namespace LearningCenter.Repository
{
    public class DatabaseAccessor
    {
        private static readonly LearningCenterDbEntities entities;

        static DatabaseAccessor()
        {
            entities = new LearningCenterDbEntities();
            entities.Database.Connection.Open();
        }

        public static LearningCenterDbEntities Instance
        {
            get
            {
                return entities;
            }
        }
    }
}
