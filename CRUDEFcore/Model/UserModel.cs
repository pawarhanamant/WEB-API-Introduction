using System.ComponentModel.DataAnnotations;

namespace CRUDEFcore.Model
{
    public class UserModel
    {
        public int Id { get; set; }


        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
