using WebApplication2.Entities;

namespace WebApplication2.Models
{
    public class OrganizationViewModel
    {
        public int Id { get; set; }
        public string? OrganizationName { get; set; }
        public string? Address { get; set; } 
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Website { get; set; }
    }
}