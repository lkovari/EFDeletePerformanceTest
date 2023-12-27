namespace DomainModel
{
    public class Address
    {
        public required int Id { get; set; }
        public required string Address1 { get; set; }
        public string? Address2 { get; set; }
        public required ZipCode ZipCode { get; set; }
    }
}
