using Jaya.Shared.Base;
using System.Collections.ObjectModel;

namespace Jaya.Shared.Storage
{
    public sealed class Item: NotificationBase
    {
        string? _name;
        string? _path;
        long? _length;
        string? _imageData;
        readonly Lazy<ObservableCollection<Item>> _items;

        public Item(): base()
        {
            _items = new Lazy<ObservableCollection<Item>>();
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

        public string? Path
        {
            get => _path;
            set
            {
                _path = value;
                RaisePropertyChanged();
            }
        }

        public long? Length
        {
            get => _length;
            set
            {
                _length = value;
                RaisePropertyChanged();
            }
        }

        public string? ImageData
        {
            get => _imageData;
            set
            {
                _imageData = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<Item> Items => _items.Value;

        #endregion
    }
}
