using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Accounts.Core.Attributes
{
    public class RequiredIfAttribute : ValidationAttribute
    {
        private readonly string _propertyName;
        private readonly object? _isValue;
        private readonly object? _isValue2;

        public RequiredIfAttribute(string propertyName, object? isValue)
        {
            _propertyName = propertyName ?? throw new ArgumentNullException(nameof(propertyName));
            _isValue = isValue;
        }

        public RequiredIfAttribute(string propertyName, object? isValue, object? isValue2)
        {
            _propertyName = propertyName ?? throw new ArgumentNullException(nameof(propertyName));
            _isValue = isValue;
            _isValue2 = isValue2;
        }

        public override string FormatErrorMessage(string name)
        {
            var errorMessage = $"Property {name} is required when {_propertyName} is {_isValue}";
            return ErrorMessage ?? errorMessage;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            ArgumentNullException.ThrowIfNull(validationContext);
            var property = validationContext.ObjectType.GetProperty(_propertyName);

            if (property == null)
            {
                throw new NotSupportedException($"Can't find {_propertyName} on searched type: {validationContext.ObjectType.Name}");
            }

            var requiredIfTypeActualValue = property.GetValue(validationContext.ObjectInstance);

            if (requiredIfTypeActualValue == null && _isValue != null)
            {
                return ValidationResult.Success;
            }

            if (requiredIfTypeActualValue == null || requiredIfTypeActualValue.Equals(_isValue) || requiredIfTypeActualValue.Equals(_isValue2))
            {
                return value == null
                    ? new ValidationResult(FormatErrorMessage(validationContext.DisplayName))
                    : ValidationResult.Success;
            }

            return ValidationResult.Success;
        }
    }
}