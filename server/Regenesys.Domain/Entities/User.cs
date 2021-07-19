using System;

namespace Regenesys.Domain.Entities
{
    public class User
    {
        public Guid UserId { get; set; }
        public string FirstName {get;set;}
        public string LastName {get;set;}
        public string IDNumberOrPassport {get;set;}
        public string Nationality {get;set;}
        public string EmailAddress {get;set;}
        public string PhoneNumber {get;set;}
        public string DateOfBirth {get;set;}
        public DateTime DateStamp {get;set;}
    }
}
