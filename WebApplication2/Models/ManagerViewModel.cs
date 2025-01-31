namespace WebApplication2.Models
{
    public class ManagerViewModel
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public string ManagerName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public DateOnly HireDate { get; set; }

    }
}
