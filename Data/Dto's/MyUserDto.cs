using Data.Entities;

namespace Data.Dto_s
{
    public class MyUserDto
    {
        public int Id { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }

        public DateTime? UserDateOfBirth { get; set; }

        public string? RoleName { get; set; }

        public string? ProfilePhoto { get; set; }

        public List<string>? Photos { get; set; }

        public List<Message>? Messages { get; set; }
    }
}
