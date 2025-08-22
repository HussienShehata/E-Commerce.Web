namespace DomainLayer.Models.IdentityModule
{
    public class Address
    {
        public int Id { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Street { get; set;} = default!;
        public string City { get; set; } = default!;
        public string Country { get; set; } = default!;
        public ApplicationUser User { get; set; } = default!;
        public string UserID { get; set; } = default!;
    }
}