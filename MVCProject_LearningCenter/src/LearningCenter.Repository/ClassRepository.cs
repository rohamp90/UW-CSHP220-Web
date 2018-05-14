using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningCenter.Repository
{
    public interface IClassRepository
    {
        ClassModel[] Classes { get; }
        ClassModel getClass(int classId);
    }

    public class ClassModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

    }

    public class ClassRepository : IClassRepository
    {
        public ClassModel[] Classes
        {
            get
            {
                return DatabaseAccessor.Instance.Classes
                                               .Select(t => new ClassModel { Id = t.ClassId, Name = t.ClassName, Description = t.ClassDescription, Price = t.ClassPrice })
                                               .ToArray();
            }
        }

        public  ClassModel getClass(int classId)
        {
            var Class = DatabaseAccessor.Instance.Classes
                                                   .Where(t => t.ClassId == classId)
                                                   .Select(t => new ClassModel { Id = t.ClassId, Name = t.ClassName, Description = t.ClassDescription, Price = t.ClassPrice })
                                                   .First();
            return Class;
        }
    }
}