namespace Backend.Middleware_Components.DTO
{
    public class GetRuleDTO
    {
        public Guid Id { get; set; }
        public string name { get; set; }

        public string description { get; set; }

        public string logic { get; set; }

        //public string severity { get; set; }

        public string status { get; set; }

        public DateTime created_at { get; set; }
    }
}
