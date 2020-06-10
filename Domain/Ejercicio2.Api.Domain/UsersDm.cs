using Ejercicio2.Api.Domain.Dto;
using Ejercicio2.Api.Domain.Interfaces;
using Ejercicio2.Api.Entities;
using Ejercicio2.Api.Transversal.Common.Tools;
using Ejercicio2.Api.UnitOfWork.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ejercicio2.Api.Domain
{
    public class UsersDm : IUsers
    {
        private readonly IUnitOfWork _unitOfWork;

        public UsersDm(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<UserDataDto> AddAsync(UserDto user)
        {
            var isValidEmail = EmailTool.IsValidEmail(user.Email);

            if (!isValidEmail)
            {
                throw new Exception("Email is not valid");
            }

            var userAge = DateTool.GetAge(user.BirthDate);

            if (userAge < 18)
            {
                throw new Exception("You must be of legal age to register");
            }

            var emailExist = await this._unitOfWork.Repositories.Users
                .QueryableBy(x =>
                    x.Email == user.Email)
                .AnyAsync();

            if (emailExist)
            {
                throw new Exception("You email already exists");
            }

            var phoneNumberExist = await this._unitOfWork.Repositories.Users
                .QueryableBy(x =>
                    x.PhoneNumber == user.PhoneNumber)
                .AnyAsync();

            if (phoneNumberExist)
            {
                throw new Exception("You phone number already exists");
            }

            var password = StringTool.GetRandomString(20);
            var encryptedPassword = EncryptedTool.EncryptToMD5(password);
            var entity = new User
            {
                Name = user.Name,
                LastName = user.LastName,
                SecondLastName = user.SecondLastName,
                Email = user.Email,
                BirthDate = user.BirthDate,
                PhoneNumber = user.PhoneNumber,
                Genre = user.Genre,
                Password = encryptedPassword
            };
            this._unitOfWork.Repositories.Users.Add(entity);
            await this._unitOfWork.SaveAsync();
            return new UserDataDto 
            {
                Id = entity.Id,
                Email = entity.Email,
                Password = password
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existEntity = await this._unitOfWork.Repositories.Users
                .QueryableBy(x =>
                    x.Id == id)
                .AnyAsync();

            if (!existEntity)
            {
                return false;
            }

            this._unitOfWork.Repositories.Users.Delete(id);
            await this._unitOfWork.SaveAsync();
            return true;
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var users = await _unitOfWork.Repositories.Users.GetAllAsync();

            if (users == null)
            {
                return null;
            }

            return users
                .Select(x => new UserDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    LastName = x.LastName,
                    SecondLastName = x.SecondLastName,
                    Email = x.Email,
                    BirthDate = x.BirthDate,
                    PhoneNumber = x.PhoneNumber,
                    Genre = x.Genre
                })
                .AsEnumerable();
        }

        public async Task<UserDto> GetByAsync(string email, string password)
        {
            var user = await _unitOfWork.Repositories.Users
                .GetByAsync(x =>
                    x.Email == email &&
                    x.Password == password);

            if (user == null)
            {
                return null;
            }

            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                LastName = user.LastName,
                SecondLastName = user.SecondLastName,
                Email = user.Email,
                BirthDate = user.BirthDate,
                PhoneNumber = user.PhoneNumber,
                Genre = user.Genre
            };
        }

        public async Task<UserDto> GetByAsync(int id)
        {
            var user = await _unitOfWork.Repositories.Users
                .GetByAsync(x =>
                    x.Id == id);

            if (user == null)
            {
                return null;
            }

            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                LastName = user.LastName,
                SecondLastName = user.SecondLastName,
                Email = user.Email,
                BirthDate = user.BirthDate,
                PhoneNumber = user.PhoneNumber,
                Genre = user.Genre
            };
        }

        public async Task<UserDto> GetByAsync(string email)
        {
            var user = await _unitOfWork.Repositories.Users
                .GetByAsync(x =>
                    x.Email == email);

            if (user == null)
            {
                return null;
            }

            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                LastName = user.LastName,
                SecondLastName = user.SecondLastName,
                Email = user.Email,
                BirthDate = user.BirthDate,
                PhoneNumber = user.PhoneNumber,
                Genre = user.Genre
            };
        }

        public async Task<bool> UpdateAsync(UserDto user)
        {
            var entity = await this._unitOfWork.Repositories.Users
                .GetByAsync(x =>
                    x.Id == user.Id);

            if (entity == null)
            {
                return false;
            }

            entity.Name = user.Name;
            entity.LastName = user.LastName;
            entity.SecondLastName = user.SecondLastName;
            entity.BirthDate = user.BirthDate;
            entity.Genre = user.Genre;
            entity.PhoneNumber = user.PhoneNumber;
            this._unitOfWork.Repositories.Users.Update(entity);
            await this._unitOfWork.SaveAsync();
            return true;
        }

        public async Task<UserDataDto> UpdatePasswordAsync(string email)
        {
            var entity = await this._unitOfWork.Repositories.Users
                .GetByAsync(x =>
                    x.Email == email);

            if (entity == null)
            {
                return null;
            }

            var password = StringTool.GetRandomString(20);
            var encryptedPassword = EncryptedTool.EncryptToMD5(password);
            entity.Password = encryptedPassword;
            this._unitOfWork.Repositories.Users.Update(entity);
            await this._unitOfWork.SaveAsync();
            return new UserDataDto 
            {
                Id = entity.Id,
                Email = entity.Email,
                Password = encryptedPassword
            };
        }
    }
}
