using System.ComponentModel.DataAnnotations;

namespace Backend.Tables
{
    public class UsersTable
    {
        [Key]
        public int Id { get; set; }

        public string firstName { get; set; }

        public string lastName { get; set; }

        public string RegistrationDate { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string[] Roles { get; set; }

        public string? Img { get; set; }
    }
}
