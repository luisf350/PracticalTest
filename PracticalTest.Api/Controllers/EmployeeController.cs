using AutoMapper;
using PracticalTest.Common.Dtos;
using PracticalTest.Domain.Contract;
using PracticalTest.Entities.Entities;
using Microsoft.AspNetCore.Mvc;

namespace PracticalTest.Api.Controllers;

public class EmployeeController : BaseController
{
    private readonly IEmployeeDomain _employeeDomain;
    private readonly IMapper _mapper;

    public EmployeeController(ILogger<EmployeeController> logger, IEmployeeDomain employeeDomain, IMapper mapper) : base(logger)
    {
        _employeeDomain = employeeDomain;
        _mapper = mapper;
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> Get()
    {
        var employeeList = await _employeeDomain.GetAll();

        return Ok(_mapper.Map<List<EmployeeResponseDto>>(employeeList));
    }

    [HttpGet("Get/{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var employee = await _employeeDomain.Find(id);
        if (employee == null)
            return NotFound($"There is no Employee with Id: [{id}]");

        return Ok(_mapper.Map<EmployeeResponseDto>(employee));
    }

    [HttpPost]
    public async Task<IActionResult> Post(EmployeeCreateDto employeeDto)
    {
        var employee = _mapper.Map<Employee>(employeeDto);
        await _employeeDomain.Create(employee);

        return Created();
    }

    [HttpPut]
    public async Task<IActionResult> Put(EmployeeUpdateDto employeeDto)
    {
        var employee = await _employeeDomain.Find(employeeDto.Id);
        if (employee == null)
            return NotFound($"There is no Employee with Id: [{employeeDto.Id}]");

        employee.FullName = employeeDto.FullName;
        employee.BirthDate = employeeDto.BirthDate;

        await _employeeDomain.Update(employee);

        return StatusCode(200);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var employee = await _employeeDomain.Find(id);
        if (employee == null)
            return NotFound($"There is no Employee with Id: [{id}]");

        await _employeeDomain.Delete(employee);

        return StatusCode(200);
    }
}
