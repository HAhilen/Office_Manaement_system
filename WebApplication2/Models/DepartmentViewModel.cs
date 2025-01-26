namespace WebApplication2.Models;

public class DepartmentViewModel
{
    public int Id { get; set; }
    public string? DepartmentName { get; set; }
    public int ? ManagerId{ get; set; }
    
    public string? ManagerName { get; set; }
}
