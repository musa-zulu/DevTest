using System;
using System.ComponentModel.DataAnnotations;

namespace Regenesys.Domain.Dtos
{
    public class UserDto
    {
        public Guid UserId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string IDNumberOrPassport { get; set; }
        public string Nationality { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        public string DateOfBirth { get; set; }
        public DateTime DateStamp { get; set; }
    }
}
