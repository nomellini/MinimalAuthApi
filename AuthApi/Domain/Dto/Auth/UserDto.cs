﻿namespace AuthApi.Domain.Dto.Auth
{
    public class UserDto
    {
        public string? Id { get; set; }
        public string? Email { get; set; }
        public string? FullName { get; set; } = "";
    }
}
