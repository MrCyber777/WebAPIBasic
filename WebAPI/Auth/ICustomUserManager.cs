﻿namespace WebAPI.Auth
{
    public interface ICustomUserManager
    {
        string? Authenticate(string userName, string password);
    }
}