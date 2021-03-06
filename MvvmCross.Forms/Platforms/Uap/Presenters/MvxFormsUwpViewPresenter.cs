﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MS-PL license.
// See the LICENSE file in the project root for more information.

using MvvmCross.Forms.Presenters;
using System;
using MvvmCross.Forms.Core;
using MvvmCross.Logging;
using MvvmCross.Platforms.Uap.Presenters;
using MvvmCross.Platforms.Uap.Views;
using MvvmCross.ViewModels;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace MvvmCross.Forms.Platforms.Uap.Presenters
{
    public class MvxFormsUwpViewPresenter
        : MvxWindowsViewPresenter
        , IMvxFormsViewPresenter
    {
        public MvxFormsUwpViewPresenter(IMvxWindowsFrame rootFrame) 
            : base(rootFrame)
        {
        }

        public MvxFormsUwpViewPresenter(IMvxWindowsFrame rootFrame, Application formsApplication) 
            : this(rootFrame)
        {
            FormsApplication = formsApplication ?? throw new ArgumentNullException(nameof(formsApplication), "MvxFormsApplication cannot be null");
        }

        public Application? FormsApplication { get; set; }

        private IMvxFormsPagePresenter? _formsPagePresenter;
        public virtual IMvxFormsPagePresenter FormsPagePresenter
        {
            get
            {
                if(_formsPagePresenter == null)
                    throw new ArgumentNullException(nameof(FormsPagePresenter), "IMvxFormsPagePresenter cannot be null. Set the value in CreateViewPresenter in the setup.");
                return _formsPagePresenter;
            }
            set { _formsPagePresenter = value; }
        }

        public override void RegisterAttributeTypes()
        {
            base.RegisterAttributeTypes();
            FormsPagePresenter.RegisterAttributeTypes();
        }

        public override async ValueTask<bool> ChangePresentation(MvxPresentationHint hint)
        {
            if (!await FormsPagePresenter.ChangePresentation(hint).ConfigureAwait(false)) return false;
            return await base.ChangePresentation(hint).ConfigureAwait(false);
        }

        public override ValueTask<bool> Show(MvxViewModelRequest request)
        {
            return FormsPagePresenter.Show(request);
        }

        public override ValueTask<bool> Close(IMvxViewModel viewModel)
        {
            return FormsPagePresenter.Close(viewModel);
        }

        public virtual bool ShowPlatformHost(Type? hostViewModel = null)
        {
            MvxFormsLog.Instance.Trace($"Showing of native host View in Forms is not supported.");
            return false;
        }

        public virtual ValueTask<bool> ClosePlatformViews()
        {
            MvxFormsLog.Instance.Trace($"Closing of native Views in Forms is not supported.");
            return new ValueTask<bool>(false);
        }
    }
}
