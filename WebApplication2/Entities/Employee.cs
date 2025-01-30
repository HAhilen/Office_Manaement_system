namespace WebApplication2.Entities
{
    public class Employee
    {
        public int Id { get; set; }  //primary key 
        public string? Name { get; set; }
        public string? Email { get; set; }
        public int? OrganizationId { get; set; }  //foreign key to organization table
        
        public int? DepartmentId { get; set; }   // foreign key to department table
        public virtual Organization? Organization { get; set; }
        
        public virtual Department?   Department { get; set; }
    }
}
