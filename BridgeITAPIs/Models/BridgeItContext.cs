using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BridgeITAPIs.Models;

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

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<Faculty> Faculties { get; set; }

    public virtual DbSet<FacultyExperience> FacultyExperiences { get; set; }

    public virtual DbSet<FieldOfInterest> FieldOfInterests { get; set; }
    
    public virtual DbSet<ForgotPasswordOtp> ForgotPasswordOtps { get; set; }

    public virtual DbSet<Fyp> Fyps { get; set; }

    public virtual DbSet<Idea> Ideas { get; set; }

    public virtual DbSet<IndustryExpert> IndustryExperts { get; set; }

    public virtual DbSet<MileStone> MileStones { get; set; }
    
    public virtual DbSet<MilestoneComment> MilestoneComments { get; set; }

    public virtual DbSet<Otp> Otps { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<ProjectProposal> Proposals { get; set; }

    public virtual DbSet<ProjectImage> ProjectImages { get; set; }

    public virtual DbSet<ResearchWork> ResearchWorks { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Skill> Skills { get; set; }

    public virtual DbSet<SponsoredFyp> SponsoredFyps { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<University> Universities { get; set; }

    public virtual DbSet<UniversityAdmin> UniversityAdmins { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=tcp:bridge-it-server.database.windows.net,1433;Initial Catalog=BridgeIT-db;Persist Security Info = False;User ID=br-it-adm; Password=MfaizanA21; MultipleActiveResultSets=False; Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
    //"Server=tcp:your_server.database.windows.net,1433;Initial Catalog=your_database;Persist Security Info=False;User ID=your_user;Password=your_password;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
    //Server=tcp:bridge-it-server.database.windows.net,1433;Initial Catalog=BridgeIT-db;Persist Security Info = False;User ID=br-it-adm; Password=MfaizanA21; MultipleActiveResultSets=False; Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;Authentication=\"Active Directory Default\";"
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BoughtFyp>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BoughtFY__3213E83FCEA1BCAA");

            entity.ToTable("BoughtFYP");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Agreement)
                .HasMaxLength(255)
                .HasColumnName("agreement");
            entity.Property(e => e.FypId).HasColumnName("fyp_id");
            entity.Property(e => e.IndExpertId).HasColumnName("ind_expert_id");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.PurchaseDate).HasColumnName("purchase_date");
            entity.Property(e => e.UniversityAdminId).HasColumnName("university_admin_id");

            entity.HasOne(d => d.Fyp).WithMany(p => p.BoughtFyps)
                .HasForeignKey(d => d.FypId)
                .HasConstraintName("FK__BoughtFYP__fyp_i__25518C17");

            entity.HasOne(d => d.IndExpert).WithMany(p => p.BoughtFyps)
                .HasForeignKey(d => d.IndExpertId)
                .HasConstraintName("FK__BoughtFYP__ind_e__2645B050");

            entity.HasOne(d => d.UniversityAdmin).WithMany(p => p.BoughtFyps)
                .HasForeignKey(d => d.UniversityAdminId)
                .HasConstraintName("FK__BoughtFYP__unive__2739D489");
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Company__3213E83F9B7417C7");

            entity.ToTable("Company");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.Business)
                .HasMaxLength(255)
                .HasColumnName("business");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });
        
        modelBuilder.Entity<DegreeReport>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DegreeRe__3213E83FC391FF5B");

            entity.ToTable("DegreeReport");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Achievements).HasMaxLength(255);
            entity.Property(e => e.Activities).HasMaxLength(255);
            entity.Property(e => e.Program)
                .HasMaxLength(255)
                .HasColumnName("program");
            entity.Property(e => e.StudentId).HasColumnName("student_id");

            entity.HasOne(d => d.Student).WithMany(p => p.DegreeReports)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("FK__DegreeRep__stude__18EBB532");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Departme__3213E83F8B496504");

            entity.ToTable("Department");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Department1)
                .HasMaxLength(255)
                .HasColumnName("department");
        });
        
        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Event__3213E83F73A8ACC8");

            entity.ToTable("Event");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.EventDate)
                .HasColumnType("datetime")
                .HasColumnName("event_date");
            entity.Property(e => e.FacultyId).HasColumnName("faculty_id");
            entity.Property(e => e.SpeakerName)
                .HasMaxLength(255)
                .HasColumnName("speaker_name");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");
            entity.Property(e => e.Venue)
                .HasColumnType("nvarchar(max)")
                .HasColumnName("venue");
            entity.Property(e => e.Description)
                .HasColumnType("nvarchar(max)")
                .HasColumnName("description");

            entity.HasOne(d => d.Faculty).WithMany(p => p.Events)
                .HasForeignKey(d => d.FacultyId)
                .HasConstraintName("FK__Event__faculty_i__1CBC4616");
        });

        modelBuilder.Entity<Faculty>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Faculty__3213E83F3ED2B942");

            entity.ToTable("Faculty");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Interest)
                .HasMaxLength(255)
                .HasColumnName("interest");
            entity.Property(e => e.Post)
                .HasMaxLength(255)
                .HasColumnName("post");
            entity.Property(e => e.Department)
                .HasColumnType("nvarchar(max)")
                .HasColumnName("department");
            entity.Property(e => e.UniId).HasColumnName("uni_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Uni).WithMany(p => p.Faculties)
                .HasForeignKey(d => d.UniId)
                .HasConstraintName("FK__Faculty__uni_id__160F4887");

            entity.HasOne(d => d.User).WithMany(p => p.Faculties)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Faculty__user_id__151B244E");

        });

        modelBuilder.Entity<FacultyExperience>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FacultyE__3213E83F6FF1BC07");

            entity.ToTable("FacultyExperience");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.Duration)
                .HasMaxLength(255)
                .HasColumnName("duration");
            entity.Property(e => e.FacultyId).HasColumnName("faculty_id");
            entity.Property(e => e.JobTitle)
                .HasMaxLength(255)
                .HasColumnName("Job_title");
            entity.Property(e => e.Responsibilities).HasMaxLength(255);

            entity.HasOne(d => d.Company).WithMany(p => p.FacultyExperiences)
                .HasForeignKey(d => d.CompanyId)
                .HasConstraintName("FK__FacultyEx__compa__1AD3FDA4");

            entity.HasOne(d => d.Faculty).WithMany(p => p.FacultyExperiences)
                .HasForeignKey(d => d.FacultyId)
                .HasConstraintName("FK__FacultyEx__facul__19DFD96B");
        });

        modelBuilder.Entity<ForgotPasswordOtp>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__ForgotPa__3213E83F71F90E17");
            entity.ToTable("ForgotPasswordOtp");
            entity.Property(e => e.email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.otp)
                .HasColumnName("otp");
            entity.Property(e => e.created_at)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.HasKey(e => e.id).HasName("PK__ForgotPa__3213E83F71F90E17");
        });
        
        modelBuilder.Entity<FieldOfInterest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FieldOfI__3213E83F670D4F91");

            entity.ToTable("FieldOfInterest");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.FieldOfInterest1)
                .HasMaxLength(255)
                .HasColumnName("field_of_interest");
        });

        modelBuilder.Entity<Fyp>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FYP__3213E83F8A77B011");

            entity.ToTable("FYP");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Batch)
                .HasMaxLength(255)
                .HasColumnName("batch");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            // entity.Property(e => e.FacultyId).HasColumnName("faculty_id");
            entity.Property(e => e.Members).HasColumnName("members");
            entity.Property(i => i.fyp_id)
                .HasMaxLength(25)
                .HasColumnName("fyp_id");
            entity.Property(s => s.Status)
                .HasMaxLength(25)
                .HasColumnName("status");
            entity.Property(e => e.Technology)
                .HasMaxLength(255)
                .HasColumnName("technology");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");
            // entity.HasOne(u => u.University).WithMany(f => f.Fyps)
            //     .HasForeignKey(u => u.UniId)
            //     .HasConstraintName("FK__FYP__UNIVERSITY");

            // entity.HasOne(d => d.Faculty).WithMany(p => p.Fyps)
            //     .HasForeignKey(d => d.FacultyId)
            //     .HasConstraintName("FK__FYP__faculty_id__245D67DE");
        });

        modelBuilder.Entity<Idea>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Idea__3213E83FFDEF3EB1");

            entity.ToTable("Idea");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.FacultyId).HasColumnName("faculty_id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Technology)
                .HasMaxLength(255)
                .HasColumnName("technology");

            entity.HasOne(d => d.Faculty).WithMany(p => p.Ideas)
                .HasForeignKey(d => d.FacultyId)
                .HasConstraintName("FK__Idea__faculty_id__1DB06A4F");
        });

        modelBuilder.Entity<IndustryExpert>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Industry__3213E83FA9545AA2");

            entity.ToTable("IndustryExpert");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.Contact)
                .HasMaxLength(255)
                .HasColumnName("contact");
            entity.Property(e => e.Post)
                .HasMaxLength(255)
                .HasColumnName("post");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Company).WithMany(p => p.IndustryExperts)
                .HasForeignKey(d => d.CompanyId)
                .HasConstraintName("FK__IndustryE__compa__17F790F9");

            entity.HasOne(d => d.User).WithMany(p => p.IndustryExperts)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__IndustryE__user___17036CC0");
        });

        modelBuilder.Entity<Otp>(entity =>
        {
            entity.HasKey(e => e.email).HasName("PK__Otp__AB6E616532361977");

            entity.ToTable("Otp");

            entity.Property(e => e.email)
                .HasMaxLength(255)
                .HasColumnName("email");

            entity.Property(e => e.otp)
                .HasColumnName("otp");

            entity.Property(e => e.created_at)
                .HasMaxLength(255)
                .HasColumnName("created_at");
        });

        modelBuilder.Entity<MileStone>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MileSton__3213E83F3971CE7B");

            entity.ToTable("MileStone");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.AchievementDate).HasColumnName("achievement_date");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");

            entity.HasOne(d => d.Project).WithMany(p => p.MileStones)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK__MileStone__proje__208CD6FA");
        });

        modelBuilder.Entity<MilestoneComment>(entity =>
        {
            entity.HasKey((e => e.Id)).HasName("PK__Mileston__3213E83F7B39521F");

            entity.ToTable("MilestoneComment");
            
            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Comment)
                .HasColumnName("comment")
                .HasColumnType("NVARCHAR(MAX)");
            entity.Property(e => e.CommentDate)
                .HasColumnType("datetime")
                .HasColumnName("comment_date");

            entity.Property(e => e.Commenter_id).HasColumnName("commenter_id");
            entity.Property(e => e.Milestone_id).HasColumnName("milestone_id");

            entity.HasOne(o => o.MileStone).WithMany(m => m.MilestoneComments)
                .HasForeignKey(o => o.Milestone_id)
                .HasConstraintName("FK__Milestone__miles_2F9A1060");
            entity.HasOne(i => i.Commenter).WithMany(i => i.MilestoneComments)
                .HasForeignKey(i => i.Commenter_id)
                .HasConstraintName("FK__Milestone__comme__2EA5EC27");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Project__3213E83F9E55023D");

            entity.ToTable("Project");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CurrentStatus)
                .HasMaxLength(255)
                .HasColumnName("current_status");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.IndExpertId).HasColumnName("ind_expert_id");
            entity.Property(e => e.Stack)
                .HasMaxLength(255)
                .HasColumnName("stack");//
            entity.Property(e => e.Link).HasColumnName("link").HasMaxLength(255);
            entity.Property(e => e.Budget).HasColumnName("budget");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.StudentId).HasColumnName("student_id");
            entity.Property(e => e.FacultyId).HasColumnName("faculty_id");
            entity.Property(e => e.Team).HasColumnName("team");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");

            entity.HasOne(d => d.IndExpert).WithMany(p => p.Projects)
                .HasForeignKey(d => d.IndExpertId)
                .HasConstraintName("FK__Project__ind_exp__1F98B2C1");

            entity.HasOne(d => d.Student).WithMany(p => p.Projects)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("FK__Project__student__1EA48E88");

            entity.HasOne(d => d.Faculty).WithMany(p => p.Projects)
                .HasForeignKey(d => d.FacultyId)
                .HasConstraintName("FK_Project_Faculty");
        });

        modelBuilder.Entity<ProjectProposal>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_ProjectProposal");

            entity.ToTable("ProjectProposal");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Proposal)
                .HasColumnName("proposal")
                .HasColumnType("varbinary(max)");
            entity.Property(e => e.Status)
                .HasMaxLength(255)
                .HasColumnName("status");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.StudentId).HasColumnName("student_id");

            entity.HasOne(d => d.Project).WithMany(p => p.Proposals)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK__ProjectProposal_Project");

            entity.HasOne(d => d.Student).WithMany(p => p.Proposals)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("FK__ProjectProposal_Student");
                
        });

        modelBuilder.Entity<ProjectImage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ProjectI__3213E83FFE4CB032");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.ImageData)
                .HasMaxLength(255)
                .HasColumnName("image_data");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");

            entity.HasOne(d => d.Project).WithMany(p => p.ProjectImages)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK__ProjectIm__proje__236943A5");
        });

        modelBuilder.Entity<ResearchWork>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Research__3213E83F4DE5E04E");

            entity.ToTable("ResearchWork");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Category)
                .HasMaxLength(255)
                .HasColumnName("category");
            entity.Property(e => e.FacultyId).HasColumnName("faculty_id");
            entity.Property(e => e.OtherResearchers)
                .HasMaxLength(255)
                .HasColumnName("other_researchers");
            entity.Property(e => e.PaperName)
                .HasMaxLength(255)
                .HasColumnName("paperName");
            entity.Property(e => e.PublishChannel)
                .HasMaxLength(255)
                .HasColumnName("publish_channel");
            entity.Property(e => e.Link)
                .HasColumnType("nvarchar(max)")
                .HasColumnName("link");
            entity.Property(e => e.YearOfPublish).HasColumnName("year_of_publish");

            entity.HasOne(d => d.Faculty).WithMany(p => p.ResearchWorks)
                .HasForeignKey(d => d.FacultyId)
                .HasConstraintName("FK__ResearchW__facul__1BC821DD");
        });

        modelBuilder.Entity<RequestForFyp>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RequestF__3213E83F08D70405");
            entity.ToTable("RequestForFyp");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");

            entity.Property(e => e.Status)
                .HasColumnName("status")
                .HasColumnType("int")
                .HasMaxLength(255);
            
            entity.HasOne(e => e.Student)
                .WithMany(p => p.RequestForFyps)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Requested_Student");

            entity.HasOne(e => e.Fyp)
                .WithMany(p => p.RequestForFyps)
                .HasForeignKey(d => d.FypId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FOR_FYP");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Review__3213E83F560D307B");

            entity.ToTable("Review");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.DatePosted)
                .HasColumnType("datetime")
                .HasColumnName("date_posted");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.Review1)
                .HasMaxLength(255)
                .HasColumnName("review");
            entity.Property(e => e.ReviewerId).HasColumnName("reviewer_id");

            entity.HasOne(d => d.Project).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK__Review__project___2180FB33");

            entity.HasOne(d => d.Reviewer).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.ReviewerId)
                .HasConstraintName("FK__Review__reviewer__22751F6C");
        });

        modelBuilder.Entity<Skill>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Skill__3213E83F62CDF175");

            entity.ToTable("Skill");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            
            entity.Property(e => e.Skill1)
                .HasMaxLength(255)
                .HasColumnName("skill");

            
        });

        modelBuilder.Entity<SponsoredFyp>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Sponsore__3213E83F08D49FDF");

            entity.ToTable("SponsoredFYP");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Agreement)
                .HasMaxLength(255)
                .HasColumnName("agreement");
            entity.Property(e => e.FypId).HasColumnName("fyp_id");
            entity.Property(e => e.SponsoredById).HasColumnName("sponsored_by_id");

            entity.HasOne(d => d.Fyp).WithMany(p => p.SponsoredFyps)
                .HasForeignKey(d => d.FypId)
                .HasConstraintName("FK__Sponsored__fyp_i__282DF8C2");

            entity.HasOne(d => d.SponsoredBy).WithMany(p => p.SponsoredFyps)
                .HasForeignKey(d => d.SponsoredById)
                .HasConstraintName("FK__Sponsored__spons__29221CFB");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Student__3213E83FA8A3DFA8");

            entity.ToTable("Student");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.skills)
                .HasColumnName("Skills")
                .HasColumnType("nvarchar(max)");
            entity.Property(e => e.department)
                .HasColumnType("nvarchar(max)")
                .HasColumnName("department");
            entity.Property(e => e.RollNumber).HasColumnName("rollNumber");
            entity.Property(e => e.UniversityId).HasColumnName("university_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(f => f.FypId).HasColumnName("fyp_id");

            entity.HasOne(d => d.University).WithMany(p => p.Students)
                .HasForeignKey(d => d.UniversityId)
                .HasConstraintName("FK__Student__univers__1332DBDC");

            entity.HasOne(d => d.User).WithMany(p => p.Students)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Student__user_id__14270015");

            entity.HasOne(f => f.Fyp).WithMany(s => s.Students)
                .HasForeignKey(f => f.FypId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Student_FYP");

            //     entity.HasMany(d => d.Fyps).WithMany(p => p.Students)
            //         .UsingEntity<Dictionary<string, object>>(
            //             "StudentFyp",
            //             r => r.HasOne<Fyp>().WithMany()
            //                 .HasForeignKey("FypId")
            //                 .OnDelete(DeleteBehavior.ClientSetNull)
            //                 .HasConstraintName("FK__StudentFY__fyp_i__2CF2ADDF"),
            //             l => l.HasOne<Student>().WithMany()
            //                 .HasForeignKey("StudentId")
            //                 .OnDelete(DeleteBehavior.ClientSetNull)
            //                 .HasConstraintName("FK__StudentFY__stude__2BFE89A6"),
            //             j =>
            //             {
            //                 j.HasKey("StudentId", "FypId").HasName("PK__StudentF__2B6D7267EDC7B330");
            //                 j.ToTable("StudentFYP");
            //                 j.IndexerProperty<Guid>("StudentId").HasColumnName("student_id");
            //                 j.IndexerProperty<Guid>("FypId").HasColumnName("fyp_id");
            //             });
            // });
            //    entity.HasMany(d => d.Skills).WithMany(p => p.Students)
            //         .UsingEntity<Dictionary<string, object>>(
            //             "StudentSkill",
            //             r => r.HasOne<Skill>().WithMany()
            //                 .HasForeignKey("SkillId")
            //                 .OnDelete(DeleteBehavior.ClientSetNull)
            //                 .HasConstraintName("FK__StudentSk__skill__797309D9"),
            //             l => l.HasOne<Student>().WithMany()
            //                 .HasForeignKey("StudentId")
            //                 .OnDelete(DeleteBehavior.ClientSetNull)
            //                 .HasConstraintName("FK__StudentSk__stude__787EE5A0"),
            //             j =>
            //             {
            //                 j.HasKey("StudentId", "SkillId").HasName("PK__StudentS__A588AEAD140FF03C");
            //                 j.ToTable("StudentSkill");
            //                 j.IndexerProperty<Guid>("StudentId").HasColumnName("student_id");
            //                 j.IndexerProperty<Guid>("SkillId").HasColumnName("skill_id");
            //             }); 
        });

        modelBuilder.Entity<University>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Universi__3213E83FC4E1BD04");

            entity.ToTable("University");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.uniImage)
                .HasColumnType("varbinary(max)")
                .HasColumnName("uniImage");
        });

        modelBuilder.Entity<UniversityAdmin>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Universi__3213E83FDD7E6C0B");

            entity.ToTable("UniversityAdmin");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Contact)
                .HasMaxLength(255)
                .HasColumnName("contact");
            entity.Property(e => e.OfficeAddress)
                .HasMaxLength(255)
                .HasColumnName("officeAddress");
            entity.Property(e => e.UniId).HasColumnName("uni_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Uni).WithMany(p => p.UniversityAdmins)
                .HasForeignKey(d => d.UniId)
                .HasConstraintName("FK__Universit__uni_i__123EB7A3");

            entity.HasOne(d => d.User).WithMany(p => p.UniversityAdmins)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Universit__user___114A936A");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User__3213E83F4697F9C5");

            entity.ToTable("User");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(255)
                .HasColumnName("firstName");
            entity.Property(e => e.Hash)
                .HasMaxLength(255)
                .HasColumnName("hash");
            entity.Property(e => e.ImageData)
                .HasColumnType("varbinary(max)")
                .HasColumnName("imageData");
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .HasColumnName("lastName");
            entity.Property(e => e.Role)
                .HasMaxLength(255)
                .HasColumnName("role");
            entity.Property(e => e.Salt)
                .HasMaxLength(255)
                .HasColumnName("salt");
            entity.Property(e => e.description)
                .HasColumnType("nvarchar(max)")
                .HasColumnName("description");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
