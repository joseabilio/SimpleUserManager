using System.Text;
using FluentValidation;
using FluentValidation.Results;

namespace Manager.Domain.Entities
{
    public abstract class Base
    {
        public long Id { get; set; }
        internal List<string> errors;
        public IReadOnlyCollection<string> Errors => errors;
        public bool IsValid => errors.Count == 0;

        private void AddErrorList(IList<ValidationFailure> errors)
        {
            foreach(var erro in errors)
                this.errors.Add(erro.ErrorMessage);
        }

        protected bool Validade<V, O>(V validator, O obj) where V :AbstractValidator<O>
        {
            var validation = validator.Validate(obj);
            if(validation.Errors.Count > 0)
                AddErrorList(validation.Errors);

            return IsValid;
        }

        public string ErrorsToString()
        {
            var builder = new StringBuilder();
            foreach(var error in errors)
                builder.AppendLine(error);
            
            return builder.ToString();
        }
    }
}