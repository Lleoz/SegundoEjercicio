using Ejercicio2.Api.Transversal.Common.Entities;
using Ejercicio2.Api.Transversal.Common.Types;
using System;

namespace Ejercicio2.Api.Entities
{
    public class User : Entity<int>
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string SecondLastName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public string PhoneNumber { get; set; }
        public GenreType Genre { get; set; }
        public string Password { get; set; }
    }
}
