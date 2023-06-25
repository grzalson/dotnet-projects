namespace MechanicKol.Models
{
    public class Specialization
    {
        public int IdSpecialization { get; set; }
        public string Name { get; set; }
        public virtual IEnumerable<Mechanic> Mechanics { get; set; }
    }
}
