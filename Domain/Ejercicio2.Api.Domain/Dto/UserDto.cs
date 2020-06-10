using Ejercicio2.Api.Transversal.Common.Types;
using System;

namespace Ejercicio2.Api.Domain.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string SecondLastName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public string PhoneNumber { get; set; }
        public GenreType Genre { get; set; }
        public string FullName
        {
            get 
            {
                return $"{this.Name} {this.LastName}{(!String.IsNullOrWhiteSpace(this.SecondLastName) ? $" {this.SecondLastName}" : String.Empty)}";
            }
        }
    }
}
