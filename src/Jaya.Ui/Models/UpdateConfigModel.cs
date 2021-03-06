//
// Copyright (c) Rubal Walia. All rights reserved.
// Licensed under the 3-Clause BSD license. See LICENSE file in the project root for full license information.
//
using Jaya.Shared.Base;
using Newtonsoft.Json;
using System;
using System.IO;

namespace Jaya.Ui.Models
{
    public class UpdateConfigModel : ConfigModelBase
    {
        [JsonProperty]
        public DateTime Checked
        {
            get => Get<DateTime>();
            set => Set(value);
        }

        [JsonProperty]
        public ReleaseModel Update
        {
            get => Get<ReleaseModel>();
            set => Set(value);
        }

        [JsonProperty]
        public string DownloadDirectory
        {
            get => Get<string>();
            set => Set(value);
        }

        protected override ConfigModelBase Empty()
        {
            return new UpdateConfigModel
            {
                Checked = DateTime.MinValue,
                DownloadDirectory = Path.Combine(Constants.DATA_DIRECTORY, "Download")
            };
        }
    }
}
