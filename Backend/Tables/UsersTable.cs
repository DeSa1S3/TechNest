using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Tables
{
    public class UsersTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Roles { get; set; }
        public string Img { get; set; }
        public string RegistrationDate { get; set; }
        public string Status { get; set; }
        public DateTime created_at { get; set; }
        public DateTime? updated_at { get; set; } 
    }
}