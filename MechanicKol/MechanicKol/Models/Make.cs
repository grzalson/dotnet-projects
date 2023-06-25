namespace MechanicKol.Models
{
    public class Make
    {
        public int IdMake { get; set; }
        public string Name { get; set; }
        public virtual IEnumerable<Car> Cars { get; set; }
    }
}
