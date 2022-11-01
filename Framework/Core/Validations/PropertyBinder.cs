using System;
using System.Diagnostics.CodeAnalysis;

namespace TheXamlGuy.Framework.Core
{
    public class PropertyBinder
    {
        private readonly Action? propertyChanged;
        private readonly PropertyValidation? propertyValidation;

        internal PropertyBinder(string propertyName, Action propertyChanged)
        {
            PropertyName = propertyName;
            this.propertyChanged = propertyChanged;
        }

        internal PropertyBinder(string propertyName,
            Action propertyChanged,
            PropertyChangedMode mode)
        {
            PropertyName = propertyName;
            Mode = mode;

            this.propertyChanged = propertyChanged;
        }

        internal PropertyBinder(string propertyName,
            Action propertyChanged,
            PropertyValidation validation,
            PropertyChangedMode mode)
        {
            PropertyName = propertyName;
            Mode = mode;

            this.propertyChanged = propertyChanged;
            propertyValidation = validation;
        }

        internal PropertyBinder(string propertyName,
            Action propertyChanged,
            PropertyValidation validation)
        {
            PropertyName = propertyName;

            this.propertyChanged = propertyChanged;
            propertyValidation = validation;
        }

        internal PropertyBinder(string propertyName, PropertyValidation validation)
        {
            PropertyName = propertyName;
            propertyValidation = validation;
        }

        internal PropertyBinder(string propertyName, PropertyValidation validation, PropertyChangedMode mode)
        {
            PropertyName = propertyName;
            Mode = mode;

            propertyValidation = validation;
        }

        public PropertyChangedMode Mode { get; }

        public string? PropertyName { get; }

        public void Set()
        {
            propertyChanged?.Invoke();
        }

        public bool TryValidate([MaybeNull] out string message)
        {
            message = "";

            if (propertyValidation is not null && propertyValidation.Validation?.Invoke() == false)
            {
                message = propertyValidation.Message;
                return false;
            }

            propertyChanged?.Invoke();
            return true;
        }
    }
}