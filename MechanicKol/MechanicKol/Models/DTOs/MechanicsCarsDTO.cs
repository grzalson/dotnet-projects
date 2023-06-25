namespace MechanicKol.Models.DTOs
{
    public class MechanicsCarsDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Nickname { get; set; }
        public string Specialization { get; set; }
        public IEnumerable<CarDTO>?  Cars { get; set; }
    }
}
