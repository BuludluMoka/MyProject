using MyProject.Domain.Entities.Common;

namespace MyProject.Domain.Entities
{
    public class Make : BaseEntity
    {
        public string MakeName { get; set; }
        public ICollection<Model> Models { get; set; }
    }
}
