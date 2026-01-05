using System.ComponentModel.DataAnnotations;

namespace Backend.Tables
{
    public class RuleTable
    {
        [Key]
        public Guid Id { get; set; }
        public string name { get; set; }

        public string description { get; set; }

        public string logic { get; set; }

        public string severity { get; set; }

        public string status { get; set; }

        public Guid created_by { get; set; }

        public DateTime created_at { get; set; }

        public DateTime? updated_at { get; set; }
    }
}
