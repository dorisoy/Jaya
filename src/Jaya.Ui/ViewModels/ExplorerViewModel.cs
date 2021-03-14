//
// Copyright (c) Rubal Walia. All rights reserved.
// Licensed under the 3-Clause BSD license. See LICENSE file in the project root for full license information.
//
using Jaya.Shared;
using Jaya.Shared.Base;
using Jaya.Shared.Models;
using Jaya.Ui.Models;
using Jaya.Ui.Services;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Jaya.Ui.ViewModels
{
    public class ExplorerViewModel: ViewModelBase
    {
        readonly Subscription<SelectionChangedEventArgs> _onSelectionChanged;
        readonly SharedService _shared;

        ICommand _invokeObject;
        ProviderServiceBase _service;
        AccountModelBase _account;

        public ExplorerViewModel()
        {
            _shared = GetService<SharedService>();
            _onSelectionChanged = EventAggregator?.Subscribe<SelectionChangedEventArgs>(SelectionChanged);
        }

        ~ExplorerViewModel()
        {
            EventAggregator?.UnSubscribe(_onSelectionChanged);
        }

        #region properties

        public ICommand InvokeObjectCommand
        {
            get
            {
                if (_invokeObject == null)
                    _invokeObject = new RelayCommand<ExplorerItemModel>(InvokeObject);

                return _invokeObject;
            }
        }

        public ApplicationConfigModel ApplicationConfig => _shared.ApplicationConfiguration;

        public PaneConfigModel PaneConfig => _shared.PaneConfiguration;

        public ExplorerItemModel Item
        {
            get => Get<ExplorerItemModel>();
            private set => Set(value);
        }

        #endregion

        void InvokeObject(ExplorerItemModel obj)
        {
            if (!obj.Type.HasValue)
                return;

            IsBusy = true;

            DirectoryModel directory = null;
            switch (obj.Type.Value)
            {
                //case ItemType.Computer:
                case ItemType.Drive:
                case ItemType.Directory:
                    /*string dirPath = "NULL";
                    bool hasObj = obj.Object != null;
                    if (hasObj)
                    {
                        dirPath = "NOT NULL: ";
                        if (obj.Object is FileSystemObjectModel mdl)
                            dirPath += mdl.Path;
                        else
                            dirPath += obj.Object.GetType().FullName;
                    }

                    Debug.WriteLine("DirectoryModel Time!\n  ItemType: " + obj.Type.Value + "\n      DirectoryPath: " + dirPath);*/


                    /*if ((obj.Type.Value == ItemType.Computer) && obj.Object is AccountModelBase accnt)
                        _account = accnt;*/
                    
                    directory = obj.FileSystemObject as DirectoryModel;
                    break;

                case ItemType.File:
                    break;

                case ItemType.Computer:
                    //Debug.WriteLine("obj.Object.GetType().FullName: " + obj.Object.GetType().FullName);
                    _account = obj.Account as AccountModelBase;
                    directory = obj.FileSystemObject as DirectoryModel;
                    //obj.Children
                    //obj.
                    //Debug.WriteLine("obj.Children.Count: " + obj.Children.Count);
                    /*directory = new DirectoryModel()
                    {};*/
                    //directory = obj.Object as DirectoryModel;
                    break;
                case ItemType.Account:
                    _account = obj.Account as AccountModelBase;
                    break;

                case ItemType.Service:
                    _service = obj.Service as ProviderServiceBase;
                    _account = null;
                    break;
            }

            IsBusy = false;

            var eventArgs = new SelectionChangedEventArgs(_service, _account, directory);
            EventAggregator.Publish(eventArgs);
        }

        async void SelectionChanged(SelectionChangedEventArgs args)
        {
            Item = null;
            IsBusy = true;

            
            _service = args.Service;
            _account = args.Account;

            if (_account == null)
            {
                Debug.WriteLine("_account == null");
                var accounts = await _service.GetAccountsAsync();
                var serviceItem = new ExplorerItemModel(ItemType.Service, _service.Name, _service.ImagePath);

                foreach (var account in accounts)
                {
                    await Task.Run(new Action(() =>
                    {
                        if (_service.IsRootDrive)
                            serviceItem.Children.Add(new ExplorerItemModel(ItemType.Computer, account.Name, account: account, fsObject: new DirectoryModel(), service: _service));
                        else
                            serviceItem.Children.Add(new ExplorerItemModel(ItemType.Account, account.Name, account: account));
                    }));
                }
                
                Item = serviceItem;
            }
            else if (args.Directory != null)
            {
                Debug.WriteLine("args.Directory != null");
                var directory = await args.Service.GetDirectoryAsync(args.Account, args.Directory);
                var directoryItem = new ExplorerItemModel(directory.Type == FileSystemObjectType.Drive ? ItemType.Drive : ItemType.Directory, directory.Name, fsObject: directory);

                foreach (var subDirectory in directory.Directories)
                {
                    await Task.Run(new Action(() =>
                    {
                        var subDirectoryItem = new ExplorerItemModel(subDirectory.Type == FileSystemObjectType.Drive ? ItemType.Drive : ItemType.Directory, subDirectory.Name, fsObject: subDirectory);
                        directoryItem.Children.Add(subDirectoryItem);
                    }));
                }

                if (directory.Files != null)
                {
                    foreach (var file in directory.Files)
                    {
                        await Task.Run(new Action(() =>
                        {
                            var fileItem = new ExplorerItemModel(ItemType.File, file.Name, fsObject: file);
                            directoryItem.Children.Add(fileItem);
                        }));
                    }
                }

                Item = directoryItem;
            }
            else
                Debug.WriteLine("NO DIRECTORY");

            IsBusy = false;
        }
    }
}
