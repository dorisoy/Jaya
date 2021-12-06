using Jaya.Shared.Base;

namespace Jaya.Shared.Storage
{
    public abstract class Service: NotificationBase
    {
        bool _isEnabled;
        string _name;
        string? _description;
        string? _imageData;
        Author? _author;
        Version _version;

        protected Service(string name, Version version): base()
        {
            _name = name;
            _version = version ?? Version.Parse("0.0.0.0");
        }

        #region properties

        public string Name 
        {
            get => _name; 
            protected set
            {
                _name = value;
                RaisePropertyChanged();
            } 
        }

        public string? Description
        {
            get => _description;
            protected set
            {
                _description = value;
                RaisePropertyChanged();
            }
        }

        public string? ImageData
        {
            get => _imageData;
            protected set
            {
                _imageData = value;
                RaisePropertyChanged();
            }
        }

        public Author? Author
        {
            get => _author;
            protected set
            {
                _author = value;
                RaisePropertyChanged();
            }
        }

        public Version Version
        {
            get => _version;
            protected set
            {
                _version = value;
                RaisePropertyChanged();
            }
        }

        public bool IsEnabled
        {
            get => _isEnabled;
            set
            {
                _isEnabled = value;
                RaisePropertyChanged();
            }
        }

        #endregion
    }
}
