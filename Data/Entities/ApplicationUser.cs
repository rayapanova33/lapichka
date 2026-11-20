using Microsoft.AspNetCore.Identity;

namespace LapichkaShop.Data.Entities
{
    public class ApplicationUser: IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime RegistrationDate { get; set; }
        public ICollection<Adoption> AdoptionRequests { get; set; }
        public ICollection<Adoption> ProcessedRequests { get; set; }
    }
}
