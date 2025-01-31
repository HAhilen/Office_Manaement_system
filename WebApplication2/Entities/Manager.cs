namespace WebApplication2.Entities
{
    public class Manager
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public string ManagerName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public DateOnly HireDate { get; set; }

        // Navigation Properties
        public virtual Department Department { get; set; } = null!;
        public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }

}

