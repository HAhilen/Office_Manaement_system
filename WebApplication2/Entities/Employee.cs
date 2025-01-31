namespace WebApplication2.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public int ManagerId { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string JobTitle { get; set; } = string.Empty;
        public DateOnly HireDate { get; set; }
        public decimal Salary { get; set; }

        // Navigation Properties
        public virtual Department Department { get; set; } = null!;
        public virtual Manager Manager { get; set; } = null! ;

    }

}
