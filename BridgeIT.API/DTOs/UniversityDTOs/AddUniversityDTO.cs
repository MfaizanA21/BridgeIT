namespace BridgeIT.API.DTOs.UniversityDTOs
{
    public class AddUniversityDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public int EstYear { get; set; }
        public string uniImage { get; set; } = string.Empty;
    }
}
