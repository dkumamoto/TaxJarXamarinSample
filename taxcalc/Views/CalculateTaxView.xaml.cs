using System;
using System.Collections.Generic;
using taxcalc.ViewModels;
using Xamarin.Forms;

namespace taxcalc.Views
{
    public partial class CalculateTaxView : ContentPage
    {
        public CalculateTaxView()
        {
            InitializeComponent();
            BindingContext = App.GetViewModel<CalculateTaxViewModel>();
        }
    }
}
