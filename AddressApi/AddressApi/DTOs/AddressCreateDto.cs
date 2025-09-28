using System.ComponentModel.DataAnnotations;

namespace AddressApi.Dtos
{
    public class AddressCreateDto
    {
        [Required(ErrorMessage = "Postal code is required.")]
        [StringLength(10, MinimumLength = 4, ErrorMessage = "Postal code must be between 4 and 10 characters.")]
        public string adrPostalCode { get; set; } = default!;

        [StringLength(100, ErrorMessage = "Building name must be at most 100 characters.")]
        public string? adrBuildingName { get; set; }

        [StringLength(10, ErrorMessage = "Floor must be at most 10 characters.")]
        public string? adrFloor { get; set; }

        [StringLength(10, ErrorMessage = "House number must be at most 10 characters.")]
        public string? adrHouseNumber { get; set; }

        [StringLength(20, ErrorMessage = "Locality code must be at most 20 characters.")]
        public string? adrLocalityCode { get; set; }

        [StringLength(100, ErrorMessage = "Locality name must be at most 100 characters.")]
        public string? adrLocalityName { get; set; }

        [StringLength(50, ErrorMessage = "Locality type must be at most 50 characters.")]
        public string? adrLocalityType { get; set; }

        [StringLength(100, ErrorMessage = "Main avenue must be at most 100 characters.")]
        public string? adrMainAvenue { get; set; }

        [Required(ErrorMessage = "Province is required.")]
        [StringLength(100, ErrorMessage = "Province name must be at most 100 characters.")]
        public string? adrProvince { get; set; }

        [StringLength(10, ErrorMessage = "Side floor must be at most 10 characters.")]
        public string? adrSideFloor { get; set; }

        [StringLength(100, ErrorMessage = "Street name must be at most 100 characters.")]
        public string? adrStreet { get; set; }

        [StringLength(100, ErrorMessage = "Sub-locality must be at most 100 characters.")]
        public string? adrSubLocality { get; set; }

        [StringLength(100, ErrorMessage = "Township must be at most 100 characters.")]
        public string? adrTownShip { get; set; }

        [Range(-90, 90, ErrorMessage = "Latitude must be between -90 and 90 degrees.")]
        public decimal? adrLatitude { get; set; }

        [Range(-180, 180, ErrorMessage = "Longitude must be between -180 and 180 degrees.")]
        public decimal? adrLongitude { get; set; }
    }
}
