using System.ComponentModel.DataAnnotations.Schema;

namespace ApiEfProject.Models
{
    [Table("Projects")]
    public class Project
    {
        // Constructor
        public Project(int id, string name, int projectTypeId, int teamId)
        {
            Id = id;
            Name = name;
            ProjectTypeId = projectTypeId;
            TeamId = teamId;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int ProjectTypeId { get; set; }
        public int TeamId { get; set; }

        public ProjectType? ProjectType { get; set; }
        public Team? Team { get; set; }
    }
}