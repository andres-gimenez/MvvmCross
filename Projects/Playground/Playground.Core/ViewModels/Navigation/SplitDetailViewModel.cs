﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MS-PL license.
// See the LICENSE file in the project root for more information.

using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Playground.Core.ViewModels
{
    public class SplitDetailViewModel : MvxNavigationViewModel
    {
        public SplitDetailViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            ShowChildCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<SplitDetailNavViewModel>().ConfigureAwait(false));
            ShowTabsCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<TabsRootBViewModel>().ConfigureAwait(false));
            ShowTabbedChildCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<TabsRootViewModel>().ConfigureAwait(false));
        }

        public IMvxAsyncCommand ShowChildCommand { get; private set; }
        public IMvxAsyncCommand ShowTabsCommand { get; private set; }
        public IMvxAsyncCommand ShowTabbedChildCommand { get; private set; }

        public string ContentText => "Text for the Content Area";

        public override ValueTask ViewAppeared()
        {
            return base.ViewAppeared();
        }
    }
}
