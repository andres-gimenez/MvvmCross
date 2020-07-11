﻿
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MS-PL license.
// See the LICENSE file in the project root for more information.

using System;
using System.Threading.Tasks;
using MvvmCross.Presenters;
using MvvmCross.Presenters.Attributes;

namespace MvvmCross.Platforms.Tizen.Presenters
{
    public class MvxTizenViewPresenter : MvxAttributeViewPresenter, IMvxTizenViewPresenter
    {
        public override ValueTask<MvxBasePresentationAttribute?> CreatePresentationAttribute(Type? viewModelType, Type? viewType)
        {
            return new ValueTask<MvxBasePresentationAttribute?>((MvxBasePresentationAttribute?)null);
        }

        public override void RegisterAttributeTypes()
        {
        }
    }
}
