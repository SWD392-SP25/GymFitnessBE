using GymFitness.API.RequestDto;
using GymFitness.API.Services.Abstractions;
using GymFitness.Domain.Entities;
using GymFitness.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    // API lấy danh sách Users
    [HttpGet]
    public async Task<IActionResult> GetUsers([FromQuery] string? filterOn,
                                              [FromQuery] string? filterQuery)
    {
        var users = await _userService.GetAll(filterOn, filterQuery);
        return Ok(users);
    }

    //Task<User> GetUserByEmail(string email);
    //Task AddUser(User user);

    //Task<User> GetUserById(Guid userId);
    //Task<List<User>> GetUsersAsync(string? filterOn, string? filterQuery);
    //Task UpdateUser(User user);

    [HttpGet("{email}")]
    public async Task<IActionResult> GetUserByEmail(string email)
    {
        var user = await _userService.GetUserByEmail(email);
        return Ok(user);
    }

    [HttpPut("{userId}")]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserRequestDto dto, Guid userId)
    {
        var user = await _userService.GetUserById(userId);
        if (user == null)
        {
            return NotFound();
        }

        user.FirstName = dto.FirstName;
        user.LastName = dto.LastName;
        user.Phone = dto.Phone;
        user.DateOfBirth = dto.DateOfBirth;
        user.ProfilePictureUrl = dto.ProfilePictureUrl.ToString();
        user.AddressLine1 = dto.AddressLine1;
        user.AddressLine2 = dto.AddressLine2;
        user.City = dto.City;
        user.State = dto.State;
        user.PostalCode = dto.PostalCode;
        user.Country = dto.Country;
        user.EmergencyContactName = dto.EmergencyContactName;
        user.EmergencyContactPhone = dto.EmergencyContactPhone;

        await _userService.UpdateUser(user);
        return Ok("Update user successful");
    }


}