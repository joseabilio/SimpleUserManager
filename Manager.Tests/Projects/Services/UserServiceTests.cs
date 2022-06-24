
using AutoMapper;
using Bogus.DataSets;
using EscNet.Cryptography.Interfaces;
using Manager.Domain.Entities;
using Manager.Infra.Interfaces;
using Manager.Services.DTO;
using Manager.Services.Interfaces;
using Manager.Services.Services;
using Manager.Tests.Configuration;
using Moq;
using FluentAssertions;
using Manager.Tests.Fixtures;
using Manager.Core.Exceptions;

namespace Manager.Tests.Projects.Services
{
    public class UserServiceTests
    {
        private readonly IUserService sut;

        private readonly IMapper mapper;
        private readonly Mock<IUserRepository> userRepositoryMock;
        private readonly Mock<IRijndaelCryptography> rijndaelCryptographyMock;
        public UserServiceTests()
        {
            mapper = AutoMapperConfiguration.GetConfiguration();
            userRepositoryMock = new Mock<IUserRepository>();
            rijndaelCryptographyMock = new Mock<IRijndaelCryptography>();
            sut = new UserService(mapper, userRepositoryMock.Object, rijndaelCryptographyMock.Object);

        }
        [Fact(DisplayName = "Create Valid User")]
        [Trait("Category", "Services")]
        public async Task Create_WhenUserIsValid_ReturnUserDTO()
        {
            //Arrange - Arruma todos os dados e configurações para que o teste aconteça
            var userToCreate = UserFixture.CreateValidUserDTO();
            var userCreated = mapper.Map<User>(userToCreate);
            var encryptPassword = new Lorem().Sentence();

            userRepositoryMock.Setup(x => x.GetByEmail(It.IsAny<string>())).ReturnsAsync(()=> null);
            rijndaelCryptographyMock.Setup(x => x.Encrypt(It.IsAny<string>())).Returns(encryptPassword);
            userRepositoryMock.Setup(x => x.Create(It.IsAny<User>())).ReturnsAsync(() => userCreated);
            //Act - Chama a ação para execcutar o teste (chama o método)

            var result = await sut.Create(userToCreate);

            //Assert - Verificando se o resultado é o esperado
            result.Should().BeEquivalentTo(mapper.Map<UserDTO>(userCreated));

        }

        [Fact(DisplayName = "Create When User Exists")]
        [Trait("Category", "Services")]
        public void Create_WhenUserExists_ThrowsNewDomainException()
        {
            var userToCreate = UserFixture.CreateValidUserDTO();
            var userExists = UserFixture.CreateValidUser();

            userRepositoryMock.Setup(x => x.GetByEmail(It.IsAny<string>())).ReturnsAsync(()=> userExists);

            Func<Task<UserDTO>> act = async () =>
            {
                return await sut.Create(userToCreate);
            };
            act.Should()
               .ThrowAsync<DomainException>()
               .WithMessage("Usuário já existe com o e-mail informado");
        }

        [Fact(DisplayName = "Create When User is Invalid")]
        [Trait("Category", "Services")]
        public void Create_WhenUserIsInvalid_ThrowsNewDomainException()
        {
            var userToCreate = UserFixture.CreateInvalidUserDTO();
            userRepositoryMock.Setup(x => x.GetByEmail(It.IsAny<string>())).ReturnsAsync(() => null);

            Func<Task<UserDTO>> act = async () => 
            {
                return await sut.Create(userToCreate);
            };

            act.Should().ThrowAsync<DomainException>();
        }

        [Fact(DisplayName = "Update Valid User")]
        [Trait("Category", "Services")]
        public async Task Update_WhenUserIsValid_ReturnsUserDTO()
        {
            var oldUser = UserFixture.CreateValidUser();
            var userToUpdate = UserFixture.CreateValidUserDTO();
            var userUpdated = mapper.Map<User>(userToUpdate);

            var encryptPassword = new Lorem().Sentence();

            userRepositoryMock.Setup(x => x.Get(It.IsAny<long>())).ReturnsAsync(() => oldUser);

            rijndaelCryptographyMock.Setup(x => x.Encrypt(It.IsAny<string>())).Returns(encryptPassword);

            userRepositoryMock.Setup(x => x.Update(It.IsAny<User>())).ReturnsAsync(() => userUpdated);

            var result = await sut.Update(userToUpdate);

            result.Should().BeEquivalentTo(mapper.Map<UserDTO>(userUpdated));
        }

        [Fact(DisplayName = "Update When user not exists")]
        [Trait("Category", "Services")]
        public async Task Update_WhenUserNotExists_ThrowsNewDomainException()
        {
            var userToUpdate = UserFixture.CreateValidUserDTO();

            userRepositoryMock.Setup(x => x.Get(It.IsAny<long>())).ReturnsAsync(() => null);

            Func<Task<UserDTO>> act = async () =>
            {
                return await sut.Update(userToUpdate);
            };

            act.Should().ThrowAsync<Exception>().WithMessage("Dado não contrado");
        }

    }
}