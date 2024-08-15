using BridgeITAPIs.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BridgeITAPIs.Controllers;

[Route("api/register-user")]
[ApiController]
public class RegisterUserController : ControllerBase
{
    private readonly BridgeItContext _dbContext;

    public RegisterUserController(BridgeItContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost("student")]
    public async Task<IActionResult> RegisterStudent([FromBody] RegisterStudentDTO registerStudentDTO)
    {
        if (registerStudentDTO == null)
        {
            return BadRequest("Student Data is null.");
        }

        var university = await _dbContext.Set<University>().FindAsync(registerStudentDTO.UniversityId);
        if (university == null)
        {
            return BadRequest("University not found.");
        }

        var (passwordHash, passwordSalt) = Helper.PasswordHelper.HashPassword(registerStudentDTO.Password);

        var user = new User
        {
            Id = Guid.NewGuid(),
            FirstName = registerStudentDTO.FirstName,
            LastName = registerStudentDTO.LastName,
            Email = registerStudentDTO.Email,
            Role = registerStudentDTO.Role,
            //ImageData = registerStudentDTO.ImageData,
            Hash = passwordHash,
            Salt = passwordSalt
        };

        _dbContext.Set<User>().Add(user);
        await _dbContext.SaveChangesAsync();

        var student = new Student
        {
            Id = Guid.NewGuid(),
            RollNumber = registerStudentDTO.RollNumber,
            UserId = user.Id,
            UniversityId = registerStudentDTO.UniversityId
        };

        await _dbContext.Set<Student>().AddAsync(student);
        await _dbContext.SaveChangesAsync();

        var studentSkills = new Skill
        {
            Id = Guid.NewGuid(),
            CreatedBy = user.Id,
            CreatedAt = DateTime.Now,
            Skill1 = string.Join(",", registerStudentDTO.Skills)
        };

        await _dbContext.Set<Skill>().AddAsync(studentSkills);
        await _dbContext.SaveChangesAsync();

        return Ok("Student Registered Successfully.");

    }

    [HttpPost("faculty")]
    public async Task<IActionResult> RegisterFaculty([FromBody] RegisterFacultyDTO registerFacultyDTO)
    {
        if (registerFacultyDTO == null)
        {
            return BadRequest("Faculty Data is null.");
        }

        var university = await _dbContext.Set<University>().FindAsync(registerFacultyDTO.UniversityId);
        if (university == null)
        {
            return BadRequest("University not found.");
        }
        var (passwordHash, passwordSalt) = Helper.PasswordHelper.HashPassword(registerFacultyDTO.Password);

        var user = new User
        {
            Id = Guid.NewGuid(),
            FirstName = registerFacultyDTO.FirstName,
            LastName = registerFacultyDTO.LastName,
            Email = registerFacultyDTO.Email,
            Role = registerFacultyDTO.Role,
            Hash = passwordHash,
            Salt = passwordSalt
        };

        await _dbContext.Set<User>().AddAsync(user);
        await _dbContext.SaveChangesAsync();

        var faculty = new Faculty
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            Interest = string.Join(",", registerFacultyDTO.Interest),
            Post = registerFacultyDTO.Post,
            UniId = registerFacultyDTO.UniversityId
        };

        await _dbContext.Set<Faculty>().AddAsync(faculty);
        await _dbContext.SaveChangesAsync();

        return Ok("Faculty Registered Successfully.");
    }

    [HttpPost("industry-expert")]
    public async Task<IActionResult> RegisterIndustryExpert([FromBody] RegisterIndustryExpertDTO registerIndustryExpertDTO)
    {
        if (registerIndustryExpertDTO == null)
        {
            return BadRequest("Industry Expert Data is null.");
        }

        var company = await _dbContext.Set<Company>().FindAsync(registerIndustryExpertDTO.CompanyId);
        if (company == null)
        {
            return BadRequest("Company not found.");
        }

        var (passwordHash, passwordSalt) = Helper.PasswordHelper.HashPassword(registerIndustryExpertDTO.Password);

        var user = new User
        {
            Id = Guid.NewGuid(),
            FirstName = registerIndustryExpertDTO.FirstName,
            LastName = registerIndustryExpertDTO.LastName,
            Email = registerIndustryExpertDTO.Email,
            Role = registerIndustryExpertDTO.Role,
            Hash = passwordHash,
            Salt = passwordSalt
        };

        await _dbContext.Set<User>().AddAsync(user);
        await _dbContext.SaveChangesAsync();

        var industryExpert = new IndustryExpert
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            Contact = registerIndustryExpertDTO.Contact,
            Post = registerIndustryExpertDTO.Post,
            CompanyId = registerIndustryExpertDTO.CompanyId
        };

        await _dbContext.Set<IndustryExpert>().AddAsync(industryExpert);
        await _dbContext.SaveChangesAsync();

        return Ok("Industry Expert Registered Successfully.");

    }

    [HttpPost("university-admin")]
    public async Task<IActionResult> RegisterUniversityAdmin([FromBody] RegisterUniAdminDTO registerUniAdminDTO)
    {
        if( registerUniAdminDTO == null)
        {
            return BadRequest("University Admin Data is null.");
        }

        var university = await _dbContext.Set<University>().FindAsync(registerUniAdminDTO.UniversityId);
        if (university == null)
        {
            return BadRequest("University not found.");
        }

        var (passwordHash, passwordSalt) = Helper.PasswordHelper.HashPassword(registerUniAdminDTO.Password);

        var user = new User
        {
            Id = Guid.NewGuid(),
            FirstName = registerUniAdminDTO.FirstName,
            LastName = registerUniAdminDTO.LastName,
            Email = registerUniAdminDTO.Email,
            Role = registerUniAdminDTO.Role,
            Hash = passwordHash,
            Salt = passwordSalt
        };

        await _dbContext.Set<User>().AddAsync(user);
        await _dbContext.SaveChangesAsync();

        var universityAdmin = new UniversityAdmin
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            Contact = registerUniAdminDTO.Contact,
            OfficeAddress = registerUniAdminDTO.OfficeAddress,
            UniId = registerUniAdminDTO.UniversityId
        };

        await _dbContext.Set<UniversityAdmin>().AddAsync(universityAdmin);
        await _dbContext.SaveChangesAsync();

        return Ok("University Admin Registered Successfully.");
    }

    [HttpGet("get-all-emails")]
    public async Task<IActionResult> GetAllEmails()
    {
        var emails = await _dbContext.Users.Select(u => u.Email).ToListAsync();
        return Ok(emails);
    }

}
