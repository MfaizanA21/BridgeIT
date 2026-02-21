using Microsoft.EntityFrameworkCore;
using BridgeIT.Domain.Models;

namespace BridgeIT.Infrastructure.Persistance;

public partial class BridgeItContext : DbContext
{
    public BridgeItContext()
    {
    }

    public BridgeItContext(DbContextOptions<BridgeItContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BoughtFyp> BoughtFyps { get; set; }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<DegreeReport> DegreeReports { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<EductionalResource> EductionalResources { get; set; }
    
    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<Faculty> Faculties { get; set; }

    public virtual DbSet<FacultyExperience> FacultyExperiences { get; set; }

    public virtual DbSet<FieldOfInterest> FieldOfInterests { get; set; }
    
    public virtual DbSet<ForgotPasswordOtp> ForgotPasswordOtps { get; set; }

    public virtual DbSet<Fyp> Fyps { get; set; }
    
    public virtual DbSet<FypMeeting> FypMeetings { get; set; }

    public virtual DbSet<Idea> Ideas { get; set; }

    public virtual DbSet<IndustryExpert> IndustryExperts { get; set; }
    
    public virtual DbSet<InterestedForIdea> InterestedForIdeas { get; set; }
    
    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<MileStone> MileStones { get; set; }
    
    public virtual DbSet<MilestoneComment> MilestoneComments { get; set; }

    public virtual DbSet<Otp> Otps { get; set; }
    
    public virtual DbSet<PaymentDetail> PaymentDetails { get; set; }

    public virtual DbSet<Project> Projects { get; set; }
    
    public virtual DbSet<ProjectModule> ProjectModules { get; set; }
    
    public virtual DbSet<ProjectProgress> ProjectProgresses { get; set; }

    public virtual DbSet<ProjectProposal> Proposals { get; set; }

    public virtual DbSet<ProjectImage> ProjectImages { get; set; }

    public virtual DbSet<ResearchWork> ResearchWorks { get; set; }
    
    public virtual DbSet<RequestForFyp> RequestForFyps { get; set; }

    public virtual DbSet<RequestForProjectCompletion> RequestForProjectCompletions { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Skill> Skills { get; set; }

    public virtual DbSet<SponsoredFyp> SponsoredFyps { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<University> Universities { get; set; }

    public virtual DbSet<UniversityAdmin> UniversityAdmins { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BridgeItContext).Assembly);
        OnModelCreatingPartial(modelBuilder);

        // We will have only one Otp table for everything called Otp.

        //modelBuilder.Entity<ForgotPasswordOtp>(entity =>
        //{
        //    entity.HasKey(e => e.id).HasName("PK__ForgotPa__3213E83F71F90E17");
        //    entity.ToTable("ForgotPasswordOtp");
        //    entity.Property(e => e.email)
        //        .HasMaxLength(255)
        //        .HasColumnName("email");
        //    entity.Property(e => e.otp)
        //        .HasColumnName("otp");
        //    entity.Property(e => e.created_at)
        //        .HasColumnType("datetime")
        //        .HasColumnName("created_at");
        //    entity.HasKey(e => e.id).HasName("PK__ForgotPa__3213E83F71F90E17");
        //});

    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
