using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using taxcalc.Interfaces;
using taxcalc.Models;
using taxcalc.Services;
using taxcalc.Services.Converters;
using Xamarin.Forms;

namespace taxcalc.ViewModels
{
	public class CalculateTaxViewModel: BaseViewModel
	{
		private ITaxService _taxService;
		private TaxJarConverters taxJarConverters;
		private Order _order;
		private Thickness _framePadding;
		public Thickness FramePadding
		{
			get => _framePadding;
			set { 
				_framePadding = value;
				OnPropertyChanged(nameof(FramePadding));
			}
		}
		public CalculateTaxViewModel(ITaxService taxService)
		{
			_taxService = taxService;
			TitleText = "taxcalc";
			MainTitle = "Zip used to get Tax Rate";
			ZipLabel = "Zipcode:";
			ZipEntryText = "";
			ZipEntryPlaceholder = "Enter zipcode";
			RateLabel = "Rate:";
			RateButtonLabel = "Get Rate";

			OrderTitle = "Amount used to calculate Tax Due";
			OrderLabel = "Amount:";
			OrderEntryText = "";
			OrderEntryPlaceholder = "Enter order amount";
			TaxDueLabel = "Tax Due:";
			TaxDueValueLabel = "";
			TaxDueButtonLabel = "Calculate Tax Due";
			FramePadding = new Thickness(35);

			taxJarConverters = new TaxJarConverters();
			string orderStr = @"{ ""from_country"": ""US"", ""from_zip"": ""92093"", ""from_state"": ""CA"", ""from_city"": ""La Jolla"", ""from_street"": ""9500 Gilman Drive"", ""to_country"": ""US"", ""to_zip"": ""90002"", ""to_state"": ""CA"", ""to_city"": ""Los Angeles"", ""to_street"": ""1335 E 103rd St"", ""amount"": 15, ""shipping"": 1.5, ""line_items"": [ { ""id"": ""1"", ""quantity"": 1, ""product_tax_code"": ""20010"", ""unit_price"": 15, ""discount"": 0 } ] } ";
			_order = taxJarConverters.ReadFromJson(orderStr);

		}

		private string _titleText;
		public string TitleText
        {
			get { return _titleText; }
			set
            {
				_titleText = value;
				OnPropertyChanged(nameof(TitleText));
            }
        }


		private string _maintitle;
		public string MainTitle
		{
			get { return _maintitle; }
			set
			{
				_maintitle = value;
				OnPropertyChanged(nameof(MainTitle));
			}
		}

		private string _zipLabel;
		public string ZipLabel
		{
			get { return _zipLabel; }
			set
			{
				_zipLabel = value;
				OnPropertyChanged(nameof(ZipLabel));
			}
		}
		private string _zipEntryText;
		public string ZipEntryText
		{
			get { return _zipEntryText; }
			set
			{
				_zipEntryText = value;
				OnPropertyChanged(nameof(ZipEntryText));
			}
		}
		private string _zipEntryPlaceholder;
		public string ZipEntryPlaceholder
		{
			get { return _zipEntryPlaceholder; }
			set
			{
				_zipEntryPlaceholder = value;
				OnPropertyChanged(nameof(ZipEntryPlaceholder));
			}
		}

		private string _rateLabel;
		public string RateLabel { get { return _rateLabel; }
			set {
				_rateLabel = value;
				OnPropertyChanged(nameof(RateLabel));
			}
        }

		private string _rateValueLabel;
		public string RateValueLabel
		{
			get { return _rateValueLabel; }
			set
			{
				_rateValueLabel = value;
				OnPropertyChanged(nameof(RateValueLabel));
			}
		}

		private string _rateButtonLabel;
		public string RateButtonLabel
		{
			get { return _rateButtonLabel; }
			set
			{
				_rateButtonLabel = value;
				OnPropertyChanged(nameof(RateValueLabel));
			}
		}

		public async Task RateFromZip()
        {
			try
			{
				var rate = await _taxService.GetTaxRate(new Models.Address { Zip = ZipEntryText });
				if (rate != null)
				{
					Console.WriteLine("rate = " + rate.Rate);
					RateValueLabel = rate.Rate.ToString();
				}
				else
				{
					RateValueLabel = "Invalid Zip";
				}
			}
			catch (Exception ex)
            {
				if (ex.Message.StartsWith(TaxService.GetError))
                {
					RateValueLabel = "Server error " + ex.Message.Substring(TaxService.GetError.Length);
                }
				else
                {
					RateValueLabel = ex.Message;
                }
            }
        }

		public ICommand RateFromZipCommand => new Command(async () => await RateFromZip());


		private string _orderTitle;
		public string OrderTitle
		{
			get { return _orderTitle; }
			set
			{
				_orderTitle = value;
				OnPropertyChanged(nameof(OrderTitle));
			}
		}

		private string _orderLabel;
		public string OrderLabel
		{
			get { return _orderLabel; }
			set
			{
				_orderLabel = value;
				OnPropertyChanged(nameof(OrderLabel));
			}
		}
		private string _orderEntryText;
		public string OrderEntryText
		{
			get { return _orderEntryText; }
			set
			{
				_orderEntryText = value;
				OnPropertyChanged(nameof(OrderEntryText));
			}
		}
		private string _orderEntryPlaceholder;
		public string OrderEntryPlaceholder
		{
			get { return _orderEntryPlaceholder; }
			set
			{
				_orderEntryPlaceholder = value;
				OnPropertyChanged(nameof(OrderEntryPlaceholder));
			}
		}

		private string _taxDueLabel;
		public string TaxDueLabel
		{
			get { return _taxDueLabel; }
			set
			{
				_taxDueLabel = value;
				OnPropertyChanged(nameof(TaxDueLabel));
			}
		}

		private string _taxDueValueLabel;
		public string TaxDueValueLabel
		{
			get { return _taxDueValueLabel; }
			set
			{
				_taxDueValueLabel = value;
				OnPropertyChanged(nameof(TaxDueValueLabel));
			}
		}

		private string _taxDueButtonLabel;
		public string TaxDueButtonLabel
		{
			get { return _taxDueButtonLabel; }
			set
			{
				_taxDueButtonLabel = value;
				OnPropertyChanged(nameof(TaxDueValueLabel));
			}
		}

		public async Task TaxDueFromOrder()
		{
			try
			{
				float orderAmount = float.Parse(_orderEntryText);
				_order.Amount = orderAmount;
				_order.LineItems[0].UnitPrice = orderAmount;

				var taxDue = await _taxService.CalculateTaxOfOrder(_order);
				Console.WriteLine("taxDue = " + taxDue);
				TaxDueValueLabel = taxDue.ToString();
			}
			catch (Exception ex)
            {
				if (ex.Message.StartsWith(TaxService.PostError))
				{
					TaxDueValueLabel = "Server error " + ex.Message.Substring(TaxService.PostError.Length);
				}
				else
				{
					TaxDueValueLabel = ex.Message;
				}

			}
		}

		public ICommand TaxDueFromOrderCommand => new Command(async () => await TaxDueFromOrder());
	}
}

