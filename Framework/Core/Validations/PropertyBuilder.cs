using System;
using System.Linq.Expressions;

namespace TheXamlGuy.Framework.Core
{
    public class PropertyBuilder : IPropertyBuilder
    {
        public PropertyBuilder(IPropertyBinderCollection binders)
        {
            Binders = binders;
        }

        public IPropertyBinderCollection Binders { get; }

        public void Add<TProperty>(Expression<Func<TProperty>> property, PropertyValidation propertyChangedValidation)
        {
            string? name = GetPropertyName(property);
            Binders.Add(name, new PropertyBinder(name, propertyChangedValidation));
        }

        public void Add<TProperty>(Expression<Func<TProperty>> property, Action propertyChanged,
            PropertyValidation propertyChangedValidation)
        {
            string? name = GetPropertyName(property);
            Binders.Add(name, new PropertyBinder(name, propertyChanged, propertyChangedValidation));
        }

        public void Add<TProperty>(Expression<Func<TProperty>> property, Action propertyChanged)
        {
            string? name = GetPropertyName(property);
            Binders.Add(name, new PropertyBinder(name, propertyChanged));
        }

        public void Add<TProperty>(Expression<Func<TProperty>> property, PropertyValidation propertyChangedValidation, PropertyChangedMode mode)
        {
            string? name = GetPropertyName(property);
            Binders.Add(name, new PropertyBinder(name, propertyChangedValidation, mode));
        }

        public void Add<TProperty>(Expression<Func<TProperty>> property, Action propertyChanged, PropertyValidation propertyChangedValidation, PropertyChangedMode mode)
        {
            string? name = GetPropertyName(property);
            Binders.Add(name, new PropertyBinder(name, propertyChanged, propertyChangedValidation, mode));
        }

        public void Add<TProperty>(Expression<Func<TProperty>> property, Action propertyChanged, PropertyChangedMode mode)
        {
            string? name = GetPropertyName(property);
            Binders.Add(name, new PropertyBinder(name, propertyChanged, mode));
        }

        private string GetPropertyName<T>(Expression<Func<T>> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            Expression? body = predicate.Body;
            MemberExpression? memberExpression = body as MemberExpression ?? (MemberExpression)((UnaryExpression)body).Operand;
            return memberExpression.Member.Name;
        }
    }
}