using System.ComponentModel.DataAnnotations.Schema;

namespace ApiEfProject.Models
{
    [Table("Developers")]
    public class Developer
    {
        // Constructor
        public Developer(int id, string firstname, string lastname, int roleId, int teamId)
        {
            Id = id;
            Firstname = firstname;
            Lastname = lastname;
            RoleId = roleId;
            TeamId = teamId;
        }

        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public int RoleId { get; set; }
        public int TeamId { get; set; }

        public Role? Role { get; set; }
        public Team? Team { get; set; }
    }
}