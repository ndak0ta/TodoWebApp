﻿using System;
using Microsoft.AspNetCore.Mvc;
using Todo.Data.Models;
using Todo.Business.Service;
using Todo.Business.Models;
using System.Text.Json;

namespace Todo.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest loginRequest)
    {
        var token = _authService.Login(loginRequest);

        if (token == null)
            return BadRequest("Giriş yapılamadı");

        return Ok(new { token });
    }
}