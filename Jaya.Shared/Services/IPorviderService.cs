﻿using Jaya.Shared.Base;
using System;

namespace Jaya.Shared.Services
{
    public interface IPorviderService
    {

        bool IsRootDrive { get; }

        string Name { get; }

        string Description { get; }

        string ImagePath { get; }

        Type ConfigurationEditorType { get; }

        ConfigModelBase Configuration { get; }
    }
}