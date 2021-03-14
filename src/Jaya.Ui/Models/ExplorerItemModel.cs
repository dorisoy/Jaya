//
// Copyright (c) Rubal Walia. All rights reserved.
// Licensed under the 3-Clause BSD license. See LICENSE file in the project root for full license information.
//
using Jaya.Shared.Base;
using Jaya.Shared.Models;
using System.Collections.ObjectModel;

namespace Jaya.Ui.Models
{
    public class ExplorerItemModel: ModelBase
    {
        public ExplorerItemModel(ItemType? type, string label, string imagePath = null, ProviderServiceBase service = null, AccountModelBase account = null, FileSystemObjectModel fsObject = null)
        {
            Type = type;
            Label = label;
            Children = new ObservableCollection<ExplorerItemModel>();

            if (service != null)
                Service = service;
            
            if (account != null)
                Account = account;

            if (fsObject != null)
                FileSystemObject = fsObject;
            //Object = obj;
            ImagePath = imagePath;
        }

        #region properties

        internal ItemType? Type { get; }

        //public object Object { get; }
        public FileSystemObjectModel FileSystemObject
        {
            get => Get<FileSystemObjectModel>();
            set => Set(value);
        }

        public AccountModelBase Account { get; }

        public ProviderServiceBase Service { get; }

        public bool IsDummy => Type == ItemType.Dummy;

        public bool IsService => Type == ItemType.Service;

        public bool IsDrive => Type == ItemType.Drive;

        public bool IsDirectory => Type == ItemType.Directory;

        public bool IsAccount => Type == ItemType.Account;

        public bool IsComputer => Type == ItemType.Computer;

        public bool IsFile => Type == ItemType.File;

        public bool IsHavingMetaData => IsAccount || IsDrive || IsDirectory;

        public string Label
        {
            get => Get<string>();
            set => Set(value);
        }

        public string ImagePath
        {
            get => Get<string>();
            set => Set(value);
        }

        public ObservableCollection<ExplorerItemModel> Children { get; }

        #endregion
    }
}
