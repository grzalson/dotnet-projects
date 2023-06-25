using Microsoft.Identity.Client;

namespace WarehousesAPI.Models
{
    public class Object
    {
        public int IdObject { get; set; }

        public int IdWarehouse { get; set; }
        public int IdObjectType { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public virtual Warehouse Warehouse { get; set; }
        public virtual ObjectType ObjectType { get; set; }
        public virtual ICollection<ObjectOwner> ObjectOwners { get; set; }
    }
}
