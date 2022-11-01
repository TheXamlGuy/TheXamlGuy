using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Reactive.Disposables;

namespace TheXamlGuy.Framework.Core
{
    public class ObservableViewModelCollection<TItemViewModel> :
          ObservableCollection<TItemViewModel>, IObservableViewModel
          where TItemViewModel : class
    {
        private readonly ObservableCollection<TItemViewModel>? source;

        public ObservableViewModelCollection(IPropertyBuilder propertyBuilder,
            IEventAggregator eventAggregator,
            IServiceFactory serviceFactory,
            IDisposer disposer,
            ObservableCollection<TItemViewModel> source)
        {
            PropertyBuilder = propertyBuilder;
            EventAggregator = eventAggregator;
            ServiceFactory = serviceFactory;
            Disposer = disposer;

            this.source = source;
            source.CollectionChanged += OnSourceCollectionChanged;
            
            AddRange(source);

            ValidationErrors = new PropertyValidationError<string, string>();
        }

        public ObservableViewModelCollection(IPropertyBuilder propertyBuilder,
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

        public TItemViewModel Add()
        {
            TItemViewModel? item = ServiceFactory.Create<TItemViewModel>();
            Disposer.Add(this, item);

            base.Add(item);
            return item;
        }

        public TItemViewModel Add<T>() where T : TItemViewModel
        {
            T? item = ServiceFactory.Create<T>();
            Disposer.Add(this, item);
            base.Add(item);

            return item;
        }

        public TItemViewModel Add<T>(params object?[] parameters) where T : TItemViewModel
        {
            T? item = ServiceFactory.Create<T>(parameters);

            Disposer.Add(this, item);
            Disposer.Add(item, Disposable.Create(() =>
            {
                if (!isClearing)
                {
                    if (Contains(item))
                    {
                        Remove(item);
                    }
                }
            }));

            base.Add(item);
            return item;
        }

        public new TItemViewModel Add(TItemViewModel item)
        {
            Disposer.Add(this, item);
            Disposer.Add(item, Disposable.Create(() =>
            {
                if (!isClearing)
                {
                    if (Contains(item))
                    {
                        Remove(item);
                    }
                }
            }));

            base.Add(item);
            return item;
        }

        public TItemViewModel Add(params object?[] parameters)
        {
            TItemViewModel? item = ServiceFactory.Create<TItemViewModel>(parameters);

            Disposer.Add(this, item);
            Disposer.Add(item, Disposable.Create(() =>
            {
                if (!isClearing)
                {
                    if (Contains(item))
                    {
                        Remove(item);
                    }
                }
            }));

            base.Add(item);
            return item;
        }

        public void AddRange(ICollection<TItemViewModel> items)
        {
            foreach (TItemViewModel? item in items)
            {
                AddItemToDisposer(item);
                base.Add(item);
            }
        }

        private bool isClearing;

        public new void Clear()
        {
            isClearing = true;

            foreach (TItemViewModel? item in this)
            {
                if (item is not IKeepAlive)
                {
                    Disposer.Dispose(item);
                }
            }

            base.Clear();
            isClearing = false;
        }

        public void Dispose()
        {
            OnDisposing();

            if (source is not null)
            {
                source.CollectionChanged -= OnSourceCollectionChanged;
            }

            Disposer.Dispose(this);
            Clear();

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

        public void Insert<TItem>(params object[] parameters) where TItem : TItemViewModel
        {
            TItem? item = ServiceFactory.Create<TItem>(parameters);
            AddItemToDisposer(item);

            base.Add(item);
        }

        public new void Insert(int index, TItemViewModel item)
        {
            AddItemToDisposer(item);
            base.Insert(index, item);
        }

        public void Insert<TItem>(int index, params object[] parameters) where TItem : TItemViewModel
        {
            TItem? item = ServiceFactory.Create<TItem>(parameters);
            AddItemToDisposer(item);

            base.Insert(index, item);
        }

        public void Insert(TItemViewModel item)
        {
            base.Insert(0, item);
            AddItemToDisposer(item);
        }

        public new void Remove(TItemViewModel item)
        {
            if (item is not IKeepAlive)
            {
                Disposer.Dispose(item);
            }

            base.Remove(item);
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

            OnPropertyChanged(new PropertyChangedEventArgs(nameof(ValidationErrors)));
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

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            SetProperty(args.PropertyName, false);
            base.OnPropertyChanged(args);
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

        private void AddItemToDisposer(TItemViewModel? item)
        {
            if (item is not IKeepAlive)
            {
                Disposer.Add(this, item!);
            }
        }
        private void AddValidationError(string propertyName, string validationMessage)
        {
            if (propertyName is { })
            {
                OnErrorsChanged(propertyName);
                ValidationErrors[propertyName] = validationMessage;

                OnPropertyChanged(new PropertyChangedEventArgs(nameof(ValidationErrors)));
            }
        }

        private void ClearValidationError(string propertyName)
        {
            if (ValidationErrors.Contains(propertyName))
            {
                ValidationErrors.Remove(propertyName);
                OnErrorsChanged(propertyName);
            }

            OnPropertyChanged(new PropertyChangedEventArgs(nameof(ValidationErrors)));
        }

        private void OnSourceCollectionChanged(object? sender, NotifyCollectionChangedEventArgs args)
        {
            SynchronizeCollection(this, args);
        }

        private void SynchronizeCollection(ObservableCollection<TItemViewModel> target, NotifyCollectionChangedEventArgs args)
        {
            TItemViewModel[]? newItems = args.NewItems?.Cast<TItemViewModel>().ToArray();
            TItemViewModel[]? oldItems = args.OldItems?.Cast<TItemViewModel>().ToArray();

            switch (args.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    if (newItems is not null)
                    {
                        for (int index = 0; index < newItems.Length; index++)
                        {
                            target.Insert(args.NewStartingIndex + index, newItems[index]);
                        }
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    if (oldItems is not null)
                    {
                        for (int index = 0; index < oldItems.Length; index++)
                        {
                            RemoveAt(args.OldStartingIndex);
                        }
                        break;
                    }
                    break;
                case NotifyCollectionChangedAction.Replace:
                    if (oldItems is not null)
                    {
                        for (int index = 0; index < oldItems.Length; index++)
                        {
                            target.RemoveAt(index);
                        }
                    }
                    if (newItems is not null)
                    {
                        for (int index = 0; index < newItems.Length; index++)
                        {
                            target.Insert(index, newItems[index]);
                        }
                    }
                    break;
                case NotifyCollectionChangedAction.Move:
                    break;
                case NotifyCollectionChangedAction.Reset:
                    target.Clear();
                    if (newItems is not null)
                    {
                        for (int index = 0; index < newItems.Length; index++)
                        {
                            target.Insert(index, newItems[index]);
                        }
                    }
                    break;
                default:
                    break;
            }
        }
    }
}