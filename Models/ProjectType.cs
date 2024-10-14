using System.ComponentModel.DataAnnotations.Schema;

namespace ApiEfProject.Models
{
    [Table("ProjectTypes")]
    public class ProjectType
    {
        // Constructor
        public ProjectType(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Project>? Projects { get; set; }
    }
}