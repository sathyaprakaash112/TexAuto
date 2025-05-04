namespace TexAuto.Models.Domain.Creation
{
    public class MachineType
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public Department Department { get; set; }
        public int DepartmentId { get; set; }
    }
}
