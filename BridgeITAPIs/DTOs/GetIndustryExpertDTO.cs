using Microsoft.OpenApi.Models;

namespace BridgeITAPIs.DTOs;

public class GetIndustryExpertDTO
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string ImageData { get; set; } = string.Empty;
    //public List<string> Interest { get; set; } = new List<string>();
    public string Contact { get; set; } = string.Empty;
    public string Post { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
}
