﻿namespace AuthApi.Domain.Dto.Auth
{
    public class LoginRequestDto
    {
        public string UserName { get; set; } = "";
        public string? Password { get; set; } = String.Empty;
    }
}
