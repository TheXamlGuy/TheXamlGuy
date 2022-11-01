using System;
using System.Linq.Expressions;

namespace TheXamlGuy.Framework.Core
{
    public interface IPropertyBuilder
    {
        IPropertyBinderCollection Binders { get; }

        void Add<TProperty>(Expression<Func<TProperty>> property, Action propertyChanged);

        void Add<TProperty>(Expression<Func<TProperty>> property, Action propertyChanged,
            PropertyValidation propertyChangedValidation);

        void Add<TProperty>(Expression<Func<TProperty>> property, PropertyValidation propertyChangedValidation);

        void Add<TProperty>(Expression<Func<TProperty>> property, PropertyValidation propertyChangedValidation,
            PropertyChangedMode mode);

        void Add<TProperty>(Expression<Func<TProperty>> property, Action propertyChanged,
            PropertyValidation propertyChangedValidation, PropertyChangedMode mode);

        void Add<TProperty>(Expression<Func<TProperty>> property, Action propertyChanged, PropertyChangedMode mode);
    }
}