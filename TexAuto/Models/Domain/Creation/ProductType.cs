namespace TexAuto.Models.Domain.Creation
{
    public class ProductType
    {
        public int Id { get; set; }
        public string Name { get; set; }    
        public string Description { get; set; }

        public Department Department { get; set; }
        public int  DepartmentId { get; set; }
    }
}