using Microsoft.Identity.Client;

namespace WarehousesAPI.Models
{
    public class Warehouse
    {
        public int IdWarehouse { get; set; }

        public string Name { get; set; } 
        public virtual ICollection<Object> Objects { get; set; }
    }
}
