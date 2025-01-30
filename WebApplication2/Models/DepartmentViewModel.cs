namespace WebApplication2.Models;

public class DepartmentViewModel
{
    public int Id { get; set; }
    public string? DepartmentName { get; set; }
    public int? ManagerId { get; set; }
}

public class DepartmentEmployeeModel
{
    public DepartmentViewModel? Department { get; set; }
    public IEnumerable<EmployeeViewModel>? Employees { get; set; }
}
