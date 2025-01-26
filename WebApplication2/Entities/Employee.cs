namespace WebApplication2.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? DepartmentName { get; set; }
        public string? Email { get; set; }
        public int? OrganizationId { get; set; }
        
        public int? DepartmentId { get; set; }
        public virtual Organization Organization { get; set; }
        
        public virtual Department   Department { get; set; }
    }
}
