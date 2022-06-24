using Manager.Infra.Interfaces;
using Manager.Services.DTO;
using Manager.Services.Interfaces;
using Manager.Domain.Entities;
using Manager.Core.Exceptions;
using AutoMapper;
using EscNet.Cryptography.Interfaces;

namespace Manager.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper mapper;
        private readonly IUserRepository userRepository;
        private readonly IRijndaelCryptography rijndaelCryptography;

        public UserService(IMapper mapper, IUserRepository userRepository, IRijndaelCryptography rijndaelCryptography)
        {
            this.mapper = mapper;
            this.userRepository = userRepository;
            this.rijndaelCryptography = rijndaelCryptography;
        }

        public async Task<UserDTO> Create(UserDTO userDTO)
        {
            var userExists = await userRepository.GetByEmail(userDTO.Email);
            if(userExists != null)
                throw new DomainException("Usuário já existe com o e-mail informado");
            
            var user = mapper.Map<User>(userDTO);
            user.Validate();
            user.SetPassword(rijndaelCryptography.Encrypt(user.Password));
            var userCreated = await userRepository.Create(user);
            return mapper.Map<UserDTO>(userCreated);
        }

        public async Task<UserDTO> Update(UserDTO userDTO)
        {
            var userExists = await userRepository.Get(userDTO.Id);
            var user = mapper.Map<User>(userDTO);
            user.Validate();
            user.SetPassword(rijndaelCryptography.Encrypt(user.Password));
            var userUpdated = await userRepository.Update(user);
            return mapper.Map<UserDTO>(userUpdated);
        }

        public async Task Remove(long id)
        {
            await userRepository.Remove(id);
        }

         public async Task<UserDTO> Get(long id)
        {
            var user = await userRepository.Get(id);
            return mapper.Map<UserDTO>(user);
        }

        public async Task<List<UserDTO>> Get()
        {
            var allUsers = await userRepository.Get();
            return mapper.Map<List<UserDTO>>(allUsers);
        }

        public async Task<UserDTO> GetByEmail(string email)
        {
            var user = await userRepository.GetByEmail(email);
            return mapper.Map<UserDTO>(user);
        }

        public async Task<List<UserDTO>> SearchByEmail(string email)
        {
            var listUser = await userRepository.SearchByEmail(email);
            return mapper.Map<List<UserDTO>>(listUser);
        }

        public async Task<List<UserDTO>> SearchByName(string name)
        {
            var listUser = await userRepository.SearchByName(name);
            return mapper.Map<List<UserDTO>>(listUser);
        }
    }
}