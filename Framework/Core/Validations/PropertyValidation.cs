using System;

namespace TheXamlGuy.Framework.Core
{
    public class PropertyValidation
    {
        public PropertyValidation(Func<bool> validation)
        {
            Validation = validation;
        }

        public PropertyValidation(Func<bool> validation, string message)
        {
            Validation = validation;
            Message = message;
        }

        public string? Message { get; }

        public Func<bool>? Validation { get; }
    }
}