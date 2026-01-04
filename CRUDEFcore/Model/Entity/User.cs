using System.ComponentModel.DataAnnotations.Schema;

namespace CRUDEFcore.Model.Entity
{
    public class User
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        [ForeignKey("Role")]
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
