namespace WebApplication2.Models;
public class EmployeeViewModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Department { get; set; }
    public string? Email { get; set; }
    public int? OrganizationId { get; set; }
    
    //public virtual Organization Organization { get; set; }  
}
