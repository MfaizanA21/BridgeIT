namespace BridgeITAPIs.DTOs.UniAdminDTOs
{
    public class GetUniAdminDTO
    {
        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
        public Guid? UniId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public byte[] ProfileImage { get; set; } = Array.Empty<byte>();
        public string Description { get; set; } = string.Empty;
        public string University { get; set; } = string.Empty;
        public string OfficeAddress { get; set; } = string.Empty;
        public string Contact { get; set; } = string.Empty;
    }
}
