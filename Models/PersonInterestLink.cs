namespace RestApi.Models
{
    public class PersonInterestLink
    {
        public int PersonInterestLinkId { get; set; }

        // Composite FK
        public int PersonId { get; set; }
        public int InterestId { get; set; }

        // Navigation property
        public PersonInterest PersonInterest { get; set; } = null!;


        public string Url { get; set; } = null!;
        public string? Note { get; set; }
    }
}
