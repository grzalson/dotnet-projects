namespace MechanicKol.Models
{
    public class MechanicCar
    {
        public int IdMechanic { get; set; }
        public int IdCar { get; set; }
        public virtual Car Car { get; set; }
        public virtual Mechanic Mechanic { get; set; }
    }
}
