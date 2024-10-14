using System.ComponentModel.DataAnnotations.Schema;

namespace ApiEfProject.Models
{
    [Table("Teams")]
    public class Team
    {
        // Constructor
        public Team(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Developer>? Developers { get; set; }
        public ICollection<Project>? Projects { get; set; }
    }
}