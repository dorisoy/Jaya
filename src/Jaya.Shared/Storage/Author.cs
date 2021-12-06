using Jaya.Shared.Base;

namespace Jaya.Shared.Storage
{
    public sealed class Author : NotificationBase
    {
        string? _name;
        string? _email;
        string? _phone;
        Uri? _website;

        public Author(): base()
        {
            IsNotificationEnabled = true;
        }

        #region properties

        public string? Name
        {
            get => _name;
            set
            {
                _name = value;
                RaisePropertyChanged();
            }
        }

        public string? Email
        {
            get => _email;
            set
            {
                _email = value;
                RaisePropertyChanged();
            }
        }

        public string? Phone
        {
            get => _phone;
            set
            {
                _phone = value;
                RaisePropertyChanged();
            }
        }

        public Uri? Website
        {
            get => _website;
            set
            {
                _website = value;
                RaisePropertyChanged();
            }
        }

        #endregion
    }
}
