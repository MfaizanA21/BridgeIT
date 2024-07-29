using BridgeITAPIs.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BridgeITAPIs.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RegisterController : ControllerBase
{
    private readonly BridgeItContext _dbContext;

    public RegisterController(BridgeItContext dbContext)
    {
        _dbContext = dbContext;
    }

    /*[HttpPost("student")]
    public async Task<IActionResult> RegisterStudent(User user, Student student, University university)
    {
        var firstName = user.FirstName;
        var lastName = user.LastName;
        var email = user.Email;
        var user_id = user.Id;   // how to set uuid in an API, it should not be in params?
        var hash = user.Hash;    // how to calculate hash of password?
        var salt = user.Salt;   //  how to generate salt?
        var rollNumber = student.RollNumber;
        var student_id = student.Id;      // again how to set student uuid(shouldnt be in params)
        student.UserId = user_id;   //foriegn key of UserID in student table will the same (shouldbt be in params)
        // var student_user_id = student.User.Id    //or it should be like this?
        var uni_id = university.Id;     //same uuid issue

        //these university attributes will create a new university everytime => multiple same name unis with diff uuids

        var university_name = university.Name;
        var university_address = university.Address;
        var established_year = university.EstYear;

        //DTOs should be made as more than one model is being used??

        _dbContext.Add<User>(user);
        _dbContext.Add<Student>(student);
        _dbContext.Add<University>(university);

        await _dbContext.SaveChangesAsync();
        
        return Ok();
    }
    */

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

        var (passwordHash, passwordSalt) = PasswordHelper.PasswordHelper.HashPassword(registerStudentDTO.Password);

        var user = new User
        {
            Id = Guid.NewGuid(),
            FirstName = registerStudentDTO.FirstName,
            LastName = registerStudentDTO.LastName,
            Email = registerStudentDTO.Email,
            Role = registerStudentDTO.Role,
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

        _dbContext.Set<Student>().Add(student);
        await _dbContext.SaveChangesAsync();

        return Ok("Student Registered Successfully.");

    }


}
