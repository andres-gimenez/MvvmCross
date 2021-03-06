﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MS-PL license.
// See the LICENSE file in the project root for more information.

using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Localization;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Playground.Core.ViewModels.Bindings;

namespace Playground.Core.ViewModels
{
    public class MainViewModel : MvxNavigationViewModel
    {
        private string _bindableText = "I'm bound!";

        private int _counter = 2;

        public MainViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            ShowChildCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<ChildViewModel>().ConfigureAwait(false));

            ShowModalCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<ModalViewModel>().ConfigureAwait(false));

            ShowModalNavCommand =
                new MvxAsyncCommand(async () => await NavigationService.Navigate<ModalNavViewModel>().ConfigureAwait(false));

            ShowTabsCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<TabsRootViewModel>().ConfigureAwait(false));

            ShowSplitCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<SplitRootViewModel>().ConfigureAwait(false));

            ShowOverrideAttributeCommand = new MvxAsyncCommand(async () =>
                await NavigationService.Navigate<OverrideAttributeViewModel>().ConfigureAwait(false));

            ShowSheetCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<SheetViewModel>().ConfigureAwait(false));

            ShowWindowCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<WindowViewModel>().ConfigureAwait(false));

            ShowMixedNavigationCommand =
                new MvxAsyncCommand(async () => await NavigationService.Navigate<MixedNavFirstViewModel>().ConfigureAwait(false));

            ShowCustomBindingCommand =
                new MvxAsyncCommand(async () => await NavigationService.Navigate<CustomBindingViewModel>().ConfigureAwait(false));

            _counter = 3;
        }

        public IMvxAsyncCommand ShowChildCommand { get; }

        public IMvxAsyncCommand ShowModalCommand { get; }

        public IMvxAsyncCommand ShowModalNavCommand { get; }

        public IMvxAsyncCommand ShowTabsCommand { get; }

        public IMvxAsyncCommand ShowCustomBindingCommand { get; }

        public IMvxAsyncCommand ShowSplitCommand { get; }

        public IMvxAsyncCommand ShowOverrideAttributeCommand { get; }

        public IMvxAsyncCommand ShowSheetCommand { get; }

        public IMvxAsyncCommand ShowWindowCommand { get; }

        public IMvxAsyncCommand ShowMixedNavigationCommand { get; }

        public IMvxLanguageBinder TextSource => new MvxLanguageBinder("MvxBindingsExample", "Text");

        public string BindableText
        {
            get => _bindableText;
            set
            {
                SetProperty(ref _bindableText, value);
            }
        }

        protected override async ValueTask SaveStateToBundle(IMvxBundle bundle)
        {
            await base.SaveStateToBundle(bundle).ConfigureAwait(false);

            bundle.Data["MyKey"] = _counter.ToString();
        }

        protected override async ValueTask ReloadFromBundle(IMvxBundle state)
        {
            await base.ReloadFromBundle(state).ConfigureAwait(false);

            _counter = int.Parse(state.Data["MyKey"]);
        }
    }
}
