using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RestApi.Models
{
    public class Interest
    {
        public int InterestId { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;

        // Navigation property
        public ICollection<PersonInterest> PersonInterests { get; set; } = new List<PersonInterest>();
    }
}
