using WebApplication2.Entities;

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


public class DepartmentManagerModel
{
    public DepartmentViewModel? Department { get; set; }
    public IEnumerable<ManagerViewModel>? Managers { get; set; }
}
