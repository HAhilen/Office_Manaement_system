namespace WebApplication2.Entities;

public class Department
{
    public int Id { get; set; }
    
    public string? DepartmentName { get; set; }
    
    public int? ManagerId { get; set; }
    
    public virtual ICollection<Employee> Employees { get; set; }  
}