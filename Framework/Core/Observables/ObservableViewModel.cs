using System.Collections;
using System.ComponentModel;

namespace TheXamlGuy.Framework.Core
{
    public class ObservableViewModel : ObservableObject, IObservableViewModel
    {
        public ObservableViewModel(IPropertyBuilder propertyBuilder,
            IEventAggregator eventAggregator,
            IServiceFactory serviceFactory,
            IDisposer disposer)
        {
            PropertyBuilder = propertyBuilder;
            EventAggregator = eventAggregator;
            ServiceFactory = serviceFactory;
            Disposer = disposer;

            ValidationErrors = new PropertyValidationError<string, string>();
        }

        public event System.EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        public IDisposer Disposer { get; }

        public IEventAggregator EventAggregator { get; }

        public bool HasErrors => ValidationErrors.Count > 0;

        public bool IsInitialized { get; private set; }

        public IPropertyBuilder PropertyBuilder { get; }

        public IServiceFactory ServiceFactory { get; }

        public PropertyValidationError<string, string> ValidationErrors { get; }

        public void Dispose()
        {
            OnDisposing();

            Disposer.Dispose(this);
            GC.SuppressFinalize(this);
        }

        public IEnumerable GetErrors(string? propertyName)
        {
            return propertyName is not null && ValidationErrors.Contains(propertyName) ? ValidationErrors[propertyName]! : Array.Empty<string>();
        }

        public void Initialize()
        {
            if (IsInitialized)
            {
                return;
            }

            IsInitialized = true;
            OnInitialize();
        }

        public override void OnPropertyChanged(string propertyName, object? before, object? after)
        {
            SetProperty(propertyName, false);
            base.OnPropertyChanged(propertyName, before, after);
        }

        protected virtual void ClearValidationErrors()
        {
            foreach (PropertyBinder? binder in PropertyBuilder.Binders)
            {
                if (binder.PropertyName is { })
                {
                    if (ValidationErrors.Contains(binder.PropertyName))
                    {
                        ValidationErrors.Remove(binder.PropertyName);
                        OnErrorsChanged(binder.PropertyName);
                    }
                }
            }

            OnPropertyChanged(nameof(ValidationErrors), null, null);
        }

        protected virtual void OnDisposing()
        {

        }

        protected virtual void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        protected virtual void OnInitialize()
        {

        }

        protected void SetProperty(string propertyName)
        {
            SetProperty(propertyName, true);
        }

        protected void SetProperty(string? propertyName, bool isExplicit)
        {
            if (propertyName is { })
            {
                if (PropertyBuilder.Binders.TryGet(propertyName, out PropertyBinder? binder) && binder is { })
                {
                    if (binder.Mode == PropertyChangedMode.Explicit && !isExplicit)
                    {
                        return;
                    }

                    ClearValidationError(propertyName);
                    if (!binder.TryValidate(out string? message) && message is { })
                    {
                        AddValidationError(propertyName, message);
                    }
                }
            }
        }

        protected virtual bool Validate(bool clearPreviousErrors = true)
        {
            if (clearPreviousErrors)
            {
                ClearValidationErrors();
            }

            foreach (PropertyBinder? binder in PropertyBuilder.Binders)
            {
                if (binder.PropertyName is { })
                {
                    if (!binder.TryValidate(out string? message) && message is { })
                    {
                        AddValidationError(binder.PropertyName, message);
                    }
                }
            }

            return !HasErrors;
        }

        private void AddValidationError(string propertyName, string validationMessage)
        {
            if (propertyName is { })
            {
                OnErrorsChanged(propertyName);
                ValidationErrors[propertyName] = validationMessage;

                OnPropertyChanged(nameof(ValidationErrors), null, null);
            }
        }

        private void ClearValidationError(string propertyName)
        {
            if (ValidationErrors.Contains(propertyName))
            {
                ValidationErrors.Remove(propertyName);
                OnErrorsChanged(propertyName);
            }

            OnPropertyChanged(nameof(ValidationErrors), null, null);
        }
    }
}