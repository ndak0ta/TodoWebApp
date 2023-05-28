﻿using System.Text;
using System.Text.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Todo.Data.Contexts;
using Todo.Data.Models;
using Todo.Business.Service;

namespace Todo.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController: ControllerBase
{
	public UserController()
	{
	}

    private string GenerateJwtToken(string userId)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes("RIqObkmmz7s8T59kZKpcbzu669IcOXC60rid6uxNuX6Eq/iY0ZaTmgUMfFUYI1BM");
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("userId", userId)
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    private bool IsValidUser(string username, string password)
    { 
        return (username == "ali" && password == "123");
    }

    [HttpPost]
    [Route("login")]
    public IActionResult Login([FromBody] JsonElement body)
    {
        var user = JsonSerializer.Deserialize<User>(body.GetRawText());

        user.Id = 1;

        if (IsValidUser(user.userName, user.password))
        {
            var token = GenerateJwtToken(user.Id.ToString());

            return Ok(new { token });
        }

        return BadRequest(user);
    }

}