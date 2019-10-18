using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using JakDojade.Core.Repository;
using JakDojade.Infrastructure.Dto;

namespace JakDojade.Infrastructure.Services.User
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IEncrypter _encrypter;
        private readonly IJwtHandler _jwtHandler;
        public UserService(IUserRepository userRepository, IJwtHandler jwtHandler, IMapper mapper, IEncrypter encrypter)
        {
            _userRepository = userRepository;
            _jwtHandler = jwtHandler;
            _mapper = mapper;
            _encrypter = encrypter;
        }

        public async Task<UserDto> GetAsync(Guid id)
        {
            var user = await _userRepository.GetAsync(id);

            if (user == null)
            {
                throw new Exception($"User with id: '{id}' does not exist.");
            }

            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> GetAsync(string email)
        {
            var user = await _userRepository.GetAsync(email);

            if (user == null)
            {
                throw new Exception($"User with email: '{email}' does not exist.");
            }

            return _mapper.Map<UserDto>(user);
        }

        public async Task<IEnumerable<UserDto>> BrowseAsync()
        {
            var users = await _userRepository.BrowseAsync();

            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<TokenDto> LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetAsync(email);
            if (user == null)
            {
                throw new Exception("Invalid credentials");
            }

            var hash = _encrypter.GetHash(password, user.Salt);
            if (user.Password != hash)
            {
                throw new Exception("Invalid credentials");
            }

            var jwt = _jwtHandler.CreateToken(user.Id, user.Role);
            return new TokenDto
            {
                Token = jwt.Token,
                Expires = jwt.Expires,
                Role = user.Role
            };
        }

        public async Task RegisterAsync(Guid id, string email, string username, string password,
            string role = "user")
        {
            var user = await _userRepository.GetAsync(email);
            if (user != null)
            {
                throw new Exception($"User with email:'{email}' already exist.");
            }

            var salt = _encrypter.GetSalt(password);
            var hash = _encrypter.GetHash(password, salt);

            var newUser = new JakDojade.Core.Domain.User{ Id = id, Email = email, Username = username, Password = hash, Salt = salt,
             Role = role, CreatedAt = DateTime.UtcNow };

            await _userRepository.AddAsync(newUser);
        }

    }
}
