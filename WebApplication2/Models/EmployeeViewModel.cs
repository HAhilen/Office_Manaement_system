namespace WebApplication2.Models;
public class EmployeeViewModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Department { get; set; }
    public string? Email { get; set; }
    public int? OrganizationId { get; set; }
    
}

// public class EmployeeWithOrganizationViewModel
// {
//     public int EmpId { get; set; }
//     public string? EmpName { get; set; }
//     public string? Department { get; set; }
//     public string? Email { get; set; }
//     public int? OrganizationId { get; set; }
//     public string? OrganizationName { get; set; }
//     public string? Address { get; set; }
//     
// }
