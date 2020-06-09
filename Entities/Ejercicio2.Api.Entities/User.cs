using System;

namespace Ejercicio2.Api.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public string PhoneNumber { get; set; }
        public int Genre { get; set; }
        public string Password { get; set; }
    }
}
