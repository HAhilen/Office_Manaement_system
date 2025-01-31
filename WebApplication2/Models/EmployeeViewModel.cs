namespace WebApplication2.Models;
public class EmployeeViewModel
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

}

