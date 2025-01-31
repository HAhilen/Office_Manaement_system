namespace WebApplication2.Entities
{
    public class Department
    {
        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public string DepartmentName { get; set; } = string.Empty;

        // Navigation Properties
        public virtual Organization Organization { get; set; } = null!;
        public virtual ICollection<Manager> Managers { get; set; } = new List<Manager>();
        public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }

}


