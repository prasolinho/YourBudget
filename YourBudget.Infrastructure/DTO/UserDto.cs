using System;

namespace YourBudget.Infrastructure.DTO
{
    public class UserDto
    {
        public Guid Id { get; set; }        
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
    }
}