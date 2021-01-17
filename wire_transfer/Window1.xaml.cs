/*
 * Created by SharpDevelop.
 * User: iryna
 * Date: 5/1/2020
 * Time: 10:25 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Configuration;
using Newtonsoft.Json;
using System.Threading;
using System.Data;
using System.Diagnostics;
using System.Net;
using System.Text.RegularExpressions;
using System.Data.SqlClient;

using iTextSharp;
using iTextSharp.text;  
using iTextSharp.text.pdf;
using System.IO;


namespace wire_transfer
{

	
	public partial class Window1 : Window
	{
		
		public class States{
			public string[] states {get; set;} 
		}
		
		public class Countries{
			public string[] countries {get; set;}
		}
		
		public class Currencies{
			public string[] currencies {get; set;}
		}		
			
		public API_Obj exchange_rates;
		
		public int FutureDateBox {get; set;} // 0 for Unchecked, 1 for Checked 
		public int Status {get; set;} // 0 for Correct, 1 for Incorrect
		public int OneTime {get; set;} // 0 for No, 1 for Yes
		public int SubjectToAgreement {get; set;} // 0 for No, 1 for Yes
		
		public bool isUpdating = false; // Flag to check if a person is adding a new wire transfer (isUpdating = false) or updating an existing transfer (isUpdating = true)
		
		public static IEnumerable<T> FindVisualChildren<T> ( DependencyObject depObj ) where T : DependencyObject
		{
		    if( depObj != null )
		    {
		        foreach( object rawChild in LogicalTreeHelper.GetChildren( depObj ) )
		        {
		            if( rawChild is DependencyObject )
		            {
		                DependencyObject child = (DependencyObject)rawChild;
		                if( child is T )
		                {
		                    yield return (T)child;
		                }
		
		                foreach( T childOfChild in FindVisualChildren<T>( child ) ) 
		                {
		                    yield return childOfChild;
		                }
		            }
		        }
		    }
		}
		
		public Window1()
		{
			InitializeComponent();
			
			
			this.currencyInfoDropdown.SelectedIndex = 0;
			this.transAmount.Text = "0";
			this.transFees.Text = "0";
			this.USDTotal.Text = "0";
			this.CurrencyDropdown.Text = "USD";
			this.exchangeRate.Text = "1.00";
			this.foreignTotal.Text = "0";
			
			States states_array = new States();
			states_array.states = new string[51]{"N/A","AK","AL","AR","AZ","CA","CO","CT","DE","FL","GA","HI","IA","ID","IL","IN","KS","KY",
				"LA","MA","MD","ME","MI","MN","MO","MS","MT","NC","ND","NE","NH","NJ","NM","NV","NY","OH","OK","OR","PA","RI",
				"SC","SD","TN","TX","UT","VA","VT","WA","WI","WV","WY"
			};
			this.sStatesDropdown.DataContext = states_array;
			this.rStatesDropdown.DataContext = states_array;
			this.ibStatesDropdown.DataContext = states_array;
			this.rbStatesDropdown.DataContext = states_array; 
			
			
			Countries countries_array = new Countries();
			countries_array.countries = new string[256]{"Afghanistan", "Akrotiri", "Albania", "Algeria", "American Samoa", "Andorra", "Angola", "Anguilla", "Antarctica", "Antigua and Barbuda", "Argentina", "Armenia", "Aruba", "Ashmore and Cartier Islands", "Australia", "Austria", "Azerbaijan", "Bahamas The", "Bahrain", "Bangladesh", "Barbados", "Bassas da India", "Belarus", "Belgium", "Belize", "Benin", "Bermuda", "Bhutan", "Bolivia", "Bosnia and Herzegovina", "Botswana", "Bouvet Island", "Brazil", "British Indian Ocean Territory", "British Virgin Islands", "Brunei", "Bulgaria", "Burkina Faso", "Burma", "Burundi", "Cambodia", "Cameroon", "Canada", "Cape Verde", "Cayman Islands", "Central African Republic", "Chad", "Chile", "China", "Christmas Island", "Clipperton Island", "Cocos (Keeling) Islands", "Colombia", "Comoros", "Congo", "Cook Islands", "Coral Sea Islands", "Costa Rica", "Cote d'Ivoire", "Croatia", "Cuba", "Cyprus", "Czech Republic", "Denmark", "Dhekelia", "Djibouti", "Dominica", "Dominican Republic", "Ecuador", "Egypt", "El Salvador", "Equatorial Guinea", "Eritrea", "Estonia", "Ethiopia", "Europa Island", "Falkland Islands (Islas Malvinas)", "Faroe Islands", "Fiji", "Finland", "France", "French Guiana", "French Polynesia", "French Southern and Antarctic Lands", "Gabon", "Gambia The", "Gaza Strip", "Georgia", "Germany", "Ghana", "Gibraltar", "Glorioso Islands", "Greece", "Greenland", "Grenada", "Guadeloupe", "Guam", "Guatemala", "Guernsey", "Guinea", "Guinea-Bissau", "Guyana", "Haiti", "Heard Island and McDonald Islands", "Holy See (Vatican City)", "Honduras", "Hong Kong", "Hungary", "Iceland", "India", "Indonesia", "Iran", "Iraq", "Ireland", "Isle of Man", "Israel", "Italy", "Jamaica", "Jan Mayen", "Japan", "Jersey", "Jordan", "Juan de Nova Island", "Kazakhstan", "Kenya", "Kiribati", "Korea North", "Korea South", "Kuwait", "Kyrgyzstan", "Laos", "Latvia", "Lebanon", "Lesotho", "Liberia", "Libya", "Liechtenstein", "Lithuania", "Luxembourg", "Macau", "Macedonia", "Madagascar", "Malawi", "Malaysia", "Maldives", "Mali", "Malta", "Marshall Islands", "Martinique", "Mauritania", "Mauritius", "Mayotte", "Mexico", "Micronesia Federated States of", "Moldova", "Monaco", "Mongolia", "Montserrat", "Morocco", "Mozambique", "Namibia", "Nauru", "Navassa Island", "Nepal", "Netherlands", "Netherlands Antilles", "New Caledonia", "New Zealand", "Nicaragua", "Niger", "Nigeria", "Niue", "Norfolk Island", "Northern Mariana Islands", "Norway", "Oman", "Pakistan", "Palau", "Panama", "Papua New Guinea", "Paracel Islands", "Paraguay", "Peru", "Philippines", "Pitcairn Islands", "Poland", "Portugal", "Puerto Rico", "Qatar", "Reunion", "Romania", "Russia", "Rwanda", "Saint Helena", "Saint Kitts and Nevis", "Saint Lucia", "Saint Pierre and Miquelon", "Saint Vincent and the Grenadines", "Samoa", "San Marino", "Sao Tome and Principe", "Saudi Arabia", "Senegal", "Serbia and Montenegro", "Seychelles", "Sierra Leone", "Singapore", "Slovakia", "Slovenia", "Solomon Islands", "Somalia", "South Africa", "South Georgia and the South Sandwich Islands", "Spain", "Spratly Islands", "Sri Lanka", "Sudan", "Suriname", "Svalbard", "Swaziland", "Sweden", "Switzerland", "Syria", "Taiwan", "Tajikistan", "Tanzania", "Thailand", "Timor-Leste", "Togo", "Tokelau", "Tonga", "Trinidad and Tobago", "Tromelin Island", "Tunisia", "Turkey", "Turkmenistan", "Turks and Caicos Islands", "Tuvalu", "Uganda", "Ukraine", "United Arab Emirates", "United Kingdom", "United States", "Uruguay", "Uzbekistan", "Vanuatu", "Venezuela", "Vietnam", "Virgin Islands", "Wake Island", "Wallis and Futuna", "West Bank", "Western Sahara", "Yemen", "Zambia", "Zimbabwe"};
			this.CountriesDropdown.DataContext = countries_array;
			this.countryOfReceiptDropdown.DataContext = countries_array;

			
			Currencies curr_array = new Currencies();	
    		curr_array.currencies = new string[51]{"AED", "ARS", "AUD", "BGN", "BRL", "BSD", "CAD", "CHF", "CLP", "CNY", "COP", "CZK", "DKK", "DOP", "EGP", "EUR", "FJD", "GBP", "GTQ", "HKD", "HRK", "HUF", "IDR", "ILS", "INR", "ISK", "JPY", "KRW", "KZT", "MXN", "MYR", "NOK", "NZD", "PAB", "PEN", "PHP", "PKR", "PLN", "PYG", "RON", "RUB", "SAR", "SEK", "SGD", "THB", "TRY", "TWD", "UAH", "USD", "UYU", "ZAR",};
			this.CurrencyDropdown.DataContext = curr_array;
			this.CurrencyDropdown.SelectedIndex = 48;
		
			exchange_rates = Rates.Import();
			
			if (this.exchangeRate.Text == String.Empty){
				this.exchangeRate.Text = "1.00";
			}
			if (this.transFees.Text == String.Empty){
				this.transFees.Text = "0";
			}
				
		}
	
		
		private void Window_Load(object sender, RoutedEventArgs e){
			// Tests connection to db; if can't connect, then can't use the program
			
			DatabaseHandler db = new DatabaseHandler();
			db.initializeConnection();
			bool connected = db.connectionSuccessful;
			Debug.WriteLine(connected.ToString());
			if (connected == false){
				Debug.WriteLine("Disabling buttons...");
				this.newWireButton.IsEnabled = false;
				this.wireLookupButton.IsEnabled = false; 
				Debug.WriteLine("Buttons disabled.");
			}
		}
		
		#region CURRENCY WINDOW FUNCTIONS 
		
		
		private void INT_Boxes_On_Focus(object sender, RoutedEventArgs e){
			TextBox input = (TextBox)sender;
			if (input.Text == "0"){
				input.Text = String.Empty;
			}
		}
		
		private void INT_Boxes_Off_Focus(object sender, RoutedEventArgs e){
			TextBox input = (TextBox)sender;
			if (input.Text == String.Empty){
				input.Text = "0";
			}
		}
		
		private void TransAmount_OnLostFocus(object sender, RoutedEventArgs e){
			INT_Boxes_Off_Focus(sender, e);
			TransAmount_Handler();
		}
		
		private void TransAmount_Enter(object sender, KeyEventArgs e){
			if (e.Key == Key.Return | e.Key == Key.Enter){
				TransAmount_Handler();
			}
		}
		
		private void TransAmount_Handler(){
			double trans_amount, trans_fees, exchange_rate, total, foreign_total; 
			
			try{
				exchange_rate = Convert.ToDouble(this.exchangeRate.Text);
				trans_amount = Convert.ToDouble(this.transAmount.Text);
				trans_fees = Convert.ToDouble(this.transFees.Text);
				total = Math.Round((trans_amount + trans_fees), 2, MidpointRounding.AwayFromZero);
				foreign_total = Math.Round((trans_amount*exchange_rate), 2, MidpointRounding.AwayFromZero);
				
				this.transAmount.Text = String.Format("{0:.00}", trans_amount);
				if (trans_fees != 0){
					this.transFees.Text = String.Format("{0:.00}", trans_fees);	
				}
				
				
				this.USDTotal.Text = String.Format("{0:.00}", total);
				this.foreignTotal.Text = String.Format("{0:.00}", foreign_total);
			} catch (FormatException){
				// do nothing; form validation takes care of this
			}
		}
		
		private void ExchangeRate_OnLostFocus(object sender, RoutedEventArgs e){
			TextBox input = (TextBox)sender;
			if (input.Text == String.Empty){
				input.Text = "1.00";
			}
			ExchangeRate_Handler();
		}
		
		private void ExchangeRate_Enter(object sender, KeyEventArgs e){
			if (e.Key == Key.Return | e.Key == Key.Enter){
				ExchangeRate_Handler();
			}
		}
		
		private void ExchangeRate_Handler(){
			double transfer_amount, exchange_rate, foreign_total;
			try{
				transfer_amount = Convert.ToDouble(this.transAmount.Text);
				exchange_rate = Convert.ToDouble(this.exchangeRate.Text);
				foreign_total = Math.Round((transfer_amount*exchange_rate), 2, MidpointRounding.AwayFromZero);
				
				this.foreignTotal.Text = String.Format("{0:.00}", foreign_total);
			} catch (FormatException){
				// do nothing; form validation takes care of this
			}
		}
		
		private void ForeignAmount_OnLostFocus(object sender, RoutedEventArgs e){
			INT_Boxes_Off_Focus(sender, e);
			ForeignAmount_Handler();
		}
		
		private void ForeignAmount_Enter(object sender, KeyEventArgs e){
			if (e.Key == Key.Return | e.Key == Key.Enter){
				ForeignAmount_Handler();
			}
		}
		
		private void ForeignAmount_Handler(){
			try{
				// To convert from Foreign to USD: Foreign / Exch_Rate 
				double foreign_amount = Convert.ToDouble(this.foreignTotal.Text);
				double exchange_rate = Convert.ToDouble(this.exchangeRate.Text);
				double trans_fees = Convert.ToDouble(this.transFees.Text);
				
				double USD_amount = Math.Round((foreign_amount/exchange_rate), 2, MidpointRounding.AwayFromZero); 
				
				string USD_total = Math.Round((USD_amount + trans_fees), 2, MidpointRounding.AwayFromZero).ToString();
				
				this.transAmount.Text = String.Format("{0:.00}", USD_amount.ToString());
				this.USDTotal.Text = String.Format("{0:.00}", USD_total);
				
			} catch (FormatException){
				// do nothing; form validation takes care of this
			}
		}
		
		private void FundsAvailableBy_LostFocus(object sender, RoutedEventArgs e)
		{
			FundsAvailableBy_Handler();
		}
		
		private void FundsAvailableBy_Enter(object sender, KeyEventArgs e){
			if (e.Key == Key.Return | e.Key == Key.Enter){
				FundsAvailableBy_Handler();
			}
		}
		
		private void FundsAvailableBy_Handler(){
			string date = this.transferDate.Text.Trim();
			try{
				this.fundsAvailableBy.Text = Globals.getFundsAvailableByDateManual(date);
			} catch { 
				// do nothing, FormValidation handles this
			}
		}

		private void CurrencyDropdown_DropDownClosed(object sender, EventArgs e) {
			
            double rate, transfer_amount, foreign_amount;
			string CURR_TYPE = this.CurrencyDropdown.Text;
            try{
	            //string CURR_TYPE = this.CurrencyDropdown.Text;
	            
	            string exchange_rate = exchange_rates.rates.getRate(CURR_TYPE).ToString(); // Test line
	      		this.exchangeRate.Text = exchange_rate;
	      			
	      		rate = Convert.ToDouble(exchange_rate);
	      		transfer_amount = Convert.ToDouble(this.transAmount.Text);
	      		foreign_amount = Math.Round(rate*transfer_amount, 2, MidpointRounding.AwayFromZero);
	      		
	      		this.foreignTransAmount.Text = foreign_amount.ToString();
      		
      			this.foreignTotal.Text = foreign_amount.ToString();
      				
            } catch (Exception){
            	MessageBox.Show("The Exchange Rate API is down! \nYou will have to manually enter the conversion rate for this currency.\n");
            }
            
            this.currencyTypeLabel.Text = CURR_TYPE;
            
      		
		}
		#endregion		
			
		private void Home_Button_onFocus(object sender, RoutedEventArgs e){
			this.homeBtn.Opacity = 0.5; 
		}
		
		private void Home_Button_offFocus(object sender, RoutedEventArgs e){
			this.homeBtn.Opacity = 1;
		}
		
		private void Return_Home(object sender, EventArgs e){
			foreach (Window window in Application.Current.Windows)
			{
				if (window.GetType() == typeof(Window1))
				{
					(window as Window1).newWireForm.Visibility = Visibility.Collapsed;
					(window as Window1).welcomePage.Visibility = Visibility.Visible;
				}
			}
			this.homeBtn.Visibility = Visibility.Collapsed;
		}
		
		private void NextPage_Button(object sender, RoutedEventArgs e){
			this.newWireForm.SelectedIndex++; 
		}
		
		private void PreviousPage_Button(object sender, RoutedEventArgs e){
			this.newWireForm.SelectedIndex--; 
		}
		
		private void Clear_Wire_Form(){
			// If anything has been pre-filled for the wire transfer, this part will clear it 
			
			
			foreach (TextBox tb in FindVisualChildren<TextBox>(this))
			{
	            tb.Text = String.Empty;
		    }
			
			foreach (ComboBox cb in FindVisualChildren<ComboBox>(this))
			{
				cb.Text = String.Empty;
			}
			
			foreach (CheckBox cb in FindVisualChildren<CheckBox>(this))
			{
				cb.IsChecked = false;
			}
			

			this.remittanceOpt.IsChecked = false;
			this.nonremittanceOpt.IsChecked = false;

			this.CurrencyDropdown.IsEnabled = true;
			this.CurrencyDropdown.Text = "USD";
			this.exchangeRate.Text = "1.00";
		}
		
		private void Enable_New_Wire(object sender, RoutedEventArgs e)
		{
			this.newWireForm.Visibility = Visibility.Visible;
			this.welcomePage.Visibility = Visibility.Collapsed;
			
			this.homeBtn.Visibility = Visibility.Visible;
			
			Clear_Wire_Form();
			
			Enable_All_Boxes();
			
			this.employeeName.Text = Globals.EMPLOYEE_USER;
			
			this.nonremittanceOpt.IsChecked = true;
			
			this.Correct.IsChecked = true;
			
			this.wireBranchDropdown.SelectedIndex = 0;
			
			this.currencyInfoDropdown.SelectedIndex = 0;
			this.currencyTypeLabel.Text = "USD";
			this.transAmount.Text = "0";
			this.transFees.Text = "0";
			this.USDTotal.Text = "0";
			this.CurrencyDropdown.Text = "USD";
			this.exchangeRate.Text = "1.00";
			this.foreignTotal.Text = "0";
			
			isUpdating = false;
			this.updateBtn.Visibility = Visibility.Hidden;
			this.Incorrect.IsEnabled = false;
			
			this.newWireForm.SelectedIndex = 0;

		}
		
		private void Disable_New_Wire(object sender, RoutedEventArgs e)
		{
			
			MessageBoxResult result = MessageBox.Show(
					"Are you sure you want to go back?\nYour entire form will get cleared.", 
            		"Go Back To Home Page", 
            		MessageBoxButton.YesNo,
            		MessageBoxImage.Warning);
	        if (result == MessageBoxResult.Yes)
	        {
	            this.newWireForm.Visibility = Visibility.Collapsed;
				this.welcomePage.Visibility = Visibility.Visible;
			
				this.homeBtn.Visibility = Visibility.Collapsed;
	        }        
		}
		
		private void Enable_Wire_Lookup(object sender, RoutedEventArgs e)
		{
			this.welcomePage.Visibility = Visibility.Collapsed;
			this.wireLookupPage.Visibility = Visibility.Visible;
			
			this.searchWireIDBox.Text = String.Empty;
			this.searchSenderNameBox.Text = String.Empty;
			this.searchRecipientNameBox.Text = String.Empty;
			this.searchDateBox.Text = String.Empty;
			
			this.searchWireIDBox.IsEnabled = false;
			this.searchSenderNameBox.IsEnabled = false;
			this.searchRecipientNameBox.IsEnabled = false;
			this.searchDateBox.IsEnabled = false;
			
			this.searchWireIDOpt.IsChecked = false;
			this.searchSNameOpt.IsChecked = false;
			this.searchRNameOpt.IsChecked = false;
			this.searchDateOpt.IsChecked = false;
			
			this.wireGrid.ItemsSource = null;
		
		}
		
		private void Disable_Wire_Lookup(object sender, RoutedEventArgs e)
		{
			this.welcomePage.Visibility = Visibility.Visible;
			this.wireLookupPage.Visibility = Visibility.Collapsed;
			
		}
		
		
		private void Correct_Checked(object sender, RoutedEventArgs e){
			Status = 0; 
			// this.Incorrect.IsEnabled = false;
			Enable_All_Boxes();
			this.updateBtn.Visibility = Visibility.Hidden;
//			if (this.futureDateBox.IsChecked == false){
//				this.futureDate1.IsEnabled = false;
//				this.futureDate2.IsEnabled = false;
//			}
		}
		
		
		private void Incorrect_Checked(object sender, RoutedEventArgs e){
			Status = 1; 
		//	this.Correct.IsEnabled = false;
			this.updateBtn.Visibility = Visibility.Visible;
			Disable_All_Boxes();
			
			isUpdating = false;
		}
		
		private void OneTime_Checked(object sender, RoutedEventArgs e){
			OneTime = 1; 
		}
		
		private void OneTime_Unchecked(object sender, RoutedEventArgs e){
			OneTime = 0;
		}
		
		private void TransAgreement_Checked(object sender, RoutedEventArgs e){
			SubjectToAgreement = 1;
		}
		
		private void TransAgreement_Unchecked(object sender, RoutedEventArgs e){
			SubjectToAgreement = 0; 
		}
		
		private void FutureDate_Checked(object sender, RoutedEventArgs e){
			FutureDateBox = 1;
			if (this.Incorrect.IsChecked == false){
				this.futureDate1.IsEnabled = true;
				this.futureDate2.IsEnabled = true;
			}
		}
		
		private void FutureDate_Unchecked(object sender, RoutedEventArgs e){
			FutureDateBox = 0;
			this.futureDate1.IsEnabled = false;
			this.futureDate2.IsEnabled = false;
		}
		
		private void Disable_All_Boxes(){
			
			foreach (TextBox tb in FindVisualChildren<TextBox>(this))
			{
	            tb.IsEnabled = false;
		    }			
			
			foreach (ComboBox cb in FindVisualChildren<ComboBox>(this))
			{
	            cb.IsEnabled = false;
		    }
			
			foreach (CheckBox cb in FindVisualChildren<CheckBox>(this))
			{
	            cb.IsEnabled = false;
		    }
		
			
			this.remittanceOpt.IsEnabled = false;
			this.nonremittanceOpt.IsEnabled = false;
			
			this.finishBtn.IsEnabled = false;
			
			
		}
		
		private void Enable_All_Boxes(){
			
			foreach (TextBox tb in FindVisualChildren<TextBox>(this))
			{
	            tb.IsEnabled = true;
		    }
			
			foreach (ComboBox cb in FindVisualChildren<ComboBox>(this))
			{
	            cb.IsEnabled = true;
		    }
			
			foreach (CheckBox cb in FindVisualChildren<CheckBox>(this))
			{
	            cb.IsEnabled = true;
		    }
			
			this.finishBtn.IsEnabled = true;
	
			this.remittanceOpt.IsEnabled = true;
			this.nonremittanceOpt.IsEnabled = true;
			
//			if (this.futureDateBox.IsChecked == false){
//				this.futureDate1.IsEnabled = false;
//				this.futureDate2.IsEnabled = false;
//			}
		}
		
		
		private void Sender_Member_Num_On_LostFocus(object sender, RoutedEventArgs e){
			Sender_Member_Num_Handler();
		}
		
		private void Sender_Member_Num_On_Enter(object sender, KeyEventArgs e){
			if (e.Key == Key.Return | e.Key == Key.Enter){
				Sender_Member_Num_Handler();
			}
		}
		
		private void Sender_Member_Num_Handler(){
			// Function that runs query on Sender using member num and fills in info automatically if the member num exists in DB 

			string member_num = this.sMemberNum.Text; //.Trim();
			
			Sender pSender = new Sender();
			int exists = pSender.exists(member_num);
			if (exists != 0){
				pSender = pSender.retrieveSender(member_num);
				this.sMemberNum.Text = pSender.MemberNum;
				this.sSubAccount.Text = pSender.SubAccount;
				this.sName.Text = pSender.Name;
			//	this.sAddress.Text = pSender.Address; // Don't populate address since it might be changed
				this.sCity.Text = pSender.City;
				this.sStatesDropdown.Text = pSender.State;
				this.sZip.Text = pSender.Zip;
				this.sPhone.Text = pSender.Phone;
			}	
		}
		
		private void Recipient_Account_Num_On_LostFocus(object sender, RoutedEventArgs e){		
			Recipient_Account_Num_Handler();
		}
		
		private void Recipient_Account_Num_On_Enter(object sender, KeyEventArgs e){
			if (e.Key == Key.Return | e.Key == Key.Enter){
				Recipient_Account_Num_Handler();
			}
		}
		
		private void Recipient_Account_Num_Handler(){
			// Function that runs query on Recipient using account num IBAN and fills in info automatically if the account num IBAN exists in DB 
					
			string account_num_IBAN = this.rAccountNumIBAN.Text.Trim();
			
			Recipient recipient = new Recipient();
			int exists = recipient.exists(account_num_IBAN);
			if (exists != 0){
			    recipient = recipient.retrieveRecipient(account_num_IBAN);
			    this.rName.Text = recipient.Name;
			    this.rAddress.Text = recipient.Address;
			    this.rCity.Text = recipient.City;
			    this.rStatesDropdown.Text = recipient.State;
				this.rZip.Text = recipient.Zip;
				this.CountriesDropdown.Text = recipient.Country;
				this.rAccountNumIBAN.Text = recipient.AccountNumIBAN;
				this.rSSN.Text = recipient.SSN;
				this.rTIN.Text = recipient.TIN;
				this.rDLnum.Text = recipient.DLNum;
			}
			
		}
		
		private void Bank_Routing_Num_On_LostFocus(object sender, RoutedEventArgs e){
			Bank_Routing_Num_Handler();
		}
		
		private void Bank_Routing_Num_On_Enter(object sender, KeyEventArgs e){
			if (e.Key == Key.Return | e.Key == Key.Enter){
				Bank_Routing_Num_Handler();
			}
		}
		
		private void Bank_Routing_Num_Handler(){

			string ib_ABA_routing_num = this.ibRoutingNum.Text.Trim();
			string rb_ABA_routing_num = this.rbRoutingNum.Text.Trim();
			int exists;
			
			Bank bank = new Bank();
			
			exists = bank.exists(ib_ABA_routing_num);
			if (exists != 0){
				bank = bank.retrieveBank(ib_ABA_routing_num);
				this.ibName.Text = bank.Name;
				this.ibAddress.Text = bank.Address;
				this.ibCity.Text = bank.City;
				this.ibStatesDropdown.Text = bank.State;
				this.ibZip.Text = bank.Zip;
				this.ibRoutingNum.Text = bank.RoutingNum;
				this.ibSwiftSortCode.Text = bank.SwiftSortCode;
				this.ibBranchInfo.Text = bank.BranchInfo;
				this.ibRoutingInstr.Text = bank.RoutingInstr;
			}
			
			exists = bank.exists(rb_ABA_routing_num);
			if (exists != 0){
				bank = bank.retrieveBank(rb_ABA_routing_num);
				this.rbName.Text = bank.Name;
				this.rbAddress.Text = bank.Address;
				this.rbCity.Text = bank.City;
				this.rbStatesDropdown.Text = bank.State;
				this.rbZip.Text = bank.Zip;
				this.rbRoutingNum.Text = bank.RoutingNum;
				this.rbSwiftSortCode.Text = bank.SwiftSortCode;
				this.rbBranchInfo.Text = bank.BranchInfo;
				this.rbRoutingInstr.Text = bank.RoutingInstr;
			}
		}
		
		
		private void Enable_WireID_Search_Option(object sender, RoutedEventArgs e){
			this.searchWireIDOpt.IsChecked = true;
			
			this.searchWireIDBox.IsEnabled = true;
			this.searchSenderNameBox.IsEnabled = false;
			this.searchRecipientNameBox.IsEnabled = false;
			this.searchDateBox.IsEnabled = false;
				
			this.searchSenderNameBox.Text = String.Empty;
			this.searchRecipientNameBox.Text = String.Empty;
			this.searchDateBox.Text = String.Empty;
		}
		
		private void Enable_SName_Search_Option(object sender, RoutedEventArgs e){
			this.searchSNameOpt.IsChecked = true;
			
			this.searchWireIDBox.IsEnabled = false;
			this.searchSenderNameBox.IsEnabled = true;
			this.searchRecipientNameBox.IsEnabled = false;
			this.searchDateBox.IsEnabled = false;
				
			this.searchWireIDBox.Text = String.Empty;
			this.searchRecipientNameBox.Text = String.Empty;
			this.searchDateBox.Text = String.Empty;
			
		}
		
		private void Enable_RName_Search_Option(object sender, RoutedEventArgs e){
			this.searchRNameOpt.IsChecked = true;
			
			this.searchWireIDBox.IsEnabled = false;
			this.searchSenderNameBox.IsEnabled = false;
			this.searchRecipientNameBox.IsEnabled = true;
			this.searchDateBox.IsEnabled = false;
				
			this.searchWireIDBox.Text = String.Empty;
			this.searchSenderNameBox.Text = String.Empty;
			this.searchDateBox.Text = String.Empty;
		}
		
		private void Enable_Date_Search_Option(object sender, RoutedEventArgs e){
			this.searchDateOpt.IsChecked = true;
			
			this.searchWireIDBox.IsEnabled = false;
			this.searchSenderNameBox.IsEnabled = false;
			this.searchRecipientNameBox.IsEnabled = false;
			this.searchDateBox.IsEnabled = true;
				
			this.searchWireIDBox.Text = String.Empty;
			this.searchSenderNameBox.Text = String.Empty;
			this.searchRecipientNameBox.Text = String.Empty;
		}
		
		private void Enable_Remittance_Search_Option(object sender, RoutedEventArgs e){
			this.searchRemittanceOpt.IsChecked = true;
		}
		
		private void Enable_Nonremittance_Search_Option(object sender, RoutedEventArgs e){
			this.searchNonremittanceOpt.IsChecked = true;
		}
		
		private void Enable_Both_Search_Option(object sender, RoutedEventArgs e){
			this.searchBothOpt.IsChecked = true;
		}
		
		private void Enter_Key_OnSearch(object sender, KeyEventArgs e){
			if (e.Key == Key.Return | e.Key == Key.Enter){
				Display_Search_Results(sender, e);
			}
		}
		
		private void Clear_Search(object sender, RoutedEventArgs e){
			this.noSearchResultsLabel.Visibility = Visibility.Hidden;
			this.wireGrid.ItemsSource = null;
			
			this.searchWireIDOpt.IsChecked = false;
			this.searchSNameOpt.IsChecked = false;
			this.searchRNameOpt.IsChecked = false;
			this.searchDateOpt.IsChecked = false;
			
			this.searchWireIDBox.IsEnabled = false;
			this.searchSenderNameBox.IsEnabled = false;
			this.searchRecipientNameBox.IsEnabled = false;
			this.searchDateBox.IsEnabled = false;
				
			this.searchWireIDBox.Text = String.Empty;
			this.searchSenderNameBox.Text = String.Empty;
			this.searchRecipientNameBox.Text = String.Empty;
			this.searchDateBox.Text = String.Empty;
			
		}
		
		private void Display_Search_Results(object sender, RoutedEventArgs e){
			// Returns the search results for searching wire based on person's name or wire id
			
			this.noSearchResultsLabel.Visibility = Visibility.Hidden;
			
			SqlCommand cmd; 
			SqlDataAdapter da;
			DataTable dt = new DataTable("Wire");

			DatabaseHandler db = new DatabaseHandler();
			db.initializeConnection();
			
			string wire_id = this.searchWireIDBox.Text.Trim();
			string s_name = this.searchSenderNameBox.Text.Trim();
			string r_name = this.searchRecipientNameBox.Text.Trim();
			string date = this.searchDateBox.Text.Trim();
			string query;
			
			// NOTE: the difference between WireHistory.datetime and Wire.datetime is that WireHistory.datetime is the most recently updated date, and Wire.datetime
			// is the date the wire was originally created... I'll be using Wire.datetime 			
			
			// Should these queries be here too ? TODO: figure out if they need to be here 
			if (wire_id != String.Empty){
				try{
					query = "SELECT wire.id AS wire_id, wire_history.datetime AS datetime, wire_history.s_name, wire_history.r_name, wire_history.rb_name, wire_history.transfer_amount_USD, wire_history.wire_type, wire.status FROM wire_history INNER JOIN wire ON (wire.id=wire_history.wire_id AND wire.status = wire_history.status) WHERE wire_history.wire_id="+wire_id+" ORDER BY wire_history.datetime DESC;";
					cmd = new SqlCommand(query, db.getConnection());
					da = new SqlDataAdapter(cmd);
					da.Fill(dt);
					
					dt = removeDuplicateRows(dt);
					
		        	this.wireGrid.ItemsSource = dt.DefaultView;
		        	if (this.wireGrid.Items.Count == 0){
		        		this.noSearchResultsLabel.Visibility = Visibility.Visible;
		        	}
				} catch (Exception err){
					MessageBox.Show(err.ToString());
				}
			}
			else if (s_name != String.Empty){
				try{
					query = "SELECT wire.id AS wire_id, wire_history.datetime AS datetime, wire_history.s_name, wire_history.r_name, wire_history.rb_name, wire_history.transfer_amount_USD, wire_history.wire_type, wire.status FROM wire_history INNER JOIN wire ON (wire.id=wire_history.wire_id AND wire.status = wire_history.status) WHERE s_name LIKE '"+s_name+"%' ORDER BY wire_history.datetime DESC;";
					cmd = new SqlCommand(query, db.getConnection());
					da = new SqlDataAdapter(cmd);
					da.Fill(dt);
					
					dt = removeDuplicateRows(dt);
					
		        	this.wireGrid.ItemsSource = dt.DefaultView;
		        	if (this.wireGrid.Items.Count == 0){
		        		this.noSearchResultsLabel.Visibility = Visibility.Visible;
		        	}
				} catch (Exception err){
					MessageBox.Show(err.ToString());
				}
			}
			else if (r_name != String.Empty){
				try{
					query = "SELECT wire.id AS wire_id, wire_history.datetime AS datetime, wire_history.s_name, wire_history.r_name, wire_history.rb_name, wire_history.transfer_amount_USD, wire_history.wire_type, wire.status FROM wire_history INNER JOIN wire ON (wire.id=wire_history.wire_id AND wire.status = wire_history.status) WHERE r_name LIKE '"+r_name+"%' ORDER BY wire_history.datetime DESC;";
					cmd = new SqlCommand(query, db.getConnection());
					da = new SqlDataAdapter(cmd);
					da.Fill(dt);
					
					dt = removeDuplicateRows(dt);
					
		        	this.wireGrid.ItemsSource = dt.DefaultView;
		        	if (this.wireGrid.Items.Count == 0){
		        		this.noSearchResultsLabel.Visibility = Visibility.Visible;
		        	}
				} catch (Exception err){
					MessageBox.Show(err.ToString());
				}
			}
			else if (date != String.Empty){
				try{
					query = "SELECT wire.id AS wire_id, wire_history.datetime AS datetime, wire_history.s_name, wire_history.r_name, wire_history.rb_name, wire_history.transfer_amount_USD, wire_history.wire_type, wire.status FROM wire_history INNER JOIN wire ON (wire.id=wire_history.wire_id AND wire.status = wire_history.status) WHERE wire_history.datetime LIKE '"+date+"%'  ORDER BY wire_history.datetime DESC;";
					cmd = new SqlCommand(query, db.getConnection());
					da = new SqlDataAdapter(cmd);
					da.Fill(dt);
					
					dt = removeDuplicateRows(dt);
					
		        	this.wireGrid.ItemsSource = dt.DefaultView;
		        
		        	if (this.wireGrid.Items.Count == 0){
		        		this.noSearchResultsLabel.Visibility = Visibility.Visible;
		        	}
				} catch (Exception err){
					MessageBox.Show(err.ToString());
				}
			}
			else{
				try{
				// If the person just hits "Search", all transfers will show up 
					
					query = "SELECT wire.id AS wire_id, wire_history.datetime AS datetime, wire_history.s_name, wire_history.r_name, wire_history.rb_name, wire_history.transfer_amount_USD, wire_history.wire_type, wire.status FROM wire_history INNER JOIN wire ON wire.id=wire_history.wire_id AND wire.status = wire_history.status ORDER BY wire_history.datetime DESC;";
					cmd = new SqlCommand(query, db.getConnection());
					da = new SqlDataAdapter(cmd);
					da.Fill(dt);

					dt = removeDuplicateRows(dt);		
					
		        	this.wireGrid.ItemsSource = dt.DefaultView;
		        	
		        	if (this.wireGrid.Items.Count == 0){
		        		this.noSearchResultsLabel.Visibility = Visibility.Visible;
		        	}
				} catch (Exception err){
					MessageBox.Show(err.ToString());
				}
			}
			
			// Filter by Remittance, Nonremittance 
			if (this.searchRemittanceOpt.IsChecked == true){
				try{
					dt = Filter_By_Wire_Type(dt, "R");
					this.wireGrid.ItemsSource = dt.DefaultView;
					
					if (this.wireGrid.Items.Count == 0){
		        		this.noSearchResultsLabel.Visibility = Visibility.Visible;
		        	}
			
				} catch (Exception err){
					MessageBox.Show(err.ToString());
				}
				
			}
			else if (this.searchNonremittanceOpt.IsChecked == true){
				try{
					dt = Filter_By_Wire_Type(dt, "NR");
					this.wireGrid.ItemsSource = dt.DefaultView;
					
					if (this.wireGrid.Items.Count == 0){
		        		this.noSearchResultsLabel.Visibility = Visibility.Visible;
		        	}
			
				} catch (Exception err){
					MessageBox.Show(err.ToString());
				}
			}
			
			db.cleanup();
			
		}
		
		
		public DataTable removeDuplicateRows(DataTable dt){
			List<DataRow> duplicateList = new List<DataRow>();
			Hashtable hTable = new Hashtable();
			
			foreach (DataRow drow in dt.Rows){
				if (hTable.Contains(drow["wire_id"])){
					 duplicateList.Add(drow);
				} 
				else
			 		hTable.Add(drow["wire_id"], string.Empty); 
			}
			
			foreach (DataRow dRow in duplicateList){
				
				dt.Rows.Remove(dRow);
			}
			
			return dt;
		}
		
		public DataTable Filter_By_Wire_Type(DataTable dt, string type){
			List<DataRow> unwantedOptions = new List<DataRow>();
			foreach (DataRow row in dt.Rows){
				if (row["wire_type"].ToString() != type){
					unwantedOptions.Add(row);
					Debug.WriteLine(row["wire_type"].ToString());
				}
			}
			
			foreach (DataRow dRow in unwantedOptions){
				dt.Rows.Remove(dRow);
			}
			
			return dt;
		}
		
		private void Print_Search_Selected_Wire(object sender, RoutedEventArgs e){
			Mouse.OverrideCursor = Cursors.Wait;
                
            DataRowView row = (DataRowView)this.wireGrid.SelectedItem; 
			int wire_id = Convert.ToInt32(row["wire_id"]);
			Globals.WIRE_ID = wire_id;
			
			WireHistory wire = new WireHistory();
			//int wire_id = Globals.WIRE_ID;
			Debug.WriteLine(wire_id.ToString());
			wire = wire.returnOldWire(wire_id);
			
			WirePDFWriter pdf = new WirePDFWriter();
			
			if (wire.WireType == "R"){
				pdf.fill_remittance_PDF(wire);
			}
			else if (wire.WireType == "NR"){
				pdf.fill_nonremittance_PDF(wire);
			}

			Mouse.OverrideCursor = null;
		}
		
		private void Populate_Wire(object sender, RoutedEventArgs e){	
			// Need to do this in case anything is left over from a search
			Clear_Wire_Form();
			
			
			this.wireLookupPage.Visibility = Visibility.Collapsed;
            this.newWireForm.Visibility = Visibility.Visible;
            this.homeBtn.Visibility = Visibility.Visible;
            
            this.Incorrect.IsEnabled = true;
                   
            DataRowView row = (DataRowView)this.wireGrid.SelectedItem; 
			int wire_id = Convert.ToInt32(row["wire_id"]);
			Globals.WIRE_ID = wire_id; 
			

			WireHistory old_wire = new WireHistory();
			old_wire = old_wire.returnOldWire(wire_id);
			
			
			this.rIDused.Text = old_wire.IDUsed;  this.methodOfTransferDropdown.Text = old_wire.MethodOfTransfer;  this.employeeName.Text = old_wire.WireProcessedBy;  this.ofacVerificationBy.Text = old_wire.OFACVerificationBy;  this.ofacDateTime.Text = old_wire.OFACDateTime;  this.internalSpecialInstr.Text = old_wire.InternalSpecialInstructions;  
			this.securityMethodUsed.Text = old_wire.SecurityMethodUsed;  this.securityProcessedBy.Text = old_wire.SecurityProcessedBy;  this.securityDateTime.Text = old_wire.SecurityDateTime;
			this.employeeCallback.Text = old_wire.EmployeeCallback;  this.phoneNumCallback.Text = old_wire.PhoneNumForCallback;  this.sourcePhoneVerificationCallback.Text = old_wire.SourcePhoneVerification;  this.memberCancellingRequest.Text = old_wire.sName;  this.cancelDate.Text = old_wire.CancelDate;  this.cancelProcessedBy.Text = old_wire.CancelProcessedBy; 
			
			this.transferDate.Text = old_wire.TransferDate;  this.fundsAvailableBy.Text = old_wire.FundsAvailableBy;
			this.countryOfReceiptDropdown.Text = old_wire.CountryOfReceipt;  this.wireBranchDropdown.Text = old_wire.WireBranch;  this.sPaymentInstr.Text = old_wire.PaymentInstr;  this.memberConfirmingFunds.Text = old_wire.MemberConfirmingFunds;
			this.pickupName.Text = old_wire.PickupName;  this.pickupAddress.Text = old_wire.PickupAddress;   this.pickupCityStateZip.Text = old_wire.PickupCityStateZip;   this.pickupPhone.Text = old_wire.PickupPhone;			
			this.futureDate1.Text = old_wire.FutureDate1;  this.futureDate2.Text = old_wire.FutureDate2;
			
			this.sMemberNum.Text = old_wire.sMemberNum;  this.sSubAccount.Text = old_wire.sSubAccount;  this.sName.Text = old_wire.sName;  this.sAddress.Text = old_wire.sAddress;  this.sCity.Text = old_wire.sCity;  this.sStatesDropdown.Text = old_wire.sState;  this.sZip.Text = old_wire.sZip;  this.sPhone.Text = old_wire.sPhoneNum;
			this.rName.Text = old_wire.rName;   this.rAddress.Text = old_wire.rAddress;   this.rCity.Text = old_wire.rCity;  this.rStatesDropdown.Text = old_wire.rState;  this.rZip.Text = old_wire.rZip;  this.CountriesDropdown.Text = old_wire.rCountry;  this.rPhone.Text = old_wire.rPhone;
			this.rAccountNumIBAN.Text = old_wire.rAccountNumIBAN;  this.rSSN.Text = old_wire.rSSN;  this.rTIN.Text = old_wire.rTIN;  this.rDLnum.Text = old_wire.rDLNum;
			this.ibName.Text = old_wire.ibName;  this.ibAddress.Text = old_wire.ibAddress;  this.ibCity.Text = old_wire.ibCity;  this.ibStatesDropdown.Text = old_wire.ibState;  this.ibZip.Text = old_wire.ibZip;  this.ibRoutingNum.Text = old_wire.ibRoutingNum;  this.ibSwiftSortCode.Text = old_wire.ibSwiftSortCode;  this.ibBranchInfo.Text = old_wire.ibBranch;  this.ibRoutingInstr.Text = old_wire.ibRoutingInstr;
			this.rbName.Text = old_wire.rbName;  this.rbAddress.Text = old_wire.rbAddress;  this.rbCity.Text = old_wire.rbCity;  this.rbStatesDropdown.Text = old_wire.rbState;  this.rbZip.Text = old_wire.rbZip;  this.rbRoutingNum.Text = old_wire.rbRoutingNum;  this.rbSwiftSortCode.Text = old_wire.rbSwiftSortCode;  this.rbBranchInfo.Text = old_wire.rbBranch;  this.rbRoutingInstr.Text = old_wire.rbRoutingInstr;

			this.currencyInfoDropdown.Text = old_wire.CurrencyInfo;  this.transAmount.Text = old_wire.TransAmountUSD;  this.transFees.Text = old_wire.TransFeesUSD;   this.USDTotal.Text = old_wire.TotalUSD; this.CurrencyDropdown.Text = old_wire.CurrencyType;   this.exchangeRate.Text = old_wire.ExchangeRate;   this.foreignTransAmount.Text = old_wire.TransAmountForeign;   this.foreignTotal.Text = old_wire.TotalForeign;
			
			if (old_wire.WireType == "R"){
				this.remittanceOpt.IsChecked = true;
			}
			if (old_wire.WireType == "NR"){
				this.nonremittanceOpt.IsChecked = true;
			}
			
			
			if (old_wire.OneTime == 1){
				this.oneTime.IsChecked = true;
			}
			if (old_wire.SubjectToTransAgreement == 1){
				this.subjectToTransAgreement.IsChecked = true;
			}
			if (old_wire.FutureDateBox == 1){
				this.futureDateBox.IsChecked = true;
			}
			
			string status = old_wire.Status.ToString();
			
			Debug.WriteLine("Wire status: "+status);
			if (status == "0"){
				// since 0 = correct and 1 = incorrect 
				this.Correct.IsChecked = true;
			}
			else if (status == "1"){
				this.Incorrect.IsChecked = true;
			}
			
			isUpdating = true;
			this.updateBtn.Visibility = Visibility.Visible;
            
			this.newWireForm.SelectedIndex = 0;
						
		}
		

		
		public bool formHasErrors(){
			bool valid = false;
			
			foreach (TextBox tb in FindVisualChildren<TextBox>(this))
			{
				if (Validation.GetHasError(tb)){
					valid = true;
					return valid;
				}
		    }
			
			foreach (ComboBox cb in FindVisualChildren<ComboBox>(this))
			{
				if (Validation.GetHasError(cb)){
					valid = true;
					return valid;
				}
		    }
			
			return valid;
			
		}
		
		public void Add_Wire_To_Database(object sender, EventArgs e){    
     		
  
			if (formHasErrors()){
				MessageBox.Show("Your form contains errors! \nPlease go back and fix them in order to submit the wire.");
			}
			else{
				
			
	            string id_type, bank_type;
			  	int sender_id, recipient_id, ibank_id, rbank_id, wire_id, internal_use_id; //, wire_history_id;
			  	
	            // Create Sender object 
	            id_type = "S";
	            Sender pSender = new Sender();
	            pSender.Type = id_type;  pSender.Phone = this.sPhone.Text.Trim();  pSender.MemberNum = this.sMemberNum.Text.Trim();  pSender.SubAccount = this.sSubAccount.Text.Trim();  pSender.Name = this.sName.Text.Trim();  pSender.Address = this.sAddress.Text.Trim();  pSender.City = this.sCity.Text.Trim(); pSender.State = this.sStatesDropdown.Text;  pSender.Zip = this.sZip.Text.Trim();
	            // Check if this person exists in DB
	            sender_id = pSender.exists(pSender.MemberNum);
				if (sender_id == 0){
					sender_id = pSender.insertInfo();
					pSender.insertSenderInfo(sender_id);
				}
				Debug.WriteLine("Sender ID: "+sender_id.ToString());
				Globals.SENDER_ID = sender_id;
			
				
				// Create Recipient object 
				id_type = "R";
				Recipient pRecipient = new Recipient();
				pRecipient.Type = id_type;  pRecipient.AccountNumIBAN = this.rAccountNumIBAN.Text.Trim();  pRecipient.SSN = this.rSSN.Text.Trim();  pRecipient.TIN = this.rTIN.Text.Trim();  pRecipient.PhoneNum = this.rPhone.Text.Trim();   pRecipient.DLNum = this.rDLnum.Text.Trim();  pRecipient.Country = this.CountriesDropdown.Text;  pRecipient.Name = this.rName.Text.Trim();  pRecipient.Address = this.rAddress.Text.Trim();  pRecipient.City = this.rCity.Text.Trim();  pRecipient.State = this.rStatesDropdown.Text;  pRecipient.Zip = this.rZip.Text.Trim();
				// Check if this person exists in DB
				recipient_id = pRecipient.exists(pRecipient.AccountNumIBAN);
				if (recipient_id == 0){
					recipient_id = pRecipient.insertInfo();
					pRecipient.insertRecipientInfo(recipient_id);
				}
				Globals.RECIPIENT_ID = recipient_id;
			    Debug.WriteLine("Recipient ID: "+recipient_id.ToString());
	
				
				// Create Bank object (Intermediary bank)
				bank_type = "IB";
				Bank iBank = new Bank();
				iBank.Name = this.ibName.Text.Trim();  iBank.Address = this.ibAddress.Text.Trim();  iBank.City = this.ibCity.Text.Trim();  iBank.State = this.ibStatesDropdown.Text;  iBank.Zip = this.ibZip.Text.Trim();  iBank.RoutingNum = this.ibRoutingNum.Text.Trim();  iBank.SwiftSortCode = this.ibSwiftSortCode.Text.Trim();  iBank.BranchInfo = this.ibBranchInfo.Text.Trim();  iBank.RoutingInstr = this.ibRoutingInstr.Text.Trim();  iBank.Type = bank_type;
				ibank_id = iBank.exists(iBank.RoutingNum);
				if (ibank_id == 0){
					ibank_id = iBank.insertInfo();
				}
				Globals.IBANK_ID = ibank_id;
				Debug.WriteLine("Intermediary bank id: "+ibank_id.ToString());
				
				// Create Bank object (Recipient bank)
				bank_type = "RB";
				Bank rBank = new Bank();
				rBank.Name = this.rbName.Text.Trim();  rBank.Address = this.rbAddress.Text.Trim();  rBank.City = this.rbCity.Text.Trim();  rBank.State = this.rbStatesDropdown.Text; rBank.Zip = this.rbZip.Text.Trim();  rBank.RoutingNum = this.rbRoutingNum.Text.Trim();  rBank.SwiftSortCode = this.rbSwiftSortCode.Text.Trim();  rBank.BranchInfo = this.rbBranchInfo.Text.Trim();  rBank.RoutingInstr = this.rbRoutingInstr.Text.Trim();  rBank.Type = bank_type;
				rbank_id = rBank.exists(rBank.RoutingNum);
				if (rbank_id == 0){
					rbank_id = rBank.insertInfo();
				}
				Globals.RBANK_ID = rbank_id;
				Debug.WriteLine("Recipient bank id: "+rbank_id.ToString());			
				
              	string time = Globals.getCurrentTime();
              	string datetime = Globals.getCurrentDateAndTime();
              	string wire_type = "";
				
				// Create Internal Use Info object
				InternalUseInfo info = new InternalUseInfo();
				info.MemberConfirmingFunds = this.memberConfirmingFunds.Text.Trim();  info.DateAndTime = datetime;    info.FeeAmount = this.transFees.Text.Trim(); //info.FeeAmount = TransFeesValueUSD;
				info.IDUsed = this.rIDused.Text;
				info.MethodOfTransfer = this.methodOfTransferDropdown.Text;  info.WireProcessedBy = this.employeeName.Text.Trim();  
				info.OFACVerificationBy = this.ofacVerificationBy.Text.Trim();  info.OFACDateTime = this.ofacDateTime.Text.Trim();  info.InternalSpecialInstructions = this.internalSpecialInstr.Text.Trim();
				info.SecurityMethodUsed = this.securityMethodUsed.Text.Trim(); 	info.SecurityProcessedBy = this.securityProcessedBy.Text.Trim();  info.SecurityDateTime = this.securityDateTime.Text.Trim();
				info.EmployeeCallback = this.employeeCallback.Text.Trim();  info.PhoneNumForCallback = this.phoneNumCallback.Text.Trim();  info.SourcePhoneVerification = this.sourcePhoneVerificationCallback.Text.Trim();  
				info.MemberCancellingRequest = this.memberCancellingRequest.Text.Trim();  info.CancelDate = this.cancelDate.Text.Trim();  info.CancelProcessedBy = this.cancelProcessedBy.Text.Trim(); 
				info.TransactionControlNum = "";
			
				internal_use_id = info.insertInfo();
				Globals.INTERNAL_USE_ID = internal_use_id;
				
				
				if (this.remittanceOpt.IsChecked == true){
					wire_type = "R";
				}
				else if (this.nonremittanceOpt.IsChecked == true){
					wire_type = "NR";
				}
				
				
				// Create Wire object
				// NOTE: for the bank_id, it saves the recipient bank  
				Wire wire = new Wire();
				wire.TransferDate = this.transferDate.Text.Trim();  wire.TransferTime = time;   wire.FundsAvailableBy = this.fundsAvailableBy.Text.Trim();  
				
				wire.CurrencyInfo = this.currencyInfoDropdown.Text;  wire.TransAmountUSD = this.transAmount.Text.Trim();  wire.TransFeesUSD = this.transFees.Text.Trim();   wire.TotalUSD = this.USDTotal.Text.Trim();  wire.CurrencyType = this.CurrencyDropdown.Text;  wire.ExchangeRate = this.exchangeRate.Text.Trim();  wire.TransAmountForeign = this.foreignTransAmount.Text.Trim();   wire.TotalForeign = this.foreignTotal.Text.Trim();  
			
				wire.PaymentInstr = this.sPaymentInstr.Text;  wire.SenderID = sender_id;  wire.RecipientID = recipient_id;  wire.BankID = rbank_id;
				
				wire.WireType = wire_type;  wire.Status = Status;   wire.CountryOfReceipt = this.countryOfReceiptDropdown.Text;  wire.WireBranch = this.wireBranchDropdown.Text;
				wire.PickupName = this.pickupName.Text.Trim();  wire.PickupAddress = this.pickupAddress.Text.Trim();  wire.PickupCityStateZip = this.pickupCityStateZip.Text.Trim();  wire.PickupPhone = this.pickupPhone.Text.Trim();
				wire.FutureDateBox = FutureDateBox;  wire.FutureDate1 = this.futureDate1.Text.Trim();  wire.FutureDate2 = this.futureDate2.Text.Trim();
				wire.OneTime = OneTime; 	
				wire.SubjectToTransAgreement = SubjectToAgreement;
				wire.InternalUseID = internal_use_id;
				Debug.WriteLine("Computer name: "+Globals.COMPUTER_NAME);
				wire.ComputerName = Globals.COMPUTER_NAME;
				wire.IPAddress = Globals.IP_ADDRESS;
				wire.EmployeeUser = Globals.EMPLOYEE_USER;

				wire_id = wire.insertInfo();
				Globals.WIRE_ID = wire_id;     // Update global Wire ID variable so it can be referenced later for updating, etc.  	
				Debug.WriteLine("Wire id: "+Globals.WIRE_ID+", "+wire_id);
				
				// NOTE: the Transaction Control Num is Member Num + Wire ID	
				string trans_control_num = this.sMemberNum.Text.Trim()+"-"+wire_id.ToString();
				info.TransactionControlNum = trans_control_num;
				info.updateTransactionControlNum(internal_use_id.ToString());
			
				// Create WireHistory object 
				WireHistory hist = new WireHistory();
				hist.WireID = wire_id; 
				hist.DateTime = datetime;
				hist.TransferDate = this.transferDate.Text.Trim();  hist.TransferTime = time;   hist.FundsAvailableBy = this.fundsAvailableBy.Text.Trim();
				hist.sName = this.sName.Text.Trim();   hist.sAddress = this.sAddress.Text.Trim();   hist.sCity = this.sCity.Text.Trim();   hist.sState = this.sStatesDropdown.Text;   hist.sZip = this.sZip.Text.Trim();  hist.sPhoneNum = this.sPhone.Text.Trim();  hist.sMemberNum = this.sMemberNum.Text.Trim();  hist.sSubAccount = this.sSubAccount.Text.Trim();
				hist.rName = this.rName.Text.Trim();   hist.rAddress = this.rAddress.Text.Trim();   hist.rCity = this.rCity.Text.Trim();   hist.rState = this.rStatesDropdown.Text;   hist.rZip = this.rZip.Text.Trim();  hist.rPhone = this.rPhone.Text.Trim();  hist.rCountry = this.CountriesDropdown.Text;  hist.rAccountNumIBAN = this.rAccountNumIBAN.Text.Trim();  hist.rSSN = this.rSSN.Text.Trim();  hist.rTIN = this.rTIN.Text.Trim();  hist.rDLNum = this.rDLnum.Text.Trim();
				hist.ibName = this.ibName.Text.Trim();  hist.ibAddress = this.ibAddress.Text.Trim();  hist.ibCity = this.ibCity.Text.Trim();  hist.ibState = this.ibStatesDropdown.Text;  hist.ibZip = this.ibZip.Text.Trim();  hist.ibRoutingNum = this.ibRoutingNum.Text.Trim();  hist.ibSwiftSortCode = this.ibSwiftSortCode.Text.Trim();  hist.ibBranch = this.ibBranchInfo.Text.Trim();  hist.ibRoutingInstr = this.ibRoutingInstr.Text.Trim();
				hist.rbName = this.rbName.Text.Trim();  hist.rbAddress = this.rbAddress.Text.Trim();  hist.rbCity = this.rbCity.Text.Trim();  hist.rbState = this.rbStatesDropdown.Text;  hist.rbZip = this.rbZip.Text.Trim();  hist.rbRoutingNum = this.rbRoutingNum.Text.Trim();  hist.rbSwiftSortCode = this.rbSwiftSortCode.Text.Trim();  hist.rbBranch = this.rbBranchInfo.Text.Trim();  hist.rbRoutingInstr = this.rbRoutingInstr.Text.Trim();
				
				hist.CurrencyInfo = this.currencyInfoDropdown.Text;  hist.TransAmountUSD = this.transAmount.Text.Trim();  hist.TransFeesUSD = this.transFees.Text.Trim();  hist.TotalUSD = this.USDTotal.Text.Trim();   hist.CurrencyType = this.CurrencyDropdown.Text;   hist.ExchangeRate = this.exchangeRate.Text.Trim();  hist.TransAmountForeign = this.foreignTransAmount.Text.Trim();  hist.TotalForeign = this.foreignTotal.Text.Trim();
				
				hist.WireType = wire_type;  hist.Status = Status;  hist.CountryOfReceipt = this.countryOfReceiptDropdown.Text;  hist.WireBranch = this.wireBranchDropdown.Text;  hist.PaymentInstr = this.sPaymentInstr.Text.Trim();  hist.PickupName = this.pickupName.Text.Trim();  hist.PickupAddress = this.pickupAddress.Text.Trim();  hist.PickupCityStateZip = this.pickupCityStateZip.Text.Trim();  hist.PickupPhone = this.pickupPhone.Text.Trim();  hist.FutureDateBox = FutureDateBox;  hist.FutureDate1 = this.futureDate1.Text.Trim();  hist.FutureDate2 = this.futureDate2.Text.Trim();  hist.OneTime = OneTime;  hist.SubjectToTransAgreement = SubjectToAgreement;
				hist.MemberConfirmingFunds = this.memberConfirmingFunds.Text.Trim();  hist.IDUsed = this.rIDused.Text;  hist.MethodOfTransfer = this.methodOfTransferDropdown.Text;  hist.TransactionControlNum = trans_control_num;  hist.WireProcessedBy = this.employeeName.Text.Trim();
				hist.OFACVerificationBy = this.ofacVerificationBy.Text.Trim();  hist.OFACDateTime = this.ofacDateTime.Text.Trim();  hist.InternalSpecialInstructions = this.internalSpecialInstr.Text.Trim();  hist.SecurityMethodUsed = this.securityMethodUsed.Text.Trim();  hist.SecurityProcessedBy = this.securityProcessedBy.Text.Trim();  hist.SecurityDateTime = this.securityDateTime.Text.Trim();  
				hist.EmployeeCallback = this.employeeCallback.Text.Trim();  hist.PhoneNumForCallback = this.phoneNumCallback.Text.Trim();  hist.SourcePhoneVerification = this.sourcePhoneVerificationCallback.Text.Trim();   hist.MemberCancellingRequest = this.memberCancellingRequest.Text.Trim();  hist.CancelDate = this.cancelDate.Text.Trim();  hist.CancelProcessedBy = this.cancelProcessedBy.Text.Trim();
				hist.ComputerName = Globals.COMPUTER_NAME;  hist.IPAddress = Globals.IP_ADDRESS;  hist.EmployeeUser = Globals.EMPLOYEE_USER;
				hist.insertInfo();
				
	            
	           // Globals.WIRE_ID = 6; // NOTE: this is for testing purposes ONLY; delete when done 
	            
	            
	            // TODO: find similar blocks of code and put them into separate functions...maybe 
				this.newWireForm.Visibility = Visibility.Collapsed;
				this.welcomePage.Visibility = Visibility.Visible;
				this.homeBtn.Visibility = Visibility.Collapsed;
	
				
	           // finish_window.ShowDialog();  
	            FinishPage finish_window = new FinishPage();
	            finish_window.Show();
	            
     		}
        }	
		
		public void Update_Wire(object sender, EventArgs e){
					
			string wire_id = Globals.WIRE_ID.ToString();
			string status = Status.ToString(); 
			
			
			if (this.isUpdating == false){
				Wire wire = new Wire();
				wire.updateStatus(status, wire_id);
				
				DateTime dt = DateTime.Now;
				string date = dt.ToString("MM/dd/yyyy HH:mm:ss");
				
				WireHistory old_wire = new WireHistory();
				old_wire = old_wire.returnOldWire(Globals.WIRE_ID);
			
				old_wire.Status = Status;
				old_wire.DateTime = date;
				
				Debug.WriteLine("new status: "+old_wire.Status);
				old_wire.insertInfo();
				
				this.newWireForm.Visibility = Visibility.Collapsed;
				this.welcomePage.Visibility = Visibility.Visible;
				
				MessageBox.Show("You have successfully updated the status of this wire.");
			}
			else if (this.isUpdating == true){
			  	
	            // Update Sender/Person in DB 
	            Sender pSender = new Sender();
	            pSender.Phone = this.sPhone.Text.Trim();  pSender.MemberNum = this.sMemberNum.Text.Trim();  pSender.SubAccount = this.sSubAccount.Text.Trim();  pSender.Name = this.sName.Text.Trim();  pSender.Address = this.sAddress.Text.Trim();  pSender.City = this.sCity.Text.Trim(); pSender.State = this.sStatesDropdown.Text;  pSender.Zip = this.sZip.Text.Trim();
	            pSender.updatePerson(Globals.SENDER_ID);
	            pSender.updateSender(Globals.SENDER_ID);
				
				//Update Recipient/Person in DB
				Recipient pRecipient = new Recipient();
				pRecipient.AccountNumIBAN = this.rAccountNumIBAN.Text.Trim();  pRecipient.SSN = this.rSSN.Text.Trim();  pRecipient.TIN = this.rTIN.Text.Trim();  pRecipient.PhoneNum = this.rPhone.Text.Trim();   pRecipient.DLNum = this.rDLnum.Text.Trim();  pRecipient.Country = this.CountriesDropdown.Text;  pRecipient.Name = this.rName.Text.Trim();  pRecipient.Address = this.rAddress.Text.Trim();  pRecipient.City = this.rCity.Text.Trim();  pRecipient.State = this.rStatesDropdown.Text;  pRecipient.Zip = this.rZip.Text.Trim();
				pRecipient.updatePerson(Globals.RECIPIENT_ID);
				pRecipient.updateRecipient(Globals.RECIPIENT_ID);
				
				// Update Bank in DB (Intermediary bank)
				Bank iBank = new Bank();
				iBank.Name = this.ibName.Text.Trim();  iBank.Address = this.ibAddress.Text.Trim();  iBank.City = this.ibCity.Text.Trim();  iBank.State = this.ibStatesDropdown.Text;  iBank.Zip = this.ibZip.Text.Trim();  iBank.RoutingNum = this.ibRoutingNum.Text.Trim();  iBank.SwiftSortCode = this.ibSwiftSortCode.Text.Trim();  iBank.BranchInfo = this.ibBranchInfo.Text.Trim();  iBank.RoutingInstr = this.ibRoutingInstr.Text.Trim();
				iBank.updateBank(Globals.IBANK_ID);
				
				// Create Bank object (Recipient bank)
				Bank rBank = new Bank();
				rBank.Name = this.rbName.Text.Trim();  rBank.Address = this.rbAddress.Text.Trim();  rBank.City = this.rbCity.Text.Trim();  rBank.State = this.rbStatesDropdown.Text; rBank.Zip = this.rbZip.Text.Trim();  rBank.RoutingNum = this.rbRoutingNum.Text.Trim();  rBank.SwiftSortCode = this.rbSwiftSortCode.Text.Trim();  rBank.BranchInfo = this.rbBranchInfo.Text.Trim();  rBank.RoutingInstr = this.rbRoutingInstr.Text.Trim(); 
				rBank.updateBank(Globals.RBANK_ID);
				
              	string time = Globals.getCurrentTime();
              	string datetime = Globals.getCurrentDateAndTime();
              	string date = Globals.getCurrentDate();
              	string wire_type = "";
              	string trans_control_num = this.sMemberNum.Text.Trim()+"-"+Globals.WIRE_ID;
				if (this.remittanceOpt.IsChecked == true){
					wire_type = "R";
				}
				else if (this.nonremittanceOpt.IsChecked == true){
					wire_type = "NR";
				}
              	
				// Update Internal Use Info object in DB
				InternalUseInfo info = new InternalUseInfo();
				info.MemberConfirmingFunds = this.memberConfirmingFunds.Text.Trim();  info.DateAndTime = datetime;  info.FeeAmount = this.transFees.Text.Trim(); //info.FeeAmount = TransFeesValueUSD;
				info.IDUsed = this.rIDused.Text;
				info.MethodOfTransfer = this.methodOfTransferDropdown.Text;  info.WireProcessedBy = this.employeeName.Text.Trim();  
				info.OFACVerificationBy = this.ofacVerificationBy.Text.Trim();  info.OFACDateTime = this.ofacDateTime.Text.Trim();  info.InternalSpecialInstructions = this.internalSpecialInstr.Text.Trim();
				info.SecurityMethodUsed = this.securityMethodUsed.Text.Trim(); 	info.SecurityProcessedBy = this.securityProcessedBy.Text.Trim();  info.SecurityDateTime = this.securityDateTime.Text.Trim();
				info.EmployeeCallback = this.employeeCallback.Text.Trim();  info.PhoneNumForCallback = this.phoneNumCallback.Text.Trim();  info.SourcePhoneVerification = this.sourcePhoneVerificationCallback.Text.Trim();  
				info.MemberCancellingRequest = this.memberCancellingRequest.Text.Trim();  info.CancelDate = this.cancelDate.Text.Trim();  info.CancelProcessedBy = this.cancelProcessedBy.Text.Trim();
				info.TransactionControlNum = trans_control_num;
				info.updateInternalUseInfo(Globals.INTERNAL_USE_ID);
				
				// Update Wire object in DB
				Wire wire = new Wire();
				wire.TransferDate = this.transferDate.Text.Trim();  wire.TransferTime = time;   wire.FundsAvailableBy = this.fundsAvailableBy.Text.Trim();  
				
				wire.CurrencyInfo = this.currencyInfoDropdown.Text;  wire.TransAmountUSD = this.transAmount.Text.Trim();  wire.TransFeesUSD = this.transFees.Text.Trim();   wire.TotalUSD = this.USDTotal.Text.Trim();  wire.CurrencyType = this.CurrencyDropdown.Text;  wire.ExchangeRate = this.exchangeRate.Text.Trim();  wire.TransAmountForeign = this.foreignTransAmount.Text.Trim();   wire.TotalForeign = this.foreignTotal.Text.Trim();  
				
				wire.PaymentInstr = this.sPaymentInstr.Text;  wire.SenderID = Globals.SENDER_ID;  wire.RecipientID = Globals.RECIPIENT_ID;  wire.BankID = Globals.RBANK_ID;
				wire.WireType = wire_type;  wire.Status = Status;   wire.CountryOfReceipt = this.countryOfReceiptDropdown.Text;  wire.WireBranch = this.wireBranchDropdown.Text;
				wire.PickupName = this.pickupName.Text.Trim();  wire.PickupAddress = this.pickupAddress.Text.Trim();  wire.PickupCityStateZip = this.pickupCityStateZip.Text.Trim();  wire.PickupPhone = this.pickupPhone.Text.Trim();
				wire.FutureDateBox = FutureDateBox;  wire.FutureDate1 = this.futureDate1.Text.Trim();  wire.FutureDate2 = this.futureDate2.Text.Trim();
				wire.OneTime = OneTime;  wire.SubjectToTransAgreement = SubjectToAgreement;  wire.InternalUseID = Globals.INTERNAL_USE_ID;
				wire.ComputerName = Globals.COMPUTER_NAME;  wire.IPAddress = Globals.IP_ADDRESS;   wire.EmployeeUser = Globals.EMPLOYEE_USER;
				wire.updateWire(Globals.WIRE_ID);
	
				// Create WireHistory object 
				WireHistory hist = new WireHistory();
				hist.WireID = Convert.ToInt32(wire_id);
				hist.DateTime = datetime;
				hist.TransferDate = this.transferDate.Text.Trim();  hist.TransferTime = time;   hist.FundsAvailableBy = this.fundsAvailableBy.Text.Trim();
				hist.sName = this.sName.Text.Trim();   hist.sAddress = this.sAddress.Text.Trim();   hist.sCity = this.sCity.Text.Trim();   hist.sState = this.sStatesDropdown.Text;   hist.sZip = this.sZip.Text.Trim();  hist.sPhoneNum = this.sPhone.Text.Trim();  hist.sMemberNum = this.sMemberNum.Text.Trim();  hist.sSubAccount = this.sSubAccount.Text.Trim();
				hist.rName = this.rName.Text.Trim();   hist.rAddress = this.rAddress.Text.Trim();   hist.rCity = this.rCity.Text.Trim();   hist.rState = this.rStatesDropdown.Text;   hist.rZip = this.rZip.Text.Trim();  hist.rPhone = this.rPhone.Text.Trim();  hist.rCountry = this.CountriesDropdown.Text;  hist.rAccountNumIBAN = this.rAccountNumIBAN.Text.Trim();  hist.rSSN = this.rSSN.Text.Trim();  hist.rTIN = this.rTIN.Text.Trim();  hist.rDLNum = this.rDLnum.Text.Trim();
				//hist.sbName = this.sbName.Text.Trim();  hist.sbAddress = this.sbAddress.Text.Trim();  hist.sbCity = this.sbCity.Text.Trim();  hist.sbState = this.sbStatesDropdown.Text;  hist.sbZip = this.sbZip.Text.Trim();  hist.sbRoutingNum = this.sbRoutingNum.Text.Trim();  hist.sbSwiftSortCode = this.sbSwiftSortCode.Text.Trim();  hist.sbBranch = this.sbBranchInfo.Text.Trim();  hist.sbRoutingInstr = this.sbRoutingInstr.Text.Trim();
				hist.ibName = this.ibName.Text.Trim();  hist.ibAddress = this.ibAddress.Text.Trim();  hist.ibCity = this.ibCity.Text.Trim();  hist.ibState = this.ibStatesDropdown.Text;  hist.ibZip = this.ibZip.Text.Trim();  hist.ibRoutingNum = this.ibRoutingNum.Text.Trim();  hist.ibSwiftSortCode = this.ibSwiftSortCode.Text.Trim();  hist.ibBranch = this.ibBranchInfo.Text.Trim();  hist.ibRoutingInstr = this.ibRoutingInstr.Text.Trim();
				hist.rbName = this.rbName.Text.Trim();  hist.rbAddress = this.rbAddress.Text.Trim();  hist.rbCity = this.rbCity.Text.Trim();  hist.rbState = this.rbStatesDropdown.Text;  hist.rbZip = this.rbZip.Text.Trim();  hist.rbRoutingNum = this.rbRoutingNum.Text.Trim();  hist.rbSwiftSortCode = this.rbSwiftSortCode.Text.Trim();  hist.rbBranch = this.rbBranchInfo.Text.Trim();  hist.rbRoutingInstr = this.rbRoutingInstr.Text.Trim();
			
				hist.CurrencyInfo = this.currencyInfoDropdown.Text;  hist.TransAmountUSD = this.transAmount.Text.Trim();  hist.TransFeesUSD = this.transFees.Text.Trim();  hist.TotalUSD = this.USDTotal.Text.Trim();   hist.CurrencyType = this.CurrencyDropdown.Text;   hist.ExchangeRate = this.exchangeRate.Text.Trim();  hist.TransAmountForeign = this.foreignTransAmount.Text.Trim();  hist.TotalForeign = this.foreignTotal.Text.Trim();
				
				hist.WireType = wire_type;  hist.Status = Status;  hist.CountryOfReceipt = this.countryOfReceiptDropdown.Text;  hist.WireBranch = this.wireBranchDropdown.Text;  hist.PaymentInstr = this.sPaymentInstr.Text.Trim();  hist.PickupName = this.pickupName.Text.Trim();  hist.PickupAddress = this.pickupAddress.Text.Trim();  hist.PickupCityStateZip = this.pickupCityStateZip.Text.Trim();  hist.PickupPhone = this.pickupPhone.Text.Trim();  hist.FutureDateBox = FutureDateBox;  hist.FutureDate1 = this.futureDate1.Text.Trim();  hist.FutureDate2 = this.futureDate2.Text.Trim();  hist.OneTime = OneTime;  hist.SubjectToTransAgreement = SubjectToAgreement;
				hist.MemberConfirmingFunds = this.memberConfirmingFunds.Text.Trim();  hist.IDUsed = this.rIDused.Text;  hist.MethodOfTransfer = this.methodOfTransferDropdown.Text;  hist.TransactionControlNum = trans_control_num;  hist.WireProcessedBy = this.employeeName.Text.Trim();
				hist.OFACVerificationBy = this.ofacVerificationBy.Text.Trim();  hist.OFACDateTime = this.ofacDateTime.Text.Trim();  hist.InternalSpecialInstructions = this.internalSpecialInstr.Text.Trim();  hist.SecurityMethodUsed = this.securityMethodUsed.Text.Trim();  hist.SecurityProcessedBy = this.securityProcessedBy.Text.Trim();  hist.SecurityDateTime = this.securityDateTime.Text.Trim();  
				hist.EmployeeCallback = this.employeeCallback.Text.Trim();  hist.PhoneNumForCallback = this.phoneNumCallback.Text.Trim();  hist.SourcePhoneVerification = this.sourcePhoneVerificationCallback.Text.Trim();   hist.MemberCancellingRequest = this.memberCancellingRequest.Text.Trim();  hist.CancelDate = this.cancelDate.Text.Trim();  hist.CancelProcessedBy = this.cancelProcessedBy.Text.Trim();
				hist.ComputerName = Globals.COMPUTER_NAME;  hist.IPAddress = Globals.IP_ADDRESS;  hist.EmployeeUser = Globals.EMPLOYEE_USER;
				hist.insertInfo();
				
				this.newWireForm.Visibility = Visibility.Collapsed;
				this.welcomePage.Visibility = Visibility.Visible;
				
				MessageBox.Show("You have successfully updated this wire.");
			}
			
			this.isUpdating = false;
			
		}
		
		private void IB_Bank_Expanded(object sender, RoutedEventArgs args){
			// TODO: FIX THIS 
			//this.ibBank2Expander.IsExpanded = false;
		}
		
		private void IB_Bank_2_Expanded(object sender, RoutedEventArgs args){
			//this.ibBankExpander.IsExpanded = false;
		}
		
		
		
		
		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e){
			// Deleting all created PDFs in temp folder 
			string[] tempFiles = Directory.GetFiles(Path.GetTempPath(), "*.pdf");
			try{
				foreach (string file in tempFiles)
				{
					File.Delete(file);
				}
			} catch { }
			
			// Fully shuts down program and ends all processes
            System.Windows.Application.Current.Shutdown();
        }
		
	}
}