using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using JakDojade.Core.Repository;
using JakDojade.Infrastructure.Dto;

namespace JakDojade.Infrastructure.Services.User
{
    public class UserService:IUserService
    {
        private IUserRepository _userRepository;
        public IMapper _mapper;
        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
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

        public Task Login(string email, string password)
        {
            throw new NotImplementedException();
        }

        public Task Register(Guid id, string email, string username, string password, string role = "user")
        {
            throw new NotImplementedException();
        }
    }
}
