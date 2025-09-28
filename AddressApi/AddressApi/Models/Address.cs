namespace AddressApi.Models
{
    public class Address
    {
        public int AdrId { get; set; }
        public string adrPostalCode { get; set; } = default!;
        public string? adrBuildingName { get; set; }
        public string? adrFloor { get; set; }
        public string? adrHouseNumber { get; set; }
        public string? adrLocalityCode { get; set; }
        public string? adrLocalityName { get; set; }
        public string? adrLocalityType { get; set; }
        public string? adrMainAvenue { get; set; }
        public string? adrProvince { get; set; }
        public string? adrSideFloor { get; set; }
        public string? adrStreet { get; set; }
        public string? adrSubLocality { get; set; }
        public string? adrTownShip { get; set; }
        public decimal? adrLatitude { get; set; }
        public decimal? adrLongitude {get; set;}
    }
}