namespace WarehousesAPI.Models
{
    public class ObjectOwner
    {
        public int IdObject{ get; set; }
        public int IdOwner { get; set; }
        public virtual Object Object { get; set; }
        public virtual Owner Owner { get; set; }
    }
}
