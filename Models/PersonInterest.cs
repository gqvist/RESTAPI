namespace RestApi.Models
{
    public class PersonInterest
    {
        // Composite Key part 1: PersonId + InterestId
        public int PersonId { get; set; }
        public Person Person { get; set; } = null!;

        // Composite Key part 2: PersonId + InterestId
        public int InterestId { get; set; }
        public Interest Interest { get; set; } = null!;

        // Collection of related links
        public ICollection<PersonInterestLink> Links { get; set; } = new List<PersonInterestLink>();
    }
}
