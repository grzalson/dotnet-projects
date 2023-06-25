namespace MechanicKol.Models
{
    public class Car
    {
        public int IdCar { get; set; }
        public string RegistrationPlate { get; set; }
        public DateTime ProductionYear { get; set; }
        public int IdMake { get; set; }
        public virtual Make Make { get; set; }
        public virtual IEnumerable<MechanicCar> MechanicCars { get; set; }
    }
}
