﻿using Ejercicio2.Api.Domain.Dto;

namespace Ejercicio2.Api.Domain.Interfaces
{
    public interface ISecurityDm
    {
        void SendPasswordByEmail(UserDto user, string password);
        void SendPasswordByEmail(string name, string email, string password);
    }
}
