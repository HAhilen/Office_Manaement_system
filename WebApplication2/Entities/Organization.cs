namespace WebApplication2.Entities
{
    public class Organization
    {
        public int Id { get; set; }
        public string? OrganizationName { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Website { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}