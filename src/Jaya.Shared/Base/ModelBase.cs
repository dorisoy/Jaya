using System.Runtime.CompilerServices;

namespace Jaya.Shared.Base
{
    public abstract class ModelBase : NotificationBase
    {
        readonly Lazy<Dictionary<string, object>> _backingStore;

        protected ModelBase() : base()
        {
            _backingStore = new Lazy<Dictionary<string, object>>();

            IsNotificationEnabled = true;
        }

        protected T Get<T>([CallerMemberName] string propertyName = "")
        {
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentNullException(nameof(propertyName), "Property name can't be empty.");

            if (_backingStore.Value.ContainsKey(propertyName))
                return (T)_backingStore.Value[propertyName];

            return default;
        }

        protected void Set<T>(T value, [CallerMemberName] string propertyName = "")
        {
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentNullException(nameof(propertyName), "Property name can't be empty.");

            if (_backingStore.Value.ContainsKey(propertyName))
                _backingStore.Value[propertyName] = value;
            else
                _backingStore.Value.Add(propertyName, value);
        }
    }
}
