using System;
namespace Manager.Core.Exceptions
{
    public class DomainException: Exception
    {
        internal List<string> errors;
        public IReadOnlyCollection<string> Errors => errors;
        public DomainException(){}
        public DomainException(string message, List<string> errors): base(message)
        {
            this.errors = errors;
        }
        public DomainException(string message): base(message){}
        public DomainException(string message, Exception innerException): base(message, innerException){}
    }
}