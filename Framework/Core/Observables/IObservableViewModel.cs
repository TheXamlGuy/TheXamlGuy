using System.ComponentModel;

namespace TheXamlGuy.Framework.Core
{
    public interface IObservableViewModel : INotifyDataErrorInfo, IDisposable
    {
        void Initialize();

        bool IsInitialized { get; }
    }
}