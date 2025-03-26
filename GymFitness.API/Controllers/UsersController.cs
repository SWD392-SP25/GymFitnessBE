using GymFitness.API.RequestDto;
using GymFitness.API.Services.Abstractions;
using GymFitness.Application.Abstractions.Services;
using GymFitness.Domain.Entities;
using GymFitness.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

[Route("api/[controller]")]
[ApiController]
[Authorize]
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
    public async Task<IActionResult> UpdateUser([FromForm] UpdateUserRequestDto dto, Guid userId)
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
        user.AddressLine1 = dto.AddressLine1;
        user.AddressLine2 = dto.AddressLine2;
        user.City = dto.City;
        user.State = dto.State;
        user.PostalCode = dto.PostalCode;
        user.Country = dto.Country;
        user.EmergencyContactName = dto.EmergencyContactName;
        user.EmergencyContactPhone = dto.EmergencyContactPhone;

        // Xử lý upload ảnh nếu có ảnh mới
        if (dto.ProfilePictureUrl != null)
        {
            var firebaseService = HttpContext.RequestServices.GetRequiredService<IFirebaseStorageService>();

            // Xóa ảnh cũ nếu có
            if (!string.IsNullOrEmpty(user.ProfilePictureUrl))
            {
                await firebaseService.DeleteFileAsync(user.ProfilePictureUrl);
            }

            // Upload ảnh mới
            var newProfilePicUrl = await firebaseService.UploadFileAsync(dto.ProfilePictureUrl, "profile_pictures");
            user.ProfilePictureUrl = newProfilePicUrl;
        }

        await _userService.UpdateUser(user);
        return Ok(new { message = "Update user successful", profilePictureUrl = user.ProfilePictureUrl });
    }


    [HttpPost("ban/{email}")]
    public async Task<IActionResult> BanUser(string email)
    {
        try
        {
            
            var result = await _userService.BanUser(email);
            if (!result)
            {
                return NotFound(new { message = "User not found or already banned." });
            }
            return Ok(new { message = "User has been banned successfully." });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred.", error = ex.Message });
        }
    }

}