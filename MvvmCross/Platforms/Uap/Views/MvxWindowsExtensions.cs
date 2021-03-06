﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MS-PL license.
// See the LICENSE file in the project root for more information.

using System;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using MvvmCross.Platforms.Uap.Presenters.Attributes;
using MvvmCross.ViewModels;
using System.Threading.Tasks;
using System.Reflection.Metadata.Ecma335;

namespace MvvmCross.Platforms.Uap.Views
{
    public static class MvxWindowsExtensions
    {
        public static ValueTask OnViewCreate(this IMvxWindowsView storeView, string requestText, Func<IMvxBundle?> bundleLoader)
        {
            if (bundleLoader == null) throw new NullReferenceException(nameof(bundleLoader));

            return storeView.OnViewCreate(() => storeView.LoadViewModel(requestText, bundleLoader()));
        }

        public static async ValueTask OnViewCreate(this IMvxWindowsView storeView, Func<ValueTask<IMvxViewModel>> viewModelLoader)
        {
            if (storeView == null) throw new NullReferenceException(nameof(storeView));
            if (viewModelLoader == null) throw new NullReferenceException(nameof(viewModelLoader));

            if (storeView.ViewModel != null)
                return;

            var viewModel = await viewModelLoader().ConfigureAwait(false);
            storeView.ViewModel = viewModel;
        }

        public static void OnViewDestroy(this IMvxWindowsView storeView, int key)
        {
            if (key > 0)
            {
                var viewModelLoader = Mvx.IoCProvider.Resolve<IMvxWindowsViewModelRequestTranslator>();
                viewModelLoader.RemoveSubViewModelWithKey(key);
            }
        }

        public static bool HasRegionAttribute(this Type view)
        {
            if (view == null) throw new NullReferenceException(nameof(view));

            var attributes = view
                .GetCustomAttributes(typeof(MvxRegionPresentationAttribute), true);

            return attributes.Any();
        }

        public static string GetRegionName(this Type view)
        {

            var attributes = view
                .GetCustomAttributes(typeof(MvxRegionPresentationAttribute), true);

            if (!attributes.Any())
                throw new InvalidOperationException("The IMvxWindowsView has no region attribute.");

            return ((MvxRegionPresentationAttribute)attributes.First()).Name;
        }

        public static T? FindControl<T>(this UIElement parent, string? name = null) where T : FrameworkElement
        {
            if (parent == null)
            {
                return null;
            }

            if (parent is T typedParent &&
                (string.IsNullOrWhiteSpace(name) || parent.GetValue(FrameworkElement.NameProperty).Equals(name)))
            {
                return typedParent;
            }

            T? result = null;
            var count = VisualTreeHelper.GetChildrenCount(parent);
            for (var i = 0; i < count; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i) as UIElement;

                result = FindControl<T>(child!, name);
                if (result != null)
                {
                    return result;
                }
            }

            return result;
        }

        private static ValueTask<IMvxViewModel> LoadViewModel(this IMvxWindowsView storeView,
                                                    string requestText,
                                                    IMvxBundle? bundle)
        {
#warning ClearingBackStack disabled for now

            //            if (viewModelRequest.ClearTop)
            //            {
            //#warning TODO - BackStack not cleared for WinRT
            //phoneView.ClearBackStack();
            //            }
            var viewModelLoader = Mvx.IoCProvider.Resolve<IMvxWindowsViewModelLoader>();
            return viewModelLoader.Load(requestText, bundle);
        }
    }
}
