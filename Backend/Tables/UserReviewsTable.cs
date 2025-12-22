using System.ComponentModel.DataAnnotations;

namespace Backend.Tables
{
    public class UserReviewsTable
    {
        [Key]
        public int Id { get; set; } 
        public int UserID { get; set; }

        public float Grade { get; set; }

        public string Text { get; set; }
    }
}
