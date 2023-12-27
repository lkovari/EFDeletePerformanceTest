using System.Text.Json.Serialization;

namespace DomainModel
{
    [JsonUnmappedMemberHandling(JsonUnmappedMemberHandling.Disallow)]
    public class ZipCode
    {
        [JsonRequired]
        public required int Id { get; set; }
        [JsonRequired]
        public required string City { get; set; }
        [JsonRequired]
        public required string State { get; set; }
        [JsonRequired]
        public required string Zip { get; set; }
        public string? County { get; set; }
    }
}
