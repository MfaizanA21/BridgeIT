using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BridgeIT.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    business = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Company__3213E83F9B7417C7", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    department = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Departme__3213E83F8B496504", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "FieldOfInterest",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    field_of_interest = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__FieldOfI__3213E83F670D4F91", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ForgotPasswordOtp",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    otp = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ForgotPa__3213E83F71F90E17", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    sender_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    recipient_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    content = table.Column<string>(type: "NVARCHAR(MAX)", nullable: false),
                    time_stamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Message__3213E83F25D4FD14", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Otp",
                columns: table => new
                {
                    email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    otp = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Otp__AB6E616532361977", x => x.email);
                });

            migrationBuilder.CreateTable(
                name: "Skill",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    skill = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Skill__3213E83F62CDF175", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "University",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    EstYear = table.Column<int>(type: "int", nullable: true),
                    uniImage = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Universi__3213E83FC4E1BD04", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    firstName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    lastName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    role = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    hash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    salt = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    imageData = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__User__3213E83F4697F9C5", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Faculty",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    interest = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    post = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    department = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    uni_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Faculty__3213E83F3ED2B942", x => x.id);
                    table.ForeignKey(
                        name: "FK__Faculty__uni_id__160F4887",
                        column: x => x.uni_id,
                        principalTable: "University",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__Faculty__user_id__151B244E",
                        column: x => x.user_id,
                        principalTable: "User",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "IndustryExpert",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    contact = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    post = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    company_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Industry__3213E83FA9545AA2", x => x.id);
                    table.ForeignKey(
                        name: "FK__IndustryE__compa__17F790F9",
                        column: x => x.company_id,
                        principalTable: "Company",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__IndustryE__user___17036CC0",
                        column: x => x.user_id,
                        principalTable: "User",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "UniversityAdmin",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    contact = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    officeAddress = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    uni_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Universi__3213E83FDD7E6C0B", x => x.id);
                    table.ForeignKey(
                        name: "FK__Universit__uni_i__123EB7A3",
                        column: x => x.uni_id,
                        principalTable: "University",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__Universit__user___114A936A",
                        column: x => x.user_id,
                        principalTable: "User",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "EducationalResource",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    source_link = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    faculty_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Educatio__3213E83FC3E922D5", x => x.id);
                    table.ForeignKey(
                        name: "FK__Education__facul__756D6ECB",
                        column: x => x.faculty_id,
                        principalTable: "Faculty",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Event",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    speaker_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    event_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    venue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    faculty_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Event__3213E83F73A8ACC8", x => x.id);
                    table.ForeignKey(
                        name: "FK__Event__faculty_i__1CBC4616",
                        column: x => x.faculty_id,
                        principalTable: "Faculty",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "FacultyExperience",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    company_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Job_title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    duration = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Responsibilities = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    faculty_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__FacultyE__3213E83F6FF1BC07", x => x.id);
                    table.ForeignKey(
                        name: "FK__FacultyEx__compa__1AD3FDA4",
                        column: x => x.company_id,
                        principalTable: "Company",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__FacultyEx__facul__19DFD96B",
                        column: x => x.faculty_id,
                        principalTable: "Faculty",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "FYP",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    members = table.Column<int>(type: "int", nullable: true),
                    batch = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    technology = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    fyp_id = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    status = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    faculty_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    year_of_completion = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__FYP__3213E83F8A77B011", x => x.id);
                    table.ForeignKey(
                        name: "FK__FYP__faculty_id__65F62111",
                        column: x => x.faculty_id,
                        principalTable: "Faculty",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Idea",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    technology = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    faculty_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Idea__3213E83FFDEF3EB1", x => x.id);
                    table.ForeignKey(
                        name: "FK__Idea__faculty_id__1DB06A4F",
                        column: x => x.faculty_id,
                        principalTable: "Faculty",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "ResearchWork",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    paperName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    category = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    publish_channel = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    other_researchers = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    year_of_publish = table.Column<DateOnly>(type: "date", nullable: true),
                    faculty_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    link = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Research__3213E83F4DE5E04E", x => x.id);
                    table.ForeignKey(
                        name: "FK__ResearchW__facul__1BC821DD",
                        column: x => x.faculty_id,
                        principalTable: "Faculty",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "BoughtFYP",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    fyp_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ind_expert_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    university_admin_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    agreement = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    price = table.Column<long>(type: "bigint", nullable: true),
                    purchase_date = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__BoughtFY__3213E83FCEA1BCAA", x => x.id);
                    table.ForeignKey(
                        name: "FK__BoughtFYP__fyp_i__25518C17",
                        column: x => x.fyp_id,
                        principalTable: "FYP",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__BoughtFYP__ind_e__2645B050",
                        column: x => x.ind_expert_id,
                        principalTable: "IndustryExpert",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__BoughtFYP__unive__2739D489",
                        column: x => x.university_admin_id,
                        principalTable: "UniversityAdmin",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "FypMeeting",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    chosen_slot = table.Column<DateTime>(type: "datetime2", nullable: true),
                    status = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    meet_link = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    feedback = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    is_meet_done = table.Column<bool>(type: "bit", nullable: false),
                    fyp_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ind_exp_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__FypMeeti__3213E83F1DF49EAE", x => x.id);
                    table.ForeignKey(
                        name: "FK__FypMeetin__fyp_i__1B9317B3",
                        column: x => x.fyp_id,
                        principalTable: "FYP",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__FypMeetin__ind_e__1C873BEC",
                        column: x => x.ind_exp_id,
                        principalTable: "IndustryExpert",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SponsoredFYP",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    fyp_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    sponsored_by_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    agreement = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Sponsore__3213E83F08D49FDF", x => x.id);
                    table.ForeignKey(
                        name: "FK__Sponsored__fyp_i__282DF8C2",
                        column: x => x.fyp_id,
                        principalTable: "FYP",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__Sponsored__spons__29221CFB",
                        column: x => x.sponsored_by_id,
                        principalTable: "IndustryExpert",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Skills = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    rollNumber = table.Column<int>(type: "int", nullable: true),
                    department = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cv_link = table.Column<string>(type: "NVARCHAR(255)", nullable: true),
                    stripe_connect_id = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    university_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    fyp_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Student__3213E83FA8A3DFA8", x => x.id);
                    table.ForeignKey(
                        name: "FK__Student_FYP",
                        column: x => x.fyp_id,
                        principalTable: "FYP",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__Student__univers__1332DBDC",
                        column: x => x.university_id,
                        principalTable: "University",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__Student__user_id__14270015",
                        column: x => x.user_id,
                        principalTable: "User",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "DegreeReport",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Gpa = table.Column<double>(type: "float", nullable: true),
                    program = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Achievements = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Activities = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    student_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__DegreeRe__3213E83FC391FF5B", x => x.id);
                    table.ForeignKey(
                        name: "FK__DegreeRep__stude__18EBB532",
                        column: x => x.student_id,
                        principalTable: "Student",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "InterestedForIdea",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    student_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    idea_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    status = table.Column<int>(type: "int", nullable: true),
                    meet_place = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MeetTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Interest__3213E83FF623C2C6", x => x.id);
                    table.ForeignKey(
                        name: "FK__InterestedForIdea_Idea",
                        column: x => x.idea_id,
                        principalTable: "Idea",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__InterestedForIdea_Student",
                        column: x => x.student_id,
                        principalTable: "Student",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    team = table.Column<int>(type: "int", nullable: true),
                    stack = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    start_date = table.Column<DateOnly>(type: "date", nullable: true),
                    current_status = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    end_date = table.Column<DateOnly>(type: "date", nullable: true),
                    budget = table.Column<int>(type: "int", nullable: true),
                    link = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    student_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ind_expert_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    faculty_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Project__3213E83F9E55023D", x => x.id);
                    table.ForeignKey(
                        name: "FK_Project_Faculty",
                        column: x => x.faculty_id,
                        principalTable: "Faculty",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__Project__ind_exp__1F98B2C1",
                        column: x => x.ind_expert_id,
                        principalTable: "IndustryExpert",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__Project__student__1EA48E88",
                        column: x => x.student_id,
                        principalTable: "Student",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "RequestForFyp",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    status = table.Column<int>(type: "int", maxLength: 255, nullable: true),
                    student_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    fyp_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ind_exp_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__RequestF__3213E83F08D70405", x => x.id);
                    table.ForeignKey(
                        name: "FK__FOR_FYP",
                        column: x => x.fyp_id,
                        principalTable: "FYP",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__RequestFo__ind_e__0697FACD",
                        column: x => x.ind_exp_id,
                        principalTable: "IndustryExpert",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__Requested_Student",
                        column: x => x.student_id,
                        principalTable: "Student",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "MileStone",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    achievement_date = table.Column<DateOnly>(type: "date", nullable: true),
                    project_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__MileSton__3213E83F3971CE7B", x => x.id);
                    table.ForeignKey(
                        name: "FK__MileStone__proje__208CD6FA",
                        column: x => x.project_id,
                        principalTable: "Project",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "PaymentDetail",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    project_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    paid_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    payment_slip = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PaymentD__3213E83F79822DA9", x => x.id);
                    table.ForeignKey(
                        name: "FK__PaymentDe__proje__625A9A57",
                        column: x => x.project_id,
                        principalTable: "Project",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectImages",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    project_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    image_data = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ProjectI__3213E83FFE4CB032", x => x.id);
                    table.ForeignKey(
                        name: "FK__ProjectIm__proje__236943A5",
                        column: x => x.project_id,
                        principalTable: "Project",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "ProjectModule",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    project_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<bool>(type: "BIT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ProjectM__3213E83F17D82272", x => x.id);
                    table.ForeignKey(
                        name: "FK__ProjectMo__proje__2EA5EC27",
                        column: x => x.project_id,
                        principalTable: "Project",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectProgress",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    task = table.Column<string>(type: "NVARCHAR(MAX)", nullable: false),
                    description = table.Column<string>(type: "NVARCHAR(MAX)", nullable: true),
                    task_status = table.Column<string>(type: "VARCHAR(25)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ProjectP__3213E83FDFBC58FE", x => x.id);
                    table.ForeignKey(
                        name: "FK__ProjectPr__proje__3C34F16F",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectProposal",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    proposal = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    status = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    payment_intent_id = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    project_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    student_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectProposal", x => x.id);
                    table.ForeignKey(
                        name: "FK__ProjectProposal_Project",
                        column: x => x.project_id,
                        principalTable: "Project",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__ProjectProposal_Student",
                        column: x => x.student_id,
                        principalTable: "Student",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RequestToCompletePRoject",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    project_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    request_status = table.Column<string>(type: "NVARCHAR(25)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__RequestT__3213E83F1896BB7E", x => x.id);
                    table.ForeignKey(
                        name: "FK__RequestTo__proje__4F47C5E3",
                        column: x => x.project_id,
                        principalTable: "Project",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Review",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    project_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    reviewer_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    review = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    date_posted = table.Column<DateTime>(type: "datetime", nullable: true),
                    rating = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Review__3213E83F560D307B", x => x.id);
                    table.ForeignKey(
                        name: "FK__Review__project___2180FB33",
                        column: x => x.project_id,
                        principalTable: "Project",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__Review__reviewer__22751F6C",
                        column: x => x.reviewer_id,
                        principalTable: "User",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "MilestoneComment",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    comment = table.Column<string>(type: "NVARCHAR(MAX)", nullable: false),
                    comment_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    commenter_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    milestone_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Mileston__3213E83F7B39521F", x => x.id);
                    table.ForeignKey(
                        name: "FK__Milestone__comme__2EA5EC27",
                        column: x => x.commenter_id,
                        principalTable: "IndustryExpert",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Milestone__miles_2F9A1060",
                        column: x => x.milestone_id,
                        principalTable: "MileStone",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BoughtFYP_fyp_id",
                table: "BoughtFYP",
                column: "fyp_id");

            migrationBuilder.CreateIndex(
                name: "IX_BoughtFYP_ind_expert_id",
                table: "BoughtFYP",
                column: "ind_expert_id");

            migrationBuilder.CreateIndex(
                name: "IX_BoughtFYP_university_admin_id",
                table: "BoughtFYP",
                column: "university_admin_id");

            migrationBuilder.CreateIndex(
                name: "IX_DegreeReport_student_id",
                table: "DegreeReport",
                column: "student_id");

            migrationBuilder.CreateIndex(
                name: "IX_EducationalResource_faculty_id",
                table: "EducationalResource",
                column: "faculty_id");

            migrationBuilder.CreateIndex(
                name: "IX_Event_faculty_id",
                table: "Event",
                column: "faculty_id");

            migrationBuilder.CreateIndex(
                name: "IX_Faculty_uni_id",
                table: "Faculty",
                column: "uni_id");

            migrationBuilder.CreateIndex(
                name: "IX_Faculty_user_id",
                table: "Faculty",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_FacultyExperience_company_id",
                table: "FacultyExperience",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "IX_FacultyExperience_faculty_id",
                table: "FacultyExperience",
                column: "faculty_id");

            migrationBuilder.CreateIndex(
                name: "IX_FYP_faculty_id",
                table: "FYP",
                column: "faculty_id");

            migrationBuilder.CreateIndex(
                name: "IX_FypMeeting_fyp_id",
                table: "FypMeeting",
                column: "fyp_id");

            migrationBuilder.CreateIndex(
                name: "IX_FypMeeting_ind_exp_id",
                table: "FypMeeting",
                column: "ind_exp_id");

            migrationBuilder.CreateIndex(
                name: "IX_Idea_faculty_id",
                table: "Idea",
                column: "faculty_id");

            migrationBuilder.CreateIndex(
                name: "IX_IndustryExpert_company_id",
                table: "IndustryExpert",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "IX_IndustryExpert_user_id",
                table: "IndustryExpert",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_InterestedForIdea_idea_id",
                table: "InterestedForIdea",
                column: "idea_id");

            migrationBuilder.CreateIndex(
                name: "IX_InterestedForIdea_student_id",
                table: "InterestedForIdea",
                column: "student_id");

            migrationBuilder.CreateIndex(
                name: "IX_MileStone_project_id",
                table: "MileStone",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "IX_MilestoneComment_commenter_id",
                table: "MilestoneComment",
                column: "commenter_id");

            migrationBuilder.CreateIndex(
                name: "IX_MilestoneComment_milestone_id",
                table: "MilestoneComment",
                column: "milestone_id");

            migrationBuilder.CreateIndex(
                name: "UQ_PaymentDetail_ProjectId",
                table: "PaymentDetail",
                column: "project_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Project_faculty_id",
                table: "Project",
                column: "faculty_id");

            migrationBuilder.CreateIndex(
                name: "IX_Project_ind_expert_id",
                table: "Project",
                column: "ind_expert_id");

            migrationBuilder.CreateIndex(
                name: "IX_Project_student_id",
                table: "Project",
                column: "student_id");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectImages_project_id",
                table: "ProjectImages",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectModule_project_id",
                table: "ProjectModule",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectProgress_ProjectId",
                table: "ProjectProgress",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectProposal_project_id",
                table: "ProjectProposal",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectProposal_student_id",
                table: "ProjectProposal",
                column: "student_id");

            migrationBuilder.CreateIndex(
                name: "IX_RequestForFyp_fyp_id",
                table: "RequestForFyp",
                column: "fyp_id");

            migrationBuilder.CreateIndex(
                name: "IX_RequestForFyp_ind_exp_id",
                table: "RequestForFyp",
                column: "ind_exp_id");

            migrationBuilder.CreateIndex(
                name: "IX_RequestForFyp_student_id",
                table: "RequestForFyp",
                column: "student_id");

            migrationBuilder.CreateIndex(
                name: "IX_RequestToCompletePRoject_project_id",
                table: "RequestToCompletePRoject",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "IX_ResearchWork_faculty_id",
                table: "ResearchWork",
                column: "faculty_id");

            migrationBuilder.CreateIndex(
                name: "IX_Review_project_id",
                table: "Review",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "IX_Review_reviewer_id",
                table: "Review",
                column: "reviewer_id");

            migrationBuilder.CreateIndex(
                name: "IX_SponsoredFYP_fyp_id",
                table: "SponsoredFYP",
                column: "fyp_id");

            migrationBuilder.CreateIndex(
                name: "IX_SponsoredFYP_sponsored_by_id",
                table: "SponsoredFYP",
                column: "sponsored_by_id");

            migrationBuilder.CreateIndex(
                name: "IX_Student_fyp_id",
                table: "Student",
                column: "fyp_id");

            migrationBuilder.CreateIndex(
                name: "IX_Student_university_id",
                table: "Student",
                column: "university_id");

            migrationBuilder.CreateIndex(
                name: "IX_Student_user_id",
                table: "Student",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_UniversityAdmin_uni_id",
                table: "UniversityAdmin",
                column: "uni_id");

            migrationBuilder.CreateIndex(
                name: "IX_UniversityAdmin_user_id",
                table: "UniversityAdmin",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoughtFYP");

            migrationBuilder.DropTable(
                name: "DegreeReport");

            migrationBuilder.DropTable(
                name: "Department");

            migrationBuilder.DropTable(
                name: "EducationalResource");

            migrationBuilder.DropTable(
                name: "Event");

            migrationBuilder.DropTable(
                name: "FacultyExperience");

            migrationBuilder.DropTable(
                name: "FieldOfInterest");

            migrationBuilder.DropTable(
                name: "ForgotPasswordOtp");

            migrationBuilder.DropTable(
                name: "FypMeeting");

            migrationBuilder.DropTable(
                name: "InterestedForIdea");

            migrationBuilder.DropTable(
                name: "Message");

            migrationBuilder.DropTable(
                name: "MilestoneComment");

            migrationBuilder.DropTable(
                name: "Otp");

            migrationBuilder.DropTable(
                name: "PaymentDetail");

            migrationBuilder.DropTable(
                name: "ProjectImages");

            migrationBuilder.DropTable(
                name: "ProjectModule");

            migrationBuilder.DropTable(
                name: "ProjectProgress");

            migrationBuilder.DropTable(
                name: "ProjectProposal");

            migrationBuilder.DropTable(
                name: "RequestForFyp");

            migrationBuilder.DropTable(
                name: "RequestToCompletePRoject");

            migrationBuilder.DropTable(
                name: "ResearchWork");

            migrationBuilder.DropTable(
                name: "Review");

            migrationBuilder.DropTable(
                name: "Skill");

            migrationBuilder.DropTable(
                name: "SponsoredFYP");

            migrationBuilder.DropTable(
                name: "UniversityAdmin");

            migrationBuilder.DropTable(
                name: "Idea");

            migrationBuilder.DropTable(
                name: "MileStone");

            migrationBuilder.DropTable(
                name: "Project");

            migrationBuilder.DropTable(
                name: "IndustryExpert");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropTable(
                name: "FYP");

            migrationBuilder.DropTable(
                name: "Faculty");

            migrationBuilder.DropTable(
                name: "University");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
