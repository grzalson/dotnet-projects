namespace WarehousesAPI.Models
{
    public class Owner
    {
        public int IdOwner { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public virtual ICollection<ObjectOwner> ObjectOwners { get; set; }
    }
}
