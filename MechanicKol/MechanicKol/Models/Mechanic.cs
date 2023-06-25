namespace MechanicKol.Models
{
    public class Mechanic
    {
        public int IdMechanic { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Nickname { get; set; }
        public int IdSpecialization { get; set; }
        public virtual Specialization Specialization { get; set; }
        public virtual IEnumerable<MechanicCar> MechanicCars { get; set; }
    }
}
