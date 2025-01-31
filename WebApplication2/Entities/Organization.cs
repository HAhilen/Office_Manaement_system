namespace WebApplication2.Entities
{
    public class Organization
    {
        public int Id { get; set; }
        public string OrganizationName { get; set; } = string.Empty;
        public int EstablishedYear { get; set; }
        public string Country { get; set; } = string.Empty;
        // One-to-many relationship with Department
        public virtual ICollection<Department> Departments { get; set; } = null!;

        // Reverse relationship with Employee
    }

}
