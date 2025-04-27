namespace TexAuto.Models.Domain.Creation
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public ICollection<Machine>? Machines { get; set; }
    }
}
