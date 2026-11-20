using LapichkaShop.Data.Constants;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LapichkaShop.Data.Entities
{
    public class Application
    {
        public Application()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }
        public string UserId { get; set; }
        public VolunteerActivity VolunteerActivity { get; set; }
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        public bool Sunday { get; set; }
        public string Message { get; set; }
        public DateTime RequestedAt { get; set; }
        public ApplicationStatus ApplicationStatus { get; set; }
        public ApplicationUser User { get; set; }
    }
}
