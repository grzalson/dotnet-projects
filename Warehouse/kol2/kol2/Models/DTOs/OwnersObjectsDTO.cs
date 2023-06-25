using Microsoft.Identity.Client;

namespace WarehousesAPI.Models.DTOs
{
    public class OwnersObjectsDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public IEnumerable<ObjectDTO> OwnerObjects { get; set; }
    }
}
