using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using BridgeITAPIs.DTOs.CompanyDTOs;

namespace BridgeITAPIs.Controllers;

[Route("api/companies")]
[ApiController]
public class CompaniesController : ControllerBase
{
    private readonly BridgeItContext _dbContext;

    public CompaniesController(BridgeItContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet("get-company-by-id/{companyId}")]
    public async Task<IActionResult> GetCompany(Guid companyId)
    {
        var company = await _dbContext.Companies
            .FirstOrDefaultAsync(c => c.Id == companyId);

        if (company == null)
        {
            return NotFound("Company not found.");
        }

        var dto = new GetCompanyDTO
        {
            Id = company.Id,
            Name = company.Name ?? string.Empty,
            Address = company.Address ?? string.Empty,
            Business = company.Business ?? string.Empty,
            Description = company.Description ?? string.Empty,
        };

        return Ok(dto);
    }

    [HttpGet("get-all-companies")]
    public async Task<IActionResult> GetCompanies()
    {
        var companies = await _dbContext.Companies.ToListAsync();

        if (!companies.Any())
        {
            return NotFound("Companies not found.");
        }

        var dtoList = companies.Select(c => new GetCompanyDTO
        {
            Id = c.Id,
            Name = c.Name ?? string.Empty,
            Address = c.Address ?? string.Empty,
            Business = c.Business ?? string.Empty,
            Description = c.Description ?? string.Empty,
        }).ToList();

        return Ok(dtoList);
    }

    [HttpGet("get-company-by-name/{name}")]
    public async Task<IActionResult> GetCompanyByName(string name)
    {
        var company = await _dbContext.Companies
            .Where(c => c.Name!.ToLower().Contains(name.ToLower()))
            .ToListAsync();

        if (!company.Any())
        {
            return NotFound("Company not found.");
        }

        var dtoList = company.Select( c =>  new GetCompanyDTO
        {
            Id = c.Id,
            Name = c.Name ?? string.Empty,
            Address = c.Address ?? string.Empty,
            Business = c.Business ?? string.Empty,
            Description = c.Description ?? string.Empty,
        }).ToList();

        return Ok(dtoList);
    }

    [HttpPost("add-company")]
    public async Task<IActionResult> AddCompany([FromBody] AddCompanyDTO dto)
    {
        var company = new Company
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Address = dto.Address,
            Business = dto.Business,
            Description = dto.Description,
        };

        await _dbContext.Companies.AddAsync(company);
        await _dbContext.SaveChangesAsync();

        return Ok("Company added successfully.");
    }

    [HttpDelete("delete-company/{Id}")]
    public async Task<IActionResult> DeleteCompany(Guid Id)
    {
        var company = await _dbContext.Companies
            .FirstOrDefaultAsync(c => c.Id == Id);

        if (company == null)
        {
            return NotFound("Company not found.");
        }

        _dbContext.Companies.Remove(company);
        await _dbContext.SaveChangesAsync();

        return Ok("Company deleted successfully.");
    }
}
