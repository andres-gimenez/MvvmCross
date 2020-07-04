﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MS-PL license.
// See the LICENSE file in the project root for more information.

using System.Threading.Tasks;

namespace MvvmCross.Plugin.JsonLocalization
{
    public interface IMvxJsonDictionaryTextLoader
    {
        ValueTask LoadJsonFromResource(string namespaceKey, string typeKey, string resourcePath);

        void LoadJsonFromText(string namespaceKey, string typeKey, string rawJson);
    }
}
