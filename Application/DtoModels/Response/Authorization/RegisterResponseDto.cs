﻿    namespace Application.DtoModels.Response.Authorization
{
    public class RegisterResponseDto
    {
        public string? Id { get; set; }

        public string? Email { get; set; }
        
        public string? Address { get; set; }

        public string? UserName { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Token { get; set; }

        public string? Message { get; set; }
    }
}
