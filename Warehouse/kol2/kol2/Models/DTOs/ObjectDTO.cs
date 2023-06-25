using Microsoft.Identity.Client;

namespace WarehousesAPI.Models.DTOs
{
    public class ObjectDTO
    {
        public int IdObject { get; set; }

        public double Width { get; set; }
        public double Height { get; set; }
        public string Type { get; set; }
        public string WarehouseName { get; set; }
    }
}
