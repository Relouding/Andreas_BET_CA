using System.ComponentModel.DataAnnotations.Schema;

namespace ApiEfProject.Models
{
    [Table("Roles")]
    public class Role
    {
        // Constructor
        public Role(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Developer>? Developers { get; set; }
    }
}