using System;
using Microsoft.Extensions.DependencyInjection;
using taxcalc.Interfaces;
using taxcalc.Services;
using taxcalc.Services.TaxCalculators;
using taxcalc.ViewModels;
using taxcalc.Views;
using Xamarin.Forms;

namespace taxcalc
{
    public partial class App : Application
    {
        // Dependency Injection based on https://blog.infernored.com/using-dotnet-extensions-to-do-dependency-injection-in-xamarin-forms/
        public App()
        {
            InitializeComponent();

            SetupServices();

            MainPage = new CalculateTaxView();
        }

        static protected IServiceProvider ServiceProvider { get; set; }

        void SetupServices()
        {
            var services = new ServiceCollection();
            //services.AddSingleton<ITaxCalculator, CalculatorTaxJar>();
            services.AddSingleton<ITaxCalculator, CalculatorSim>();
            services.AddSingleton<ITaxService, TaxService>();

            services.AddTransient<CalculateTaxViewModel>();

            ServiceProvider = services.BuildServiceProvider();
        }


        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        public static BaseViewModel GetViewModel<TViewModel>()
            where TViewModel : BaseViewModel
        {
            return ServiceProvider.GetService<TViewModel>();
        }
    }
}
