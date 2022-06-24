using Manager.Core.Exceptions;
using Manager.Domain.Interfaces;
using Manager.Domain.Validators;

namespace Manager.Domain.Entities
{
    public class User : Base, IAggregateRoot
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        protected User(){}//Particularidade do EntitieFramework
        public User(string name, string email, string password)
        {
            SetName(name);
            SetEmail(email);
            SetPassword(password);

            errors = new List<string>();

            Validate();
        }
        public void SetName(string name)
        {
            Name = name;
        }
        public void SetEmail(string email)
        {
            Email = email;
        }

        public void SetPassword(string password)
        {
            Password = password;
        }
        
        public bool Validate() => base.Validade<UserValidator, User>(new UserValidator(), this);
    }
}