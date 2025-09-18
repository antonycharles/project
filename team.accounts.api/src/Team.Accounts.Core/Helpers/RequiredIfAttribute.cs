using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Team.Accounts.Core.Helpers
{
    public class RequiredIfAttribute : ValidationAttribute
{
    private readonly string _dependentProperty;
    private readonly object _targetValue;

    public RequiredIfAttribute(string dependentProperty, object targetValue)
    {
        _dependentProperty = dependentProperty;
        _targetValue = targetValue;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var property = validationContext.ObjectType.GetProperty(_dependentProperty);

        if (property == null)
            return new ValidationResult($"Propriedade {_dependentProperty} não encontrada.");

        var dependentValue = property.GetValue(validationContext.ObjectInstance);

        if ((dependentValue == null && _targetValue == null) ||
            (dependentValue != null && dependentValue.Equals(_targetValue)))
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return new ValidationResult(ErrorMessage ?? $"{validationContext.DisplayName} é obrigatório.");
            }
        }

        return ValidationResult.Success;
    }
}
}