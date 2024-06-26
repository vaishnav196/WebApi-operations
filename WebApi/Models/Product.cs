namespace WebApi.Models
{
    public class Product
    {   
        public int Id { get; set; } 
        public string Pname { get; set; }    
         public string Pcat {  get; set; }  

        public string Price {  get; set; }
        public string ImagePath { get; set; }
    }
}
