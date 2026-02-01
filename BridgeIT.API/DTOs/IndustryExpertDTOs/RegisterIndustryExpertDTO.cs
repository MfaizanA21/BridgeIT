namespace BridgeIT.API.DTOs.IndustryExpertDTOs
{
    public class RegisterIndustryExpertDTO
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string Contact { get; set; } = string.Empty;

        public string Post { get; set; } = string.Empty;

        public Guid CompanyId { get; set; }
    }
}
