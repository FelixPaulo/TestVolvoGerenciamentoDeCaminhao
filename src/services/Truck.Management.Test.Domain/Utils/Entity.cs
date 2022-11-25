using FluentValidation;
using FluentValidation.Results;

namespace Truck.Management.Test.Domain.Utils
{
    public abstract class Entity<T> : AbstractValidator<T> where T : Entity<T>
    {
        protected Entity()
        {
            ValidationResult = new ValidationResult();
        }

        public abstract bool IsValid();
        public ValidationResult ValidationResult { get; protected set; }
    }
}
