namespace WebApplication2.Models
{
    public class ManagerViewModel
    {
        public int Id { get; set; }            
        public string? Name { get; set; }        
        public string? Email { get; set; }      
        public string? PhoneNumber { get; set; } 
        public string? Address { get; set; }    
        
        public IEnumerable<string>? DepartmentNames { get; set; }  
    }
}
