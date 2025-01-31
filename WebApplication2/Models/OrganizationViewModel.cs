using WebApplication2.Entities;

namespace WebApplication2.Models
{
    public class OrganizationViewModel
    {
        public int Id { get; set; }
        public string OrganizationName { get; set; } = string.Empty;
        public int EstablishedYear { get; set; }
        public string Country { get; set; } = string.Empty;

    }
}