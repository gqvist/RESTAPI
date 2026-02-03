namespace RestApi.Models
{
    public class Person
    {
        public int PersonId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Phone { get; set; } = null!;

        // Navigation property
        public ICollection<PersonInterest> PersonInterests { get; set; } = new List<PersonInterest>();
    }
}
