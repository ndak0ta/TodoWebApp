﻿using System;


namespace Todo.Business.Models;

public class LoginRequest
{
    public string? Username { get; set; }
    public string? Password { get; set; }
}