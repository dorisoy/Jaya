using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Jaya.Shared.Base
{
    public abstract class NotificationBase : INotifyPropertyChanged, INotifyPropertyChanging
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public event PropertyChangingEventHandler? PropertyChanging;

        public bool IsNotificationEnabled { get; set; }

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentNullException(nameof(propertyName), "Property name can't be empty.");

            if (!IsNotificationEnabled || PropertyChanged == null)
                return;

            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
