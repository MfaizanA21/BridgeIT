using BridgeIT.Domain.Enums;

namespace BridgeIT.Domain.Models;

public partial class User
{
    public Guid Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public string? Role { get; set; }

    public string? Hash { get; set; }

    public string? Salt { get; set; }

    public byte[]? ImageData { get; set; }

    public string? description { get; set; }
    
    public string? otpCode { get; set; }

    public OtpType? otpType { get; set; }

    public DateTime? otpCreatedAt { get; set; }
    
    public DateTime? otpExpiresAt { get; set; }

    public virtual ICollection<Faculty> Faculties { get; set; } = new List<Faculty>();

    public virtual ICollection<IndustryExpert> IndustryExperts { get; set; } = new List<IndustryExpert>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    //public virtual ICollection<Skill> Skills { get; set; } = new List<Skill>();

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();

    public virtual ICollection<UniversityAdmin> UniversityAdmins { get; set; } = new List<UniversityAdmin>();
}
