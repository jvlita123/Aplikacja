using Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dto_s
{
    public class MyUserDto
    {
        public int Id { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string? RoleName { get; set; }

        public string? ProfilePhoto { get; set; }

        public List<string>? Photos { get; set; }
        public List<Message>? Messages { get; set; }
    }
}
