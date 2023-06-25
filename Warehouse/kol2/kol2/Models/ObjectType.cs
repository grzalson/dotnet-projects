namespace WarehousesAPI.Models
{
    public class ObjectType
    {
        public int IdObjectType { get; set; }

        public string Name { get; set; }
        public virtual ICollection<Object> Objects { get; set; }
    }
}
