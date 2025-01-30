namespace WebApplication2.Entities
{
    public class Manager
    {
        public int Id { get; set; }  // primary key   of Manager
        public string? Name { get; set; }        
        public string? Email { get; set; }       
        public string? PhoneNumber { get; set; } 
        public string? Address { get; set; }
    
        public int ? OrganizationId { get; set; }  //foreign key to  refrence organization table
    
        public virtual Organization? Organization { get; set; }
        
        public virtual ICollection<Department>? Departments { get; set; }  
        
     
    }
}

