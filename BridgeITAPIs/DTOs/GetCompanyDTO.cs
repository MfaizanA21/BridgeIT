namespace BridgeITAPIs.DTOs
{
    public class GetCompanyDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Business { get; set; } = string.Empty;
    }
}
