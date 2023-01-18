using System;
using System.Collections.Generic;
using taxcalc.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace taxcalc.Views
{
    public partial class CalculateTaxView : ContentPage
    {
        public CalculateTaxView()
        {
            InitializeComponent();
            BindingContext = App.GetViewModel<CalculateTaxViewModel>();
        }

        protected override void OnAppearing()
        {
            var safeArea = On<iOS>().SafeAreaInsets();
            if (safeArea != null)
            {
                var vm = BindingContext as CalculateTaxViewModel;
                var origPad = vm.FramePadding;
                var newPad = new Thickness(origPad.Top);
                newPad.Top += safeArea.Top;
                vm.FramePadding = newPad;
            }
            base.OnAppearing();
        }
    }
}
